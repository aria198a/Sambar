using Library.Model.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Apply
{
    public class VmApplyDetailOutIn: BaseModel
    {
        public VmApplyDetailOutIn()
        {
            Main = new APPLY_MAIN();
            Main.APM_ID = "";
            Main.USERID = "";
            Main.UPDATE_USERID = "";
            ApY = new APPLY_Y();
            ApY.APM_ID = "";
            ApX =new APPLY_X();
            ApX.APM_ID = "";
            ApYS =new APPLY_Y_SUB();
            ApYS.APM_ID = "";
            ApXS = new APPLY_X_SUB();
            ApXS.APM_ID = "";
        }
        /// <summary>
        /// 申請主檔
        /// </summary>
        public APPLY_MAIN Main { get; set; }
        
        /// <summary>
        /// 申請 再利用脈絡Y
        /// </summary>
        public APPLY_Y ApY { get; set; }

        /// <summary>
        /// 申請 再利用脈絡X
        /// </summary>
        public APPLY_X ApX { get; set; }

        /// <summary>
        /// 申請 再利用脈絡Y 研究目標
        /// </summary>
        public APPLY_Y_SUB ApYS { get; set; }

        /// <summary>
        /// 申請 再利用脈絡x  健康資料來源
        /// </summary>
        public APPLY_X_SUB ApXS { get; set; }
    }
}
