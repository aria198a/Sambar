using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Apply
{
    public class VmApplyedIn : BaseModel
    {      
        /// <summary>
        /// 關鍵字
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// 申請起日
        /// </summary>
        public DateTime? applyDataSt { get; set; }        

        /// <summary>
        /// 申請迄日
        /// </summary>
        public DateTime? applyDataEnd { get; set; }


    }
}
