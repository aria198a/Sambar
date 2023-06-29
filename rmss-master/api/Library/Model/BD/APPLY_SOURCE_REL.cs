using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class APPLY_SOURCE_REL
    {
        /// <summary>
        /// 申請ID
        /// </summary>
        public string APM_ID { get; set; }

        /// <summary>
        /// 資料來源ID
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 狀態：1:申請中;  2:已通過;  3: 已拒絕
        /// </summary>
        public int STATUS { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CREATED_DATETIME { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UPDATE_DATETIME { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UPDATE_USERID { get; set; }

        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool IS_ENABLED { get; set; }
        
        /// <summary>
        /// 申請審核是否閱讀
        /// </summary>
        public bool AC_IS_READED { get; set; }
        
        /// <summary>
        /// 申請回覆是否閱讀
        /// </summary>
        public bool APM_IS_READED { get; set; }
        
        
    }
}
