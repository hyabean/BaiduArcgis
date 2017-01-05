using System;

namespace ArcgisBaiduMap.ArcgisExtend
{
    /// <summary>
    /// 火星坐标系 (GCJ-02)与百度坐标系 (BD-09) 转换帮助类
    /// </summary>
    public class BDGCJLatLonHelper
    {
        /*
         *参考：
         *BD09坐标系：即百度坐标系，GCJ02坐标系经加密后的坐标系。
         */
        #region 常量
        const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        #endregion 
        #region 将GCJ-02坐标转换成BD-09坐标
        /// <summary>
        /// 将GCJ-02坐标转换成BD-09坐标
        /// </summary>
        /// <param name="gcjPoint">GCJ-02坐标</param>
        /// <returns>BD-09坐标</returns>
        public static LatLngPoint GCJ02ToBD09(LatLngPoint gcjPoint)
        {
            LatLngPoint _bdPoint = new LatLngPoint();
            double _x = gcjPoint.LonX, y = gcjPoint.LatY;
            double _z = Math.Sqrt(_x * _x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double _theta = Math.Atan2(y, _x) + 0.000003 * Math.Cos(_x * x_pi);
            _bdPoint.LonX = _z * Math.Cos(_theta) + 0.0065;
            _bdPoint.LatY = _z * Math.Cos(_theta) + 0.006;
            return _bdPoint;
        }

        #endregion 
        #region 将BD-09坐标转换成GCJ-02坐标
        /// <summary>
        /// 将BD-09坐标转换成GCJ-02坐标
        /// </summary>
        /// <param name="bdPoint">BD-09坐标</param>
        /// <returns>GCJ-02坐标</returns>
        public static LatLngPoint BD09ToGCJ02(LatLngPoint bdPoint)
        {
            LatLngPoint _gcjPoint = new LatLngPoint();
            double _x = bdPoint.LonX - 0.0065, _y = bdPoint.LatY - 0.006;
            double _z = Math.Sqrt(_x * _x + _y * _y) - 0.00002 * Math.Sin(_y * x_pi);
            double _theta = Math.Atan2(_y, _x) - 0.000003 * Math.Cos(_x * x_pi);
            _gcjPoint.LonX = _z * Math.Cos(_theta);
            _gcjPoint.LatY = _z * Math.Sin(_theta);
            return _gcjPoint;
        }
        #endregion 
    }
}