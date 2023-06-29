using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Apply
{
    public class VmApplyMatchInfoOut
    {
        /// <summary>
        /// 申請ID
        /// </summary>
        public string apmId { get; set; }

        /// <summary>
        /// 來源ID
        /// </summary>
        public string acId { get; set; }

        /// <summary>
        /// 資料編號
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// 資料來源
        /// </summary>
        public string source { get; set; }        

        /// <summary>
        /// 資料類型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 偏好設定
        /// </summary>
        public string preference { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 申請回覆是否已閱讀
        /// </summary>
        public bool ApmIsRead { get; set; }



    }
}
