using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.OUTPUT
{
    public class ANS_CONTENT_OTHER_OUT
    {
        public string name { get; set; }
        public List<content> content_list { get; set; }
    }

    public class content
    {
        public string dcId { get; set; }
        public string Name { get; set; }
        public int ANS { get; set; }
        public string ANS_SUB { get; set; }
        public List<sub> ANS_SUB_OPTIONS { get; set; }
    }

    public class sub
    {
        public bool ANS { get; set; }
        public string DS_ID { get; set; }
        public string CNAME { get; set; }
        
    }

    public class ANS_CONTENT_OTHER_SUB_join
    {
        /// <summary>
        /// 資料提供者開放設定 單欄編號 子集合
        /// </summary>
        public string AOS_ID { get; set; }

        /// <summary>
        /// 資料提供者開放設定 單欄編號
        /// </summary>
        public string AOS_AO_ID { get; set; }

        /// <summary>
        /// 填寫內容ID
        /// </summary>
        public string AOS_DCID { get; set; }

        /// <summary>
        /// 填寫答案
        /// </summary>
        public int AOS_ANS { get; set; }

        /// <summary>
        /// 主類別名稱
        /// </summary>
        public string DC_MAIN { get; set; }
    }
}
