using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class RCODate
    {
        /// <summary>
        ///  民國年轉換
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public string ToFullTaiwanDate(DateTime? datetime)
        {
            if (datetime == null)
            {
                return "";
            }
            else
            {
                DateTime datetime2 = (DateTime)datetime;

                TaiwanCalendar taiwanCalendar = new TaiwanCalendar();

                return string.Format("民國 {0} 年 {1} 月 {2} 日",
                    taiwanCalendar.GetYear(datetime2),
                    datetime2.Month,
                    datetime2.Day);
            }


        }
    }
}
