using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class InsertAccountGroup : BaseModel
    {

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AG_NAME { get; set; }

        /// <summary>
        /// 權限群組頁面
        /// </summary>
        public string AG_PJID { get; set; }
    }
    /// <summary>
    /// 權限群組表
    /// </summary>
    public class ACCOUNT_GROUP
    {
        /// <summary>
        /// 權限群組編號
        /// </summary>
        public string AG_ID { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AG_NAME { get; set; }

        /// <summary>
        /// 權限群組頁面
        /// </summary>
        public string AG_PJID { get; set; }

        /// <summary>
        /// 權限群組建立時間
        /// </summary>
        public DateTime AG_MDATE { get; set; }

        /// <summary>
        /// 權限群組建立帳號編號
        /// </summary>
        public string AG_MUSERID { get; set; }

        /// <summary>
        /// 權限群組編輯時間
        /// </summary>
        public DateTime AG_EDATE { get; set; }

        /// <summary>
        /// 權限群組編輯帳號編號
        /// </summary>
        public string AG_EUSERID { get; set; }

        /// <summary>
        /// 權限群組類別
        /// </summary>
        public string AG_TYPE { get; set; }

        /// <summary>
        /// 是否刪除[0:否 1:是]
        /// </summary>
        public int AG_ISDEL { get; set; }
    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class ACCOUNT_GROUP_EXTEND : ACCOUNT_GROUP
    {
        /// <summary>
        /// 刪除權限群組
        /// </summary>
        /// <returns></returns>
        public ACCOUNT_GROUP Delete()
        {
            ACCOUNT_GROUP model = SelectByAGID2();
            model.AG_ISDEL = 1;
            model.AG_EDATE = DateTime.Now;
            model.AG_EUSERID = AG_EUSERID;
            return UpdateExecute(model);
        }

        /// <summary>
        /// 新增權限群組
        /// </summary>
        /// <returns></returns>
        public ACCOUNT_GROUP Insert()
        {
            ACCOUNT_GROUP model = new ACCOUNT_GROUP();
            model.AG_NAME = AG_NAME;
            model.AG_PJID = AG_PJID;
            model.AG_MDATE = DateTime.Now;
            model.AG_MUSERID = AG_MUSERID;
            model.AG_EDATE = DateTime.Now;
            model.AG_EUSERID = AG_EUSERID;
            model.AG_ISDEL = 0;
            return InsertExecute(model);
        }

        /// <summary>
        /// 新增權限群組
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ACCOUNT_GROUP InsertExecute(ACCOUNT_GROUP model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

        /// <summary>
        /// 查詢權限群組
        /// </summary>
        /// <returns></returns>
        public List<ACCOUNT_GROUP> Select()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT_GROUP>()
                               .Where(x => x.AG_ISDEL == 0)
                               .ToList();
            }
        }

        /// <summary>
        /// 依AG_ID查詢群組
        /// </summary>
        /// <returns></returns>
        public ACCOUNT_GROUP SelectByAGID2()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT_GROUP>()
                               .Where(x => x.AG_ID == AG_ID)
                               .Single();
            }
        }

        /// <summary>
        /// 依AG_ID查詢群組
        /// </summary>
        /// <returns></returns>
        public ACCOUNT_GROUP Get(ACCOUNT_GROUP model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT_GROUP>()
                               .Where(x => x.AG_ID == model.AG_ID)
                               .Single();
            }
        }

        /// <summary>
        /// 依AG_ID查詢權限群組
        /// </summary>
        /// <returns></returns>
        public ACCOUNT_GROUP SelectByAGID()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT_GROUP>()
                               .Where(x => x.AG_ISDEL == 0 && x.AG_ID == AG_ID)
                               .Single();
            }
        }

        /// <summary>
        /// 編輯權限群組
        /// </summary>
        /// <returns></returns>
        public ACCOUNT_GROUP Update()
        {
            ACCOUNT_GROUP model = SelectByAGID2();
            model.AG_NAME = AG_NAME;
            model.AG_PJID = AG_PJID;
            model.AG_EDATE = DateTime.Now;
            model.AG_EUSERID = AG_EUSERID;
            return UpdateExecute(model);
        }

        /// <summary>
        /// 編輯權限群組
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ACCOUNT_GROUP UpdateExecute(ACCOUNT_GROUP model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

    }

    public class ACCOUNT_GROUPLIST:BaseModel
    {
        public List<ACCOUNT_GROUP> Select()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT_GROUP>()
                               .Where(x => x.AG_ISDEL == 0)
                               .ToList();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateAccountGroup : BaseModel
    {
        /// <summary>
        /// 權限群組編號
        /// </summary>
        public string AG_ID { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AG_NAME { get; set; }

        /// <summary>
        /// 權限群組頁面
        /// </summary>
        public string AG_PJID { get; set; }
    }

}
