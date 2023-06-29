using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Apply
{
    public class VmApplyOut
    {
        /// <summary>
        /// 申請pk
        /// </summary>
        public string apmId { get; set; }

        /// <summary>
        /// 計畫編號
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// 計畫名稱
        /// </summary>
        public string planName { get; set; }        

        /// <summary>
        /// 計畫類型1
        /// </summary>
        public string planType1 { get; set; }

        /// <summary>
        /// 計畫類型2
        /// </summary>
        public string planType2 { get; set; }

        /// <summary>
        /// 計畫類型3
        /// </summary>
        public string planType3 { get; set; }

        /// <summary>
        /// 匹配資料
        /// </summary>
        public int match { get; set; }

    }
}
