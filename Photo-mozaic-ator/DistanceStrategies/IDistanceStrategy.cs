using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Photo_mozaic_ator
{
    interface IDistanceStrategy
    {
        int Distance(Color a, Color b);
    }
}
