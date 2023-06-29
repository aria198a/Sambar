using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class DS_MAPPING
    {
        /// <summary>
        /// 對應ID
        /// </summary>
        public string DM_ID { get; set; }
        /// <summary>
        /// 對應文字
        /// </summary>
        public string DM_TEXT { get; set; }
        /// <summary>
        /// 是否刪除
        /// </summary>
        public bool DM_ISDEL { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public string DM_OTHER { get; set; }
       
    }
}
