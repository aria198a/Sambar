using Library.Controller;
using Library.Model;
using System;
using System.IO;
using System.Web;
// System.Web.UI.WebControls;

namespace Library.Functions
{
    public class FileFunc : MediaFile
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public FileFunc()
        {
            Id = "MC" + DateTime.Now.ToString("yyyyMMddHHssfff");
            FilePath = "";
            SourceId = "";
            Title = "";
            Name = "";
            FileName = "";
            Size = 0;
            Extension = "";
            ContentType = "";
            Clicks = 0;
            CreateDate = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 設置檔案數據
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        //public MessageEnum SetFile(FileUpload fileUpload)
        //{
        //    string date = DateTime.Now.ToString("yyyyMMddHHmmss");

        //    if (fileUpload.HasFile)
        //    {
        //        Id = "MC" + date;
        //        Name = Path.GetFileNameWithoutExtension(fileUpload.FileName);
        //        FileName = Path.GetFileNameWithoutExtension(fileUpload.FileName) + "_" + date;
        //        Size = fileUpload.PostedFile.ContentLength;
        //        Extension = Path.GetExtension(fileUpload.FileName).ToLower();
        //        ContentType = fileUpload.PostedFile.ContentType;
        //        return MessageEnum.Success;
        //    }

        //    return MessageEnum.None;
        //}

        /// <summary>
        /// 設置檔案數據
        /// </summary>
        /// <param name="httpPostedFile"></param>
        /// <returns></returns>
        //public MessageEnum SetFile(HttpPostedFile httpPostedFile)
        //{
        //    string date = DateTime.Now.ToString("yyyyMMddHHmmss");

        //    if (httpPostedFile.ContentLength > 0)
        //    {
        //        Id = "MC" + date;
        //        Name = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);
        //        FileName = Path.GetFileNameWithoutExtension(httpPostedFile.FileName) + "_" + date;
        //        Size = httpPostedFile.ContentLength;
        //        Extension = Path.GetExtension(httpPostedFile.FileName).ToLower();
        //        ContentType = httpPostedFile.ContentType;              
        //        return MessageEnum.Success;
        //    }

        //    return MessageEnum.None;
        //}

        /// <summary>
        /// 設置來源ID
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public MessageEnum SetSourceId(string sourceId)
        {
            if (!string.IsNullOrEmpty(sourceId))
            {
                SourceId = sourceId;
                return MessageEnum.Success;
            }

            return MessageEnum.None;
        }

        /// <summary>
        /// 設置路徑
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public MessageEnum SetFilePath(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                FilePath = filePath;
                return MessageEnum.Success;
            }

            return MessageEnum.None;
        }

        /// <summary>
        /// 設置預設路徑
        /// </summary>
        /// <returns></returns>
        //public MessageEnum SetFilePathDefault()
        //{
        //    SqlController sqlController = new SqlController();
        //    string filePath = sqlController.GetFilePathDefault();

        //    if (!string.IsNullOrEmpty(filePath))
        //    {
        //        FilePath = filePath;
        //        return MessageEnum.Success;
        //    }

        //    return MessageEnum.Fail;
        //}


    }
}
