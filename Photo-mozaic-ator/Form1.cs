using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Photo_mozaic_ator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //SetStatus(Directory.GetCurrentDirectory());
            tilesetDestinationDialog.SelectedPath = Path.GetFullPath(AplicationStatus.workingDirectory);
            tilesetSourceDialog.SelectedPath = Path.GetFullPath(AplicationStatus.workingDirectory);
        }

        #region Create Mozaic
        private void createMozaicButton_Click(object sender, EventArgs e)
        {
            if (asyncWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                asyncWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>
        /// <para>0 = success</para>
        /// <para>1 = input file error</para>
        /// </returns>
        private void AsyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            /*
             if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
            */
            if (AplicationStatus.inputFile == null)
            {
                //SetStatus("Input file not specified");
                throw new FileNotFoundException("Input file not specified");
            }
            string source = AplicationStatus.inputFile;

            Image fromFile = Image.FromFile(source);
            Image targetImage = Mozaicator.ResizeImage(fromFile, fromFile.Width * AplicationStatus.imageScale, fromFile.Height * AplicationStatus.imageScale);

            int width = targetImage.Width;
            int height = targetImage.Height;

            int horizontalFaces = width / AplicationStatus.tileSize;
            int verticalFaces = height / AplicationStatus.tileSize;

            int overflowWidth = width % AplicationStatus.tileSize;
            int overflowHeight = height % AplicationStatus.tileSize;

            Bitmap generatedImage = new Bitmap(width - overflowWidth, height - overflowHeight);

            Bitmap b = (Bitmap)targetImage;
            int progress = 0;
            for (int x = 0; x < horizontalFaces; x++)
            {
                for (int y = 0; y < verticalFaces; y++)
                {
                    //find color of region (filename)
                    string filename = Mozaicator.FindClosestColorAndReturnImageName(ref b, x * AplicationStatus.tileSize, y * AplicationStatus.tileSize);

                    //load file to bitmap
                    Bitmap selectedFace;
                    //if ()
                    //{

                    //}
                    //else
                    //{
                    //    selectedFace = (Bitmap)Image.FromFile("blank.bmp");
                    //}
                    selectedFace = (Bitmap)Image.FromFile(AplicationStatus.existingTilesetDir +"/" + filename);

                    Mozaicator.CopyRegionIntoImage(selectedFace, new Rectangle(0, 0, AplicationStatus.tileSize, AplicationStatus.tileSize), ref generatedImage, new Rectangle(new Point(x * AplicationStatus.tileSize, y * AplicationStatus.tileSize), new Size(AplicationStatus.tileSize, AplicationStatus.tileSize)));
                }

                worker.ReportProgress((int)(++progress * 100.0f / horizontalFaces));
            }
            worker.ReportProgress(100);
            AplicationStatus.outputImage = generatedImage;
            generatedImage.Save(AplicationStatus.GetOutputFile());
        }

        // This event handler updates the progress.
        private void AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetStatus(e.ProgressPercentage + "%");
        }

        // This event handler deals with the results of the background operation.
        private void AsyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                SetStatus("Canceled!");
            }
            else if (e.Error != null)
            {
                SetStatus(e.Error.Message);
            }
            else
            {
                //SetStatus("Done!");
                doneMozaic.Image = AplicationStatus.outputImage;
            }
        }
        #endregion

        #region Stone generation
        private void generateStonesButton_Click(object sender, EventArgs e)
        {
            if (asyncWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                stoneGeneratingWorker.RunWorkerAsync();
            }
        }

        private void StoneGeneratingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //for each of 10000 images
            int images = 899;
            for (int imageNum = 1; imageNum < 899; imageNum++)
            {
                StoneGenerator.GenerateOneTile(imageNum);
                worker.ReportProgress((int)(imageNum * 100.0f / images));
            }
            worker.ReportProgress(100);
        }

        private void StoneGeneratingWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetStatus(e.ProgressPercentage + "%");
        }

        private void StoneGeneratingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                SetStatus("Canceled!");
            }
            else if (e.Error != null)
            {
                SetStatus(e.Error.Message);
            }
            else
            {
                //SetStatus("Done!");
            }
        }
        #endregion

        #region File IO Events
        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            AplicationStatus.inputFile = openFileDialog.FileName;
        }
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            AplicationStatus.outputFile = saveFileDialog.FileName;
        }
        private void openFile_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }
        private void saveFile_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }
        private void chooseSetDestinationButton_Click(object sender, EventArgs e)
        {
            if(tilesetDestinationDialog.ShowDialog() == DialogResult.OK)
            {
                SetStatus(tilesetDestinationDialog.SelectedPath);
                AplicationStatus.newTilesetDir = tilesetDestinationDialog.SelectedPath;
            }
        }
        #endregion

        private void SetStatus<T>(T status)
        {
            statusBar.Text = "Status: " + status.ToString();
        }

        private void chooseTilesetButton_Click(object sender, EventArgs e)
        {
            if (tilesetSourceDialog.ShowDialog() == DialogResult.OK)
            {
                SetStatus(tilesetSourceDialog.SelectedPath);
                AplicationStatus.existingTilesetDir = tilesetSourceDialog.SelectedPath;
            }
        }
    }
}
