using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Photo_mozaic_ator
{
    class Mozaicator
    {
        public static string FindClosestColorAndReturnImageName(ref Bitmap targetImage, int offsetX, int offsetY)
        {
            int red = 0, green = 0, blue = 0;
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    Color c = targetImage.GetPixel(offsetX + x, offsetY + y);
                    red += c.R;
                    green += c.G;
                    blue += c.B;
                }
            }

            //normalizing
            red /= 16 * 16;
            green /= 16 * 16;
            blue /= 16 * 16;

            //snapping to 8*8*8 color pallete (512 colors)
            const int factor = 4;
            red /= factor;
            red *= factor;
            green /= factor;
            green *= factor;
            blue /= factor;
            blue *= factor;

            Color representingColor = Color.FromArgb(255, red, green, blue);

            //save as #RRGGBB.bmp
            string name = "";
            name += (red + 256).ToString("X").Substring(1);
            name += (green + 256).ToString("X").Substring(1);
            name += (blue + 256).ToString("X").Substring(1);

            string filename = "#" + name + ".bmp";
            string temp = filename;

            //if file exists return
            if (File.Exists(@"faces by color down 4\" + filename))
            {
                return filename;
            }
            else
            {
                //look in matched colors
                foreach (Match match in matches)
                {
                    if (match.nonExistingColor == filename)
                    {
                        return match.alternative;
                    }
                }

                DirectoryInfo dir = new DirectoryInfo(@"faces by color down 4");  //dir with colors

                //find minimum
                int minimum = int.MaxValue;

                //foreach file find distance
                //find minimum
                foreach (var file in dir.GetFiles("*.bmp"))
                {
                    //convert filename to color
                    Color fileColor = ColorTranslator.FromHtml(file.Name.Substring(0, 7));
                    int dist = Distance(fileColor, representingColor);
                    if (dist < minimum)
                    {
                        minimum = dist;
                        filename = file.Name;
                    }
                }

                //save to matches
                matches.Add(new Match(temp, filename));

                return filename;
            }
        }

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


        public static int Distance2(Color a, Color b)
        {
            int binaryA = 0;
            binaryA += a.R * 256;
            binaryA = binaryA << 16;
            binaryA += a.G * 256;
            binaryA = binaryA << 8;
            binaryA += a.B * 256;

            int binaryB = 0;
            binaryB += b.R * 256;
            binaryB = binaryB << 16;
            binaryB += b.G * 256;
            binaryB = binaryB << 8;
            binaryB += b.B * 256;

            int compare = binaryA ^ binaryB;
            int diff = 0;
            for (int i = 0; i < 24; i++)
            {
                if (compare % 2 == 1)
                {
                    diff++;
                }
                compare = compare >> 1;
            }
            return diff;
        }

        public static int Distance(Color current, Color match)
        {
            int redDifference;
            int greenDifference;
            int blueDifference;

            redDifference = current.R - match.R;
            greenDifference = current.G - match.G;
            blueDifference = current.B - match.B;

            return redDifference * redDifference + greenDifference * greenDifference + blueDifference * blueDifference;
        }

        public static Image ResizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.DrawImage(img, 0, 0, width, height);
            }

            return (Image)b;
        }

        public static void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }
    }
}
