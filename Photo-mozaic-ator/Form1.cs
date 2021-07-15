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
using Photo_mozaic_ator.DistanceStrategies;

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
        private void SetStatus<T>(T status)
        {
            statusBar.Text = "Status: " + status.ToString();
        }

        #region Create Mozaic
        private void createMozaicButton_Click(object sender, EventArgs e)
        {
            if (asyncWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                asyncWorker.RunWorkerAsync(argument: AplicationStatus.inputFile);
            }
            else
            {
                SetStatus("Program is busy");
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

            if (AplicationStatus.inputFile == null)
            {
                throw new FileNotFoundException("Input file not specified");
            }
            //string source = AplicationStatus.inputFile;
            string source = (string)e.Argument;

            Image fromFile = Image.FromFile(source);
            int width = (int)(fromFile.Width * AplicationStatus.imageScale);
            int height = (int)(fromFile.Height * AplicationStatus.imageScale);
            Image targetImage = Mozaicator.ResizeImage(fromFile, width, height);
            fromFile.Dispose();

            int horizontalFaces = width / AplicationStatus.tileSize;
            int verticalFaces = height / AplicationStatus.tileSize;

            int overflowWidth = width % AplicationStatus.tileSize;
            int overflowHeight = height % AplicationStatus.tileSize;

            Bitmap generatedImage = new Bitmap(width - overflowWidth, height - overflowHeight);

            Bitmap b = (Bitmap)targetImage;
            Bitmap selectedFace = null;
            int progress = 0;
            for (int x = 0; x < horizontalFaces; x++)
            {
                for (int y = 0; y < verticalFaces; y++)
                {
                    //find color of region (filename)
                    string filename = Mozaicator.FindClosestColorAndReturnImageName(ref b, x * AplicationStatus.tileSize, y * AplicationStatus.tileSize, AplicationStatus.strategy);

                    //load file to bitmap
                    selectedFace = (Bitmap)Image.FromFile(AplicationStatus.existingTilesetDir + "/" + filename);

                    Mozaicator.CopyRegionIntoImage(selectedFace, new Rectangle(0, 0, AplicationStatus.tileSize, AplicationStatus.tileSize), ref generatedImage, new Rectangle(new Point(x * AplicationStatus.tileSize, y * AplicationStatus.tileSize), new Size(AplicationStatus.tileSize, AplicationStatus.tileSize)));
                }

                worker.ReportProgress((int)(++progress * 100.0f / horizontalFaces));
            }
            worker.ReportProgress(100);
            worker.Dispose();
            b.Dispose();
            selectedFace.Dispose();

            if (AplicationStatus.beforeAfterComparation)
            {
                //add before image
                Image beforeFromFile = Image.FromFile(AplicationStatus.inputFile);
                width = (int)(beforeFromFile.Width * AplicationStatus.imageScale);
                height = (int)(beforeFromFile.Height * AplicationStatus.imageScale);
                Image beforeImg = Mozaicator.ResizeImage(beforeFromFile, width, height);
                Bitmap before = new Bitmap(beforeImg);
                Bitmap after = generatedImage;

                Bitmap comparation = new Bitmap(before.Width + after.Width, Math.Max(before.Height, after.Height));
                using (Graphics g = Graphics.FromImage(comparation))
                {
                    g.DrawImage(before, 0, 0);
                    g.DrawImage(after, before.Width, 0);
                }

                //AplicationStatus.SetOutputImage(comparation);
                //AplicationStatus.outputImage = comparation;
                e.Result = comparation;
                comparation.Save(AplicationStatus.GetOutputFile());

                beforeFromFile.Dispose();
                beforeImg.Dispose();
                after.Dispose();
            }
            else
            {
                //AplicationStatus.SetOutputImage(generatedImage);
                //AplicationStatus.outputImage = generatedImage;
                e.Result = generatedImage;
                generatedImage.Save(AplicationStatus.GetOutputFile());
            }
        }

        private void AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetStatus(e.ProgressPercentage + "%");
        }

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
                SetStatus("Done!");
                //doneMozaic.Image = AplicationStatus.outputImage;
                doneMozaic.Image = (Bitmap)e.Result;
                AplicationStatus.outputImage = (Bitmap)e.Result;
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

            //for each of N images
            int images = Directory.GetFiles(@"./tileset_source", "*", SearchOption.TopDirectoryOnly).Length;
            for (int imageNum = 1; imageNum <= images; imageNum++)
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
            if (tilesetDestinationDialog.ShowDialog() == DialogResult.OK)
            {
                SetStatus(tilesetDestinationDialog.SelectedPath);
                AplicationStatus.newTilesetDir = tilesetDestinationDialog.SelectedPath;
            }
        }
        private void chooseTilesetButton_Click(object sender, EventArgs e)
        {
            if (tilesetSourceDialog.ShowDialog() == DialogResult.OK)
            {
                SetStatus(tilesetSourceDialog.SelectedPath);
                AplicationStatus.existingTilesetDir = tilesetSourceDialog.SelectedPath;
            }
        }
        #endregion

        #region Config
        private void imageScaleInput_TextChanged(object sender, EventArgs e)
        {
            double newImageScale = AplicationStatus.imageScale;
            if (double.TryParse(imageScaleInput.Text, out newImageScale))
            {
                AplicationStatus.imageScale = newImageScale;
            }
            SetStatus($"imageScale = {AplicationStatus.imageScale}");
        }

        private void snappingFactorInput_TextChanged(object sender, EventArgs e)
        {
            double newSnappingFactor = AplicationStatus.snappingFactor;
            if (double.TryParse(snappingFactorInput.Text, out newSnappingFactor))
            {
                AplicationStatus.snappingFactor = newSnappingFactor;
            }
            SetStatus($"snappingFactor = {AplicationStatus.snappingFactor}");
        }

        private void tileSizeInput_TextChanged(object sender, EventArgs e)
        {
            int newTileSize = AplicationStatus.tileSize;
            if (int.TryParse(tileSizeInput.Text, out newTileSize))
            {
                AplicationStatus.tileSize = newTileSize;
            }
            SetStatus($"tileSize = {AplicationStatus.tileSize}");
        }

        private void ignoreBlackPixelsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int ignoreBlackPixels = ignoreBlackPixelsCheckBox.Checked ? 1 : 0;
            AplicationStatus.ignoreBlackPixels = ignoreBlackPixels;
            SetStatus($"ignoreBlackPixels = " + (AplicationStatus.ignoreBlackPixels==1 ? "true" : "false"));
        }

        private void beforeAfterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AplicationStatus.beforeAfterComparation = beforeAfterCheckBox.Checked;
            SetStatus($"beforeAfterComparation = " + (AplicationStatus.beforeAfterComparation ? "true" : "false"));
        }
        #endregion

        private void helpButton_Click(object sender, EventArgs e)
        {
            var form = new HelpForm();
            form.Show();
        }

        private void colorDistanceDomain_SelectedItemChanged(object sender, EventArgs e)
        {
            switch (colorDistanceDomain.Items.ToArray()[colorDistanceDomain.SelectedIndex])
            {
                case "Square distance":
                    //AplicationStatus.strategy = new SquareDistanceStrategy();
                    AplicationStatus.SetColorDistanceStrategy(new SquareDistanceStrategy());
                    SetStatus($"Chosen strategy is {AplicationStatus.strategy.ToString()}");
                    break;
                case "Bitwise distance":
                    //AplicationStatus.strategy = new BitwiseDistanceStrategy();
                    AplicationStatus.SetColorDistanceStrategy(new BitwiseDistanceStrategy());
                    SetStatus($"Chosen strategy is {AplicationStatus.strategy.ToString()}");
                    break;
                case "CIE76 distance":
                    //AplicationStatus.strategy = new CIE76DistanceStrategy();
                    AplicationStatus.SetColorDistanceStrategy(new CIE76DistanceStrategy());
                    SetStatus($"Chosen strategy is {AplicationStatus.strategy.ToString()}");
                    break;
                default:
                    SetStatus("Strategy not found");
                    break;
            }
        }
    }
}
