using Google.Protobuf.WellKnownTypes;
using Library.Extension;
using Library.Functions;
using Library.Model.Apply;
using Library.Model.BD;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using SqlSugar;
using System.Collections.Generic;

namespace Library.Model.Notify;

public class NotifyHelper
{
     public List<VmNotifyListOut> GetReviewNotifyList(string Userid)
     {
        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
        {
            var NotifyList = new List<VmNotifyListOut>();
            
            // Get apply Info
            var apNotifyList = sqlSugar.Queryable<APPLY_SOURCE_REL, ANS_CONTENT, APPLY_MAIN, V_APPLY_INFO, V_SUB_SOURCE_TYPE>((rel, ac, am, Apply, Source) => new object[] { 
                JoinType.Left,rel.AC_ID==ac.AC_ID, 
                JoinType.Left,rel.APM_ID==am.APM_ID,
                JoinType.Left,rel.APM_ID==Apply.APM_ID,
                JoinType.Left,rel.AC_ID==Source.AC_ID
            })
                .Where((rel, ac, am, Apply, Source) => rel.STATUS == 1 && ac.AC_USERID==Userid)
                .Select((rel, ac, am, Apply, Source) => new VmNotifyListOut()
                {
                    DateInfo = rel.CREATED_DATETIME.ToString("yyyy/MM/dd"),
                    TYPE = rel.STATUS==1?1:2,
                    ApmID = rel.APM_ID,
                    AcID = rel.AC_ID,
                    isRead = rel.AC_IS_READED,
                    PlanName = am.NAME,
                    EnglishName = am.ENGLISHNAME,
                    ResearchinsTitute = am.RESEARCHINSTITUTE,
                    Requester = am.REQUESTER,
                    Funding = am.FUNDING,
                    HostName = am.HOSTNAME,
                    HostJobTtitle = am.HOSTJOBTITLE,
                    ViceHostName = am.VICEHOSTNAME,
                    ViceHostJobTtitle = am.VICEHOSTJOBTITLE,
                    ContactPerson = am.CONTACTPERSON,
                    ContactNumber = am.CONTACTNUMBER,
                    Purpose = am.PURPOSE,
                    apmNo = Apply.APM_NO,
                    planType = (Apply.PUBLICINFO == 1 ? "公共" : "非公共") + (Apply.BUSINESS == 1 ? "/商業" : "/非商業") + (Apply.FOREIGNINFO == 1 ? "/國內" : "/國外"),
                    applySource = Source.ANS_SOURCECN,
                    applyType = Source.ANS_TYPECN,
                    applyDate = rel.CREATED_DATETIME,
                    preference = (Source.AC_ANS == 3 ||
                        Apply.PUBLICINFO == 1 && Source.PUBLICINFO == 3 || Apply.NPUBLICINFO == 1 && Source.NPUBLICINFO == 3 ||
                        Apply.BUSINESS == 1 && Source.BUSINESS == 3 || Apply.EDU == 1 && Source.EDU == 3 ||
                        Apply.FOREIGNINFO == 1 && Source.FOREIGNINFO == 3 || Apply.NFOREIGNINFO == 1 && Source.NFOREIGNINFO == 3
                    ) ? "逐次決定" : "符合",// GetPreference(Source, Apply),
                })
                .ToList();
            if(apNotifyList.Count>0)
                NotifyList.AddRange(apNotifyList);

            return NotifyList;
        }
     }
    
     public List<VmNotifyListOut2> GetBackNotifyList(string Userid)
     {
        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
        {
            ApplyHelper ah = new ApplyHelper();
            VmApplyMatchIn vm = new VmApplyMatchIn();

            //抓取 請求者的所有申請 
            var NotifyList = sqlSugar.Queryable<V_APPLY_INFO>()
                    .Where(main => main.USERID == Userid)
                    .Select(it => new VmNotifyListOut2()
                    {
                        ApmID = it.APM_ID,
                        PlanName = it.NAME
                    })
                    .Mapper(x =>
                    {
                        vm.apmId = x.ApmID;
                        var ApplyMatchList = ah.GetApplyMatchList(vm);
                        x.MatchList = ApplyMatchList
                            .Where(item => (item.state == 2 || item.state == 3) && item.ApmIsRead == false)
                            .ToList();
                    })
            .ToList();
            return NotifyList;
        }
     }

    public bool NotifyListIsRead(string Userid, string AC_ID)
    {
        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
        {
            ReturnModel returnModel = new ReturnModel();

            var model = sqlSugar.Queryable<APPLY_SOURCE_REL, ANS_CONTENT>((rel, ac) => new object[] { JoinType.Right,rel.AC_ID==ac.AC_ID })
                .Where((rel, ac) => rel.STATUS == 1 && rel.AC_IS_READED == false && ac.AC_USERID == Userid && ac.AC_ID == AC_ID)
                .First();

            bool result = false;
            if (model != null)
            {
                var updateResult = sqlSugar.Updateable<APPLY_SOURCE_REL>()
                    .SetColumns(it => new APPLY_SOURCE_REL()
                    {
                        AC_IS_READED = true
                    })
                    .Where(rel => rel.APM_ID == model.APM_ID && rel.AC_ID == model.AC_ID)
                    .ExecuteCommand();
                    
                if(updateResult > 0)
                    result = true;
                else
                    result = false;
            }
            return result;
        }
    }

    public bool NotifyListAPMIsRead(string Userid, ACIDList[] List)
    {
        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
        {
            bool result = false;
            foreach (var item in List)
            {
                var updateResult = sqlSugar.Updateable<APPLY_SOURCE_REL>()
                    .SetColumns(it => new APPLY_SOURCE_REL()
                    {
                        APM_IS_READED = true
                    })
                    .Where(rel => rel.APM_ID == item.apmId && rel.AC_ID == item.acId)
                    .ExecuteCommand();

                if (updateResult > 0)
                    result = true;
                else
                    result = false;
            }
            return result;
        }
    }
}