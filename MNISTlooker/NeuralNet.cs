using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MNISTlooker
{
    /// <summary>
    /// old version each neuron is object
    /// </summary>

    class NeuralNode
    {
        public double error;
        public double activation;
        public double summ;
        public string activationFunction;
        public List<double> weights;
        public List<double> errorAcamulator;

        public NeuralNode(string aFunction = "tanh")
        {
            activationFunction = aFunction;
        }
        public void ResetErrors()
        {
            if (errorAcamulator != null)
            {
                for (int i = 0; i < errorAcamulator.Count; i++)
                {
                    errorAcamulator[i] = 0;
                }
            }
        }
        public double Signal(int index)
        {
            return activation * weights[index];
        }
        public void Input(double value)
        {
            summ = value;
            Activate();
        }
        public void Activate()
        {
            switch (activationFunction)
            {
                case "tanh":
                    activation = 2 / (1 + Math.Exp(-summ * 2)) - 1;
                    break;
                case "sigmoid":
                    activation = 1 / (1 + Math.Pow(Math.E, -summ));
                    break;
                default:///identity
                    activation = 2 / (1 + Math.Exp(-summ * 2)) - 1;
                    break;
            }

        }
        public double ActivationDerrivative()
        {
            switch (activationFunction)
            {
                case "tanh":
                    return 1 - activation * activation;
                case "sigmoid":
                    return activation * (1 - activation);
                default:///identity
                    return 1;
            }
        }
    }

    public class NeuralNetOld : ISimpleNeuralNetwork
    {
        int inputLayer;
        int outputLayer;
        List<List<NeuralNode>> neurons;
        List<double> biasInLayer;
        List<double> biasErrors;

        public NeuralNetOld(List<int> config, int seed)
        {
            neurons = new List<List<NeuralNode>>(config.Count);
            for (int i = 0; i < config.Count; i++)
            {
                neurons.Add(new List<NeuralNode>(config[i]));
                for (int j = 0; j < config[i]; j++)
                {
                    neurons[i].Add(new NeuralNode());
                }
            }
            inputLayer = 0;
            outputLayer = config.Count - 1;
            biasInLayer = new List<double>(outputLayer);
            biasErrors = new List<double>();
            for (int i = 0; i < outputLayer; i++)
            {
                biasInLayer.Add(0.01);
                biasErrors.Add(0);
            }
            ///Инициализация весов
            Random RND = new Random(seed);
            for (int layer = inputLayer; layer < outputLayer; layer++)
            {
                for (int i = 0; i < neurons[layer].Count; i++)
                {
                    neurons[layer][i].weights = new List<double>(neurons[layer + 1].Count);
                    neurons[layer][i].errorAcamulator = new List<double>(neurons[layer + 1].Count);
                    for (int j = 0; j < neurons[layer + 1].Count; j++)
                    {
                        neurons[layer][i].weights.Add(RND.NextDouble() - .5);
                        neurons[layer][i].errorAcamulator.Add(0);
                    }
                }
            }
        }


        public bool ForwardPropogation(List<double> data) /// нету Байасов, пока
        {
            if (data.Count != neurons[inputLayer].Count)
            {
                Console.Write("Invalid Data");
                return false;
            }
            for (int i = 0; i < neurons[inputLayer].Count; i++) ///Вводим данные
            {
                neurons[inputLayer][i].Input(data[i]);
            }
            for (int layer = 1; layer < neurons.Count; layer++) /// Для всех слоев
            {
                for (int i = 0; i < neurons[layer].Count; i++) /// Для каждого нейрона в слое
                {
                    double sum = 0;
                    foreach (var neuron in neurons[layer - 1]) /// Получем от каждого нейрона в предидущем слое активацию * вес к текущему нейрону
                    {
                        sum += neuron.Signal(i);
                    }
                    sum += biasInLayer[layer - 1];
                    neurons[layer][i].Input(sum); /// Активируем нейрон в текущем слое
                }
            }
            return true;
        }
        void ErrorBackProp(List<double> answers)
        {
            Console.Write(" " + MSEout(answers).ToString("F3"));
            /// OutputError
            for (int i = 0; i < neurons[outputLayer].Count; i++)
            {
                var a = neurons[outputLayer][i];
                a.error = a.ActivationDerrivative() * (a.activation - answers[i]);
            }
            /// Error Back propogation
            for (int layer = outputLayer - 1; layer >= 0; layer--) // от предпоследнего слоя до входящего
            {
                for (int i = 0; i < neurons[layer].Count; i++) // для каждого нейрона в слое вычисляем ошибку 
                {
                    double tmpError = 0;
                    for (int j = 0; j < neurons[layer + 1].Count; j++) // ошибка зависит от ошибок в следующем слое и весов
                    {
                        tmpError += neurons[layer][i].weights[j] * neurons[layer + 1][j].error;
                    }
                    neurons[layer][i].error = tmpError * neurons[layer][i].ActivationDerrivative();
                }
            }
        }
        public void TrainBath(List<List<double>> dataSet, List<List<double>> answersSet, double learningRate)
        { /// обучение на нескольких примерах, должно работать быстрей
            int dataSize = dataSet.Count;
            ResetAccamulatedError();
            for (int i = 0; i < dataSize; i++)
            {
                ForwardPropogation(dataSet[i]);
                ErrorBackProp(answersSet[i]);
                AccamulateError();
            }
            ImplyAccamulatedError(dataSize, learningRate);
        }
        void ResetAccamulatedError()
        {
            foreach (var layer in neurons)
            {
                foreach (var neuron in layer)
                {
                    neuron.ResetErrors();
                }
            }
            for (int i = 0; i < biasErrors.Count; i++) biasErrors[i] = 0;
        }
        void AccamulateError()
        {
            for (int layer = 0; layer < outputLayer; layer++)
            {
                for (int i = 0; i < neurons[layer].Count; i++) // ОТКУДА
                {
                    /// del W(i,j) КУДА ОТКУДА  = nu *  Ai КУДА * Ej ОТКУДА
                    for (int j = 0; j < neurons[layer + 1].Count; j++) // КУДА
                    {
                        //double deltaW = learningRate * neurons[layer+1][j].activation * neurons[layer][i].error;
                        double error = neurons[layer][i].activation * neurons[layer + 1][j].error;
                        neurons[layer][i].errorAcamulator[j] += error;
                    }
                }
            }
            for (int layer = 0; layer < outputLayer; layer++)
            {
                double deltaBias = 0;
                foreach (var neuron in neurons[layer + 1])
                {
                    deltaBias += neuron.error;
                }
                biasErrors[layer] += deltaBias;
            }
        }
        void ImplyAccamulatedError(double count, double learningRate)
        {
            for (int layer = 0; layer < outputLayer; layer++)
            {
                for (int i = 0; i < neurons[layer].Count; i++) // ОТКУДА
                {
                    for (int j = 0; j < neurons[layer][i].weights.Count; j++) // КУДА
                    {
                        neurons[layer][i].weights[j] -= neurons[layer][i].errorAcamulator[j] * learningRate / count;
                    }
                }
            }
            for (int layer = 0; layer < outputLayer; layer++)
            {
                biasInLayer[layer] -= learningRate * biasErrors[layer];
            }
        }
        public void AdjustWeights(double learningRate)
        {/// обучение на одном примере и сразу изменение весов
            for (int layer = 0; layer < outputLayer; layer++)
            {
                for (int i = 0; i < neurons[layer].Count; i++) // ОТКУДА
                {
                    /// del W(i,j) КУДА ОТКУДА  = nu *  Ai КУДА * Ej ОТКУДА
                    for (int j = 0; j < neurons[layer + 1].Count; j++) // КУДА
                    {
                        //double deltaW = learningRate * neurons[layer+1][j].activation * neurons[layer][i].error;
                        double deltaW = learningRate * neurons[layer][i].activation * neurons[layer + 1][j].error;
                        neurons[layer][i].weights[j] -= deltaW;
                    }
                }
            }
            for (int layer = 0; layer < outputLayer; layer++)
            {
                double deltaBias = 0;
                foreach (var neuron in neurons[layer + 1])
                {
                    deltaBias += neuron.error;
                }
                biasInLayer[layer] += -1 * learningRate * deltaBias;
            }
        }
        public void TrainSingle(List<double> data, List<double> answers, double learningRate)
        {
            ResetAccamulatedError();
            ForwardPropogation(data);
            ErrorBackProp(answers);
            AdjustWeights(learningRate);
        }
        public string Show(bool minimum = true)
        {
            string ret;
            if (minimum)
            { /// только результат            
                ret = "In: ";
                foreach (var neuron in neurons[inputLayer])
                {
                    ret += " " + neuron.summ.ToString("F1") + "| ";
                }
                ret += "Out: ";
                foreach (var neuron in neurons[outputLayer])
                {
                    ret += neuron.activation.ToString("F2") + "| ";
                }
            }
            else
            { ///Вся сеть
                ret = "";
                int l = 0;
                foreach (var layer in neurons)
                {
                    int n = 0;
                    foreach (var neuron in layer)
                    {
                        if (l == 0) ret += "Input";
                        else if (l == outputLayer) ret += "Output";
                        else ret += "HidLayer" + l.ToString() + "-";
                        ret += n.ToString() + " Act:" + neuron.activation.ToString("F2");
                        if (l != outputLayer)
                        {
                            ret += "| W: ";
                            foreach (var weight in neuron.weights)
                            {
                                ret += weight.ToString("F2") + " | ";
                            }
                        }
                        else
                        {
                            ret += "Result: " + neuron.activation.ToString("F2");
                        }
                        ret += "\n";
                        n++;
                    }
                    if (l < biasInLayer.Count) ret += "Bias in layer" + l.ToString() + " " + biasInLayer[l] + "\n";
                    l++;
                }
            }
            return ret;
        }
        public double MSEout(List<double> correct)
        {
            int outputs = neurons[outputLayer].Count;
            double mse = 0;
            for (int i = 0; i < outputs; i++)
            {
                mse += Math.Pow((correct[i] - neurons[outputLayer][i].activation), 2);
            }
            return mse / outputs;
        }

        public List<double> Result()
        {
            List<double> ret = new List<double>(neurons[outputLayer].Count);
            foreach (var neuron in neurons[outputLayer])
            {
                ret.Add(neuron.activation);
            }
            return ret;

        }

        public List<double> Result(List<double> input)
        {
            ForwardPropogation(input);
            List<double> ret = new List<double>();
            foreach (var neuron in neurons[outputLayer])
            {
                ret.Add(neuron.activation);
            }
            return ret;
        }




        public static ISimpleNeuralNetwork Create(List<int> config, string activation)
        {
            return new NeuralNetOld(config, 0);
        }

        public float[] Solve(float[] input)
        {
            ForwardPropogation(input.Select((x) => (double)x).ToList());
            return neurons.Last().Select((x) => (float)x.activation).ToArray();
        }

        public void Train(List<(float[], float[])> dataset)
        {
            List<List<double>> inputs = new List<List<double>>();
            List<List<double>> results = new List<List<double>>();

            foreach (var (i, o) in dataset)
            {
                inputs.Add(i.Select((x) => (double)x).ToList());
                results.Add(o.Select((x) => (double)x).ToList());
            }

            TrainBath(inputs, results, 0.02);
        }
    }


}

