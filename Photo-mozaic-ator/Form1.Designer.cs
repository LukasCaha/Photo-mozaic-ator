
using System.IO;

namespace Photo_mozaic_ator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.titleLabel = new System.Windows.Forms.Label();
            this.asyncWorker = new System.ComponentModel.BackgroundWorker();
            this.createMozaicButton = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.doneMozaic = new System.Windows.Forms.PictureBox();
            this.openFile = new System.Windows.Forms.Button();
            this.saveFile = new System.Windows.Forms.Button();
            this.generateStonesButton = new System.Windows.Forms.Button();
            this.stoneGeneratingWorker = new System.ComponentModel.BackgroundWorker();
            this.chooseSetDestinationButton = new System.Windows.Forms.Button();
            this.tilesetDestinationDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.chooseTilesetButton = new System.Windows.Forms.Button();
            this.tilesetSourceDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.imageScaleInput = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.snappingFactorInput = new System.Windows.Forms.TextBox();
            this.tileSizeInput = new System.Windows.Forms.TextBox();
            this.ignoreBlackPixelsCheckBox = new System.Windows.Forms.CheckBox();
            this.beforeAfterCheckBox = new System.Windows.Forms.CheckBox();
            this.helpButton = new System.Windows.Forms.Button();
            this.colorDistanceDomain = new System.Windows.Forms.DomainUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.doneMozaic)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Roboto", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.Location = new System.Drawing.Point(13, 13);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(213, 29);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Photo-mozaic-ator";
            // 
            // asyncWorker
            // 
            this.asyncWorker.WorkerReportsProgress = true;
            this.asyncWorker.WorkerSupportsCancellation = true;
            this.asyncWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AsyncWorker_DoWork);
            this.asyncWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AsyncWorker_ProgressChanged);
            this.asyncWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AsyncWorker_RunWorkerCompleted);
            // 
            // createMozaicButton
            // 
            this.createMozaicButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.createMozaicButton.Location = new System.Drawing.Point(13, 397);
            this.createMozaicButton.Name = "createMozaicButton";
            this.createMozaicButton.Size = new System.Drawing.Size(281, 23);
            this.createMozaicButton.TabIndex = 1;
            this.createMozaicButton.Text = "Create mozaic";
            this.createMozaicButton.UseMnemonic = false;
            this.createMozaicButton.UseVisualStyleBackColor = false;
            this.createMozaicButton.Click += new System.EventHandler(this.createMozaicButton_Click);
            // 
            // statusBar
            // 
            this.statusBar.AutoSize = true;
            this.statusBar.Location = new System.Drawing.Point(13, 423);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(42, 15);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "Status:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // doneMozaic
            // 
            this.doneMozaic.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.doneMozaic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.doneMozaic.Image = ((System.Drawing.Image)(resources.GetObject("doneMozaic.Image")));
            this.doneMozaic.Location = new System.Drawing.Point(300, 45);
            this.doneMozaic.Name = "doneMozaic";
            this.doneMozaic.Size = new System.Drawing.Size(488, 375);
            this.doneMozaic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.doneMozaic.TabIndex = 3;
            this.doneMozaic.TabStop = false;
            // 
            // openFile
            // 
            this.openFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.openFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.openFile.Location = new System.Drawing.Point(13, 310);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(281, 23);
            this.openFile.TabIndex = 4;
            this.openFile.Text = "Input File";
            this.openFile.UseVisualStyleBackColor = false;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // saveFile
            // 
            this.saveFile.Location = new System.Drawing.Point(13, 368);
            this.saveFile.Name = "saveFile";
            this.saveFile.Size = new System.Drawing.Size(281, 23);
            this.saveFile.TabIndex = 5;
            this.saveFile.Text = "Output File";
            this.saveFile.UseVisualStyleBackColor = true;
            this.saveFile.Click += new System.EventHandler(this.saveFile_Click);
            // 
            // generateStonesButton
            // 
            this.generateStonesButton.Location = new System.Drawing.Point(13, 74);
            this.generateStonesButton.Name = "generateStonesButton";
            this.generateStonesButton.Size = new System.Drawing.Size(281, 23);
            this.generateStonesButton.TabIndex = 6;
            this.generateStonesButton.Text = "Generate stone set";
            this.generateStonesButton.UseVisualStyleBackColor = true;
            this.generateStonesButton.Click += new System.EventHandler(this.generateStonesButton_Click);
            // 
            // stoneGeneratingWorker
            // 
            this.stoneGeneratingWorker.WorkerReportsProgress = true;
            this.stoneGeneratingWorker.WorkerSupportsCancellation = true;
            this.stoneGeneratingWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StoneGeneratingWorker_DoWork);
            this.stoneGeneratingWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.StoneGeneratingWorker_ProgressChanged);
            this.stoneGeneratingWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.StoneGeneratingWorker_RunWorkerCompleted);
            // 
            // chooseSetDestinationButton
            // 
            this.chooseSetDestinationButton.Location = new System.Drawing.Point(13, 45);
            this.chooseSetDestinationButton.Name = "chooseSetDestinationButton";
            this.chooseSetDestinationButton.Size = new System.Drawing.Size(281, 23);
            this.chooseSetDestinationButton.TabIndex = 7;
            this.chooseSetDestinationButton.Text = "Choose New Tileset directory";
            this.chooseSetDestinationButton.UseVisualStyleBackColor = true;
            this.chooseSetDestinationButton.Click += new System.EventHandler(this.chooseSetDestinationButton_Click);
            // 
            // chooseTilesetButton
            // 
            this.chooseTilesetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.chooseTilesetButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chooseTilesetButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chooseTilesetButton.Location = new System.Drawing.Point(13, 339);
            this.chooseTilesetButton.Name = "chooseTilesetButton";
            this.chooseTilesetButton.Size = new System.Drawing.Size(281, 23);
            this.chooseTilesetButton.TabIndex = 8;
            this.chooseTilesetButton.Text = "Tileset directory";
            this.chooseTilesetButton.UseVisualStyleBackColor = false;
            this.chooseTilesetButton.Click += new System.EventHandler(this.chooseTilesetButton_Click);
            // 
            // imageScaleInput
            // 
            this.imageScaleInput.Location = new System.Drawing.Point(0, 0);
            this.imageScaleInput.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.imageScaleInput.MaxLength = 10;
            this.imageScaleInput.Name = "imageScaleInput";
            this.imageScaleInput.PlaceholderText = "Image scale  ";
            this.imageScaleInput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.imageScaleInput.Size = new System.Drawing.Size(92, 23);
            this.imageScaleInput.TabIndex = 9;
            this.imageScaleInput.TextChanged += new System.EventHandler(this.imageScaleInput_TextChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.imageScaleInput);
            this.flowLayoutPanel1.Controls.Add(this.snappingFactorInput);
            this.flowLayoutPanel1.Controls.Add(this.tileSizeInput);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 281);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(282, 23);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // snappingFactorInput
            // 
            this.snappingFactorInput.Location = new System.Drawing.Point(95, 0);
            this.snappingFactorInput.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.snappingFactorInput.MaxLength = 10;
            this.snappingFactorInput.Name = "snappingFactorInput";
            this.snappingFactorInput.PlaceholderText = "Snapping factor";
            this.snappingFactorInput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.snappingFactorInput.Size = new System.Drawing.Size(92, 23);
            this.snappingFactorInput.TabIndex = 10;
            this.snappingFactorInput.TextChanged += new System.EventHandler(this.snappingFactorInput_TextChanged);
            // 
            // tileSizeInput
            // 
            this.tileSizeInput.Location = new System.Drawing.Point(190, 0);
            this.tileSizeInput.Margin = new System.Windows.Forms.Padding(0);
            this.tileSizeInput.MaxLength = 10;
            this.tileSizeInput.Name = "tileSizeInput";
            this.tileSizeInput.PlaceholderText = "Tile size";
            this.tileSizeInput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tileSizeInput.Size = new System.Drawing.Size(92, 23);
            this.tileSizeInput.TabIndex = 11;
            this.tileSizeInput.TextChanged += new System.EventHandler(this.tileSizeInput_TextChanged);
            // 
            // ignoreBlackPixelsCheckBox
            // 
            this.ignoreBlackPixelsCheckBox.AutoSize = true;
            this.ignoreBlackPixelsCheckBox.Location = new System.Drawing.Point(13, 256);
            this.ignoreBlackPixelsCheckBox.Name = "ignoreBlackPixelsCheckBox";
            this.ignoreBlackPixelsCheckBox.Size = new System.Drawing.Size(123, 19);
            this.ignoreBlackPixelsCheckBox.TabIndex = 11;
            this.ignoreBlackPixelsCheckBox.Text = "Ignore black pixels";
            this.ignoreBlackPixelsCheckBox.UseVisualStyleBackColor = true;
            this.ignoreBlackPixelsCheckBox.CheckedChanged += new System.EventHandler(this.ignoreBlackPixelsCheckBox_CheckedChanged);
            // 
            // beforeAfterCheckBox
            // 
            this.beforeAfterCheckBox.AutoSize = true;
            this.beforeAfterCheckBox.Location = new System.Drawing.Point(142, 256);
            this.beforeAfterCheckBox.Name = "beforeAfterCheckBox";
            this.beforeAfterCheckBox.Size = new System.Drawing.Size(160, 19);
            this.beforeAfterCheckBox.TabIndex = 12;
            this.beforeAfterCheckBox.Text = "Before/after comparation";
            this.beforeAfterCheckBox.UseVisualStyleBackColor = true;
            this.beforeAfterCheckBox.CheckedChanged += new System.EventHandler(this.beforeAfterCheckBox_CheckedChanged);
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(748, 13);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(40, 23);
            this.helpButton.TabIndex = 13;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // colorDistanceDomain
            // 
            this.colorDistanceDomain.AllowDrop = true;
            this.colorDistanceDomain.Items.Add("CIE2000 distance");
            this.colorDistanceDomain.Items.Add("Square distance");
            this.colorDistanceDomain.Items.Add("Bitwise distance");
            this.colorDistanceDomain.Items.Add("CIE76 distance");
            this.colorDistanceDomain.Location = new System.Drawing.Point(13, 227);
            this.colorDistanceDomain.Name = "colorDistanceDomain";
            this.colorDistanceDomain.ReadOnly = true;
            this.colorDistanceDomain.Size = new System.Drawing.Size(282, 23);
            this.colorDistanceDomain.TabIndex = 14;
            this.colorDistanceDomain.Text = "Choose color distance method";
            this.colorDistanceDomain.SelectedItemChanged += new System.EventHandler(this.colorDistanceDomain_SelectedItemChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.colorDistanceDomain);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.beforeAfterCheckBox);
            this.Controls.Add(this.ignoreBlackPixelsCheckBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.chooseTilesetButton);
            this.Controls.Add(this.chooseSetDestinationButton);
            this.Controls.Add(this.generateStonesButton);
            this.Controls.Add(this.saveFile);
            this.Controls.Add(this.openFile);
            this.Controls.Add(this.doneMozaic);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.createMozaicButton);
            this.Controls.Add(this.titleLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Photo-mozaic-ator";
            ((System.ComponentModel.ISupportInitialize)(this.doneMozaic)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.ComponentModel.BackgroundWorker asyncWorker;
        private System.Windows.Forms.Button createMozaicButton;
        private System.Windows.Forms.Label statusBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox doneMozaic;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.Button saveFile;
        private System.Windows.Forms.Button generateStonesButton;
        private System.ComponentModel.BackgroundWorker stoneGeneratingWorker;
        private System.Windows.Forms.Button chooseSetDestinationButton;
        private System.Windows.Forms.Button chooseTilesetButton;
        public System.Windows.Forms.FolderBrowserDialog tilesetDestinationDialog;
        public System.Windows.Forms.FolderBrowserDialog tilesetSourceDialog;
        private System.Windows.Forms.TextBox imageScaleInput;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox snappingFactorInput;
        private System.Windows.Forms.TextBox tileSizeInput;
        private System.Windows.Forms.CheckBox ignoreBlackPixelsCheckBox;
        private System.Windows.Forms.CheckBox beforeAfterCheckBox;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.DomainUpDown colorDistanceDomain;
    }
}

