using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal1
{
    public class Area : IArea , INotifyPropertyChanged
    {
        Cartesian TopLeft;
        Cartesian BottomRight;

        public double Top
        {
            get
            {
                return TopLeft.Y;
            }

            set
            {
                TopLeft.Y = value;
                this.NotifyPropertyChanged("Top");
            }
        }

        public double Bottom
        {
            get
            {
                return BottomRight.Y;
            }

            set
            {
                BottomRight.Y = value;
                this.NotifyPropertyChanged("Bottom");

            }
        }

        public double Left
        {
            get
            {
                return TopLeft.X;
            }

            set
            {
                TopLeft.X = value;
                this.NotifyPropertyChanged("Left");

            }
        }

        public double Right
        {
            get
            {
                return BottomRight.X;
            }

            set
            {
                BottomRight.X = value;
                this.NotifyPropertyChanged("Right");

            }
        }

        public double Height
        {
            get
            {
                return Top - Bottom;
            }
        }

        public double Width
        {
            get
            {
                return Right - Left;
            }
        }

        public Area()
        {
            TopLeft = new Cartesian(0, 0);
            BottomRight = new Cartesian(0, 0);
        }

        public Area(ICartesian topLeft, ICartesian bottomRight)
        {
            TopLeft = new Cartesian(topLeft.X, topLeft.Y);
            BottomRight = new Cartesian(bottomRight.X, bottomRight.Y);
        }

        public void Transform (ICartesian newCenter, ICartesian zoom)
        {
            // first translate
            double oldCenterX = Left + (Width / 2);
            double xBy = newCenter.X - oldCenterX;
            Left += xBy;
            Right += xBy;

            double oldCenterY = Bottom + (Height / 2);
            double yBy = newCenter.Y - oldCenterY;
            Top += yBy;
            Bottom += yBy;

            // then scale
            xBy = Width * (1 - (1 / zoom.X));
            Left += xBy/2;
            Right -= xBy/2;

            yBy = Height * (1 -(1 / zoom.Y));
            Top -= yBy/2;
            Bottom += yBy/2;
        }

        public ICartesian ProportionFromPoint(ICartesian point)
        {
            if (Width==0 || Height==0)
            {
                throw new Exception("Cannot calculate on 0-sized area");
            }

            return new Cartesian((point.X- Left)/Width,
                                 (point.Y-Bottom)/Height);
        }

        public ICartesian PointFromProportion(ICartesian proportion)
        {
            if (Width == 0 || Height == 0)
            {
                throw new Exception("Cannot calculate on 0-sized area");
            }

            return new Cartesian(Left + (proportion.X * Width),
                                 Bottom + (proportion.Y * Height));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
