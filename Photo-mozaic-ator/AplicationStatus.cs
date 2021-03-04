using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Photo_mozaic_ator
{
    static class AplicationStatus
    {
        public static string inputFile { get; set; }
        public static string outputFile { get; set; }
        public static Bitmap outputImage { get; set; }
        static AplicationStatus()
        {
            inputFile = null;
            outputFile = null;
        }
    }
}
