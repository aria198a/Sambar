using NLog;
using static api2.Models.General.LogHelper;

namespace api2.Models.General
{
    public class LogHelper
    {
        //紀錄器
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // API模組
        public class LogAPIModule
        {
            /// <summary>
            /// 服務名稱
            /// </summary>
            public string data { get; set; }
            /// <summary>
            /// 服務網址
            /// </summary>
            public string url { get; set; }
        }

        public static void Info(string content, string name, LogAPIModule logAPIModule)
        {
            logger = LogManager.GetLogger("LOGAPI");
            LogEventInfo logEventInfo = new LogEventInfo();
            logEventInfo.Properties["data"] = $"Post-Data: {logAPIModule.data}";
            logEventInfo.Properties["url"] = $"Url:{logAPIModule.url}";
            logEventInfo.Properties["Message"] = $"Response:{content}";
            logger.Info(logEventInfo);

            //把檔名通過屬性傳輸
            //logger.WithProperty("filename", fileName).Info(message);
        }
        public static void Debug(string message, string fileName = "DEBUG")
        {
            logger.WithProperty("filename", fileName).Debug(message);
        }
        public static void Error(string message, string fileName = "Error")
        {
            logger.WithProperty("filename", fileName).Error(message);
        }
        public static void Warn(string message, string fileName = "Warn")
        {
            logger.WithProperty("filename", fileName).Warn(message);
        }
    }
}
