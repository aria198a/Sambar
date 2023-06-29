using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class APPLY_MAIN
    {
        /// <summary>
        /// 資料來源pk
        /// </summary>
        public string APM_ID { get; set; }

        /// <summary>
        /// 申請編號
        /// </summary>
        public int APM_NO { get; set; }

        /// <summary>
        /// 計畫名稱
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 計畫英文名稱
        /// </summary>
        public string ENGLISHNAME { get; set; }

        /// <summary>
        /// 研究機構/執行單位
        /// </summary>
        public string RESEARCHINSTITUTE { get; set; }

        /// <summary>
        /// 委託單位/藥廠
        /// </summary>
        public string REQUESTER { get; set; }

        /// <summary>
        /// 經費來源
        /// </summary>
        public string FUNDING { get; set; }

        /// <summary>
        /// 主要主持人_姓名
        /// </summary>
        public string HOSTNAME { get; set; }

        /// <summary>
        /// 主要主持人_職稱
        /// </summary>
        public string HOSTJOBTITLE { get; set; }

        /// <summary>
        /// 協同主持人_姓名
        /// </summary>
        public string VICEHOSTNAME { get; set; }

        /// <summary>
        /// 協同主持人_職稱
        /// </summary>
        public string VICEHOSTJOBTITLE { get; set; }

        /// <summary>
        /// 聯絡人
        /// </summary>
        public string CONTACTPERSON { get; set; }

        /// <summary>
        /// 上班時間聯絡電話
        /// </summary>
        public string CONTACTNUMBER { get; set; }

        /// <summary>
        /// 研究目的
        /// </summary>
        public string PURPOSE { get; set; }

        /// <summary>
        /// 試驗進行地點 1:境內  2:境外
        /// </summary>
        public int TPLACE { get; set; }

        /// <summary>
        /// 資料申請者
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime CREATED_DATETIME { get; set; }


        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UPDATE_DATETIME { get; set; }

        /// <summary>
        /// 更新人員
        /// </summary>
        public string UPDATE_USERID { get; set; }


        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool IS_ENABLED { get; set; }
    }
}
