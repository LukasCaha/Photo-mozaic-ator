using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Photo_mozaic_ator
{
    class StoneGenerator
    {
        public void Generate()
        {
            //for each of 10000 images
            for (int imageNum = 0; imageNum < 10000; imageNum++)
            {
                if (imageNum % 1000 == 0)
                {
                    Console.WriteLine(imageNum);
                }
                //load image
                Image face = Image.FromFile("10000 faces\\image" + imageNum + ".png");

                //scale down to 16x16 px
                Bitmap smallFace = (Bitmap)Mozaicator.ResizeImage(face, 16, 16);

                //determine single color representing face
                int red = 0, green = 0, blue = 0;
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        Color c = smallFace.GetPixel(x, y);
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
                smallFace.Save("faces by color down 4\\#" + name + ".bmp");
            }
        }
    }
}
