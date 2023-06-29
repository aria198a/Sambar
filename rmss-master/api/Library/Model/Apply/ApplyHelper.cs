using Library.Functions;
using Library.Model.BD;
using MySqlX.XDevAPI.Common;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Apply
{
    public class ApplyHelper
    {
        //Get
        /// <summary>
        /// 取得 申請資料
        /// </summary>
        /// <param name="ampId"></param>
        /// <returns></returns>
        public VmApplyDetailOutIn GetApplyDetail(string apmId)
        {
            if (apmId == "0")
            {
                return new VmApplyDetailOutIn();
            }

            var result = new VmApplyDetailOutIn();
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var Main = sqlSugar.Queryable<APPLY_MAIN>().InSingle(apmId);
                var ApY = sqlSugar.Queryable<APPLY_Y>().InSingle(apmId);
                var ApX = sqlSugar.Queryable<APPLY_X>().InSingle(apmId);
                var ApYS = sqlSugar.Queryable<APPLY_Y_SUB>().InSingle(apmId);
                var ApXS = sqlSugar.Queryable<APPLY_X_SUB>().InSingle(apmId);

                if (Main != null && ApY != null && ApX != null && ApYS != null && ApXS != null)
                {
                    result.Main = Main;
                    result.ApY = ApY;
                    result.ApX = ApX;
                    result.ApYS = ApYS;
                    result.ApXS = ApXS;
                }

                return result;
            }
        }

        /// <summary>
        /// 取得 申請計畫列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<VmApplyOut> GetApplyList(string userid)
        {
            var result = new List<VmApplyOut>();

            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                //抓取 請求者的所有申請 
                var applyList = sqlSugar.Queryable<V_APPLY_INFO>()
                                        .Where(main => main.USERID == userid)
                                        .ToList();

                var resultMatchList = new List<V_SUB_SOURCE_TYPE_STATUS>();
                foreach (var ap in applyList) // 依 每一申請 匹配
                {
                    resultMatchList = CalMappingSource(ap);
                    var apMatchInfo = new VmApplyOut();
                    apMatchInfo.apmId = ap.APM_ID;
                    apMatchInfo.number = ap.APM_NO;
                    apMatchInfo.planName = ap.NAME;
                    apMatchInfo.planType1 = ap.PUBLICINFO == 1 ? "公共" : ap.NPUBLICINFO == 1 ? "非公共" : "";
                    apMatchInfo.planType2 = ap.BUSINESS == 1 ? "商業" : ap.EDU == 1 ? "非商業" : "";
                    apMatchInfo.planType3 = ap.FOREIGNINFO == 1 || ap.TPLACE == 2 ? "境外" :  "境內" ;
                    apMatchInfo.match = resultMatchList.Count;

                    result.Add(apMatchInfo);
                }

                return result;
            }
        }

        private List<V_SUB_SOURCE_TYPE_STATUS> CalMappingSource(V_APPLY_INFO ap)
        {
            var resultMatchList = new List<V_SUB_SOURCE_TYPE_STATUS>();

            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                //--------------舊的  沒有單欄位條件寫法--------------------
                // // 初步篩選 匹配出符合的ANS   Match1
                // var ansMatchList = sqlSugar.Queryable<V_SUB_SOURCE_TYPE,APPLY_SOURCE_REL>((ans, rel)=>new object[] { JoinType.Left, ans.AC_ID==rel.AC_ID && rel.APM_ID==ap.APM_ID && rel.IS_ENABLED})
                //.WhereIF(ap.PUBLICINFO == 1 && ap.NPUBLICINFO != 1, (ans, rel) => ans.PUBLICINFO < 4)
                // .WhereIF(ap.NPUBLICINFO == 1 && ap.PUBLICINFO != 1, (ans, rel) => ans.NPUBLICINFO < 4)
                // .WhereIF(ap.BUSINESS == 1 && ap.EDU != 1, (ans, rel) => ans.BUSINESS < 4)
                // .WhereIF(ap.EDU == 1 && ap.BUSINESS != 1, (ans, rel) => ans.EDU < 4)
                // .WhereIF(ap.FOREIGNINFO == 1 || ap.TPLACE == 2, (ans, rel) => ans.FOREIGNINFO < 4)
                // .WhereIF(ap.NFOREIGNINFO == 1 && ap.FOREIGNINFO != 1 && ap.TPLACE == 1, (ans, rel) => ans.NFOREIGNINFO < 4)
                // .Where((ans, rel) => ap.DT_RECORD == 1 && ans.ANS_TYPE == "DT_RECORD" && ans.AC_ANS < 4
                // || ap.DT_SPECIMEN == 1 && ans.ANS_TYPE == "DT_SPECIMEN" && ans.AC_ANS < 4
                // || ap.HE_RECORD == 1 && ans.ANS_TYPE == "HE_RECORD" && ans.AC_ANS < 4
                // || ap.HE_SPECIMEN == 1 && ans.ANS_TYPE == "HE_SPECIMEN" && ans.AC_ANS < 4
                // || ap.AR_RECORD == 1 && ans.ANS_TYPE == "AR_RECORD" && ans.AC_ANS < 4
                // || ap.AR_SPECIMEN == 1 && ans.ANS_TYPE == "AR_SPECIMEN" && ans.AC_ANS < 4
                // )
                // .Select((ans,rel) => new V_SUB_SOURCE_TYPE_STATUS()
                // {
                //     AC_ID = ans.AC_ID,
                //     AM_ID = ans.AM_ID,  
                //     AC_USERID = ans.AC_USERID,
                //     AM_NO=ans.AM_NO,
                //     ANS_TYPE = ans.ANS_TYPE,
                //     ANS_SOURCECN = ans.ANS_SOURCECN,    
                //     ANS_TYPECN = ans.ANS_TYPECN,                    
                //     AC_ANS = ans.AC_ANS,    
                //     SUB=ans.SUB,    
                //     PUBLICINFO=ans.PUBLICINFO,  
                //     NPUBLICINFO=ans.NPUBLICINFO,
                //     BUSINESS=ans.BUSINESS,
                //     EDU=ans.EDU,
                //     NFOREIGNINFO=ans.NFOREIGNINFO,
                //     FOREIGNINFO=ans.FOREIGNINFO,
                //     PUBLICINFO_SUB=ans.PUBLICINFO_SUB,  
                //     NPUBLICINFO_SUB=ans.NPUBLICINFO_SUB,
                //     BUSINESS_SUB=ans.BUSINESS_SUB,
                //     EDU_SUB=ans.EDU_SUB,
                //     NFOREIGNINFO_SUB=ans.NFOREIGNINFO_SUB,
                //     FOREIGNINFO_SUB=ans.FOREIGNINFO_SUB,
                //     CREATED_DATETIME=ans.CREATED_DATETIME,
                //     APPLY_STATUS=rel.STATUS,
                //     APM_IS_READED = rel.APM_IS_READED,
                // })

                // .ToList();

                // resultMatchList = ansMatchList.ToList();
                // bool isContain = true;
                // foreach (var amatch in ansMatchList) // 檢查每一ANS，是否有選擇性開放，若有，則比對內容是否符合
                // {
                //     isContain = true;

                //     if (amatch.AC_ANS == 2 && ap.X_SUB != null)
                //     {
                //         isContain = isContain && MatchSub(ap.X_SUB.Split(","),amatch.SUB.Split(","));

                //     }
                //     if (ap.PUBLICINFO == 1 && amatch.PUBLICINFO == 2 && ap.Y_SUB != null)
                //     {
                //         isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.PUBLICINFO_SUB.Split(","));
                //     }
                //     if (ap.NPUBLICINFO == 1 && amatch.NPUBLICINFO == 2 && ap.Y_SUB != null)
                //     {
                //         isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.NPUBLICINFO_SUB.Split(","));
                //     }
                //     if (ap.BUSINESS == 1 && amatch.BUSINESS == 2 && ap.Y_SUB != null)
                //     {
                //         isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.BUSINESS_SUB.Split(","));
                //     }
                //     if (ap.EDU == 1 && amatch.EDU == 2 && ap.Y_SUB != null)
                //     {
                //         isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.EDU_SUB.Split(","));
                //     }
                //     if (ap.NFOREIGNINFO == 1 && amatch.NFOREIGNINFO == 2 && ap.Y_SUB != null )
                //     {
                //         isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.NFOREIGNINFO_SUB.Split(","));
                //     }

                //     if (ap.FOREIGNINFO == 1 && amatch.FOREIGNINFO == 2 && ap.Y_SUB != null)
                //     {
                //         isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.FOREIGNINFO_SUB.Split(","));
                //     }

                //     if (!isContain)
                //     {
                //         resultMatchList.Remove(amatch);
                //     }
                // }

                //--------------加入單欄位條件寫法--------------------
                var ansMatchList = sqlSugar.Queryable<V_SUB_SOURCE_TYPE, APPLY_SOURCE_REL>((ans, rel) => new object[] { JoinType.Left, ans.AC_ID == rel.AC_ID && rel.APM_ID == ap.APM_ID && rel.IS_ENABLED })
                .Where((ans, rel) => ap.DT_RECORD == 1 && ans.ANS_TYPE == "DT_RECORD"
                || ap.DT_SPECIMEN == 1 && ans.ANS_TYPE == "DT_SPECIMEN"
                || ap.HE_RECORD == 1 && ans.ANS_TYPE == "HE_RECORD"
                || ap.HE_SPECIMEN == 1 && ans.ANS_TYPE == "HE_SPECIMEN"
                || ap.AR_RECORD == 1 && ans.ANS_TYPE == "AR_RECORD"
                || ap.AR_SPECIMEN == 1 && ans.ANS_TYPE == "AR_SPECIMEN"
                )
                .Select((ans, rel) => new V_SUB_SOURCE_TYPE_STATUS()
                {
                    AC_ID = ans.AC_ID,
                    AM_ID = ans.AM_ID,
                    AC_USERID = ans.AC_USERID,
                    AM_NO = ans.AM_NO,
                    ANS_TYPE = ans.ANS_TYPE,
                    ANS_SOURCECN = ans.ANS_SOURCECN,
                    ANS_TYPECN = ans.ANS_TYPECN,
                    AC_ANS = ans.AC_ANS,
                    SUB = ans.SUB,
                    PUBLICINFO = ans.PUBLICINFO,
                    NPUBLICINFO = ans.NPUBLICINFO,
                    BUSINESS = ans.BUSINESS,
                    EDU = ans.EDU,
                    NFOREIGNINFO = ans.NFOREIGNINFO,
                    FOREIGNINFO = ans.FOREIGNINFO,
                    PUBLICINFO_SUB = ans.PUBLICINFO_SUB,
                    NPUBLICINFO_SUB = ans.NPUBLICINFO_SUB,
                    BUSINESS_SUB = ans.BUSINESS_SUB,
                    EDU_SUB = ans.EDU_SUB,
                    NFOREIGNINFO_SUB = ans.NFOREIGNINFO_SUB,
                    FOREIGNINFO_SUB = ans.FOREIGNINFO_SUB,
                    CREATED_DATETIME = ans.CREATED_DATETIME,
                    APPLY_STATUS = rel.STATUS,
                    APM_IS_READED = rel.APM_IS_READED,
                })
                .ToList();

                ANS_CONTENT_OTHER_EXTEND ANS_CONTENT_OTHER_EXTEND = new ANS_CONTENT_OTHER_EXTEND();
                ANS_CONTENT_OTHER_SUB_EXTEND ANS_CONTENT_OTHER_SUB_EXTEND = new ANS_CONTENT_OTHER_SUB_EXTEND();
                ANS_SUB_CONTENT_EXTEND ANS_SUB_CONTENT_EXTEND = new ANS_SUB_CONTENT_EXTEND();
                resultMatchList = ansMatchList.ToList();
                var apDcid = Info2Id(ap);
                bool isContain = true;
                foreach(var amatch in ansMatchList) // 檢查每一ANS，是否有選擇性開放，若有，則比對內容是否符合
                {
                    isContain = true;
                    
                    var dcid = Type2Id(amatch.ANS_TYPE);
                    var ANS_CONTENT_OTHERs = ANS_CONTENT_OTHER_EXTEND.GetListByAmidAndDcid(amatch.AM_ID, dcid + "," + apDcid, amatch.AC_USERID);

                    //-------------單欄設定
                    if (ANS_CONTENT_OTHERs.Count > 0)
                    {
                        foreach (var otehr in ANS_CONTENT_OTHERs)
                        {
                            if (isContain)
                            {
                                var ANS_CONTENT_OTHER_SUBs = ANS_CONTENT_OTHER_SUB_EXTEND.GetListByAOID(otehr.AO_ID);
                                foreach (var otherSub in ANS_CONTENT_OTHER_SUBs)
                                {
                                    if (otherSub.AOS_ANS == 4)
                                    {
                                        isContain = false;
                                        break;
                                    }
                                    else if (otherSub.AOS_ANS == 2)
                                    {
                                        var subContents = ANS_SUB_CONTENT_EXTEND.GetListByACID(otherSub.AOS_ID);
                                        foreach (var subContent in subContents)
                                        {
                                            //檢查X軸
                                            if (otherSub.AOS_DCID == "DC001" || otherSub.AOS_DCID == "DC002" || otherSub.AOS_DCID == "DC003" || otherSub.AOS_DCID == "DC004" || otherSub.AOS_DCID == "DC005" || otherSub.AOS_DCID == "DC006")
                                            {
                                                if (!MatchSub(ap.X_SUB.Split(","),new string[1] { subContent.AS_DSID }))
                                                {
                                                    isContain = false;
                                                    break;
                                                }
                                            }
                                            //檢查Y軸
                                            else if (otherSub.AOS_DCID == "DC007" || otherSub.AOS_DCID == "DC008" || otherSub.AOS_DCID == "DC009" || otherSub.AOS_DCID == "DC010" || otherSub.AOS_DCID == "DC011" || otherSub.AOS_DCID == "DC012")
                                            {
                                                if (!MatchSub(ap.Y_SUB.Split(","), new string[1] { subContent.AS_DSID }))
                                                {
                                                    isContain = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        
                        }
                    }
                    //--------------整體設定
                    else
                    {
                        isContain = false;
                        if ((ap.PUBLICINFO == 1 && ap.NPUBLICINFO != 1) && amatch.PUBLICINFO < 4)
                            isContain = true; 
                        if ((ap.NPUBLICINFO == 1 && ap.PUBLICINFO != 1) && amatch.NPUBLICINFO < 4)
                            isContain = true;
                        if ((ap.BUSINESS == 1 && ap.EDU != 1) && amatch.BUSINESS < 4)
                            isContain = true;
                        if ((ap.EDU == 1 && ap.BUSINESS != 1) && amatch.EDU < 4)
                            isContain = true;
                        if ((ap.FOREIGNINFO == 1 && ap.TPLACE != 1) && amatch.FOREIGNINFO < 4)
                            isContain = true;
                        if ((ap.NFOREIGNINFO == 1 && ap.FOREIGNINFO != 1 && ap.TPLACE != 1) && amatch.NFOREIGNINFO < 4)
                            isContain = true;

                        if(isContain)
                        {
                            if (amatch.AC_ANS == 2 && ap.X_SUB != null)
                            {
                                isContain = isContain && MatchSub(ap.X_SUB.Split(","), amatch.SUB.Split(","));
                            }
                            if (ap.PUBLICINFO == 1 && amatch.PUBLICINFO == 2 && ap.Y_SUB != null)
                            {
                                isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.PUBLICINFO_SUB.Split(","));
                            }
                            if (ap.NPUBLICINFO == 1 && amatch.NPUBLICINFO == 2 && ap.Y_SUB != null)
                            {
                                isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.NPUBLICINFO_SUB.Split(","));
                            }
                            if (ap.BUSINESS == 1 && amatch.BUSINESS == 2 && ap.Y_SUB != null)
                            {
                                isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.BUSINESS_SUB.Split(","));
                            }
                            if (ap.EDU == 1 && amatch.EDU == 2 && ap.Y_SUB != null)
                            {
                                isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.EDU_SUB.Split(","));
                            }
                            if (ap.NFOREIGNINFO == 1 && amatch.NFOREIGNINFO == 2 && ap.Y_SUB != null)
                            {
                                isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.NFOREIGNINFO_SUB.Split(","));
                            }
                            if (ap.FOREIGNINFO == 1 && amatch.FOREIGNINFO == 2 && ap.Y_SUB != null)
                            {
                                isContain = isContain && MatchSub(ap.Y_SUB.Split(","), amatch.FOREIGNINFO_SUB.Split(","));
                            }
                        }

                    }

                    if (!isContain)
                    {
                        resultMatchList.Remove(amatch);
                    }
                }

            }


            return resultMatchList;
        }

        private bool MatchSub(string[] apS, string[] SoS)
        {
           foreach(var s in apS)
            {
                if(SoS.Contains(s))
                    return true;                
            }
            return false;
        }

        private string Type2Id(string ANS_TYPE)
        {
            var id = "";
            var sp = ANS_TYPE.Split('_');
            if (sp[0] == "DT")
            {
                if (sp[1] == "RECORD")
                {
                    id = "DC001";
                } 
                else if (sp[1] == "SPECIMEN")
                {
                    id = "DC002";
                }
            }
            else if (sp[0] == "HE")
            {
                if (sp[1] == "RECORD")
                {
                    id = "DC003";
                }
                else if (sp[1] == "SPECIMEN")
                {
                    id = "DC004";
                }
            }
            else if (sp[0] == "AR")
            {
                if (sp[1] == "RECORD")
                {
                    id = "DC005";
                }
                else if (sp[1] == "SPECIMEN")
                {
                    id = "DC006";
                }
            }

            return id;
        }

        private string Info2Id(V_APPLY_INFO V_APPLY_INFO)
        {
            var id = "";
            if (V_APPLY_INFO.PUBLICINFO == 1)
            {
                id += "DC007,";
            }
            else if (V_APPLY_INFO.NPUBLICINFO == 1)
            {
                id += "DC008,";
            }

            if (V_APPLY_INFO.BUSINESS == 1)
            {
                id += "DC009,";
            }
            else if (V_APPLY_INFO.EDU == 1)
            {
                id += "DC010,";
            }

            if (V_APPLY_INFO.FOREIGNINFO == 1)
            {
                id += "DC011,";
            }
            else if (V_APPLY_INFO.NFOREIGNINFO == 1)
            {
                id += "DC012,";
            }

            return id.Substring(0,id.Length-1); ;
        }



        /// <summary>
        /// 取得已申請的資料
        /// </summary>
        /// <param name="queryPara"></param>
        /// <returns></returns>
        public List<VmApplyedOut> GetApplyedList(VmApplyedIn queryPara,string userid)
        {
            List<VmApplyedOut> result = new List<VmApplyedOut>();
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                result = sqlSugar.Queryable<APPLY_SOURCE_REL, V_APPLY_INFO, V_SUB_SOURCE_TYPE>((rel, Apply, Source) => new object[] {
                    JoinType.Left,rel.APM_ID==Apply.APM_ID,
                    JoinType.Left,rel.AC_ID==Source.AC_ID
                })
                 .Where((rel, Apply, Source) => Source.AC_USERID == userid)
                 .WhereIF(queryPara.applyDataSt != null && queryPara.applyDataEnd != null, (rel, Apply, Source) => rel.CREATED_DATETIME >= queryPara.applyDataSt && rel.CREATED_DATETIME <= queryPara.applyDataEnd)
                 .WhereIF(queryPara.applyDataSt == null && queryPara.applyDataEnd != null, (rel, Apply, Source) => rel.CREATED_DATETIME <= queryPara.applyDataEnd)
                 .WhereIF(queryPara.applyDataSt != null && queryPara.applyDataEnd == null, (rel, Apply, Source) => rel.CREATED_DATETIME >= queryPara.applyDataSt)
                 .WhereIF(!string.IsNullOrEmpty(queryPara.keyWord), (rel, Apply, Source) => Apply.APM_NO.Contains(queryPara.keyWord) || Apply.NAME.Contains(queryPara.keyWord) ||
                       ((Apply.PUBLICINFO == 1 ? "公共" : "非公共") + (Apply.BUSINESS == 1 ? "/商業" : "/非商業") + (Apply.FOREIGNINFO == 1 ? "/境外" : "/國內")).Contains(queryPara.keyWord))
                 .Select((rel, Apply, Source) => new VmApplyedOut
                 {
                    apmId = Apply.APM_ID,
                    amId = Source.AM_ID,
                    apmNo = Apply.APM_NO,
                    acId = Source.AC_ID,
                    planName = Apply.NAME,
                    planType = (Apply.PUBLICINFO == 1 ? "公共" : "非公共") + (Apply.BUSINESS == 1 ? "/商業" : "/非商業") + (Apply.FOREIGNINFO == 1 ? "/境外" : "/國內"),
                    applySource = Source.ANS_SOURCECN,
                    applyType = Source.ANS_TYPECN,
                    applyDate = rel.CREATED_DATETIME,
                    preference = (Source.AC_ANS == 3 ||
                        Apply.PUBLICINFO == 1 && Source.PUBLICINFO == 3 || Apply.NPUBLICINFO == 1 && Source.NPUBLICINFO == 3 ||
                        Apply.BUSINESS == 1 && Source.BUSINESS == 3 || Apply.EDU == 1 && Source.EDU == 3 ||
                        Apply.FOREIGNINFO == 1 && Source.FOREIGNINFO == 3 || Apply.NFOREIGNINFO == 1 && Source.NFOREIGNINFO == 3
                    ) ? "逐次決定" : "符合",// GetPreference(Source, Apply),
                    state = rel.STATUS,
                 }).ToList();
               

                  
               // result = result.WhereIF(!string.IsNullOrEmpty(queryPara.keyWord), rel => rel.apmNo.Contains(queryPara.keyWord) || rel.planName.Contains(queryPara.keyWord) || rel.planType.Contains(queryPara.keyWord))
                
               //  .ToList();
                return result.OrderBy(x=>x.state).ToList();
            }
        }

        private string GetPreference(V_SUB_SOURCE_TYPE source, V_APPLY_INFO apply)
        {
            if (source == null)
                return "";
            if (source.AC_ANS == 3 ||
                apply.PUBLICINFO == 1 && source.PUBLICINFO == 3 || apply.NPUBLICINFO == 1 && source.NPUBLICINFO == 3 ||
                apply.BUSINESS == 1 && source.BUSINESS == 3 || apply.EDU == 1 && source.EDU == 3 ||
                apply.FOREIGNINFO == 1 && source.FOREIGNINFO == 3 || apply.NFOREIGNINFO == 1 && source.NFOREIGNINFO == 3
                )
            {
                return "逐次決定";
            }
            else
                return "符合";
        }

        /// <summary>
        /// 取得 匹配後的 資料來源資料
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<VmApplyMatchInfoOut> GetApplyMatchList(VmApplyMatchIn obj)
        {
            List<VmApplyMatchInfoOut> result = new List<VmApplyMatchInfoOut>();
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                // 取得 apply 計畫設定
                var ap = sqlSugar.Queryable<V_APPLY_INFO>().Where(r => r.APM_ID == obj.apmId).First();

                // 比對條件，符合列表出來
                if (ap != null)
                {
                    var calSList = CalMappingSource(ap);
                    result = calSList
                        .WhereIF(!string.IsNullOrWhiteSpace(obj.keyWord), item => item.AM_NO.Contains(obj.keyWord))
                        .WhereIF(!string.IsNullOrWhiteSpace(obj.source), item => item.ANS_SOURCECN == obj.source)
                        .WhereIF(!string.IsNullOrWhiteSpace(obj.type), item => item.ANS_TYPECN == obj.type)
                       // .WhereIF(obj.sourceDataSt != null && obj.sourceDataEnd != null, r => r.CREATED_DATETIME >= obj.sourceDataSt && r.CREATED_DATETIME <= obj.sourceDataEnd)
                       // .WhereIF(obj.sourceDataSt == null && obj.sourceDataEnd != null, r => r.CREATED_DATETIME <= obj.sourceDataEnd)
                       // .WhereIF(obj.sourceDataSt != null && obj.sourceDataEnd == null, r => r.CREATED_DATETIME >= obj.sourceDataSt)
                        .Select(r => new VmApplyMatchInfoOut
                        {
                            apmId = ap.APM_ID,
                            acId = r.AC_ID,
                            number = r.AM_NO,
                            source = r.ANS_SOURCECN,
                            type = r.ANS_TYPECN,
                            preference = GetPreference(r, ap),
                            state = r.APPLY_STATUS,
                            ApmIsRead = r.APM_IS_READED
                        }).ToList();
                }

                return result.OrderByDescending(x=>x.state).ToList();
            }
        }

        /// <summary>
        /// 取得單一申請資料
        /// </summary>
        /// <param name="apmId"></param>
        /// <returns></returns>
        public VmApplyOut GetApplySingle(string apmId)
        {
            VmApplyOut result = new VmApplyOut();
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                // 取得 apply 計畫設定
                var ap = sqlSugar.Queryable<V_APPLY_INFO>().Where(r => r.APM_ID == apmId).First();

                //比對條件，符合的數量
                if (ap != null)
                {
                    var calSList = CalMappingSource(ap);

                    result.apmId = ap.APM_ID;
                    result.number = ap.APM_NO;
                    result.planName = ap.NAME;
                    result.planType1 = ap.PUBLICINFO == 1 ? "公共" : ap.NPUBLICINFO == 1 ? "非公共" : "";
                    result.planType2 = ap.BUSINESS == 1 ? "商業" : ap.EDU == 1 ? "非商業" : "";
                    result.planType3 = ap.FOREIGNINFO == 1 ? "境外" : ap.NFOREIGNINFO == 1 ? "境內" : "";
                    result.match = calSList.Count;
                }
                return result;
            }
        }

        #region 新增


        /// <summary>
        /// 新增 申請計畫
        /// </summary>
        /// <param name="apInfo"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool InsertApplyInfo(VmApplyDetailOutIn apInfo, string userid)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                string apmId = Guid.NewGuid().ToString();

                APPLY_MAIN main = new APPLY_MAIN();
                main.APM_ID = apmId;
                main.NAME = apInfo.Main.NAME;
                main.ENGLISHNAME = apInfo.Main.ENGLISHNAME;
                main.RESEARCHINSTITUTE = apInfo.Main.RESEARCHINSTITUTE;
                main.REQUESTER = apInfo.Main.REQUESTER;
                main.FUNDING = apInfo.Main.FUNDING;
                main.HOSTNAME = apInfo.Main.HOSTNAME;
                main.HOSTJOBTITLE = apInfo.Main.HOSTJOBTITLE;
                main.VICEHOSTNAME = apInfo.Main.VICEHOSTNAME;
                main.VICEHOSTJOBTITLE = apInfo.Main.VICEHOSTJOBTITLE;
                main.CONTACTPERSON = apInfo.Main.CONTACTPERSON;
                main.CONTACTNUMBER = apInfo.Main.CONTACTNUMBER;
                main.PURPOSE = apInfo.Main.PURPOSE;
                main.TPLACE = apInfo.Main.TPLACE;
                main.USERID = userid;
                main.CREATED_DATETIME = DateTime.Now;
                main.UPDATE_DATETIME = DateTime.Now;
                main.UPDATE_USERID = userid;
                main.IS_ENABLED = true;
                var mainResult = sqlSugar.Insertable(main).ExecuteCommand();

                if (mainResult > 0)
                {
                    APPLY_Y apY = new APPLY_Y();
                    apY.APM_ID = apmId;
                    apY.PUBLICINFO = apInfo.ApY.PUBLICINFO;
                    apY.NPUBLICINFO = apInfo.ApY.NPUBLICINFO;
                    apY.BUSINESS = apInfo.ApY.BUSINESS;
                    apY.EDU = apInfo.ApY.EDU;
                    apY.FOREIGNINFO = apInfo.ApY.FOREIGNINFO;
                    apY.NFOREIGNINFO = apInfo.ApY.NFOREIGNINFO;
                    var apYResult = sqlSugar.Insertable(apY).ExecuteCommand();

                    APPLY_X apX = new APPLY_X();
                    apX.APM_ID = apmId;
                    apX.DT_RECORD = apInfo.ApX.DT_RECORD;
                    apX.DT_SPECIMEN = apInfo.ApX.DT_SPECIMEN;
                    apX.HE_RECORD = apInfo.ApX.HE_RECORD;
                    apX.HE_SPECIMEN = apInfo.ApX.HE_SPECIMEN;
                    apX.AR_RECORD = apInfo.ApX.AR_RECORD;
                    apX.AR_SPECIMEN = apInfo.ApX.AR_SPECIMEN;
                    var apXResult = sqlSugar.Insertable(apX).ExecuteCommand();

                    APPLY_Y_SUB apYS = new APPLY_Y_SUB();
                    apYS.APM_ID = apmId;
                    apYS.DIAGNOSIS = apInfo.ApYS.DIAGNOSIS;
                    apYS.PREVENTION = apInfo.ApYS.PREVENTION;
                    apYS.CARE = apInfo.ApYS.CARE;
                    apYS.SAFETY = apInfo.ApYS.SAFETY;
                    apYS.TRUCTURE = apInfo.ApYS.TRUCTURE;
                    apYS.POLICY = apInfo.ApYS.POLICY;
                    apYS.RESEARCH = apInfo.ApYS.RESEARCH;
                    var apYSResult = sqlSugar.Insertable(apYS).ExecuteCommand();

                    APPLY_X_SUB apXS = new APPLY_X_SUB();
                    apXS.APM_ID = apmId;
                    apXS.CANCER = apInfo.ApXS.CANCER;
                    apXS.CARDIO = apInfo.ApXS.CARDIO;
                    apXS.DIABETES = apInfo.ApXS.DIABETES;
                    apXS.NEURO = apInfo.ApXS.NEURO;
                    apXS.MENTAL = apInfo.ApXS.MENTAL;
                    apXS.BIRTH = apInfo.ApXS.BIRTH;
                    apXS.INFECTIOUS = apInfo.ApXS.INFECTIOUS;
                    apXS.EAR = apInfo.ApXS.EAR;
                    apXS.PEDIATRICS = apInfo.ApXS.PEDIATRICS;
                    apXS.ELDERLY = apInfo.ApXS.ELDERLY;
                    var apXSResult = sqlSugar.Insertable(apXS).ExecuteCommand();

                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// 新增 資料來源申請資料
        /// </summary>
        /// <param name="addInfo"></param>
        /// /// <param name="userid"></param>
        /// <returns></returns>
        public bool AddApplyState(VmApplyedStateIn addInfo, string userid)
        {

            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                // 新增
                var addAP = new APPLY_SOURCE_REL();
                addAP.APM_ID = addInfo.apmId;
                addAP.AC_ID = addInfo.acId;
                addAP.STATUS = 1;
                addAP.CREATED_DATETIME = DateTime.Now;
                addAP.UPDATE_DATETIME = DateTime.Now;
                addAP.UPDATE_USERID = userid;
                addAP.IS_ENABLED = true;

                var addresult = sqlSugar.Insertable(addAP).ExecuteCommand();
                if (addresult > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 新增 資料來源申請資料 -批量
        /// </summary>
        /// <param name="addInfo"></param>
        /// /// <param name="userid"></param>
        /// <returns></returns>
        public bool AddApplyStateBatch(List<applyedInfo> addInfo, string userid)
        {

            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                List<APPLY_SOURCE_REL> APPLY_SOURCE_RELs = new List<APPLY_SOURCE_REL>();

                foreach (var item in addInfo)
                {
                    // 新增
                    var addAP = new APPLY_SOURCE_REL();
                    addAP.APM_ID = item.apmId;
                    addAP.AC_ID = item.acId;
                    addAP.STATUS = 1;
                    addAP.CREATED_DATETIME = DateTime.Now;
                    addAP.UPDATE_DATETIME = DateTime.Now;
                    addAP.UPDATE_USERID = userid;
                    addAP.IS_ENABLED = true;

                    APPLY_SOURCE_RELs.Add(addAP);
                }
               

                var addresult = sqlSugar.Insertable(APPLY_SOURCE_RELs).ExecuteCommand();
                if (addresult > 0)
                    return true;
                else
                    return false;
            }
        }


        #endregion

        #region 更新

        /// <summary>
        /// 更新 申請資料
        /// </summary>
        /// <param name="apInfo"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool UpdateApplyInfo(VmApplyDetailOutIn apInfo, string userid)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {

                apInfo.Main.UPDATE_DATETIME = DateTime.Now;
                apInfo.Main.UPDATE_USERID = userid;

                var mainResult = sqlSugar.Updateable(apInfo.Main).ExecuteCommand();
                if (mainResult > 0)
                {
                    var apYResult = sqlSugar.Updateable(apInfo.ApY).ExecuteCommand();

                    var apXResult = sqlSugar.Updateable(apInfo.ApX).ExecuteCommand();

                    var apYSResult = sqlSugar.Updateable(apInfo.ApYS).ExecuteCommand();

                    var apXSResult = sqlSugar.Updateable(apInfo.ApXS).ExecuteCommand();

                    return true;
                }
                else
                    return false;
            }
        }


        /// <summary>
        ///  申請來源資料更新
        /// </summary>
        /// <param name="upInfo"></param>
        /// /// <param name="userid"></param>
        /// <returns></returns>
        public bool UpdateApplyedState(VmApplyedStateIn upInfo, string userid)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var addresult = sqlSugar.Updateable<APPLY_SOURCE_REL>()
                    .SetColumns(it => new APPLY_SOURCE_REL()
                    {
                        STATUS = upInfo.state,
                        UPDATE_DATETIME = DateTime.Now,
                        UPDATE_USERID = userid,
                    }).Where(r => r.APM_ID == upInfo.apmId && r.AC_ID == upInfo.acId).ExecuteCommand();

                if (addresult > 0)
                    return true;
                else
                    return false;


            }
        }

        #endregion
    }
}
