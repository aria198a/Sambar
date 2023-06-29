using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model.BD;
using Library.Functions;

namespace Library.Model.General
{
    public class LogicFunc
    {
        /// <summary>
        /// 驗證Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsVerifyDate(string token)
        {
            DateTime time = new DateTime();

            if (!string.IsNullOrEmpty(token) &&
                DateTime.TryParse(new AES().Decryption(token), out time) &&
                time.AddMinutes(5) > DateTime.Now)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 驗證Token(兩小時)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsVerifyDateToo(string token)
        {
            DateTime time = new DateTime();
            var tokens = GetDecryptionId(token);

            if (!string.IsNullOrEmpty(token) &&
                DateTime.TryParse(tokens[1], out time) &&
                time.AddHours(2) > DateTime.Now)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 取得GAID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetAgId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string[] accountStr = GetDecryptionId(id);

                if (accountStr.Length != 2)
                {
                    return "";
                }

                ACCOUNT_EXTEND ac_extend = new ACCOUNT_EXTEND();
                ac_extend.AC_ID = accountStr[0];
                ac_extend.AC_PWD = accountStr[1];
                ACCOUNT ac = ac_extend.SelectByIdAndPwd();
                return ac.AC_GROUP_ID;


            }

            return "";
        }
        /// <summary>
        /// 驗證Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="PJ_ID"></param>
        /// <returns></returns>
        public static bool IsVerifyId(string id, string PJ_ID)
        {
            //無驗證模式模式
            //return true;

            if (!string.IsNullOrEmpty(id))
            {
                string[] accountStr = GetDecryptionId(id);

                if (accountStr.Length != 2)
                {
                    return false;
                }

                ACCOUNT_EXTEND ac_extend = new ACCOUNT_EXTEND();
                ac_extend.AC_ID = accountStr[0];
                ac_extend.AC_PWD = accountStr[1];
                ACCOUNT ac = ac_extend.SelectByIdAndPwd();

                if (ac != null && !string.IsNullOrEmpty(ac.AC_GROUP_ID))
                {
                    try
                    {
                        //Search Group
                        ACCOUNT_GROUP_EXTEND ag_extend = new ACCOUNT_GROUP_EXTEND();
                        ag_extend.AG_ID = ac.AC_GROUP_ID;
                        ACCOUNT_GROUP ag = ag_extend.SelectByAGID2();

                        //Search PageName
                        List<PROJECT> out_pj = new PROJECT_EXTEND().SelectList3(ag);

                        //return out_pj.Select(x => x.PJ_ID == PJ_ID).Count() > 0 ? true : false;
                        return out_pj.Where(x => x.PJ_ID == PJ_ID).Count() > 0 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// 驗證Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsVerifyId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string[] accountStr = GetDecryptionId(id);

                if (accountStr.Length != 2)
                {
                    return false;
                }

                ACCOUNT_EXTEND ac_extend = new ACCOUNT_EXTEND();
                ac_extend.AC_ID = accountStr[0];
                ac_extend.AC_PWD = accountStr[1];
                ACCOUNT ac = ac_extend.SelectByIdAndPwd();

                if (ac != null && !string.IsNullOrEmpty(ac.AC_GROUP_ID))
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 驗證Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ACCOUNT IsVerifyIdReturnAccount(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string[] accountStr = GetDecryptionId(id);

                if (accountStr.Length != 2)
                {
                    return null;
                }

                ACCOUNT_EXTEND ac_extend = new ACCOUNT_EXTEND();
                ac_extend.AC_ID = accountStr[0];
                ac_extend.AC_PWD = accountStr[1];
                ACCOUNT ac = ac_extend.SelectByIdAndPwd();

                if (ac != null && !string.IsNullOrEmpty(ac.AC_GROUP_ID))
                {
                    return ac;
                }
            }

            return null;
        }
        /// <summary>
        /// 單簽用取帳號資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ACCOUNT MyACCOUNTBySingleSign(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string[] accountStr = GetDecryptionId(id);

                if (accountStr.Length != 2)
                {
                    return null;
                }

                ACCOUNT_EXTEND ac_extend = new ACCOUNT_EXTEND();
                ac_extend.AC_ID = accountStr[0];
                ac_extend.AC_PWD = accountStr[1];
                ACCOUNT ac = ac_extend.SelectByIdAndPwd();


                if (ac != null && !string.IsNullOrEmpty(ac.AC_GROUP_ID))
                {
                    return ac;
                }
            }

            return null;
        }
        /// <summary>
        /// mail get id
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static ACCOUNT IsVerifyMailReturnAccount(string mail)
        {
            if (!string.IsNullOrEmpty(mail))
            {


                ACCOUNT_EXTEND ac_extend = new ACCOUNT_EXTEND();
                ac_extend.AC_USERID = mail;
                ACCOUNT ac = ac_extend.SelectByMail();

                if (ac != null && !string.IsNullOrEmpty(ac.AC_GROUP_ID))
                {
                    return ac;
                }
            }

            return null;
        }
        /// <summary>
        /// 解密字串
        /// </summary>
        /// <param name="id">加密TokenID</param>
        /// <returns></returns>
        public static string[] GetDecryptionId(string id)
        {
            string DecryptionID = new AES().Decryption(id);
            return DecryptionID.Split('§');
        }
        /// <summary>
        /// 取得IP位置
        /// </summary>
        /// <returns></returns>
        public static string GetIP4Address()
        {
            string IP4Address = string.Empty;
            //var HostName = Dns.GetHostName();
            //foreach (IPAddress IPA in Dns.GetHostAddresses(HostName))
            //{
            //    if (IPA.AddressFamily.ToString() == "InterNetwork")
            //    {
            //        IP4Address += IPA.ToString();
            //        break;
            //    }
            //}

            return IP4Address;
        }
    }
}
