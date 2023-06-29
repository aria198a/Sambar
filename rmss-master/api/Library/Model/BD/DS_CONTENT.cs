using Library.Functions;
using Library.Model.OUTPUT;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class DS_CONTENT
    {
        /// <summary>
        /// 資料設定ID 
        /// </summary>
        public string DC_ID { get; set; }

        /// <summary>
        /// 編號
        /// </summary>
        public int DC_NUM { get; set; }

        /// <summary>
        /// 主類別名稱
        /// </summary>
        public string DC_MAIN { get; set; }
        /// <summary>
        /// 標題名稱
        /// </summary>
        public string DC_TITLE { get; set; }
        /// <summary>
        /// 資料分享偏好內容
        /// </summary>
        public string DC_CONTENT { get; set; }

        /// <summary>
        /// 是否刪除
        /// </summary>
        public bool DC_ISDEL { get; set; }
    }

    public class V_DS_CONTENT : DS_CONTENT
    {

        public string CMAIN { get; set; }
        public string CTITLE { get; set; }
        public string CCONTENT { get; set; }
    }


    #region 作業邏輯
    public class DS_CONTENT_EXTEND : DS_CONTENT
    {


        /// <summary>
        /// 確認是否已填寫
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckPried(string userId)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {

                var result = sqlSugar.Queryable<ANS_CONTENT>()
                                     .Where(x => x.AC_USERID == userId)
                                     .ToList();
                if (result.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 取得 隱私偏好設定項目清單包含回答
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<OUT_DS_CONTENT> GetPriContAns(string userId)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {

                var result = sqlSugar.Queryable<V_DS_CONTENT, ANS_CONTENT>((c, a) => new object[] { JoinType.Left, c.DC_ID == a.AC_DCID && a.AC_USERID==userId })                                    
                                     .Select((c, a) => new OUT_DS_CONTENT
                                     {
                                         DC_ID = c.DC_ID,
                                         DC_NUM = c.DC_NUM,
                                         DC_MAIN = c.DC_MAIN,
                                         DC_TITLE = c.DC_TITLE,
                                         DC_CONTENT = c.DC_CONTENT,
                                         CMAIN = c.CMAIN,
                                         CTITLE = c.CTITLE,
                                         CCONTENT = c.CCONTENT,
                                         ANS = a.AC_ANS,
                                         ACID = a.AC_ID==null?"":a.AC_ID,
                                         ACOTHER=a.AC_OTHER==null?"":a.AC_OTHER, 
                                        
                                         
                                     })
                                     .ToList();
                if (result.Count > 0)
                {
                    // 取 Sub
                    foreach (var item in result)
                    {
                        item.ANS_SUB = sqlSugar.Queryable<V_DS_SUB_CONTENT, ANS_SUB_CONTENT>((sc, sa) => new object[] { JoinType.Left, sc.DS_ID == sa.AS_DSID && sa.AS_ACID == item.ACID })
                                       .Where((sc, sa) => sc.DS_MAIN == item.DC_MAIN)
                                       .Select((sc, sa) => new OUT_SUB_CONTENT
                                       {
                                           DS_ID = sc.DS_ID,
                                           DS_MAIN = sc.DS_MAIN,
                                           DS_NAME = sc.DS_NAME,
                                           DS_DESC = sc.DS_DESC,
                                           CMAIN = sc.CMAIN,
                                           CNAME = sc.CNAME,
                                           CDESC = sc.CDESC,
                                           ANS = (sa.AS_ID != null ? true : false),

                                       }).ToList();
                    }
                    return result;
                }
                return new List<OUT_DS_CONTENT>();
            }
        }

        public List<OUT_DS_CONTENT> GetPriContAnsByCidList(string userId, List<string> cids)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {

                var result = sqlSugar.Queryable<V_DS_CONTENT, ANS_CONTENT>((c, a) => new object[] { JoinType.Left, c.DC_ID == a.AC_DCID && a.AC_USERID == userId })
                                     .Where((c,a)=> cids.Contains(c.DC_CONTENT)  )
                                     .Select((c, a) => new OUT_DS_CONTENT
                                     {
                                         DC_ID = c.DC_ID,
                                         DC_NUM = c.DC_NUM,
                                         DC_MAIN = c.DC_MAIN,
                                         DC_TITLE = c.DC_TITLE,
                                         DC_CONTENT = c.DC_CONTENT,
                                         CMAIN = c.CMAIN,
                                         CTITLE = c.CTITLE,
                                         CCONTENT = c.CCONTENT,
                                         ANS = a.AC_ANS,
                                         ACID=a.AC_ID,
                                         ACOTHER = a.AC_OTHER,
                                     })
                                     .ToList();
                if (result.Count > 0)
                {
                    return result;
                }
                return new List<OUT_DS_CONTENT>();
            }
        }


        public List<ANS_CONTENT_OTHER_OUT> GetPriContOtherAns(string userId)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                List<ANS_CONTENT_OTHER_OUT> ANS_CONTENT_OTHER_OUT_LIST = new List<ANS_CONTENT_OTHER_OUT>();

                var result = sqlSugar.Queryable<ANS_CONTENT_OTHER>()
                                     .Where(x => x.AO_USERID == userId)
                                     .ToList();
                foreach (var ao in result)
                {
                    ANS_CONTENT_OTHER_OUT ANS_CONTENT_OTHER_OUT = new ANS_CONTENT_OTHER_OUT();
                    ANS_CONTENT_OTHER_OUT.name = ao.AO_NAME;
                    ANS_CONTENT_OTHER_OUT.content_list = new List<content>();

                    var aosList = sqlSugar.Queryable<ANS_CONTENT_OTHER_SUB,DS_CONTENT>((x, y) => new object[] { JoinType.Left, x.AOS_DCID == y.DC_ID })
                                     .Where(x => x.AOS_AO_ID == ao.AO_ID)
                                     .OrderBy(x => x.AOS_DCID)
                                     .Select<ANS_CONTENT_OTHER_SUB_join>()
                                     .ToList();
                    foreach (var aos in aosList)
                    {
                        content content = new content();
                        content.dcId = aos.AOS_DCID;
                        content.ANS = aos.AOS_ANS;
                        content.Name = aos.DC_MAIN;
                        content.ANS_SUB_OPTIONS = new List<sub>();

                        var ascList = sqlSugar.Queryable<ANS_SUB_CONTENT,DS_SUB_CONTENT, DS_MAPPING> ((x, y,z) => new object[] { 
                                        JoinType.Left, x.AS_DSID == y.DS_ID,
                                        JoinType.Left, y.DS_NAME == z.DM_ID,
                                    })
                                    .Where(x => x.AS_ACID == aos.AOS_ID)
                                    .OrderBy(x => x.AS_DSID)
                                    .Select<ANS_SUB_CONTENT_PriContOtherAns_OUT>()
                                    .ToList();

                        var subStr = "";
                        foreach (var asc in ascList)
                        {
                            sub sub = new sub();
                            sub.ANS = true;
                            sub.DS_ID = asc.AS_DSID;
                            sub.CNAME = asc.DM_TEXT;
                            subStr += asc.DM_TEXT + ",";

                            content.ANS_SUB_OPTIONS.Add(sub);
                        }
                        content.ANS_SUB = subStr;

                        ANS_CONTENT_OTHER_OUT.content_list.Add(content);

                    }

                    ANS_CONTENT_OTHER_OUT_LIST.Add(ANS_CONTENT_OTHER_OUT);
                }



                return ANS_CONTENT_OTHER_OUT_LIST;
            }
        }


    }
    #endregion

    #region IN
    public class InCidList : BaseModel
    {
        public List<string> CIDs { get; set; }
    }

    public class InContAns : BaseModel
    {
        public List<OUT_DS_CONTENT> ContAnsList { get; set; }
    }
    #endregion

    #region OUT
    public class OUT_DS_CONTENT : V_DS_CONTENT
    {
        public int ANS { get; set; }
        public string ACOTHER { get; set; }
        public string ACID { get; set; }
        public List<OUT_SUB_CONTENT> ANS_SUB { get; set; }
    }


    #endregion

}
