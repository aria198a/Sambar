using Library.Functions;

namespace Library.Model
{
    public class ReturnModel2
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 資料
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="data"></param>
        /// <param name="messageEnum"></param>
        public void SetReturnModel(bool status, string data, MessageEnum messageEnum)
        {
            this.Status = status;
            this.Data = data;
            this.Message = new SendMessage().GetMessage(messageEnum);
        }
    }

    public class ReturnModel
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 資料
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="data"></param>
        /// <param name="messageEnum"></param>
        public void SetReturnModel(bool status, object data, MessageEnum messageEnum)
        {
            this.Status = status;
            this.Data = data;
            this.Message = new SendMessage().GetMessage(messageEnum);
        }
    }
}
