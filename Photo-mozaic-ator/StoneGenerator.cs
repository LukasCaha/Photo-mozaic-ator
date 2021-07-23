using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Photo_mozaic_ator
{
    /// <summary>
    /// Class responsible for tile set creation.
    /// </summary>
    class StoneGenerator
    {
        /// <summary>
        /// Calculates average color of tile and save it to file with #RRGGBB.bmp file name.
        /// </summary>
        /// <param name="imageNum">Sequential number of source image</param>
        public static void GenerateOneTile(int imageNum)
        {
            //load image
            Image face = Image.FromFile(AplicationStatus.workingDirectory + @"tileset_source/image (" + imageNum + ").png");

            //scale down to 16x16 px
            Bitmap smallFace = (Bitmap)Mozaicator.ResizeImage(face, AplicationStatus.tileSize, AplicationStatus.tileSize);

            //determine single color representing face
            int red = 0, green = 0, blue = 0;
            //transparent images tiles fix
            int blackPixels = 0;
            //fix end
            for (int x = 0; x < AplicationStatus.tileSize; x++)
            {
                for (int y = 0; y < AplicationStatus.tileSize; y++)
                {
                    Color c = smallFace.GetPixel(x, y);
                    if (c.R == 0 && c.G == 0 && c.B == 0) blackPixels++;
                    red += c.R;
                    green += c.G;
                    blue += c.B;
                }
            }
            //normalizing
            int tileSizeSquared = (int)Math.Pow(AplicationStatus.tileSize, 2);
            red /= Math.Max(1, tileSizeSquared - (blackPixels * AplicationStatus.ignoreBlackPixels));
            green /= Math.Max(1, tileSizeSquared - (blackPixels * AplicationStatus.ignoreBlackPixels));
            blue /= Math.Max(1, tileSizeSquared - (blackPixels * AplicationStatus.ignoreBlackPixels));

            //snapping to smaller color pallete to eliminate lot output colors
            red = (int)(red / AplicationStatus.snappingFactor);
            red = (int)(red * AplicationStatus.snappingFactor);
            green = (int)(green / AplicationStatus.snappingFactor);
            green = (int)(green * AplicationStatus.snappingFactor);
            blue = (int)(blue / AplicationStatus.snappingFactor);
            blue = (int)(blue * AplicationStatus.snappingFactor);

            //save as #RRGGBB.bmp
            string name = "";
            name += (red + 256).ToString("X").Substring(1);
            name += (green + 256).ToString("X").Substring(1);
            name += (blue + 256).ToString("X").Substring(1);
            var i2 = new Bitmap(smallFace);
            string savePath = $"{AplicationStatus.newTilesetDir}/#{name}.bmp";
            i2.Save(savePath, ImageFormat.Bmp);
        }
    }
}
