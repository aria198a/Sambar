using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.INPUT
{
    public class INHERITANCE_INPUT
    {
        /// <summary>
        /// 數位遺產編號
        /// </summary>
        public string IH_ID { get; set; }
    }

    public class INHERITANCE_INPUT_ADD: BaseModel
    {

        /// <summary>
        /// 0-全數捐贈 1-循環生前資料利用 2- 授予繼承人或代理人
        /// </summary>
        public int? IH_TYPE { get; set; }

        /// <summary>
        /// 繼承人
        /// </summary>
        public string? IH_HEIR { get; set; }

        /// <summary>
        /// 代理人聯絡資訊
        /// </summary>
        public string? IH_HEIR_DESCRIPTION { get; set; }

        /// <summary>
        /// 代理人
        /// </summary>
        public string? IH_AGENT { get; set; }

        /// <summary>
        /// 代理人聯絡資訊
        /// </summary>
        public string? IH_AGENT_DESCRIPTION { get; set; }
    }

   
}
