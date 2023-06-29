using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Functions
{
    public class WebRequestFunc
    {
        /// <summary>
        /// POST/GET 模組
        /// </summary>
        /// <param name="method">POST/GET</param>
        /// <param name="url">網址</param>
        /// <param name="referer"></param>
        /// <param name="postData">POST數據</param>
        /// <param name="accountInfo">帳號資料</param>
        /// <returns></returns>
        public static string SetLoginHttpWebRequest(string method, string url, string referer, string postData, AccountInfo accountInfo)
        {
            //協定
            HttpWebRequest request;
            request = WebRequest.CreateHttp(url);
            request.Method = method;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.75 Safari/537.36";
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.AllowAutoRedirect = false;
            request.KeepAlive = true;
            request.Referer = referer;
            request.Timeout = 10000;
            request.ReadWriteTimeout = 10000;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(accountInfo.CookieCollection);

            //傳送資料
            if (postData != null)
            {
                byte[] postByte = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = postByte.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postByte, 0, postByte.Length);
                stream.Close();
            }

            //接收回應
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string html = string.Empty;

            if (response != null)
            {
                StreamReader reader;

                if (string.Equals("gzip", response.ContentEncoding, StringComparison.CurrentCultureIgnoreCase))
                {
                    reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.UTF8);
                }
                else
                {
                    reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                }

                html = reader.ReadToEnd();
                reader.Close();

                foreach (Cookie cookie in response.Cookies)
                {
                    accountInfo.CookieCollection.Add(cookie);
                }

                response.Close();
            }

           
            request.Abort();

            return html;
        }

    }

    /// <summary>
    /// 帳號型態
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 跳轉網址
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 登入狀態
        /// </summary>
        public bool LoginedStatus { get; set; }

        /// <summary>
        /// 登入金鑰
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 存放來源Cookie
        /// </summary>
        public CookieCollection CookieCollection { get; set; }

        /// <summary>
        /// 登入失敗
        /// </summary>
        public bool IsFailure { get; set; }

        /// <summary>
        /// 連續服務器錯誤計數
        /// </summary>
        public int WebExCount { get; set; }

        /// <summary>
        /// 建構子 設定帳密
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        public AccountInfo()
        {
            CookieCollection = new CookieCollection();
        }
    }
}
