using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class ANS_SUB_CONTENT
    {
        /// <summary>
        /// 選擇性開放項目回答ID
        /// </summary>
        public string AS_ID { get; set; }

        /// <summary>
        /// 填答項目ID
        /// </summary>
        public string AS_ACID { get; set; }

        /// <summary>
        /// 選擇性開放項目ID
        /// </summary>
        public string AS_DSID { get; set; }
    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class ANS_SUB_CONTENT_EXTEND : ANS_SUB_CONTENT
    {

        /// <summary>
        /// 依AMID查詢
        /// </summary>
        /// <returns></returns>
        public List<ANS_SUB_CONTENT> GetListByACID(string ACID)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ANS_SUB_CONTENT>()
                               .Where(x => x.AS_ACID == ACID)
                               .ToList();
            }
        }

    }

}
