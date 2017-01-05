namespace ArcgisBaiduMap.ArcgisExtend
{
    public class LatLngPoint
    {
        public LatLngPoint(double x, double y)
        {
            LonX = x;
            LatY = y;
        }

        public LatLngPoint()
        {

        }

        public double LatY { get; internal set; }
        public double LonX { get; internal set; }
    }
}