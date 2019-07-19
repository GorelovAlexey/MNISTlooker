using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace MNISTlooker
{


    public partial class Form1 : Form
    {
        MNISTWorker MNIST;

        string testReport = "";
        string trainingReport = "";

        int _trained = 0;

        bool _buisy = false;
        bool Buisy
        {
            get
            {
                return _buisy;
            }
            set
            {
                _buisy = value;
                if (_buisy)  buisyTimer.Start();
                else
                {
                    buisyTimer.Stop();
                    progressBarWorking.Value = 0;
                    LabelWorking.Text = "";
                }
                    
            }
        }

        public int Trained
        {
            get
            {
                return _trained;
            }
            set
            {
                _trained = value;
                UpdateReport();
            }
        }


        public Form1()
        {
            InitializeComponent();
            InitItems();
            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }






        private void InitItems()
        {
            for (int i = 0; i < 10; i++)
                this.tableLayoutPictures.Controls.Add(InitItem(i));
        }

        private SplitContainer InitItem(int index)
        {
            var itemSplitContainer = new SplitContainer();
            // var itemPictureBox = new PictureBox();
            //itemSplitContainer.Panel1.Controls.Add(itemPictureBox);
            itemSplitContainer.Panel1.BackgroundImageLayout = ImageLayout.Stretch;


            var itemLabel = new Label();
            itemSplitContainer.Panel2.Controls.Add(itemLabel);


            // 
            // itemSplitContainer
            // 
            itemSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            itemSplitContainer.IsSplitterFixed = true;
            itemSplitContainer.Location = new System.Drawing.Point(0, 0);
            itemSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            itemSplitContainer.Name = $"itemSplitContainer{index}";
            itemSplitContainer.Panel2MinSize = 0;
            itemSplitContainer.Panel1MinSize = 0;

            itemSplitContainer.SizeChanged += (sender, eventArgs) =>
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    var container = sender as SplitContainer;
                    var splitterDisnace = container.Size.Height;
                    container.SplitterDistance = splitterDisnace;
                }                   
            };
      

            var height = itemSplitContainer.Size.Height;
            //itemSplitContainer.Panel1.Size = new Size(height, height);
            itemSplitContainer.SplitterDistance = height;

            // 
            // itemPictureBox
            /*
            itemPictureBox.Dock = DockStyle.Fill;
            itemPictureBox.Location = new System.Drawing.Point(0, 0);
            itemPictureBox.Margin = new System.Windows.Forms.Padding(0);
            itemPictureBox.Name = $"itemPictureBox{index}";
            itemPictureBox.Size = new System.Drawing.Size(75, 80);
            itemPictureBox.TabIndex = 0;
            itemPictureBox.TabStop = false;
            itemPictureBox.BackColor = Color.AliceBlue;
            */ 
            // itemLabel
            // 
            itemLabel.AutoSize = false;
            itemLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            itemLabel.Location = new System.Drawing.Point(0, 0);
            itemLabel.Name = $"itemLabel{index}";
            itemLabel.Size = new System.Drawing.Size(35, 13);
            itemLabel.TabIndex = 0;
            itemLabel.Text = "";

            return itemSplitContainer;
        }


        private void Reset()
        {
            foreach(SplitContainer sc in tableLayoutPictures.Controls)
            {
                foreach (PictureBox picBox in sc.Panel1.Controls) picBox.Image = null;
                foreach (Label l in sc.Panel2.Controls) l.Text = "";
            }
            testReport = "";
            trainingReport = "";
            Trained = 0;
            MNIST = new MNISTWorker();
            buisyTimer?.Dispose();
            buisyTimer = new Timer { Interval = 100, Enabled = true };
            buisyTimer.Stop();
            buisyTimer.Tick += ShowHowBuisy;

        }

        private async void ButtonStartTraining_Click(object sender, EventArgs e)
        {
            if (!Buisy)
            {
                Buisy = true;
                int iterations = (int)numericIterations.Value;
                int batch = (int)numericBathSize.Value;
                await Task.Factory.StartNew( ()=>  MNIST.TrainBatch(batch, iterations));
                Trained += iterations * batch;
                trainingReport = $"Last training - batch on {batch} items {iterations} times";
                Buisy = false;
            }
        }

        private void ButtonPublicTest_Click(object sender, EventArgs e)
        {

            int itemsAmount = 10;
            var result = MNIST.PublicSingleTraining(itemsAmount);
            int index = 0;
            int successful = 0;
            foreach (SplitContainer item in tableLayoutPictures.Controls)
            {
                var (picture, correct, predictions) = result[index];

                item.Panel1.BackgroundImage = picture;

                foreach (PictureBox picB in item.Panel1.Controls) picB.Image = picture;

                var sb = new StringBuilder();
                var predicted = predictions.ToList().IndexOf(predictions.Max());
                sb.Append($"Correct: {correct} \nPrediction: {predicted} \nOutput values: ");

                for (int i = 0; i < predictions.Length; i++)
                {
                    sb.Append($"{i}: {predictions[i].ToString("0.000")} ,  ");
                }

                foreach (Label label in item.Panel2.Controls) label.Text = sb.ToString();

                index++;
                if (predicted == correct) successful++;
            }
            trainingReport = $"Last training - correct predictions {successful} out of {itemsAmount}";
            UpdateReport();            
        }

        private void ToolStripMenuReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void UpdateReport()
        {
            labelReport.Text = $"{trainingReport}\nTraining: {Trained}  \n {testReport}"; 
        }

        private async void ToolStripMenuTestFull_Click(object sender, EventArgs e)
        {
            if (!Buisy)
            {
                Buisy = true;
                var result = await Task.Factory.StartNew(() => MNIST.TestOnTestData());
                testReport += $"Test on training data, accuracy: {result.ToString("0.0000")}";
                labelReport.Text = $"{trainingReport}\nTraining: {Trained}  \n {testReport}";
                Buisy = false;
            }
        }


        void ShowHowBuisy (object sender, EventArgs args)
        {
            var conditions = new string[] { "Buisy.", "Buisy..", "Buisy...", "Buisy...." };
 
            var text = LabelWorking.Text;
            bool chanded = false;

            progressBarWorking.Value = (9 + progressBarWorking.Value) % progressBarWorking.Maximum;

            for (int i=0; i<conditions.Length;i++)
            {
                if (text == conditions[i])
                {
                    text = conditions[++i % conditions.Length];
                    chanded = true;
                    break;
                }
            }
            if (!chanded) text = conditions[0];

            LabelWorking.Text = text;
        }

        private void TableLayoutContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OptionsToolStripMenuAbout_Click(object sender, EventArgs e)
        {
            string about = "Приложение";
            MessageBox.Show(about);
        }
    }

}
