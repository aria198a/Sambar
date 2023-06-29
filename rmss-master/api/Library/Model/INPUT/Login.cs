using Library.Functions;
using Library.Model.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.INPUT
{
    public class Login : BaseModel
    {
        /// <summary>
        /// 類別 0-當事人 1-利用者
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string DecryptionPass { get; set; }
        /// <summary>
        /// 圖形碼
        /// </summary>
        public string CodePass { get; set; }
        /// <summary>
        /// 圖形碼密文
        /// </summary>
        public string DecryptionCodePass { get; set; }
        /// <summary>
        /// 驗證帳號
        /// </summary>
        /// <returns></returns>
        public string CheckPass(string IP)
        {
            string result = "";

            ACCOUNT_EXTEND account_extend = new ACCOUNT_EXTEND();
            ACCOUNT account = account_extend.Get(new ACCOUNT() { AC_USERID = Id, AC_STATUS = 1 });
            if (account != null)
            {
                string msg;
                int status;

                if (account.AC_STATUS == 1)
                {
                    string pass = new AES().Encryption(DecryptionPass);
                    string passasd = new AES().Decryption(account.AC_PWD);
                    if (account.AC_PWD == pass)
                    {
                        status = 1;
                        msg = "";
                        result = account.AC_ID + "§" + account.AC_PWD;
                    }
                    else
                    {
                        status = 2;
                        msg = "密碼錯誤";
                        result = "密碼錯誤";
                    }
                }
                else
                {
                    status = 2;
                    msg = "帳號未開通";
                    result = "帳號未開通";
                }

                ACCOUNT_LOGIN_EXTEND log_extend = new ACCOUNT_LOGIN_EXTEND();
                log_extend.AL_ACID = account.AC_ID;
                log_extend.AL_IP = IP;
                log_extend.AL_STATUS = status;
                log_extend.AL_LOGINTYPE = account.AC_USERTYPE;
                log_extend.AL_MSG = msg;
                log_extend.AL_AGNAME = FixFunc.FixCrossSiteScripting(account.AC_GROUP_ID);
                log_extend.AL_MDATE = DateTime.Now;
                log_extend.Insert();

                return result;
            }
            else
            {
                return result;
            }
        }
        /// <summary>
        /// 驗證圖形碼
        /// </summary>
        /// <returns></returns>
        public bool CheckCodePass()
        {
            if (string.IsNullOrEmpty(CodePass) || string.IsNullOrEmpty(DecryptionCodePass))
            {
                return false;
            }

            string pass = new AES().Decryption(DecryptionCodePass);

            return (pass == CodePass) ? true : false;
        }
    }
}
