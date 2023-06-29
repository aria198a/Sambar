namespace api.Commons
{
    public class EmailOptions
    {
        public const string EmailConfiguration = "EmailConfiguration";
        /// <summary>
        /// 寄件者信箱
        /// </summary>
        public string From { get; set; } = String.Empty;
        /// <summary>
        /// smtp
        /// </summary>
        public string SmtpServer { get; set; } = String.Empty;
        /// <summary>
        /// port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 寄件者名稱
        /// </summary>
        public string UserName { get; set; } = String.Empty;
        /// <summary>
        /// 寄件者密碼
        /// </summary>
        public string Password { get; set; } = String.Empty;
    }
}
