using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class INHERITANCE
    {
        /// <summary>
        /// 數位遺產編號
        /// </summary>
        public string IH_ID { get; set; }

        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 0-全數捐贈 1-循環生前資料利用 2- 授予繼承人或代理人
        /// </summary>
        public int? IH_TYPE { get; set; }

        /// <summary>
        /// 繼承人
        /// </summary>
        public string IH_HEIR { get; set; }

        /// <summary>
        /// 代理人聯絡資訊
        /// </summary>
        public string IH_HEIR_DESCRIPTION { get; set; }

        /// <summary>
        /// 代理人
        /// </summary>
        public string IH_AGENT { get; set; }

        /// <summary>
        /// 代理人聯絡資訊
        /// </summary>
        public string IH_AGENT_DESCRIPTION { get; set; }
    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class INHERITANCE_EXTEND : INHERITANCE
    {

        /// <summary>
        /// 依AOID查詢
        /// </summary>
        /// <returns></returns>
        public INHERITANCE GetByACID(string AC_ID)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                 var result = sqlSugar.Queryable<INHERITANCE>()
                               .Where(x => x.AC_ID == AC_ID)
                               .ToList();
                return result.Count > 0 ? result[0] : null;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public INHERITANCE InsertExecute(INHERITANCE model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }


        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public INHERITANCE UpdateExecute(INHERITANCE model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

    }
}
