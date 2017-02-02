using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal1
{
    class FractalRenderer
    {
        IFractal myFractal;
        IArea myDrawingArea;
        IColourPalette myColourPalette;


        public FractalRenderer(IFractal fractal, IArea drawingArea, IColourPalette colourPalette)
        {
            myFractal = fractal;
            myDrawingArea = drawingArea;
            myColourPalette = colourPalette;
        }

        internal Bitmap render(double imageWidth, double imageHeight)
        {
            System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap((int)Math.Floor(imageWidth), (int)Math.Floor(imageHeight));

            for (int x = 0; x < myBitmap.Width; x++)
            {
                for (int y = 0; y < myBitmap.Height; y++)
                {
                    System.Drawing.Color color = GetColourForCoordinate(myFractal, x, y);
                    myBitmap.SetPixel(x, y, color);
                }
            }
            return myBitmap;
        }

        public void Zoom (ICartesian imageCenterCoordinates, double zoomFactor)
        {
            ICartesian imageCenterProportion = myDrawingArea.ProportionFromPoint(imageCenterCoordinates);
            myFractal.Zoom(imageCenterProportion, new Cartesian(zoomFactor, zoomFactor));
        }

        private System.Drawing.Color GetColourForCoordinate(IFractal f, double x, double y)
        {
            ICartesian proportion = myDrawingArea.ProportionFromPoint(new Cartesian(x, y));

            int v = f.PointValue(proportion);

            return myColourPalette.ColourFromValue(v);
        }
    }
}
