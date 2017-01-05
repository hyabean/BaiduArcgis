using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;

namespace ArcgisBaiduMap.ArcgisExtend
{
    public class BaidumapWmtsLayer : TiledMapServiceLayer
    {
        private const double cornerCoordinate = 20037508.3427892;
        private string _baseURL = "Image"; //百度影像  

        public override void Initialize()
        {
            ESRI.ArcGIS.Client.Projection.WebMercator mercator = new ESRI.ArcGIS.Client.Projection.WebMercator();
            this.FullExtent = new ESRI.ArcGIS.Client.Geometry.Envelope(-20037508.3427892, -20037508.3427892, 20037508.3427892, 20037508.3427892)
            {
                SpatialReference = new SpatialReference(102100)
            };
            //this.FullExtent = new ESRI.ArcGIS.Client.Geometry.Envelope(9001735.65795624, 2919532.04645186, 19020489.8293508, 8346937.81802098)
            //{
            //    SpatialReference = new SpatialReference(102100)
            //};
            //图层的空间坐标系  
            this.SpatialReference = new SpatialReference(102100);
            // 建立切片信息，每个切片大小256*256px，共16级.  
            this.TileInfo = new TileInfo()
            {
                Height = 256,
                Width = 256,
                Origin = new MapPoint(-cornerCoordinate, cornerCoordinate) { SpatialReference = new ESRI.ArcGIS.Client.Geometry.SpatialReference(102100) },

                Lods = new Lod[32]
            };
            //为每级建立方案，每一级是前一级别的一半.  
            double resolution = cornerCoordinate * 2 / 256;
            for (int i = 0; i < TileInfo.Lods.Length; i++)
            {
                TileInfo.Lods[i] = new Lod() { Resolution = resolution };
                resolution /= 2;
            }
            // 调用初始化函数  
            base.Initialize();
        }
        string _mapType = "";
        public override string GetTileUrl(int level, int row, int col)
        {
            int zoom = level - 1;
            int offsetX = Convert.ToInt32(Math.Pow(2, zoom));
            int offsetY = offsetX - 1;
            int numX = col - offsetX;
            int numY = (-row) + offsetY;
            zoom = level + 1;
            int num = (col + row) % 8 + 1;
            String url = null;
            if (MapType == "Map")
            {
                url = "http://online" + num + ".map.bdimg.com/tile/?qt=tile&x=" + numX + "&y=" + numY + "&z=" + zoom + "&styles=pl";
            }
            else if (MapType == "Image")
            {
                url = " http://shangetu" + num + ".map.bdimg.com/it/u=x=" + numX + ";y=" + numY + ";z=" + zoom + ";v=009;type=sate&fm=46";
            }
            else if (MapType == "POI")
            {
                url = "http://online" + num + ".map.bdimg.com/tile/?qt=tile&x=" + numX + "&y=" + numY + "&z=" + zoom + "&styles=sl&v=020";
            }
            return string.Format(url);
        }
        public string MapType
        {
            get
            {
                return _mapType;
            }
            set
            {
                _mapType = value;
            }
        }
    }
}
