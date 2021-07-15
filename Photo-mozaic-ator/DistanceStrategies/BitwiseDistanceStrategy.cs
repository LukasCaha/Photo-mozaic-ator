using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Photo_mozaic_ator.DistanceStrategies
{
    class BitwiseDistanceStrategy : IDistanceStrategy
    {
        public override int Distance(Color a, Color b)
        {
            int binaryA = 0;
            binaryA += a.R * 256;
            binaryA = binaryA << 8;
            binaryA += a.G * 256;
            binaryA = binaryA << 8;
            binaryA += a.B * 256;

            int binaryB = 0;
            binaryB += b.R * 256;
            binaryB = binaryB << 8;
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

        public override string ToString()
        {
            return "Bitwise Distance Strategy";
        }
    }
}
