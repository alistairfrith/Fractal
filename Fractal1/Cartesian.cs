using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal1
{
    public class Cartesian : ICartesian
    {
        double XValue;
        double YValue;

        public double X
        {
            get
            {
                return XValue;
            }

            set
            {
                XValue = value;
            }
        }

        public double Y
        {
            get
            {
                return YValue;
            }

            set
            {
                YValue = value;
            }
        }

        public Cartesian(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
