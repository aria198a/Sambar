using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class APPLY_Y_SUB
    {

        /// <summary>
        /// 申請pk
        /// </summary>
        public string APM_ID { get; set; }

        /// <summary>
        /// 診斷，1:是;  2:否;  3:不適用
        /// </summary>
        public int DIAGNOSIS { get; set; }

        /// <summary>
        /// 預防與治療
        /// </summary>
        public int PREVENTION { get; set; }

        /// <summary>
        /// 病患照護
        /// </summary>
        public int CARE { get; set; }

        /// <summary>
        /// 病患安全
        /// </summary>
        public int SAFETY { get; set; }

        /// <summary>
        /// 醫療機構的組織與架構        /// 
        /// </summary>
        public int TRUCTURE { get; set; }

        /// <summary>
        /// 公共衛生政策
        /// </summary>
        public int POLICY { get; set; }

        /// <summary>
        /// 疾病研究
        /// </summary>
        public int RESEARCH { get; set; }
    }
}
