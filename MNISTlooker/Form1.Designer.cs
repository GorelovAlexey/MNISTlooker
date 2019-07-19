namespace MNISTlooker
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutContent = new System.Windows.Forms.TableLayoutPanel();
            this.labelReport = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPictures = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.numericIterations = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericBathSize = new System.Windows.Forms.NumericUpDown();
            this.buttonStartTraining = new System.Windows.Forms.Button();
            this.buttonPublicTest = new System.Windows.Forms.Button();
            this.LabelWorking = new System.Windows.Forms.Label();
            this.progressBarWorking = new System.Windows.Forms.ProgressBar();
            this.tableLayoutWrapper = new System.Windows.Forms.TableLayoutPanel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuTestFull = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.buisyTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutContent.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBathSize)).BeginInit();
            this.tableLayoutWrapper.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutContent
            // 
            this.tableLayoutContent.ColumnCount = 2;
            this.tableLayoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutContent.Controls.Add(this.labelReport, 0, 0);
            this.tableLayoutContent.Controls.Add(this.label1, 0, 1);
            this.tableLayoutContent.Controls.Add(this.tableLayoutPictures, 1, 0);
            this.tableLayoutContent.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutContent.Controls.Add(this.LabelWorking, 0, 2);
            this.tableLayoutContent.Controls.Add(this.progressBarWorking, 1, 2);
            this.tableLayoutContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutContent.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutContent.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutContent.Name = "tableLayoutContent";
            this.tableLayoutContent.RowCount = 3;
            this.tableLayoutContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutContent.Size = new System.Drawing.Size(784, 426);
            this.tableLayoutContent.TabIndex = 0;
            // 
            // labelReport
            // 
            this.labelReport.AutoSize = true;
            this.labelReport.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.labelReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelReport.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReport.Location = new System.Drawing.Point(3, 5);
            this.labelReport.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.labelReport.Name = "labelReport";
            this.labelReport.Size = new System.Drawing.Size(194, 338);
            this.labelReport.TabIndex = 4;
            this.labelReport.Text = "Stats:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 346);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 60);
            this.label1.TabIndex = 3;
            this.label1.Text = "Iterations:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPictures
            // 
            this.tableLayoutPictures.ColumnCount = 2;
            this.tableLayoutPictures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPictures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPictures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPictures.Location = new System.Drawing.Point(203, 3);
            this.tableLayoutPictures.Name = "tableLayoutPictures";
            this.tableLayoutPictures.RowCount = 5;
            this.tableLayoutPictures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPictures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPictures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPictures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPictures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPictures.Size = new System.Drawing.Size(578, 340);
            this.tableLayoutPictures.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Silver;
            this.flowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.flowLayoutPanel1.Controls.Add(this.numericIterations);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.numericBathSize);
            this.flowLayoutPanel1.Controls.Add(this.buttonStartTraining);
            this.flowLayoutPanel1.Controls.Add(this.buttonPublicTest);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(200, 346);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(584, 60);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // numericIterations
            // 
            this.numericIterations.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericIterations.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericIterations.Location = new System.Drawing.Point(3, 11);
            this.numericIterations.Margin = new System.Windows.Forms.Padding(3, 11, 3, 3);
            this.numericIterations.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericIterations.Name = "numericIterations";
            this.numericIterations.Size = new System.Drawing.Size(113, 38);
            this.numericIterations.TabIndex = 6;
            this.numericIterations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 18, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Batch size:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericBathSize
            // 
            this.numericBathSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericBathSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBathSize.Location = new System.Drawing.Point(240, 11);
            this.numericBathSize.Margin = new System.Windows.Forms.Padding(3, 11, 3, 3);
            this.numericBathSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericBathSize.Name = "numericBathSize";
            this.numericBathSize.Size = new System.Drawing.Size(69, 38);
            this.numericBathSize.TabIndex = 5;
            this.numericBathSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonStartTraining
            // 
            this.buttonStartTraining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(255)))), ((int)(((byte)(148)))));
            this.buttonStartTraining.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStartTraining.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartTraining.Location = new System.Drawing.Point(315, 11);
            this.buttonStartTraining.Margin = new System.Windows.Forms.Padding(3, 11, 3, 3);
            this.buttonStartTraining.Name = "buttonStartTraining";
            this.buttonStartTraining.Size = new System.Drawing.Size(79, 38);
            this.buttonStartTraining.TabIndex = 0;
            this.buttonStartTraining.Text = "Train";
            this.buttonStartTraining.UseVisualStyleBackColor = false;
            this.buttonStartTraining.Click += new System.EventHandler(this.ButtonStartTraining_Click);
            // 
            // buttonPublicTest
            // 
            this.buttonPublicTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(148)))));
            this.buttonPublicTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPublicTest.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPublicTest.Location = new System.Drawing.Point(402, 11);
            this.buttonPublicTest.Margin = new System.Windows.Forms.Padding(5, 11, 3, 3);
            this.buttonPublicTest.Name = "buttonPublicTest";
            this.buttonPublicTest.Size = new System.Drawing.Size(173, 38);
            this.buttonPublicTest.TabIndex = 7;
            this.buttonPublicTest.Text = "Show 10 training";
            this.buttonPublicTest.UseVisualStyleBackColor = false;
            this.buttonPublicTest.Click += new System.EventHandler(this.ButtonPublicTest_Click);
            // 
            // LabelWorking
            // 
            this.LabelWorking.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LabelWorking.AutoSize = true;
            this.LabelWorking.ForeColor = System.Drawing.Color.DarkRed;
            this.LabelWorking.Location = new System.Drawing.Point(3, 409);
            this.LabelWorking.Name = "LabelWorking";
            this.LabelWorking.Size = new System.Drawing.Size(0, 13);
            this.LabelWorking.TabIndex = 5;
            // 
            // progressBarWorking
            // 
            this.progressBarWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarWorking.Location = new System.Drawing.Point(200, 406);
            this.progressBarWorking.Margin = new System.Windows.Forms.Padding(0);
            this.progressBarWorking.Name = "progressBarWorking";
            this.progressBarWorking.Size = new System.Drawing.Size(584, 20);
            this.progressBarWorking.TabIndex = 6;
            // 
            // tableLayoutWrapper
            // 
            this.tableLayoutWrapper.ColumnCount = 1;
            this.tableLayoutWrapper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutWrapper.Controls.Add(this.tableLayoutContent, 0, 1);
            this.tableLayoutWrapper.Controls.Add(this.MainMenu, 0, 0);
            this.tableLayoutWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutWrapper.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutWrapper.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutWrapper.Name = "tableLayoutWrapper";
            this.tableLayoutWrapper.RowCount = 2;
            this.tableLayoutWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutWrapper.Size = new System.Drawing.Size(784, 461);
            this.tableLayoutWrapper.TabIndex = 1;
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMenu.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuInfo,
            this.optionsToolStripMenuAbout});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainMenu.Size = new System.Drawing.Size(784, 35);
            this.MainMenu.TabIndex = 1;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuReset,
            this.toolStripMenuTestFull});
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(60, 31);
            this.toolStripMenuItem1.Text = "Tools";
            // 
            // toolStripMenuReset
            // 
            this.toolStripMenuReset.BackColor = System.Drawing.Color.Gray;
            this.toolStripMenuReset.Name = "toolStripMenuReset";
            this.toolStripMenuReset.Size = new System.Drawing.Size(222, 26);
            this.toolStripMenuReset.Text = "Reset";
            this.toolStripMenuReset.Click += new System.EventHandler(this.ToolStripMenuReset_Click);
            // 
            // toolStripMenuTestFull
            // 
            this.toolStripMenuTestFull.BackColor = System.Drawing.Color.Gray;
            this.toolStripMenuTestFull.Name = "toolStripMenuTestFull";
            this.toolStripMenuTestFull.Size = new System.Drawing.Size(222, 26);
            this.toolStripMenuTestFull.Text = "Test (with test data)";
            this.toolStripMenuTestFull.Click += new System.EventHandler(this.ToolStripMenuTestFull_Click);
            // 
            // toolStripMenuInfo
            // 
            this.toolStripMenuInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuInfo.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuInfo.Name = "toolStripMenuInfo";
            this.toolStripMenuInfo.Size = new System.Drawing.Size(52, 31);
            this.toolStripMenuInfo.Text = "Info";
            // 
            // optionsToolStripMenuAbout
            // 
            this.optionsToolStripMenuAbout.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsToolStripMenuAbout.ForeColor = System.Drawing.Color.White;
            this.optionsToolStripMenuAbout.Name = "optionsToolStripMenuAbout";
            this.optionsToolStripMenuAbout.Size = new System.Drawing.Size(68, 31);
            this.optionsToolStripMenuAbout.Text = "About";
            this.optionsToolStripMenuAbout.Click += new System.EventHandler(this.OptionsToolStripMenuAbout_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.tableLayoutWrapper);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutContent.ResumeLayout(false);
            this.tableLayoutContent.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBathSize)).EndInit();
            this.tableLayoutWrapper.ResumeLayout(false);
            this.tableLayoutWrapper.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutWrapper;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPictures;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numericIterations;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericBathSize;
        private System.Windows.Forms.Button buttonStartTraining;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuAbout;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuReset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuTestFull;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuInfo;
        private System.Windows.Forms.Button buttonPublicTest;
        private System.Windows.Forms.Label LabelWorking;
        private System.Windows.Forms.ProgressBar progressBarWorking;
        private System.Windows.Forms.Timer buisyTimer;
    }
}

