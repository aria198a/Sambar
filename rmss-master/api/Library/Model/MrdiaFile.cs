using System;

namespace Library.Model
{
    public class MediaFile
    {
        /// <summary>
        /// 檔案ID
        /// </summary>
        protected string Id { get; set; }
        /// <summary>
        /// 檔案路徑
        /// </summary>
        protected string FilePath { get; set; }
        /// <summary>
        /// 檔案來源ID
        /// </summary>
        protected string SourceId { get; set; }
        /// <summary>
        /// 檔案標題
        /// </summary>
        protected string Title { get; set; }
        /// <summary>
        /// 檔案顯示名稱
        /// </summary>
        protected string Name { get; set; }
        /// <summary>
        /// 檔案實際名稱
        /// </summary>
        protected string FileName { get; set; }
        /// <summary>
        /// 檔案大小
        /// </summary>
        protected int Size { get; set; }
        /// <summary>
        /// 附檔名
        /// </summary>
        protected string Extension { get; set; }
        /// <summary>
        /// 下載附檔名類別[MIME]
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 點擊次數
        /// </summary>
        protected int Clicks { get; set; }
        /// <summary>
        /// 新建日期
        /// </summary>
        protected DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        protected DateTime UpdateTime { get; set; }

    }
}
