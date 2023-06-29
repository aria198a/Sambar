using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Apply
{
    public class VmApplyMatchIn : BaseModel
    {
     
        public string apmId { get; set; }

        /// <summary>
        /// 關鍵字
        /// </summary>
        public string keyWord { get; set; }

        public string source { get; set; }

        public string type { get; set; }

        /// <summary>
        /// 建檔日期起
        /// </summary>
        public DateTime? sourceDataSt { get; set; }        

        /// <summary>
        /// 建檔日期迄
        /// </summary>
        public DateTime? sourceDataEnd { get; set; }


    }
}
