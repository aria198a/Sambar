using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class APPLY_X
    {
        public string APM_ID { get; set; }

        /// <summary>
        /// 現有診斷_資料紀錄, 1:是  2:否 3:不適用
        /// </summary>
        public int DT_RECORD { get; set; }

        /// <summary>
        /// 現有診斷_檢體
        /// </summary>
        public int DT_SPECIMEN { get; set; }

        /// <summary>
        /// 預防健檢_資料紀錄
        /// </summary>
        public int HE_RECORD { get; set; }

        /// <summary>
        /// 預防健檢_檢體
        /// </summary>
        public int HE_SPECIMEN { get; set; }

        /// <summary>
        /// 參與學術_資料紀錄   
        /// </summary>
        public int AR_RECORD { get; set; }

        /// <summary>
        /// 參與學術_檢體
        /// </summary>
        public int AR_SPECIMEN { get; set; }
    }
}
