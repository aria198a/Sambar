using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Apply
{
    public class VmApplyedStateIn : BaseModel
    {      
        /// <summary>
        /// 申請id
        /// </summary>
        public string apmId { get; set; }

        /// <summary>
        /// 資料來源id
        /// </summary>
        public string acId { get; set; }        

        /// <summary>
        /// 狀態
        /// </summary>
        public int state { get; set; }


    }

    public class VmApplyedStateBatchIn : BaseModel
    {
        public List<applyedInfo> applyedInfos { get; set; }
    }

    public class applyedInfo
    {
        /// <summary>
        /// 申請id
        /// </summary>
        public string apmId { get; set; }

        /// <summary>
        /// 資料來源id
        /// </summary>
        public string acId { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int state { get; set; }
    }
}
