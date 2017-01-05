using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;

namespace ArcgisBaiduMap.ArcgisExtend
{
    public class BaidumapWmsLayer : DynamicMapServiceLayer
    {
        // 百度地图静态图API介绍 ： http://lbsyun.baidu.com/index.php?title=static
        private string urlFormat = "http://api.map.baidu.com/staticimage/v2?ak=OTvrPNbb76dYiSmmPVqRwyuLzoHUHxpP&center={0},{1}&width={2}&height={3}&zoom={4}";

        public override void GetUrl(ImageParameters properties, OnUrlComplete onComplete)
        {
            Envelope extent = properties.Extent;
            int width = properties.Width;
            int height = properties.Height;
            int num = (extent.SpatialReference != null) ? extent.SpatialReference.WKID : 0;

            width = Math.Min(width, 1024);
            height = Math.Min(height, 1024);

            // zoom 范围3-19
            double offset = Math.Max(Math.Abs(extent.XMax - extent.XMin), Math.Abs(extent.YMax - extent.YMin));

            double zoom = Convert.ToInt32(Math.Log(offset / 2880, 0.5));

            if (zoom < 3)
            {
                zoom = 3;
            }
            else if (zoom > 19)
            {
                zoom = 19;
            }

            onComplete(string.Format(urlFormat, extent.GetCenter().X, extent.GetCenter().Y, width, height, zoom), new DynamicLayer.ImageResult(new Envelope
            {
                XMin = extent.XMin,
                YMin = extent.YMin,
                XMax = extent.XMax,
                YMax = extent.YMax
            }));
        }
    }
}
