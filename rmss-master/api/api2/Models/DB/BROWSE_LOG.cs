
using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api2.Models.DB
{
    public class BROWSE_LOG
    {
        /// <summary>
        /// 編號
        /// </summary>
        public string BL_ID { get; set; }

        /// <summary>
        /// 使用者類別(管理者：Admin，社區：Community，縣市：County，分局：Branch，本局：Swcb，輔導團隊：Bacon，中央機關：Center，一般瀏覽者：Viewer)
        /// </summary>
        public string BL_TYPE { get; set; }

        /// <summary>
        /// 使用者ID(Session("LoginID"))
        /// </summary>
        public string BL_UID { get; set; }

        /// <summary>
        /// 使用者姓名(Session("LoginName"))
        /// </summary>
        public string BL_UNAME { get; set; }

        /// <summary>
        /// 使用者類別(Session("LoginTypeName"))
        /// </summary>
        public string BL_LOGINTYPENAME { get; set; }

        /// <summary>
        /// 時間
        /// </summary>
        public DateTime BL_TIME { get; set; }

        /// <summary>
        /// 功能名稱
        /// </summary>
        public string BL_FUNCTION { get; set; }

        /// <summary>
        /// 用戶IP
        /// </summary>
        public string BL_IP { get; set; }

    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class BROWSE_LOG_EXTEND : BROWSE_LOG
    {
        /// <summary>
        /// 紀錄
        /// </summary>
        /// <param name="function">API</param>
        /// <param name="ip">IP</param>
        /// <param name="account">帳號</param>
        public void BrowseLog(string function, string ip, ACCOUNT account = null)
        {
            try
            {
                BROWSE_LOG model = new BROWSE_LOG();
                model.BL_ID = "BL" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.BL_TYPE = account == null ? "" : account.AC_USERTYPE;
                model.BL_UID = account == null ? "" : account.AC_ID;
                model.BL_UNAME = account == null ? "" : account.AC_USERNAME;
                model.BL_LOGINTYPENAME = "";
                model.BL_TIME = DateTime.Now;
                model.BL_FUNCTION = function;
                model.BL_IP = ip;
                Insert(model);
            }
            catch (Exception ex)
            {
                new Library.Functions.NLog().LogInformation(ex);
            }
        }

        /// <summary>
        /// 新建紀錄
        /// </summary>
        /// <returns></returns>
        public BROWSE_LOG Insert2(BROWSE_LOG model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }


        /// <summary>
        /// 新建紀錄
        /// </summary>
        /// <returns></returns>
        private BROWSE_LOG Insert(BROWSE_LOG model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

        /// <summary>
        /// 查詢操作紀錄
        /// </summary>
        /// <returns></returns>
        public List<BROWSE_LOG> Select(BROWSE_LOG model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<BROWSE_LOG>()
                               .Where(x => x.BL_UID == model.BL_UID)
                               .OrderBy(x => x.BL_TIME, SqlSugar.OrderByType.Desc)
                               .Select(x => new BROWSE_LOG()
                               {
                                   BL_TIME = x.BL_TIME,
                                   BL_UNAME = x.BL_UNAME,
                                   BL_FUNCTION = x.BL_FUNCTION,
                                   BL_IP = x.BL_IP
                               })
                               .Take(1000)
                               .ToList();
            }
        }

        /// <summary>
        /// 查詢操作紀錄
        /// </summary>
        /// <returns></returns>
        public List<BROWSE_LOG> Select(DateTime? sTime, DateTime? eTime, string word)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<BROWSE_LOG>()
                               .WhereIF(sTime != null, x => x.BL_TIME >= sTime)
                               .WhereIF(eTime != null, x => x.BL_TIME <= eTime)
                               .WhereIF(!string.IsNullOrEmpty(word), x => word.Contains(x.BL_UNAME))
                               .OrderBy(x => x.BL_TIME, SqlSugar.OrderByType.Desc)
                               .Select(x => new BROWSE_LOG()
                               {
                                   BL_TIME = x.BL_TIME,
                                   BL_UNAME = x.BL_UNAME,
                                   BL_FUNCTION = x.BL_FUNCTION,
                                   BL_IP = x.BL_IP
                               })
                               .Take(1000)
                               .ToList();
            }
        }

    }

    public class BROWSE_LOG2 {
        /// <summary>
        /// 使用者ID(Session("LoginID"))
        /// </summary>
        public string BL_UID { get; set; }

        /// <summary>
        /// 開始時間
        /// </summary>
        public DateTime BL_TIME_S { get; set; }

        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime BL_TIME_E { get; set; }

    }

    public class BROWSE_LOG3
    {
        /// <summary>
        /// 使用者ID(Session("LoginID"))
        /// </summary>
        public string BL_UID { get; set; }

        /// <summary>
        /// 時間
        /// </summary>
        public DateTime BL_TIME { get; set; }

        /// <summary>
        /// 次數
        /// </summary>
        public int count { get; set; }

    }
}
