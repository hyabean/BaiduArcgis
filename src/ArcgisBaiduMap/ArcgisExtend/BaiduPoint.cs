namespace ArcgisBaiduMap.ArcgisExtend
{
    public class BaiduPoint
    {
        public BaiduPoint()
        {

        }

        public BaiduPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public double x { get; set; }
        public double y { get; set; }
    }
}