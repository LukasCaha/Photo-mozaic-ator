using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Photo_mozaic_ator.DistanceStrategies
{
    class SquareDistanceStrategy: IDistanceStrategy
    {
        public override int Distance(Color a, Color b)
        {
            int redDifference = a.R - b.R;
            int greenDifference = a.G - b.G;
            int blueDifference = a.B - b.B;

            return redDifference * redDifference + greenDifference * greenDifference + blueDifference * blueDifference;
        }

        public override string ToString()
        {
            return "Square Distance Strategy";
        }
    }
}
