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
            tilesetDestinationDialog.SelectedPath = Path.GetFullPath(AplicationStatus.workingDirectory);
            tilesetSourceDialog.SelectedPath = Path.GetFullPath(AplicationStatus.workingDirectory);
        }

        #region Other UI controls
        /// <summary>
        /// <para>Opens help form with instructions.</para>
        /// </summary>
        private void helpButton_Click(object sender, EventArgs e)
        {
            var form = new HelpForm();
            form.Show();
        }

        /// <summary>
        /// <para>Prints object from parameter to status bar in main form.</para>
        /// <para>Object will be printed using ToString() method.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="status">Object to be printed.</param>
        private void SetStatus<T>(T status)
        {
            statusBar.Text = "Status: " + status.ToString();
        }
        #endregion

        #region Create Mozaic
        /// <summary>
        /// <para>Result of this event is starting mozaic creating process.</para>
        /// <para>Starts only if responsible worker is free to compute.</para>
        /// <para>All information/parameters for mozaic creating process are contained in <see cref="AplicationStatus"/> class.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createMozaicButton_Click(object sender, EventArgs e)
        {
            if (asyncWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                asyncWorker.RunWorkerAsync();
            }
            else
            {
                SetStatus("Program is busy");
            }
        }

        /// <summary>
        /// <para>Async worker responsible for creation of mozaics.</para>
        /// <para>Method comunicates with outside program using <see cref="AplicationStatus"/> class.</para>
        /// <list type="number">
        /// <listheader>
        /// Order of operations
        /// </listheader>
        /// <item>
        /// Loads and prepares input file.
        /// </item>
        /// <item>
        /// Finds proper tile representation of all size X size blocks.
        /// </item>
        /// <item>
        /// Saves final image to <see cref="AplicationStatus"/> class.
        /// </item>
        /// </list>
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
                //caught and processed which outputs using SetStatus() method
                throw new FileNotFoundException("Input file not specified");
            }
            string source = AplicationStatus.inputFile;
            Image fromFile = Image.FromFile(source);
            int width = (int)(fromFile.Width * AplicationStatus.imageScale);
            int height = (int)(fromFile.Height * AplicationStatus.imageScale);
            Image targetImage = Mozaicator.ResizeImage(fromFile, width, height);
            fromFile.Dispose();
            //based on original image size and tile size, calculates how many X,Y tiles will image have
            int horizontalFaces = width / AplicationStatus.tileSize;
            int verticalFaces = height / AplicationStatus.tileSize;

            int overflowWidth = width % AplicationStatus.tileSize;
            int overflowHeight = height % AplicationStatus.tileSize;
            //crops overflow so there are no half tiles at the end of mozaic
            Bitmap generatedImage = new Bitmap(width - overflowWidth, height - overflowHeight);

            Bitmap sourceImage = (Bitmap)targetImage;
            Bitmap selectedTileFile = null;
            int progress = 0;
            Size tileSize = new Size(AplicationStatus.tileSize, AplicationStatus.tileSize);
            Rectangle tileSizeRectangleFromTileFile = new Rectangle(new Point(0, 0), tileSize);
            for (int x = 0; x < horizontalFaces; x++)
            {
                for (int y = 0; y < verticalFaces; y++)
                {
                    //finds which tile-file to use based on color
                    string filename = Mozaicator.FindClosestColorAndReturnImageName(
                        ref sourceImage,
                        x * AplicationStatus.tileSize,
                        y * AplicationStatus.tileSize,
                        AplicationStatus.strategy);

                    //loads tile-file to bitmap
                    selectedTileFile = (Bitmap)Image.FromFile(AplicationStatus.existingTilesetDir + "/" + filename);

                    Rectangle tileSizeRectangleInFinalImage =
                        new Rectangle(
                            new Point(
                                x * AplicationStatus.tileSize,
                                y * AplicationStatus.tileSize
                            ),
                            tileSize
                        );


                    //inserts into final image
                    Mozaicator.CopyRegionIntoImage(
                                selectedTileFile,
                                tileSizeRectangleFromTileFile,
                                ref generatedImage,
                                tileSizeRectangleInFinalImage);
                }

                //updates % in UI
                worker.ReportProgress((int)(++progress * 100.0f / horizontalFaces));
            }
            worker.ReportProgress(100);
            worker.Dispose();
            sourceImage.Dispose();
            selectedTileFile.Dispose();

            // SAVING section - special setting creating comparation for images
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

                e.Result = comparation;
                comparation.Save(AplicationStatus.GetOutputFile());

                beforeFromFile.Dispose();
                beforeImg.Dispose();
                after.Dispose();
            }
            else
            {
                e.Result = generatedImage;
                generatedImage.Save(AplicationStatus.GetOutputFile());
            }
        }

        /// <summary>
        /// <para>Updates status with current mozaic creating progess.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetStatus(e.ProgressPercentage + "%");
        }

        /// <summary>
        /// <para>After completion of <see cref="AsyncWorker_DoWork"/> handles errors or saves final image.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                doneMozaic.Image = (Bitmap)e.Result;
                AplicationStatus.outputImage = (Bitmap)e.Result;
            }
        }
        #endregion

        #region Stone generation 
        
        /// <summary>
        /// <para>Result of this event is starting tileset generating process.</para>
        /// <para>Starts only if responsible worker is free to compute.</para>
        /// <para>All information/parameters for tileset generating process are contained in <see cref="AplicationStatus"/> class.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateStonesButton_Click(object sender, EventArgs e)
        {
            if (asyncWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                stoneGeneratingWorker.RunWorkerAsync();
            }
            else
            {
                SetStatus("Program is busy");
            }
        }

        /// <summary>
        /// <para>Foreach image in folder creates one file with color name as file name.</para>
        /// <para>All information/parameters for tile creation process are contained in <see cref="AplicationStatus"/> class.</para>
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// <para>Updates status with tileset creating progess.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoneGeneratingWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetStatus(e.ProgressPercentage + "%");
        }

        /// <summary>
        /// <para>After completion of <see cref="StoneGeneratingWorker_DoWork"/> handles errors.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                SetStatus("Done!");
            }
        }
        #endregion

        #region File IO Events

        /// <summary>
        /// <para>Sets <see cref="AplicationStatus.inputFile"/> image to be mozaic-atored.</para>
        /// </summary>
        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            AplicationStatus.inputFile = openFileDialog.FileName;
        }

        /// <summary>
        /// <para>Sets <see cref="AplicationStatus.outputFile"/> path where to output.</para>
        /// </summary>
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            AplicationStatus.outputFile = saveFileDialog.FileName;
        }

        /// <summary>
        /// <para>Opens input image dialog.</para>
        /// </summary>
        private void openFile_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        /// <summary>
        /// <para>Opens output image dialog.</para>
        /// </summary>
        private void saveFile_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        /// <summary>
        /// <para>Opens dialog and sets <see cref="AplicationStatus.newTilesetDir"/> directory where to save new tileset.</para>
        /// </summary>
        private void chooseSetDestinationButton_Click(object sender, EventArgs e)
        {
            if (tilesetDestinationDialog.ShowDialog() == DialogResult.OK)
            {
                SetStatus(tilesetDestinationDialog.SelectedPath);
                AplicationStatus.newTilesetDir = tilesetDestinationDialog.SelectedPath;
            }
        }

        /// <summary>
        /// <para>Opens dialog and sets directory <see cref="AplicationStatus.existingTilesetDir"/> where program takes tiles.</para>
        /// </summary>
        private void chooseTilesetButton_Click(object sender, EventArgs e)
        {
            if (tilesetSourceDialog.ShowDialog() == DialogResult.OK)
            {
                SetStatus(tilesetSourceDialog.SelectedPath);
                AplicationStatus.existingTilesetDir = tilesetSourceDialog.SelectedPath;
                Mozaicator.matches.Clear();
            }
        }
        #endregion

        #region Config

        /// <summary>
        /// <para>Sets <see cref="AplicationStatus.imageScale"/> config.</para>
        /// <para>Used to scale up image before creating mozaic to multiply tiles in final image.</para>
        /// </summary>
        private void imageScaleInput_TextChanged(object sender, EventArgs e)
        {
            double newImageScale = AplicationStatus.imageScale;
            if (double.TryParse(imageScaleInput.Text, out newImageScale))
            {
                AplicationStatus.imageScale = newImageScale;
            }
            SetStatus($"imageScale = {AplicationStatus.imageScale}");
        }

        /// <summary>
        /// <para>Sets <see cref="AplicationStatus.snappingFactor"/> config.</para>
        /// <para>Snaps color to smaller color pallete to reduce number of tiles in tileset.</para>
        /// </summary>
        private void snappingFactorInput_TextChanged(object sender, EventArgs e)
        {
            double newSnappingFactor = AplicationStatus.snappingFactor;
            if (double.TryParse(snappingFactorInput.Text, out newSnappingFactor))
            {
                AplicationStatus.snappingFactor = newSnappingFactor;
                Mozaicator.matches.Clear();
            }
            SetStatus($"snappingFactor = {AplicationStatus.snappingFactor}");
        }

        /// <summary>
        /// <para>Sets <see cref="AplicationStatus.tileSize"/> config.</para>
        /// <para>Size of one mozaic tile. Both for tile creating and tile usage.</para>
        /// </summary>
        private void tileSizeInput_TextChanged(object sender, EventArgs e)
        {
            int newTileSize = AplicationStatus.tileSize;
            if (int.TryParse(tileSizeInput.Text, out newTileSize))
            {
                AplicationStatus.tileSize = newTileSize;
            }
            SetStatus($"tileSize = {AplicationStatus.tileSize}");
        }

        /// <summary>
        /// <para>Sets <see cref="AplicationStatus.ignoreBlackPixels"/> config.</para>
        /// <para>Ignores black pixels in calculating average color. Good for images with black background.</para>
        /// </summary>
        private void ignoreBlackPixelsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int ignoreBlackPixels = ignoreBlackPixelsCheckBox.Checked ? 1 : 0;
            AplicationStatus.ignoreBlackPixels = ignoreBlackPixels;
            SetStatus($"ignoreBlackPixels = " + (AplicationStatus.ignoreBlackPixels == 1 ? "true" : "false"));
            Mozaicator.matches.Clear();
        }

        /// <summary>
        /// <para>Sets <see cref="AplicationStatus.beforeAfterComparation"/> config.</para>
        /// <para>Adds input image to output to see before/after comparation.</para>
        /// </summary>
        private void beforeAfterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AplicationStatus.beforeAfterComparation = beforeAfterCheckBox.Checked;
            SetStatus($"beforeAfterComparation = " + (AplicationStatus.beforeAfterComparation ? "true" : "false"));
        }

        /// <summary>
        /// <para>Sets <see cref="AplicationStatus.strategy"/> config.</para>
        /// <para>Strategy for calculating color distance to better approximate which tile to use.</para>
        /// </summary>
        private void colorDistanceDomain_SelectedItemChanged(object sender, EventArgs e)
        {
            switch (colorDistanceDomain.Items.ToArray()[colorDistanceDomain.SelectedIndex])
            {
                case "Square distance":
                    AplicationStatus.SetColorDistanceStrategy(new SquareDistanceStrategy());
                    SetStatus($"Chosen strategy is {AplicationStatus.strategy.ToString()}");
                    break;
                case "Bitwise distance":
                    AplicationStatus.SetColorDistanceStrategy(new BitwiseDistanceStrategy());
                    SetStatus($"Chosen strategy is {AplicationStatus.strategy.ToString()}");
                    break;
                case "CIE76 distance":
                    AplicationStatus.SetColorDistanceStrategy(new CIE76DistanceStrategy());
                    SetStatus($"Chosen strategy is {AplicationStatus.strategy.ToString()}");
                    break;
                case "CIE2000 distance":
                    AplicationStatus.SetColorDistanceStrategy(new CIE2000DistanceStrategy());
                    SetStatus($"Chosen strategy is {AplicationStatus.strategy.ToString()}");
                    break;
                default:
                    SetStatus("Strategy not found");
                    break;
            }
            Mozaicator.matches.Clear();
        }
        #endregion
    
    }
}
