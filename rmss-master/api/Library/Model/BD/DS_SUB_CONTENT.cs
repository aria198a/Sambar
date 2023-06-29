using Library.Functions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class DS_SUB_CONTENT
    {
        /// <summary>
        /// 選擇性開放 ID
        /// </summary>
        public string DS_ID { get; set; }
        /// <summary>
        /// 選擇性開放 主標題
        /// </summary>
        public string DS_MAIN { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string DS_NAME { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string DS_DESC { get; set; }
        /// <summary>
        /// 是否刪除
        /// </summary>
        public bool DS_ISDEL { get; set; }

    }

    public class V_DS_SUB_CONTENT:DS_SUB_CONTENT
    {
        public string CMAIN { get; set; }
        public string CNAME { get; set; }
        public string CDESC { get; set; }
    }

    #region DB Helper
    public class DS_SUB_CONTENT_EXTEND
    {
        public List<OUT_SUB_CONTENT> GetPriContAnsSub(string acid, string dsmain)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {

                var result = sqlSugar.Queryable<V_DS_SUB_CONTENT, ANS_SUB_CONTENT>((c, a) => new object[] { JoinType.Left, c.DS_ID == a.AS_DSID && a.AS_ACID == acid })       
                                     .Where((c,a)=>c.DS_MAIN== dsmain)
                                     .Select((c, a) => new OUT_SUB_CONTENT
                                     {
                                         DS_ID = c.DS_ID,
                                         DS_MAIN = c.DS_MAIN,
                                         DS_NAME = c.DS_NAME,
                                         DS_DESC = c.DS_DESC,
                                         CMAIN = c.CMAIN,
                                         CNAME = c.CNAME,
                                         CDESC = c.CDESC,
                                         ANS = SqlFunc.IIF(a.AS_ID != null,true,false) 
                                     })
                                     .ToList();
                if (result.Count > 0)
                {
                    return result;
                }
                return new List<OUT_SUB_CONTENT>();
            }
        }

        
    }
    #endregion

    #region In
    public class IN_ACID:BaseModel
    {
        public string ACID { get; set; }
        public string DSMAIN { get; set; }
    }

    #endregion

    #region Out
    public class OUT_SUB_CONTENT : V_DS_SUB_CONTENT
    {
        public bool ANS { get; set; }
      
    }
    #endregion
}
