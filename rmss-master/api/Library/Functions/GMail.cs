using Library.Interface;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Library.Functions
{
    public class GMail:IMail
    {
        ///// <summary>
        ///// [zaomtdpwmajyupyz]
        ///// </summary>
        //private string GmailPwd { get => ConfigurationManager.ConnectionStrings["GAccount"].ConnectionString; }
        ///// <summary>
        ///// [genzz415305@gmail.com]
        ///// </summary>
        //private string Gmail { get => ConfigurationManager.ConnectionStrings["GPwd"].ConnectionString; }

        /// <summary>
        /// 傳送GMail
        /// </summary>
        /// <param name="host">主機IP</param>
        /// <param name="fromMail">寄件人mail</param>
        /// <param name="fromName">寄件人</param>
        /// <param name="toMail">收件人mail</param>
        /// <param name="toName">收件人</param>
        /// <param name="cc">附件mail</param>
        /// <param name="subject">標題</param>
        /// <param name="body">內容</param>
        /// <param name="attachedFile">檔案</param>
        /// <returns></returns>
        public MessageEnum SendMail(string host, string fromMail, string fromName, string toMail, string toName, string cc, string subject, string body, string attachedFile)
        {
            try
            {
                MailAddress mailFrom = new MailAddress(fromMail, fromName);
                MailAddress mailTo = new MailAddress(toMail, toName);
                MailMessage myMail = new MailMessage(mailFrom, mailTo);

                if (!string.IsNullOrEmpty(cc))
                {
                    string[] aryMailCC = cc.Split(';');

                    for (int i = 0; i < aryMailCC.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(aryMailCC[i].Trim()))
                        {
                            myMail.Bcc.Add(aryMailCC[i]);
                        }
                    }
                }

                myMail.BodyEncoding = Encoding.GetEncoding("UTF-8");
                myMail.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                myMail.IsBodyHtml = true;
                myMail.Subject = subject;
                myMail.Body = body;

                if (!string.IsNullOrEmpty(attachedFile))
                {
                    Attachment mailAttachment = new Attachment(attachedFile);
                    myMail.Attachments.Add(mailAttachment);
                }

                SmtpClient mySmtp = new SmtpClient("smtp.gmail.com", 587);
                //設定你的帳號密碼
                //mySmtp.Credentials = new System.Net.NetworkCredential(Gmail, GmailPwd);
                mySmtp.Credentials = new System.Net.NetworkCredential("genzz415305@gmail.com", "zaomtdpwmajyupyz");
                //Gmial 的 smtp 使用 SSL
                mySmtp.EnableSsl = true;
                mySmtp.Send(myMail);
            }
            catch (Exception ex)
            {
                new NLog().LogInformation(ex);
                return MessageEnum.Fail;
            }

            return MessageEnum.Success;
        }

    }
}
