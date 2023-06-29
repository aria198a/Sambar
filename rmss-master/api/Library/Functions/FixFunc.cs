//using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Library.Functions
{
    public class FixFunc
    {

        /// <summary>
        /// 弱點掃描項目[XSS]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FixCrossSiteScripting(string content)
        {
            return HttpUtility.HtmlEncode(content);
        }

        /// <summary>
        /// 弱點掃描項目[XSS]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FixCrossSiteScripting(object content)
        {
            if (content == null || content == DBNull.Value)
                return "";
            else
                return FixCrossSiteScripting(content.ToString());
        }

        /// <summary>
        /// 弱點掃描項目[Path Manipulation]
        /// </summary>
        /// <param name="path">路徑</param>
        /// <param name="name">檔案名稱</param>
        public static void FixPathManipulationForDelete(string path, string name)
        {
            try
            {
                path = CleanPath(path);

                foreach (string fullFileName in Directory.EnumerateFiles(path))
                {
                    string fileName = Path.GetFileName(fullFileName);

                    if (name.ToUpper() == fileName.ToUpper())
                    {
                        File.Delete(fullFileName);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 弱點掃描項目[Path Manipulation]
        /// </summary>
        /// <param name="path">路徑</param>
        /// <param name="name">檔案名稱</param>
        public static string FixPathManipulation(string path, string name)
        {
            string returnPath = "";

            try
            {
                path = CleanPath(path);

                foreach (string fullFileName in Directory.EnumerateFiles(path))
                {
                    string fileName = Path.GetFileName(fullFileName);

                    if (name.ToUpper() == fileName.ToUpper())
                    {
                        returnPath = fullFileName;
                        break;
                    }
                }

                return returnPath;
            }
            catch (Exception ex)
            {
                return returnPath;
            }
        }

        /// <summary>
        /// 弱點掃描項目[Path Manipulation]
        /// </summary>
        /// <param name="path">路徑</param>
        /// <param name="name">檔案名稱</param>
        public static Stream FixPathManipulationStream(string path, string name)
        {
            Stream fileStream = null;

            try
            {
                path = CleanPath(path);

                foreach (string fullFileName in Directory.EnumerateFiles(path))
                {
                    string fileName = Path.GetFileName(fullFileName);

                    if (name.ToUpper() == fileName.ToUpper())
                    {
                        fileStream = File.OpenRead(fullFileName);
                        break;
                    }
                }

                return fileStream;
            }
            catch (Exception ex)
            {
                return fileStream;
            }
        }

        /// <summary>
        /// 路徑清洗
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CleanPath(string path)
        {
            string rePath = string.Empty;

            /* 替換有安全疑慮的路徑字串 */
            rePath = path.Replace("..\\", string.Empty)
                         .Replace("\\\\\\", string.Empty)
                         .Replace("\\\\", string.Empty)
                         .Replace("../", string.Empty)
                         .Replace("///", string.Empty)
                         .Replace("//", string.Empty);

            return rePath;
        }

        /// <summary>
        /// 防止轉換數值錯誤[String -> Int]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int FixNullNumber(string content)
        {
            if (string.IsNullOrEmpty(content))
                return 0;
            else
            {
                int num = 0;
                Int32.TryParse(content, out num);
                return num;
            }
        }

        /// <summary>
        /// 防止轉換數值為空
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FixNullDereferenceString(string content)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;
            else
                return content;
        }

        /// <summary>
        /// 防止轉換數值錯誤[String -> Float]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static float FixNullFloat(string content)
        {
            if (string.IsNullOrEmpty(content))
                return 0;
            else
            {
                float num = 0;
                float.TryParse(content, out num);
                return num;
            }
        }

        /// <summary>
        /// 允許檔案附檔名 [文檔]
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsAllowFile(string fileName)
        {
            string[] extend = fileName.ToLower().Trim().Split('.');
            List<string> types = new List<string>() { "doc", "docx", "xls", "xlsx", "pdf", "ppt", "rar", "7z", "zip", "jpg" };

            if (types.Contains(extend[extend.Length - 1]))
                return true;

            return false;
        }

        /// <summary>
        /// 允許檔案附檔名 [文檔]
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsAllowWordFile(string fileName)
        {
            string[] extend = fileName.ToLower().Trim().Split('.');
            List<string> types = new List<string>() { "doc", "docx", "xls", "xlsx", "ppt", "pptx", "odt", "ods", "odp", "odf", "pdf", "rar", "zip" };

            if (types.Contains(extend[extend.Length - 1]))
                return true;

            return false;
        }

        /// <summary>
        /// 允許檔案附檔名 [文檔]
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsAllowWordFile2(string fileName)
        {
            string[] extend = fileName.ToLower().Trim().Split('.');
            List<string> types = new List<string>() { "pdf" };

            if (types.Contains(extend[extend.Length - 1]))
                return true;

            return false;
        }

        /// <summary>
        /// 允許檔案附檔名 [圖檔]
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsAllowImageFile(string fileName)
        {
            string[] extend = fileName.ToLower().Trim().Split('.');
            List<string> types = new List<string>() { "jpg", "jpeg", "png", "bmp" };

            if (types.Contains(extend[extend.Length - 1]))
                return true;

            return false;
        }

        /// <summary>
        /// 加密隨機
        /// </summary>
        /// <param name="arr"></param>
        public static string[] GetRandom(string[] arr)
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            arr = arr.OrderBy(x => Next(random)).ToArray();
            return arr;
        }

        /// <summary>
        /// 加密隨機
        /// </summary>
        /// <param name="arr"></param>
        public static string GetRandomSingle(string[] arr)
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            return arr.OrderBy(x => Next(random)).First();
        }

        /// <summary>
        /// 原始隨機
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        private static int Next(RNGCryptoServiceProvider random)
        {
            byte[] randomInt = new byte[4];
            random.GetBytes(randomInt);
            return Convert.ToInt32(randomInt[0]);
        }

        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="content">字串</param>
        /// <returns></returns>
        //public static string GetSha512(string content)
        //{
        //    SHA512 sha512 = new SHA512CryptoServiceProvider();
        //    return Convert.ToBase64String(sha512.ComputeHash(Encoding.Default.GetBytes(content)));
        //}


    }

}
