using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MNISTlooker
{
    /// <summary>
    /// 
    /// </summary>

    public class NeuralNetwork : ISimpleNeuralNetwork
    {
        public enum ActivationFunctions
        {
            Identity,
            Tanh,
            Sigmoid,
            Arctan,
            ReLU
        }

        Dictionary<ActivationFunctions, Func<float, float>> Functions = new Dictionary<ActivationFunctions, Func<float, float>>
        {
            { ActivationFunctions.Identity, (x) => x},
            { ActivationFunctions.Sigmoid, (x) => 1 / (1 + (float)Math.Exp(-x)) },
            { ActivationFunctions.Tanh, (x) =>
            {
                var eX = Math.Exp(x);
                var eMinusX = Math.Exp(-x);
                var res = (float)((eX - eMinusX) / (eX + eMinusX));
                if (float.IsNaN(res) || float.IsNaN(res))
                    res = float.IsPositiveInfinity(res)? float.MaxValue : float.MinValue;
                return res;
            }},
            { ActivationFunctions.Arctan, (x) => (float)Math.Atan(x) },
            {ActivationFunctions.ReLU, (x) =>
            {
                if (x > 0) return x;
                else return 0;
            } }
        };

        Dictionary<ActivationFunctions, Func<float, float>> DerrivitiveFromActivation = new Dictionary<ActivationFunctions, Func<float, float>>
        {
            {ActivationFunctions.Sigmoid, (activation) => activation * (1 - activation)},
            {ActivationFunctions.Tanh, (activation) => 1 - activation * activation }
        };

        Dictionary<ActivationFunctions, Func<float, float>> DerrivitiveFromSumm = new Dictionary<ActivationFunctions, Func<float, float>>
        {
            {ActivationFunctions.Arctan, (x) => 1 / (x * x + 1) },
            {ActivationFunctions.ReLU, (x) =>
            {
                if (x > 0) return 1;
                else return 0;
            } },
            {ActivationFunctions.Identity, (x) => 1.0f }
        };

        public class NeuralNetworkSettings
        {
            public List<int> configuration;
            public int seed = 0;
            public float leariningRate = 0.5f;
            public ActivationFunctions activationFunction = ActivationFunctions.Tanh;
        }

        ActivationFunctions activation;
        float leariningRate;
        int[] configuration;


        public int InputLength { get { return configuration[0]; } }
        public int OutputLength { get { return configuration.Last(); } }

        List<float[][]> weightsL; // Layer, IndexInRecievingLayer, IndexInSendingLayer
        List<float[]> weightsB;

        Random RND;

        public NeuralNetwork(NeuralNetworkSettings settings)
        {
            RND = new Random(settings.seed);
            SetupW(settings.configuration);
            activation = settings.activationFunction;
            leariningRate = settings.leariningRate;
        }

        public static NeuralNetwork Create(List<int> config, string activation)
        {
            ActivationFunctions a;
            switch (activation.ToLower())
            {
                case "tanh":
                    a = ActivationFunctions.Tanh;
                    break;
                case "sigmoid":
                    a = ActivationFunctions.Sigmoid;
                    break;
                default:
                    a = ActivationFunctions.Tanh;
                    break;
            }


            NeuralNetworkSettings settings = new NeuralNetworkSettings
            {
                configuration = config,
                activationFunction = a
            };

            return new NeuralNetwork(settings);
        }

        void SetupW(List<int> config)
        {
            if (config == null) throw new Exception("Invalid NeuralNet Config");
            if (config.Count < 2) throw new Exception("Invalid NeuralNet Config");

            weightsL = new List<float[][]>();
            weightsB = new List<float[]>();

            // For consistancy. layer 0 is input layer but never actualy used
            weightsL.Add(null);
            weightsB.Add(null);

            for (int i = 1; i < config.Count; i++)
            {
                var currentLayerNeurons = config[i];
                var previousLayerNeurons = config[i - 1];

                var layer = new float[currentLayerNeurons][];
                for (int j = 0; j < currentLayerNeurons; j++) layer[j] = new float[previousLayerNeurons];

                var biases = new float[currentLayerNeurons];


                weightsL.Add(layer);
                weightsB.Add(biases);
            }

            configuration = config.ToArray();

            SetWToRandomValues();
        }

        void SetWToRandomValues()
        {
            foreach (var layer in weightsL.Skip(1))
                for (int i = 0; i < layer.Length; i++)
                    for (int j = 0; j < layer[i].Length; j++)
                        layer[i][j] = (float)(RND.NextDouble() - .5f);

            foreach (var layer in weightsB.Skip(1))
                for (int i = 0; i < layer.Length; i++)
                    layer[i] = (float)(RND.NextDouble() - .5f);
        }

        public float[] Solve(float[] input)
        {
            if (configuration[0] != input.Length) throw new Exception($"Input data lenght need to be {configuration[0]}, but it is {input.Length} ");
            for (int layer = 1; layer < configuration.Length; layer++)
            {
                input = ActivateLayerParrallel(input, weightsL[layer], weightsB[layer], Functions[activation]);
            }
            return input;
        }

        float[] ActivateLayerParrallel(float[] previousLayerValues, float[][] weights, float[] biases, Func<float, float> activate)
        {
            var currentLayerLength = weights.Length;
            var previousLayerLength = previousLayerValues.Length;

            var result = new float[currentLayerLength];

            Parallel.For(0, currentLayerLength, (x) =>
            {
                float summ = 0;
                for (int i = 0; i < previousLayerLength; i++)
                {
                    summ += weights[x][i] * previousLayerValues[i];
                }
                summ += biases[x];
                result[x] = activate(summ);
            });
            return result;
        }

        public void TrainNetwork(List<(float[] input, float[] result)> dataSet)
        {
            if (dataSet.Count < 0)
                throw new Exception("Emty training dataset");

            if (!dataSet.TrueForAll(x => x.input.Length == InputLength && x.result.Length == OutputLength))
                throw new Exception("Dataset incorrect input or result wrong length");

            int iLastLayer = configuration.Length - 1;

            var weightsErrors = SetupWeightsError();
            var biasErrors = new float[configuration.Length][];
            for (int i = 0; i < biasErrors.Length; i++) biasErrors[i] = new float[configuration[i]];

            foreach (var (input, output) in dataSet)
            {
                var NeuralNet = new (float summ, float act)[configuration.Length][];
                for (int layer = 0; layer < configuration.Length; layer++)
                    NeuralNet[layer] = new (float, float)[configuration[layer]];

                // FORWARD PROPAGATION INPUT LAYER
                for (int i = 0; i < InputLength; i++)
                    NeuralNet[0][i] = (input[i], input[i]);

                // FORWARD PROPAGATION OTHER LAYERS
                for (int l = 1; l < configuration.Length; l++)
                    NeuralNet[l] = TrainingActivateLayerParrallel(NeuralNet[l - 1], weightsL[l], weightsB[l], Functions[activation]);

                var Errors = BackpropagateErrorParrallel(NeuralNet, output);


                Parallel.For(1, iLastLayer, (l) =>
                {
                    for (int j = 0; j < configuration[l]; j++)
                    {
                        for (int k = 0; k < configuration[l - 1]; k++)
                        {
                            weightsErrors[l][j][k] += Errors[l][j] * NeuralNet[l - 1][k].act;
                            if (float.IsNaN(weightsErrors[l][j][k])) throw new Exception();
                        }
                        biasErrors[l][j] += Errors[l][j];
                    }
                });
            }

            float learningR = leariningRate;

            Parallel.For(1, iLastLayer, (l) =>
            {
                for (int j = 0; j < configuration[l]; j++)
                {
                    for (int k = 0; k < configuration[l - 1]; k++)
                    {
                        weightsL[l][j][k] -= weightsErrors[l][j][k] * learningR / dataSet.Count;
                        if (float.IsNaN(weightsL[l][j][k])) throw new Exception();
                    }
                    biasErrors[l][j] -= biasErrors[l][j] * learningR / dataSet.Count;
                }
            });
        }


        List<float[][]> SetupWeightsError()
        {
            var ret = new List<float[][]> { null };
            foreach (var layer in weightsL.Skip(1))
            {
                var arr = new float[layer.Length][];
                for (int j = 0; j < layer.Length; j++) arr[j] = new float[layer[j].Length];
                ret.Add(arr);
            }
            return ret;
        }


        public void TrainNetworkSingle((float[] input, float[] result) data)
        {
            var (input, output) = data;
            if (input.Length != InputLength || output.Length != OutputLength)
                throw new Exception("Dataset incorrect input or result wrong length");

            var NeuralNet = new (float summ, float act)[configuration.Length][];
            for (int layer = 0; layer < configuration.Length; layer++)
                NeuralNet[layer] = new (float, float)[configuration[layer]];

            int iLastLayer = configuration.Length - 1;

            // FORWARD PROPAGATION INPUT LAYER
            for (int i = 0; i < InputLength; i++)
                NeuralNet[0][i] = (input[i], input[i]);

            // FORWARD PROPAGATION OTHER LAYERS
            for (int l = 1; l < configuration.Length; l++)
                NeuralNet[l] = TrainingActivateLayerParrallel(NeuralNet[l - 1], weightsL[l], weightsB[l], Functions[activation]);

            var Errors = BackpropagateErrorParrallel(NeuralNet, output);

            ApplyErrorsParallel(NeuralNet, Errors, leariningRate);
        }

        float[][] BackpropagateErrorParrallel((float summ, float act)[][] NeuralNet, float[] result)
        {
            var iLastLayer = configuration.Length - 1;
            var Error = new float[OutputLength];

            bool derrivativeUsesActivation = false;
            if (DerrivitiveFromActivation.ContainsKey(activation)) derrivativeUsesActivation = true;
            var d = (derrivativeUsesActivation) ? DerrivitiveFromActivation[activation] : DerrivitiveFromSumm[activation];

            var Errors = new float[configuration.Length][];
            for (int i = 0; i < configuration.Length; i++)
            {
                Errors[i] = new float[configuration[i]];
            }

            Parallel.For(0, OutputLength, (i) =>
            {
                var (summ, act) = NeuralNet[iLastLayer][i];
                var derrivative = derrivativeUsesActivation ? d(act) : d(summ);
                Error[i] = derrivative * (act - result[i]); // Derrivative (activation) * CostFunction (result, expectedResult)
                Errors[iLastLayer][i] += Error[i];
            });


            // ERROR BACKPROP
            for (int l = iLastLayer; l > 0; l--)
            {
                int currentLayerLength = configuration[l];
                int previousLayerLength = configuration[l - 1];

                var currentError = Error;
                Error = new float[previousLayerLength];

                Parallel.For(0, previousLayerLength, (k) =>
                {
                    float tmpError = 0;
                    for (int j = 0; j < currentLayerLength; j++)
                    {
                        tmpError += currentError[j] * weightsL[l][j][k];
                    }
                    var (summ, act) = NeuralNet[l - 1][k];
                    var derrivative = derrivativeUsesActivation ? d(act) : d(summ);
                    Error[k] = tmpError * derrivative;

                    Errors[l - 1][k] += Error[k];
                });
            }
            return Errors;
        }


        void ApplyErrorsParallel((float summ, float act)[][] NeuralNet, float[][] Errors, float learningR)
        {
            Parallel.For(1, configuration.Length, (l) =>
            {
                for (int j = 0; j < configuration[l]; j++)
                {
                    for (int k = 0; k < configuration[l - 1]; k++)
                    {
                        weightsL[l][j][k] -= Errors[l][j] * NeuralNet[l - 1][k].act * learningR;
                    }
                    weightsB[l][j] -= Errors[l][j] * learningR;
                }
            });
        }

        (float, float)[] TrainingActivateLayerParrallel((float, float activation)[] previousLayerValues, float[][] weights, float[] biases, Func<float, float> activate)
        {
            var currentLayerLength = weights.Length;
            var previousLayerLength = previousLayerValues.Length;

            var result = new (float, float)[currentLayerLength];

            Parallel.For(0, currentLayerLength, (x) =>
            {
                float summ = 0;
                for (int i = 0; i < previousLayerLength; i++)
                {
                    summ += weights[x][i] * previousLayerValues[i].activation;
                    if (float.IsNaN(summ)) throw new Exception() ;
                }
                summ += biases[x]; // bias weight * 1
                result[x] = (summ, activate(summ));
                if (float.IsNaN(result[x].Item2)) throw new Exception();
            });

            return result;
        }

        public void Train(List<(float[], float[])> dataset)
        {
            if (dataset.Count < 2) foreach (var set in dataset) TrainNetworkSingle(set);
            else TrainNetwork(dataset);
        }
    }


    public interface ISimpleNeuralNetwork
    {
        float[] Solve(float[] input);
        void Train(List<(float[], float[])> dataset);
    }

}
