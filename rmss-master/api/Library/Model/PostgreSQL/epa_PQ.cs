using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.PostgreSQL
{
    public class epa_PQ
    {
        /// <summary>
        /// 懸浮粒子
        /// </summary>
        public string PQ_name { get; set; }
        
        /// <summary>
        /// 懸浮粒子解釋
        /// </summary>
        public string description { get; set; }
        
        /// <summary>
        /// 測量類別
        /// </summary>
        public string observationType { get; set; }
        
        /// <summary>
        /// 測量單位
        /// </summary>
        public string unitname { get; set; }
        
        /// <summary>
        /// 測量單位標記
        /// </summary>
        public string unitsymbol { get; set; }
        
        /// <summary>
        /// 測量單位定義
        /// </summary>
        public string unitdefinition { get; set; }



        //=========================================================================================================

        public List<epa_PQ> GetList(epa_PQ model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<epa_PQ>()
                    .WhereIF(string.IsNullOrEmpty(model.PQ_name),x=> x.PQ_name == model.PQ_name)
                               .ToList();
            }
        }
        
        // Consider using cache to speed up function
        public List<epa_PQ> GetList(string modelName)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<epa_PQ>().Where(x => x.PQ_name == modelName).ToList();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public epa_PQ InsertExecute(epa_PQ model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }


        /// <summary>
        /// 批次新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertExecute(List<epa_PQ> model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result;
            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public epa_PQ UpdateExecute(epa_PQ model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

        /// <summary>
        /// 批次更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateExecute(List<epa_PQ> model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result;
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public epa_PQ DeleteExecute(epa_PQ model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Deleteable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

        /// <summary>
        /// 批次刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteExecute(List<epa_PQ> model)
        {

            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Deleteable(model).ExecuteCommand();
                return result;
            }
        }
        //=========================================================================================================
    }
}
