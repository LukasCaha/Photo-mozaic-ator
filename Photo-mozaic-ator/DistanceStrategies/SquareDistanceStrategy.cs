using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Photo_mozaic_ator.DistanceStrategies
{
    class SquareDistanceStrategy: IDistanceStrategy
    {
        public int Distance(Color a, Color b)
        {
            int redDifference;
            int greenDifference;
            int blueDifference;

            redDifference = a.R - b.R;
            greenDifference = a.G - b.G;
            blueDifference = a.B - b.B;

            return redDifference * redDifference + greenDifference * greenDifference + blueDifference * blueDifference;
        }
    }
}
