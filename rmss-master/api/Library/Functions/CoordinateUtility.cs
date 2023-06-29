using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Functions
{
    public class CoordinateUtility
    {
        private const double EARTH_RADIUS = 6378.137;

        private double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        /// <summary>
        /// from Google Map 腳本
        /// <para>出處：http://windperson.wordpress.com/2011/11/01/由兩點經緯度數值計算實際距離的方法/ </para>
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lng1"></param>
        /// <param name="lat2"></param>
        /// <param name="lng2"></param>
        /// <returns>回傳單位 公尺</returns>
        public double GetDistance_Google(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

    }
}
