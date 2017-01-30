using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal1
{
    class ColourPaletteGreyScale : IColourPalette, INotifyPropertyChanged
    {
        public string Name
        {
            get
            {
                return "Grey Scale";
            }
        }

        public Color ColourFromValue(int value)
        {
            value %= 256;
            return System.Drawing.Color.FromArgb(value, value, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
