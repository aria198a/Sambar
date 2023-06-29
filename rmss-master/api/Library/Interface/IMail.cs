using Library.Model;

namespace Library.Interface
{
    interface IMail
    {
        ///// <summary>
        ///// 傳送Mail
        ///// </summary>
        ///// <param name="toMail">收件人mail</param>
        ///// <param name="toName">收件人</param>
        ///// <param name="cc">附件mail</param>
        ///// <param name="subject">標題</param>
        ///// <param name="body">內容</param>
        ///// <param name="attachedFile">檔案</param>
        ///// <returns></returns>
        //MessageEnum SendMail(string toMail, string toName, string cc, string subject, string body, string attachedFile);


        /// <summary>
        /// 傳送Mail
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
        MessageEnum SendMail(string host, string fromMail, string fromName, string toMail, string toName, string cc, string subject, string body, string attachedFile);

    }
}
