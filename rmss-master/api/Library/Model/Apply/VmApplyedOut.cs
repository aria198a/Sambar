using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Apply
{
    public class VmApplyedOut
    {      
        /// <summary>
        /// 申請ID
        /// </summary>
        public string apmId { get; set; }

        /// <summary>
        /// 計畫編號
        /// </summary>
        public string apmNo { get; set; }

        public string acId { get; set; }

        /// <summary>
        /// 來源ID
        /// </summary>
        public string amId { get; set; }

        /// <summary>
        /// 計畫名稱
        /// </summary>
        public string planName { get; set; }

        /// <summary>
        /// 計畫類型
        /// </summary>
        public string planType { get; set; }

        /// <summary>
        /// 申請資料來源
        /// </summary>
        public string applySource { get; set; }

        /// <summary>
        /// 申請資料類型
        /// </summary>
        public string applyType { get; set; }

        /// <summary>
        /// 申請日期
        /// </summary>
        public DateTime applyDate { get; set; }

        /// <summary>
        /// 偏好設定
        /// </summary>
        public string preference { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int state { get; set; }

       

    }
}
