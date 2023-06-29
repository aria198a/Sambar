using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class ANS_MAIN
    {
        /// <summary>
        /// 資料提供者開放設定PK
        /// </summary>
        public string AM_ID { get; set; }
        /// <summary>
        /// 編號
        /// </summary>
        public int AM_NO { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CREATED_DATETIME { get; set; }
        //更新時間
        public DateTime UPDATE_DATETIME { get; set; }
        //更新人員id
        public string UPDATE_USERID { get; set; }
        //是否啟用
        public bool IS_ENABLED { get; set; }
    }
}
