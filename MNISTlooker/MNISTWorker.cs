using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNISTlooker
{
    class MNISTWorker
    {
        readonly MNISTData TrainingSet;
        readonly Random RND;

        readonly ISimpleNeuralNetwork NN;
        //readonly NeuralNet NeuralNet;

        public int TraningCount
        {
            get;
            private set;
        }

        public MNISTWorker()
        {
            var trainingImg = @"train-images.idx3-ubyte";
            var traininglabels = @"train-labels.idx1-ubyte";
            TrainingSet = new MNISTData(trainingImg, traininglabels);
            NN = NeuralNetwork.Create(new List<int> { 28 * 28, 30, 10 }, "");
            RND = new Random();
            TraningCount = 0;
        }

        Bitmap MnistImageToBitmap(byte[] img, Size imgSize)
        {
            var (width, height) = (imgSize.Width, imgSize.Height);
            Bitmap bmp = new Bitmap(width, height);

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    int pixel = (int)img[row * width + column];
                    bmp.SetPixel(column, row, Color.FromArgb(pixel, pixel, pixel));
                }
            }
            return bmp;
        }


        public void TrainBatch(int batchSize, int iterations)
        {
            var sv = new Stopwatch();
            sv.Restart();
            while (iterations-- > 0)
            {
                List<(float[], float[])> data = new List<(float[], float[])>();

                for (int i = 0; i < batchSize; i++)
                {
                    var (label, pixels) = MNISTRandomData(TrainingSet.Data);
                    data.Add((InputNeurons(pixels).ToArray(), ResultNeurons(label).ToArray()));
                }
                NN.Train(data);
            }
            TraningCount += batchSize * iterations;
            sv.Stop();
            MessageBox.Show($"{sv.ElapsedMilliseconds / 1000.0} {sv.ElapsedTicks}");
        }

        /// <summary>
        /// Trains neural network multiple times
        /// </summary>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public (Bitmap picture, byte correct, float[] predictions)[] PublicSingleTraining(int iterations)
        {
            var ret = new (Bitmap picture, byte correct, float[] predictions)[iterations];

            for (int i = 0; i < iterations; i++)
            {
                var (label, pixels) = MNISTRandomData(TrainingSet.Data);
                var inputLayer = InputNeurons(pixels);

                var result = NN.Solve(inputLayer);

                var img = MnistImageToBitmap(pixels, TrainingSet.ImageSize);

                ret[i] = (img, label, result);
            }

            return ret;
        }



        (byte label, byte[] pixels) MNISTRandomData((byte, byte[])[] data)
        {
            var index = RND.Next(data.Length);
            return data[index];
        }

        float[] ResultNeurons(byte label)
        {
            var ret = new float[10];
            for (int i = 0; i < ret.Length; i++) ret[i] = -1.0f;
            ret[label] = 1;
            return ret;
        }

        float[] InputNeurons(byte[] pixels)
        {
            var ret = new float[pixels.Length];

            for(int i = 0; i < pixels.Length; i++)
            {
                ret[i] = pixels[i] / 256.0f;
            }
            return ret;
        }

        double FullTest((byte label, byte[] pixels)[] data)
        {
            int correctPredictions = 0;

            foreach (var (label, pixels) in data)
            {
                var inputLayer = InputNeurons(pixels);
                var predictions = NN.Solve(inputLayer);

                var prediction = predictions.ToList().IndexOf(predictions.Max());

                if (prediction == label) correctPredictions++;
            }

            var ratio = (double)correctPredictions / data.Length;
            return ratio;
        }

        /// <summary>
        /// Full test on MNIST testing set 
        /// </summary>
        /// <returns>Ratio of correct answers to total</returns>
        public double TestOnTestData()
        {

            var testImagesPath = @"train-images.idx3-ubyte";
            var testLablesPath = @"train-labels.idx1-ubyte";

            var testData = new MNISTData(testImagesPath, testLablesPath);
            var result = FullTest(testData.Data);

            return result;
        }









        public class ActiveChangedEventArgs : EventArgs
        {
            public bool Active { get; set; }
            public ActiveChangedEventArgs(bool active)
            {
                Active = active;
            }
        }


    }

   

}
