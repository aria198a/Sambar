using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class ANS_CONTENT_OTHER_SUB
    {
        /// <summary>
        /// 資料提供者開放設定 單欄編號 子集合
        /// </summary>
        public string AOS_ID { get; set; }

        /// <summary>
        /// 資料提供者開放設定 單欄編號
        /// </summary>
        public string AOS_AO_ID { get; set; }

        /// <summary>
        /// 填寫內容ID
        /// </summary>
        public string AOS_DCID { get; set; }

        /// <summary>
        /// 填寫答案
        /// </summary>
        public int AOS_ANS { get; set; }

    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class ANS_CONTENT_OTHER_SUB_EXTEND : ANS_CONTENT_OTHER_SUB
    {

        /// <summary>
        /// 依AID查詢
        /// </summary>
        /// <returns></returns>
        public ANS_CONTENT_OTHER_SUB Get(ANS_CONTENT_OTHER_SUB model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ANS_CONTENT_OTHER_SUB>()
                               .Where(x => x.AOS_ID == model.AOS_ID)
                               .Single();
            }
        }

        /// <summary>
        /// 依AMID查詢
        /// </summary>
        /// <returns></returns>
        public List<ANS_CONTENT_OTHER_SUB> GetListByAOID(string AOID)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ANS_CONTENT_OTHER_SUB>()
                               .Where(x => x.AOS_AO_ID == AOID)
                               .ToList();
            }
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ANS_CONTENT_OTHER_SUB UpdateExecute(ANS_CONTENT_OTHER_SUB model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

    }

}
