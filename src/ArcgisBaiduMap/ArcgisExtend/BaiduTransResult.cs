using System.Collections.Generic;

namespace ArcgisBaiduMap.ArcgisExtend
{
    public class BaiduTransResult
    {
        public BaiduTransResult()
        {
            status = 1;
            result = new List<BaiduPoint>();
        }
        public int status { get; set; }

        public List<BaiduPoint> result;
    }
}