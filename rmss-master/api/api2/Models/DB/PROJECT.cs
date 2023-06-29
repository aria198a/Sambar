using Library.Functions;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api2.Models.DB
{
    /// <summary>
    /// 頁面表
    /// </summary>
    public class PROJECT
    {
        /// <summary>
        /// 頁面項目ID
        /// </summary>
        public string PJ_ID { get; set; }

        /// <summary>
        /// 階層
        /// </summary>
        public string PJ_LEVEL { get; set; }

        /// <summary>
        /// 頁面項目名稱
        /// </summary>
        public string PJ_NAME { get; set; }

        /// <summary>
        /// 頁面名稱
        /// </summary>
        public string PJ_PAGE { get; set; }

        /// <summary>
        /// 所屬類別(Community, County, Branch, Swcb)
        /// </summary>
        public string PJ_KIND { get; set; }

        /// <summary>
        /// 是否啟用(1:是,0:否)
        /// </summary>
        public int PJ_ONLINE { get; set; }

    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class PROJECT_EXTEND : PROJECT
    {
        public List<PROJECT> GetList()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<PROJECT>()
                               .ToList();
            }
        }

        /// <summary>
        /// 刪除頁面
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            PROJECT model = Select();
            model.PJ_ONLINE = 0;
            return UpdateExecute(model) != null ? 1 : 0;
        }

        /// <summary>
        /// 新增頁面
        /// </summary>
        /// <returns></returns>
        public int Insert()
        {
            PROJECT model = new PROJECT();
            model.PJ_ID = Guid.NewGuid().ToString();
            model.PJ_LEVEL = PJ_LEVEL;
            model.PJ_NAME = PJ_NAME;
            model.PJ_KIND = PJ_KIND;
            model.PJ_ONLINE = PJ_ONLINE;
            return InsertExecute(model) != null ? 1 : 0;
        }

        /// <summary>
        /// 新增頁面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PROJECT InsertExecute(PROJECT model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

        /// <summary>
        /// 依PJ_ID查詢頁面
        /// </summary>
        /// <returns></returns>
        public PROJECT Select()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<PROJECT>()
                               .Where(x => x.PJ_ID == PJ_ID)
                               .Single();
            }
        }

        /// <summary>
        /// 查詢列表
        /// </summary>
        /// <returns></returns>
        public List<OUT_PROJECT> SelectList()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<PROJECT>()
                               .Where(x => x.PJ_ONLINE == 1)
                               .OrderBy(x => x.PJ_LEVEL)
                               .Select(x => new OUT_PROJECT
                               {
                                   PJ_ID = x.PJ_ID,
                                   PJ_LEVEL = x.PJ_LEVEL,
                                   PJ_NAME = x.PJ_NAME
                               })
                               .ToList();
            }
        }

        /// <summary>
        /// 查詢權限列表
        /// </summary>
        /// <returns></returns>
        public List<OUT_PROJECT> SelectList2(string id)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<PROJECT, ACCOUNT_GROUP, ACCOUNT>((x, y, z) => new object[] { SqlSugar.JoinType.Inner, y.AG_PJID.Contains(x.PJ_ID), SqlSugar.JoinType.Inner, y.AG_ID == z.AC_GROUP_ID })
                               .OrderBy(x => x.PJ_LEVEL)
                               .Where((x, y, z) => x.PJ_ONLINE == 1 && z.AC_ID == id)
                               .Select((x, y, z) => new OUT_PROJECT
                               {
                                   PJ_ID = x.PJ_ID,
                                   PJ_LEVEL = x.PJ_LEVEL,
                                   PJ_NAME = x.PJ_NAME
                               })
                               .ToList();
            }
        }


        /// <summary>
        /// 查詢權限列表
        /// </summary>
        /// <returns></returns>
        public List<PROJECT> SelectList3(ACCOUNT_GROUP model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<PROJECT>()
                               .Where(x => model.AG_PJID.Contains(x.PJ_ID))
                               .Select(x => new PROJECT())
                               .ToList();
            }
        }

        /// <summary>
        /// 編輯頁面
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            PROJECT model = Select();
            model.PJ_LEVEL = PJ_LEVEL;
            model.PJ_NAME = PJ_NAME;
            model.PJ_KIND = PJ_KIND;
            model.PJ_ONLINE = PJ_ONLINE;
            return UpdateExecute(model) != null ? 1 : 0;
        }

        /// <summary>
        /// 編輯頁面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PROJECT UpdateExecute(PROJECT model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result > 0 ? model : null;

            }
        }

    }

    public class OUT_PROJECT
    {
        /// <summary>
        /// 頁面項目ID
        /// </summary>
        public string PJ_ID { get; set; }

        /// <summary>
        /// 階層
        /// </summary>
        public string PJ_LEVEL { get; set; }

        /// <summary>
        /// 頁面項目名稱
        /// </summary>
        public string PJ_NAME { get; set; }
    }

    public class GetProjectList: BaseModel
    {

        /// <summary>
        /// 依PJ_ID查詢頁面
        /// </summary>
        /// <returns></returns>
        public List<PROJECT> Select()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<PROJECT>()
                               .ToList();
            }
        }

    }
}
