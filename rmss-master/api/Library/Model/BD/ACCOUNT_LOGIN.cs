using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    /// <summary>
    /// 登入紀錄表
    /// </summary>
    public class ACCOUNT_LOGIN
    {
        /// <summary>
        /// 登入紀錄編號
        /// </summary>
        public string AL_ID { get; set; }

        /// <summary>
        /// 登入紀錄帳號
        /// </summary>
        public string AL_ACID { get; set; }

        /// <summary>
        /// 登入紀錄IP位置
        /// </summary>
        public string AL_IP { get; set; }

        /// <summary>
        /// 登入紀錄狀態[0:已註冊未開通 1:啟用 2:失敗]
        /// </summary>
        public int AL_STATUS { get; set; }

        /// <summary>
        /// 登入紀錄類別
        /// </summary>
        public string AL_LOGINTYPE { get; set; }

        /// <summary>
        /// 登入紀錄訊息
        /// </summary>
        public string AL_MSG { get; set; }

        /// <summary>
        /// 登入紀錄時間
        /// </summary>
        public DateTime AL_MDATE { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AL_AGNAME { get; set; }

        /// <summary>
        /// 登入Token
        /// </summary>
        public string AL_TOKEN { get; set; }

    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class InsertACCOUNT_LOGIN_EXTEND : ACCOUNT_LOGIN
    {
        /// <summary>
        /// 登入紀錄編號
        /// </summary>
        public string AL_ID { get; set; }

        /// <summary>
        /// 登入紀錄帳號
        /// </summary>
        public string AL_ACID { get; set; }

        /// <summary>
        /// 登入紀錄IP位置
        /// </summary>
        public string AL_IP { get; set; }

        /// <summary>
        /// 登入紀錄狀態[0:已註冊未開通 1:啟用 2:失敗]
        /// </summary>
        public int AL_STATUS { get; set; }

        /// <summary>
        /// 登入紀錄類別
        /// </summary>
        public string AL_LOGINTYPE { get; set; }

        /// <summary>
        /// 登入紀錄訊息
        /// </summary>
        public string AL_MSG { get; set; }

        /// <summary>
        /// 登入紀錄時間
        /// </summary>
        public DateTime AL_MDATE { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AL_AGNAME { get; set; }

        /// <summary>
        /// 登入Token
        /// </summary>
        public string AL_TOKEN { get; set; }

        /// <summary>
        /// 新增紀錄
        /// </summary>
        /// <returns></returns>
        //public int Insert()
        //{
        //    ACCOUNT_LOGIN model = new ACCOUNT_LOGIN();
        //    model.AL_ID = Guid.NewGuid().ToString();
        //    model.AL_ACID = AL_ACID;
        //    model.AL_IP = AL_IP;
        //    model.AL_STATUS = AL_STATUS;
        //    model.AL_LOGINTYPE = AL_LOGINTYPE;
        //    model.AL_MSG = AL_MSG;
        //    model.AL_AGNAME = AL_AGNAME;
        //    model.AL_MDATE = DateTime.Now;
        //    model.AL_TOKEN = AL_TOKEN;
        //    return InsertExecute(model) != null ? 1 : 0;
        //}
    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class ACCOUNT_LOGIN_EXTEND : ACCOUNT_LOGIN
    {
        /// <summary>
        /// 新增紀錄
        /// </summary>
        /// <returns></returns>
        public int Insert()
        {
            ACCOUNT_LOGIN model = new ACCOUNT_LOGIN();
            model.AL_ID = Guid.NewGuid().ToString();
            model.AL_ACID = AL_ACID;
            model.AL_IP = AL_IP;
            model.AL_STATUS = AL_STATUS;
            model.AL_LOGINTYPE = AL_LOGINTYPE;
            model.AL_MSG = AL_MSG;
            model.AL_AGNAME = AL_AGNAME;
            model.AL_MDATE = DateTime.Now;
            model.AL_TOKEN = AL_TOKEN;
            return InsertExecute(model) != null ? 1 : 0;
        }

        /// <summary>
        /// 新增紀錄
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ACCOUNT_LOGIN InsertExecute(ACCOUNT_LOGIN model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

        /// <summary>
        /// 查詢紀錄
        /// </summary>
        /// <returns></returns>
        public List<OUT_ACCOUNT_LOGIN> Select()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT_LOGIN, ACCOUNT_GROUP, ACCOUNT>((x, y, z) => new object[] { SqlSugar.JoinType.Left, x.AL_AGNAME == y.AG_ID, SqlSugar.JoinType.Left, x.AL_ACID == z.AC_ID })
                               .OrderBy(x => x.AL_MDATE, SqlSugar.OrderByType.Desc)
                               .Select((x, y, z) => new OUT_ACCOUNT_LOGIN
                               {
                                   AL_USERNAME = z.AC_USERNAME,
                                   AL_IP = x.AL_IP,
                                   AL_STATUS = x.AL_STATUS,
                                   AL_MSG = x.AL_MSG,
                                   AL_MDATE = x.AL_MDATE,
                                   AL_AGNAME = y.AG_NAME
                               })
                               .ToList();
            }
        }

        /// <summary>
        /// 查詢紀錄
        /// </summary>
        /// <returns></returns>
        public ACCOUNT_LOGIN SelectById()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT_LOGIN>()
                               .OrderBy(x => x.AL_MDATE, SqlSugar.OrderByType.Desc)
                               .Where(x => x.AL_ACID == AL_ACID)
                               .Single();
            }
        }

        /// <summary>
        /// 依帳號查詢紀錄
        /// </summary>
        /// <returns></returns>
        public List<OUT_ACCOUNT_LOGIN> SelectByACID()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT_LOGIN, ACCOUNT_GROUP, ACCOUNT>((x, y, z) => new object[] { SqlSugar.JoinType.Left, x.AL_AGNAME == y.AG_ID, SqlSugar.JoinType.Left, x.AL_ACID == z.AC_ID })
                               .OrderBy(x => x.AL_MDATE, SqlSugar.OrderByType.Desc)
                               .Where(x => x.AL_ACID == AL_ACID)
                               .Select((x, y, z) => new OUT_ACCOUNT_LOGIN
                               {
                                   AL_USERNAME = z.AC_USERNAME,
                                   AL_IP = x.AL_IP,
                                   AL_STATUS = x.AL_STATUS,
                                   AL_MSG = x.AL_MSG,
                                   AL_MDATE = x.AL_MDATE,
                                   AL_AGNAME = y.AG_NAME
                               })
                               .ToList();
            }
        }

    }

    public class OUT_ACCOUNT_LOGIN
    {
        /// <summary>
        /// 登入帳號
        /// </summary>
        public string AL_USERNAME { get; set; }

        /// <summary>
        /// 登入紀錄IP位置
        /// </summary>
        public string AL_IP { get; set; }

        /// <summary>
        /// 登入紀錄狀態
        /// </summary>
        public int AL_STATUS { get; set; }

        /// <summary>
        /// 登入紀錄訊息
        /// </summary>
        public string AL_MSG { get; set; }

        /// <summary>
        /// 登入紀錄時間
        /// </summary>
        public DateTime AL_MDATE { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AL_AGNAME { get; set; }
    }
}
