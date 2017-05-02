using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal1
{
    class FractalMadelbrot : IFractal, INotifyPropertyChanged
    {
        IArea InitialArea = new Area(new Cartesian(-2.5,1), new Cartesian(1,-1));

        IArea mRenderArea;


        public IArea RenderArea
        {
            get
            {
                return mRenderArea;
            }
        }

        int mMaxIterations = 255;
        public int MaxIterations
        {
            get
            {
                return mMaxIterations;
            }
            set
            {
                mMaxIterations = value;
                this.NotifyPropertyChanged("MaxIterations");
            }
        }

        public FractalMadelbrot(double viewWidth, double viewHeight)
        {
            mRenderArea = new Area(new Cartesian(InitialArea.Left, InitialArea.Top),
                                  new Cartesian(InitialArea.Right, InitialArea.Bottom));
        }

        public void Zoom (ICartesian newCenterByProportion, ICartesian zoomFactor)
        {
            ICartesian newCenter = mRenderArea.PointFromProportion(newCenterByProportion);
            mRenderArea.Transform(newCenter, zoomFactor);
        }

        public int PointValue(ICartesian proportion)
        {
            ICartesian coordinates = mRenderArea.PointFromProportion(proportion);

            return PointValue(coordinates.X, coordinates.Y);
        }

        public int PointValue(double x, double y)
        {
            int iteration = 0;
            double x1 = x;
            double y1 = y;

            while (x1 * x1 + y1 * y1 < 4 && iteration++ < MaxIterations)
            {
                double xtemp = x1 * x1 - y1 * y1 + x;
                y1 = 2 * x1 * y1 + y;
                x1 = xtemp;
            }

            return iteration;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void Reset()
        {
            mRenderArea = InitialArea;
        }

        public int[][] ArrayValues(int width, int height, IArea drawingArea)
        {
            int[][] result = new int[width][];

            for (int colNum=0; colNum< width; colNum++)
            {
                result[colNum] = ColumnValues(colNum, height, drawingArea);
            }

            return result;
        }

        private int[] ColumnValues(int colNum, int height, IArea drawingArea)
        {
            int[] column = new int[height];

            for (int rowNum = 0; rowNum<height; rowNum++)
            {

                ICartesian coordinate = drawingArea.ProportionFromPoint(new Cartesian(colNum, rowNum));

                column[rowNum] = PointValue(coordinate);
            }

            return column;
        }
    }
}
