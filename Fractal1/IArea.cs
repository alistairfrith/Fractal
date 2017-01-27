namespace Fractal1
{
    public interface IArea
    {
        double Bottom { get; set; }
        double Left { get; set; }
        double Right { get; set; }
        double Top { get; set; }

        double Height { get; }
        double Width { get; }

        void Transform(ICartesian newCenter, ICartesian zoom);

        ICartesian ProportionFromPoint(ICartesian point);
        ICartesian PointFromProportion(ICartesian proportion);
    }
}