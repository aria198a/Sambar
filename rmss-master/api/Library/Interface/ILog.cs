using NLog;
using System;

namespace Library.Interface
{
    interface ILog
    {
        /// <summary>
        /// 紀錄器
        /// </summary>
        Logger Logger { get; set; }
        /// <summary>
        /// 紀錄資訊
        /// </summary>
        /// <param name="content">Content</param>
        void LogInformation(string content);
        /// <summary>
        /// 紀錄資訊
        /// </summary>
        /// <param name="content"></param>
        void LogInformation(Exception content);
        /// <summary>
        /// 紀錄詳細訊息
        /// </summary>
        /// <param name="spaceName">NameSpace</param>
        /// <param name="className">ClassName</param>
        /// <param name="functionName">FunctionName</param>
        /// <param name="content">Content</param>
        void LogDetail(string spaceName, string className, string functionName, string content);

    }
}
