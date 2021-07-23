using Photo_mozaic_ator.DistanceStrategies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Photo_mozaic_ator
{
    /// <summary>
    /// Class responsible for mozaic creation.
    /// </summary>
    class Mozaicator
    {
        /// <summary>
        /// Takes section of original image. Determines what is average color. Finds tile-file with closest color according to color distance strategy.
        /// </summary>
        /// <param name="targetImage">Image from which take the section to represent.</param>
        /// <param name="offsetX">X coordinate in source image</param>
        /// <param name="offsetY">Y coordinate in source image</param>
        /// <param name="strategy">Color distance strategy</param>
        /// <returns>Returns tile file path.</returns>
        public static string FindClosestColorAndReturnImageName(ref Bitmap targetImage, int offsetX, int offsetY, IDistanceStrategy strategy)
        {
            //sum colors
            int red = 0, green = 0, blue = 0;
            for (int x = 0; x < AplicationStatus.tileSize; x++)
            {
                for (int y = 0; y < AplicationStatus.tileSize; y++)
                {
                    Color c = targetImage.GetPixel(offsetX + x, offsetY + y);
                    red += c.R;
                    green += c.G;
                    blue += c.B;
                }
            }

            //normalizing
            int tileSizeSquared = (int)Math.Pow(AplicationStatus.tileSize, 2);
            red /= tileSizeSquared;
            green /= tileSizeSquared;
            blue /= tileSizeSquared;

            //snapping to smaller color pallete
            red = (int)(red / AplicationStatus.snappingFactor);
            red = (int)(red * AplicationStatus.snappingFactor);
            green = (int)(green / AplicationStatus.snappingFactor);
            green = (int)(green * AplicationStatus.snappingFactor);
            blue = (int)(blue / AplicationStatus.snappingFactor);
            blue = (int)(blue * AplicationStatus.snappingFactor);

            Color representingColor = Color.FromArgb(255, red, green, blue);

            //save as #RRGGBB.bmp
            string name = "";
            name += (red + 256).ToString("X").Substring(1);
            name += (green + 256).ToString("X").Substring(1);
            name += (blue + 256).ToString("X").Substring(1);

            string tileFilename = "#" + name + ".bmp";
            string temp = tileFilename;

            //if file exists return
            if (File.Exists(AplicationStatus.existingTilesetDir + tileFilename))
            {
                return tileFilename;
            }
            else
            {
                //look in matched colors (cache)
                foreach (Match match in matches)
                {
                    if (match.nonExistingColor == tileFilename)
                    {
                        return match.alternative;
                    }
                }

                DirectoryInfo dir;
                try
                {
                    dir = new DirectoryInfo(AplicationStatus.existingTilesetDir);  //dir with colors
                }
                catch (System.ArgumentNullException)
                {
                    throw new Exception("Tileset directory not specified");
                }

                //minimal distance of other color
                int minimum = int.MaxValue;

                //approximating what color to use from existing colors in tiles directory
                foreach (var file in dir.GetFiles("*.bmp"))
                {
                    //convert filename to color
                    Color fileColor = ColorTranslator.FromHtml(file.Name.Substring(0, 7));
                    int dist = AplicationStatus.strategy.Distance(fileColor, representingColor);
                    if (dist < minimum)
                    {
                        minimum = dist;
                        tileFilename = file.Name;
                    }
                }

                //save to matches (cache)
                matches.Add(new Match(temp, tileFilename));

                return tileFilename;
            }
        }

        /// <summary>
        /// Cache structure fornon existing colors. Saves disk acesses.
        /// </summary>
        public struct Match
        {
            public string nonExistingColor;
            public string alternative;

            public Match(string nonExistingColor, string alternative)
            {
                this.nonExistingColor = nonExistingColor;
                this.alternative = alternative;
            }
        }

        public static List<Match> matches = new List<Match>();

        /// <summary>
        /// Resize image to new size.
        /// </summary>
        /// <param name="img">Image to resize</param>
        /// <param name="width">New width</param>
        /// <param name="height">New height</param>
        /// <returns>Resized <see cref="Image"/></returns>
        public static Image ResizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.DrawImage(img, 0, 0, width, height);
            }

            return (Image)b;
        }

        /// <summary>
        /// Copies part of one bitmap to part of another bitmap-
        /// </summary>
        /// <param name="srcBitmap">Copy from this bitmap</param>
        /// <param name="srcRegion">Copy this region</param>
        /// <param name="destBitmap">Paste to this bitmap</param>
        /// <param name="destRegion">Copy to this region</param>
        public static void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (Graphics g = Graphics.FromImage(destBitmap))
            {
                g.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }
    }
}
