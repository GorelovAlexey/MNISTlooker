using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNISTlooker
{
    class MNISTData
    {
        public (byte label, byte[] img)[] Data { get; }
        public Size ImageSize { get; }

        public MNISTData(string imagesPath, string labelsPath)
        {
            try
            {
                var imagesRaw = File.ReadAllBytes(imagesPath);
                var labelsRaw = File.ReadAllBytes(labelsPath);

                MirrorImagesAndLabelsHeading(imagesRaw, labelsRaw);

                int imagesMagicNumber = BitConverter.ToInt32(imagesRaw, 0);
                if (imagesMagicNumber != 2051) throw new Exception("wrong magic number: images");

                int labelsMagicNumber = BitConverter.ToInt32(labelsRaw, 0);
                if (labelsMagicNumber != 2049) throw new Exception("wrong magic number: labels");

                //MirrorBytes(imagesRaw, 4);
                var rows = BitConverter.ToInt32(imagesRaw, 8);
                var columns = BitConverter.ToInt32(imagesRaw, 12);
                ImageSize = new Size(columns, rows);
                Data = LoadData(labelsRaw, imagesRaw);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("You need to download MNIST DATABASE and put all 4 datasets in folder with executable for this application to work");
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(0);
            }

        }

        public void MirrorImagesAndLabelsHeading(byte[] images, byte[] labels)
        {
            for (int i = 0; i < 2; i++) MirrorBytes(labels, i * sizeof(int));
            for (int i = 0; i < 4; i++) MirrorBytes(images, i * sizeof(int));
        }

        public void MirrorBytes(byte[] data, int starting)
        {
            int res = BitConverter.ToInt32(data, starting);
            res = IPAddress.HostToNetworkOrder(res);
            var bytes = BitConverter.GetBytes(res);
            for (int i = 0; i < bytes.Length; i++)
                data[starting + i] = bytes[i];
        }

        (byte label, byte[] img)[] LoadData(byte[] labelFile, byte[] imgFile)
        {
            var amountOfItems = BitConverter.ToInt32(labelFile, 4);
            if (BitConverter.ToInt32(imgFile, 4) != amountOfItems) throw new Exception("Amount of items in files differ!");

            var data = new (byte, byte[])[amountOfItems];

            for (int i = 0; i < amountOfItems; i++)
            {
                data[i] = (LoadLabel(labelFile, i), LoadImage(imgFile, i));
            }

            return data;
        }

        byte[] LoadImage(byte[] file, int index)
        {
            int imageSize = ImageSize.Height * ImageSize.Width;
            int IMAGE_OFFSET = 16;
            int starting = IMAGE_OFFSET + index * imageSize;

            var img = new byte[imageSize];
            Array.Copy(file, starting, img, 0, imageSize);
            return img;
        }

        byte LoadLabel(byte[] file, int index)
        {
            int LABEL_OFFSET = 8;
            return file[index + LABEL_OFFSET];
        }

    }
}
