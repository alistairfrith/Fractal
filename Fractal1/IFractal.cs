namespace Fractal1
{
    public interface IFractal
    {
        // Get a point value based on absolute coordinates
        int PointValue(double x, double y);

        // Get a point value based on the relative position within the current viewing area
        int PointValue(ICartesian proportion);

        void Zoom(ICartesian newCenterByProportion, ICartesian zoomFactor);

        IArea RenderArea
        {
            get;
        }

        int MaxIterations
        {
            get;
            set;
        }

        void Reset();

        // Returns a 2-d array of the specified dimensions containing point values within
        // the current viewing area
        int[][] ArrayValues(int width, int height, IArea drawingArea);
    }
}