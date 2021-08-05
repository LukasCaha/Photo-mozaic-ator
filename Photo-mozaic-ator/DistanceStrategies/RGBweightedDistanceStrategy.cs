using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Photo_mozaic_ator.DistanceStrategies
{
    /// <summary>
    /// Takes classic square metric but uses weighting to better fit human perception of color + lightness
    /// </summary>
    class RGBweightedDistanceStrategy : IDistanceStrategy
    {
        public int Distance(Color a, Color b)
        {
            int avgR = (a.R + b.R) / 2;
            int redDifference = a.R - b.R;
            int greenDifference = a.G - b.G;
            int blueDifference = a.B - b.B;
            int Rsq = redDifference * redDifference;
            int Gsq = greenDifference * greenDifference;
            int Bsq = blueDifference * blueDifference;

            //depending on average red value use different formula
            if (avgR < 128)
            {
                return 2 * Rsq + 4 * Gsq + 3 * Bsq;
            }
            else
            {
                return 3 * Rsq + 4 * Gsq + 2 * Bsq;
            }
        }

        public override string ToString()
        {
            return "RGB weighted distance strategy";
        }
    }
}



