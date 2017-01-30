using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal1
{
    interface IColourPalette
    {
        string Name { get; }
        System.Drawing.Color ColourFromValue(int value);
    }
}
