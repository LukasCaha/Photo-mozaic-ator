using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Photo_mozaic_ator
{
    /// <summary>
    /// Interface for color distance strategy.
    /// </summary>
    interface IDistanceStrategy
    {
        /// <summary>
        /// Calculates certain distance between two different colors.
        /// <para>Order of colors doesn't matter.</para>
        /// </summary>
        /// <param name="a">One color</param>
        /// <param name="b">Other color</param>
        /// <returns>Distance between these colors in int.</returns>
        int Distance(Color a, Color b);
    }
}
