using Library.Functions;
using Library.Model.INPUT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    public class ANS_CONTENT
    {
        /// <summary>
        /// 填寫ID
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// ANS_MAIN 主鍵
        /// </summary>
        public string AM_ID { get; set; }


        /// <summary>
        /// 填寫人ID
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 填寫內容ID
        /// </summary>
        public string AC_DCID { get; set; }

        /// <summary>
        /// 填寫答案
        /// </summary>
        public int AC_ANS { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public string? AC_OTHER { get; set; }
    
}

    public class ANS_CONTENT_EXTEND
    {
        public bool InsertContAns(DS_CONTENT_ADD Object, string userid)
        {         
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                List<OUT_DS_CONTENT> contAnsList = Object.ContAnsList;
                List<ANS_CONTENT_OTHER_LIST> ContAnsOtherList = Object.ContAnsOtherList;

                List<ANS_CONTENT> acList = new List<ANS_CONTENT>();
                List<ANS_SUB_CONTENT> subList = new List<ANS_SUB_CONTENT>();
                var acMain = new ANS_MAIN();
                acMain.AM_ID = "AM"+Guid.NewGuid().ToString();
                acMain.UPDATE_USERID=userid;
                acMain.CREATED_DATETIME = DateTime.Now;
                acMain.UPDATE_DATETIME = DateTime.Now;
                acMain.IS_ENABLED = true;

                foreach (var ca in contAnsList)
                {
                    ANS_CONTENT ac = new ANS_CONTENT();
                    ac.AC_ID = "AC"+Guid.NewGuid().ToString();
                    ac.AM_ID=acMain.AM_ID;
                    ac.AC_USERID = userid;
                    ac.AC_DCID = ca.DC_ID;
                    ac.AC_ANS = ca.ANS;
                    ac.AC_OTHER=ca.ACOTHER;
                    acList.Add(ac);

                    if (ca.ANS_SUB.Count > 0)
                    {
                        foreach (var sub in ca.ANS_SUB)
                        {
                            if (sub.ANS)
                            {
                                ANS_SUB_CONTENT sc = new ANS_SUB_CONTENT();
                                sc.AS_ID = Guid.NewGuid().ToString();
                                sc.AS_ACID = ac.AC_ID;
                                sc.AS_DSID = sub.DS_ID;
                                subList.Add(sc);                              
                            }
                        }
                    }                   
                }

               var amResult =sqlSugar.Insertable(acMain).ExecuteCommand();
                if (amResult == 0)
                    return false;

               var acResult= sqlSugar.Insertable(acList.ToArray()).ExecuteCommand();
                if (acResult > 0) {
                    var subResult = sqlSugar.Insertable(subList.ToArray()).ExecuteCommand();
                    if (subResult > 0)
                    {
                        return true;
                    }
                    else { 
                        sqlSugar.Deleteable<ANS_CONTENT>().Where(x=>x.AC_USERID==userid).ExecuteCommand();
                        sqlSugar.Deleteable<ANS_MAIN>().Where(x => x.UPDATE_USERID == userid).ExecuteCommand();
                        return false; 
                    }
                }
                else
                {
                    sqlSugar.Deleteable<ANS_MAIN>().Where(x => x.UPDATE_USERID == userid).ExecuteCommand();
                    return false;
                }                               
            }            
        }

        public bool UpdateContAns(DS_CONTENT_ADD Object, string userid)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                List<OUT_DS_CONTENT> contAnsList = Object.ContAnsList;
                List<ANS_CONTENT_OTHER_LIST> contAnsOtherList = Object.ContAnsOtherList;

                List<ANS_CONTENT> acList = new List<ANS_CONTENT>();
                List<ANS_SUB_CONTENT> subList = new List<ANS_SUB_CONTENT>();

                List<ANS_CONTENT_OTHER> aoList = new List<ANS_CONTENT_OTHER>();
                List<ANS_CONTENT_OTHER_SUB> aosList = new List<ANS_CONTENT_OTHER_SUB>();
                List<ANS_SUB_CONTENT> subList2 = new List<ANS_SUB_CONTENT>();

                bool status = true;
                // 取amid
                var acid = contAnsList.Count > 0 ? contAnsList[0].ACID : "";
                var acInfo1 = sqlSugar.Queryable<ANS_CONTENT>().Where(r => r.AC_ID == acid).First();
                string amid = "";
                if (acInfo1 != null)
                    amid = acInfo1.AM_ID;

                foreach (var ca in contAnsList)
                {
                    ANS_CONTENT ac = new ANS_CONTENT();
                    ac.AC_ID = ca.ACID;
                    ac.AM_ID = amid;
                    ac.AC_USERID = userid;
                    ac.AC_DCID = ca.DC_ID;
                    ac.AC_ANS = ca.ANS;
                    ac.AC_OTHER = ca.ACOTHER;
                    acList.Add(ac);

                    if (ca.ANS_SUB.Count > 0)
                    {
                        foreach (var sub in ca.ANS_SUB)
                        {
                            if (sub.ANS)
                            {
                                ANS_SUB_CONTENT sc = new ANS_SUB_CONTENT();
                                sc.AS_ID = "AS"+Guid.NewGuid().ToString();
                                sc.AS_ACID = ac.AC_ID;
                                sc.AS_DSID = sub.DS_ID;
                                subList.Add(sc);
                            }
                        }
                    }
                }
                var acResult = sqlSugar.Updateable(acList).ExecuteCommand();
                
                if (acResult>0)
                {
                    try
                    {
                        sqlSugar.Deleteable<ANS_SUB_CONTENT>().Where(x => contAnsList.Select(s => s.ACID).ToList().Contains(x.AS_ACID)).ExecuteCommand();
                        sqlSugar.Insertable(subList).ExecuteCommand();
                    }
                    catch(Exception ex)
                    {
                        status = false;
                    }                             
                }


                var oldAoList = sqlSugar.Queryable<ANS_CONTENT_OTHER>()
                                    .Where(x => x.AM_ID == amid)
                                    .ToList();
                var oldAosList = sqlSugar.Queryable<ANS_CONTENT_OTHER_SUB>()
                                    .Where(x => oldAoList.Select(s => s.AO_ID).ToList().Contains(x.AOS_AO_ID))
                                    .ToList();
                var oldSubList2 = sqlSugar.Queryable<ANS_SUB_CONTENT>()
                                    .Where(x => oldAosList.Select(s => s.AOS_ID).ToList().Contains(x.AS_ACID))
                                    .ToList();


                foreach (var cao in contAnsOtherList)
                {
                    ANS_CONTENT_OTHER ao = new ANS_CONTENT_OTHER();
                    ao.AO_ID = "AO" + Guid.NewGuid().ToString();
                    ao.AM_ID = amid;
                    ao.AO_USERID = userid;
                    ao.AO_NAME = cao.name;
                    aoList.Add(ao);

                    if (cao.list.Count > 0)
                    {
                        foreach (var item in cao.list)
                        {
                            ANS_CONTENT_OTHER_SUB aos = new ANS_CONTENT_OTHER_SUB();
                            aos.AOS_ID = "AOS" + Guid.NewGuid().ToString();
                            aos.AOS_AO_ID = ao.AO_ID;
                            aos.AOS_DCID = item.dcId;
                            aos.AOS_ANS = item.ANS;
                            aosList.Add(aos);

                            foreach (var sub in item.sub)
                            {
                                ANS_SUB_CONTENT sc = new ANS_SUB_CONTENT();
                                sc.AS_ID = "AS" + Guid.NewGuid().ToString();
                                sc.AS_ACID = aos.AOS_ID;
                                sc.AS_DSID = sub;
                                subList2.Add(sc);
                            }
                        }
                    }
                }

                var aoDels = sqlSugar.Deleteable(oldAoList).ExecuteCommand();
                var acOtherResult = sqlSugar.Insertable(aoList).ExecuteCommand();

                if (acOtherResult > 0)
                {
                    try
                    {
                        sqlSugar.Deleteable(oldAosList).ExecuteCommand();
                        sqlSugar.Insertable(aosList).ExecuteCommand();

                        sqlSugar.Deleteable(oldSubList2).ExecuteCommand();
                        sqlSugar.Insertable(subList2).ExecuteCommand();
                    }
                    catch (Exception ex)
                    {
                        status = false;
                    }
                }



                 return status;
               
             
            }
        }
    }
}

   
