using System;
using Library.Interface;
using NLog;

namespace Library.Functions
{
    public class NLog : ILog
    {
        /// <summary>
        /// 紀錄器
        /// </summary>
        private Logger logger;
        public Logger Logger { get => logger; set => logger = value; }

        /// <summary>
        /// 紀錄詳細訊息
        /// </summary>
        /// <param name="spaceName"></param>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        /// <param name="content"></param>
        public void LogDetail(string spaceName, string className, string functionName, string content)
        {
            Logger = LogManager.GetLogger("Message");
            LogEventInfo logEventInfo = new LogEventInfo();
            logEventInfo.Properties["User"] = spaceName;
            logEventInfo.Properties["Class"] = className;
            logEventInfo.Properties["Function"] = functionName;
            logEventInfo.Properties["Message"] = content;
            Logger.Info(logEventInfo);
        }

        /// <summary>
        /// 紀錄資訊
        /// </summary>
        /// <param name="content"></param>
        public void LogInformation(string content)
        {
            Logger = LogManager.GetLogger("Information");
            Logger.Info(content);
        }

        /// <summary>
        /// 紀錄資訊
        /// </summary>
        /// <param name="content"></param>
        public void LogInformation(Exception content)
        {
            Logger = LogManager.GetLogger("InformationError");
            Logger.Info(content);
        }

    }
}
