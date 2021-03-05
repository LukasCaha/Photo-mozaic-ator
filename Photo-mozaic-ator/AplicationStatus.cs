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
        public static int imageScale { get; set; }

        //tile generation
        public static int tileSize { get; set; }
        public static int normalizingFactor { get; set; }

        static AplicationStatus()
        {
            workingDirectory = @"./";
            inputFile = null;
            outputFile = null;
            newTilesetDir = null;
            newTilesetDir = null;
            tileSize = 96;
            normalizingFactor = 1;
            imageScale = 8;
        }

        public static string GetOutputFile()
        {
            if (outputFile == null)
            {
                return workingDirectory + randomGenerator.Next(0, (int)Math.Pow(2,20)).ToString("X")+".png";
            }
            return outputFile;
        }

        public static string GetExistingTilesetDir()
        {
            if (existingTilesetDir == null)
            {
                return workingDirectory + @"faces by color down 4";
            }
            return existingTilesetDir;
        }
    }
}
