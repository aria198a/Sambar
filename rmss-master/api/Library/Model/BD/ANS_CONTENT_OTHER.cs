using Library.Functions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class ANS_CONTENT_OTHER
    {
        /// <summary>
        /// 資料提供者開放設定 單欄編號
        /// </summary>
        public string AO_ID { get; set; }

        /// <summary>
        /// 帳號USERID
        /// </summary>
        public string AO_USERID { get; set; }

        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string AO_NAME { get; set; }

        /// <summary>
        /// 資料提供者開放設定PK
        /// </summary>
        public string AM_ID { get; set; }

    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class ANS_CONTENT_OTHER_EXTEND : ANS_CONTENT_OTHER
    {

        /// <summary>
        /// 依AOID查詢
        /// </summary>
        /// <returns></returns>
        public ANS_CONTENT_OTHER Get(ANS_CONTENT_OTHER model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ANS_CONTENT_OTHER>()
                               .Where(x => x.AO_ID == model.AO_ID)
                               .Single();
            }
        }


        /// <summary>
        /// 依AMID查詢
        /// </summary>
        /// <returns></returns>
        public List<ANS_CONTENT_OTHER> GetListByAmidAndDcid(string AM_ID,string NAME,string USERID)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ANS_CONTENT_OTHER>()
                               .Where(x => x.AM_ID == AM_ID)
                               .Where(x => x.AO_NAME == NAME)
                               .Where(x => x.AO_USERID == USERID)
                               .ToList();
            }
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ANS_CONTENT_OTHER UpdateExecute(ANS_CONTENT_OTHER model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

    }

}
