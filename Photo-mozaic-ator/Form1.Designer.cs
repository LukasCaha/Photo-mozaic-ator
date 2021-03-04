
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
            ((System.ComponentModel.ISupportInitialize)(this.doneMozaic)).BeginInit();
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
            this.createMozaicButton.Location = new System.Drawing.Point(13, 397);
            this.createMozaicButton.Name = "createMozaicButton";
            this.createMozaicButton.Size = new System.Drawing.Size(281, 23);
            this.createMozaicButton.TabIndex = 1;
            this.createMozaicButton.Text = "Create mozaic";
            this.createMozaicButton.UseMnemonic = false;
            this.createMozaicButton.UseVisualStyleBackColor = true;
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
            this.openFile.Location = new System.Drawing.Point(13, 339);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(281, 23);
            this.openFile.TabIndex = 4;
            this.openFile.Text = "Input File";
            this.openFile.UseVisualStyleBackColor = true;
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
            this.generateStonesButton.Location = new System.Drawing.Point(13, 45);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.generateStonesButton);
            this.Controls.Add(this.saveFile);
            this.Controls.Add(this.openFile);
            this.Controls.Add(this.doneMozaic);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.createMozaicButton);
            this.Controls.Add(this.titleLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.doneMozaic)).EndInit();
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
    }
}

