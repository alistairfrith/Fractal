using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal1
{
    class ColourPaletteHue : IColourPalette, INotifyPropertyChanged
    {
        public string Name
        {
            get
            {
                return "Hue";
            }
        }

        public Color ColourFromValue(int value)
        {
            return new ColorDemo.HSLColor((double)(value % 256), 200.0, 100.0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
