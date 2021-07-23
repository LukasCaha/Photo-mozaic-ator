using Photo_mozaic_ator.DistanceStrategies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Photo_mozaic_ator
{
    static class AplicationStatus
    {
        public static Random randomGenerator = new Random();

        public static string workingDirectory { get; set; }

        public static string inputFile { get; set; }
        public static string outputFile;
        public static string newTilesetDir;
        public static string existingTilesetDir;
        public static Bitmap outputImage { get; set; }
        /// <summary>
        /// Scales input image so result has more tiles.
        /// </summary>
        public static double imageScale { get; set; }
        /// <summary>
        /// Setting to ignore black pixels. Suiting for tilesets with tiles with black boackground/alpha.
        /// </summary>
        public static int ignoreBlackPixels { get; set; }
        /// <summary>
        /// Adds input image to output to see difference of before/after.
        /// </summary>
        public static bool beforeAfterComparation { get; set; }
        /// <summary>
        /// Tile size in pixels. Always square.
        /// </summary>
        public static int tileSize { get; set; }
        /// <summary>
        /// <para>Snaps colors to smaller color pallete to limit number of possible colors/existing tiles.</para>
        /// </summary>
        public static double snappingFactor { get; set; }
        /// <summary>
        /// Color distance Strategy. Determines which method of calculating distance use.
        /// </summary>
        public static IDistanceStrategy strategy { get; set; }

        #region Methods
        static AplicationStatus()
        {
            //default values
            workingDirectory = @"./";
            inputFile = null;
            outputFile = null;
            newTilesetDir = null;
            newTilesetDir = null;
            tileSize = 16;
            snappingFactor = 1;
            imageScale = 1;
            ignoreBlackPixels = 1;
            beforeAfterComparation = false;
            strategy = new SquareDistanceStrategy();
        }

        /// <summary>
        /// If output file is not defined, create random name to always save the output image.
        /// </summary>
        /// <returns></returns>
        public static string GetOutputFile()
        {
            if (outputFile == null)
            {
                return workingDirectory + randomGenerator.Next(0, (int)Math.Pow(2,20)).ToString("X")+".png";
            }
            return outputFile;
        }

        /// <summary>
        /// Setter for color distance strategy.
        /// </summary>
        /// <param name="_strategy"></param>
        public static void SetColorDistanceStrategy(IDistanceStrategy _strategy)
        {
            strategy = _strategy;
        }
        #endregion
    }
}
