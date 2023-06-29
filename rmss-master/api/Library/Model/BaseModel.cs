
namespace Library.Model
{
    public class BaseModel
    {
        /// <summary>
        /// 加密ID
        /// </summary>
        public string? TokenID { get; set; }
        /// <summary>
        /// 驗證API
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// 頁面
        /// </summary>
        public string? Page { get; set; }
    }
}
