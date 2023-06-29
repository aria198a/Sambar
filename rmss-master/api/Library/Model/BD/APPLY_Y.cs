using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class APPLY_Y
    {
        /// <summary>
        /// 申請pk
        /// </summary>
        public string APM_ID { get; set; }

        /// <summary>
        /// 是否為本國公部門補助,1:是; 2:否; 3:不適用
        /// </summary>
        public int PUBLICINFO { get; set; }

        /// <summary>
        /// 是否為非受本國公部門補助
        /// </summary>
        public int NPUBLICINFO { get; set; }

        /// <summary>
        /// 是否為有商業目的
        /// </summary>
        public int BUSINESS { get; set; }

        /// <summary>
        /// 是否為僅用於學術目的
        /// </summary>
        public int EDU { get; set; }

        /// <summary>
        /// 是否涉及跨境之合作計畫
        /// </summary>
        public int FOREIGNINFO { get; set; }

        /// <summary>
        /// 是否為未涉及跨境之合作計畫
        /// </summary>
        public int NFOREIGNINFO { get; set; }
    }
}
