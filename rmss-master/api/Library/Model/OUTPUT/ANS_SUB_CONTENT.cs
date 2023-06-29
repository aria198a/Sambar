using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.OUTPUT
{
    public class ANS_SUB_CONTENT_PriContOtherAns_OUT
    {
        /// <summary>
        /// 選擇性開放項目回答ID
        /// </summary>
        public string AS_ID { get; set; }

        /// <summary>
        /// 填答項目ID
        /// </summary>
        public string AS_ACID { get; set; }

        /// <summary>
        /// 選擇性開放項目ID
        /// </summary>
        public string AS_DSID { get; set; }

        /// <summary>
        /// 對應文字
        /// </summary>
        public string DM_TEXT { get; set; }
    }
}
