using System;
using System.Text.RegularExpressions;

namespace Library.Functions
{
    public static class GuidHelper
    {
        /// <summary>
        /// 生成GUID
        /// </summary>
        /// <param name="unsigned"></param>
        /// <returns></returns>
        public static string Base64Guid(bool unsigned = false)
        {
            var guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            if (unsigned)
                return Regex.Replace(guid, "[/+=]", "");
            else
                return guid;
        }
    }
}
