using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.INPUT
{
    /// <summary>
    /// 忘記密碼 用 INPUT Class 
    /// </summary>
    public class ForgetPassword : BaseModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }
    }

    /// <summary>
    /// 修改密碼 用INPUT class 
    /// </summary>
    public class ResetPassword : BaseModel
    {
        /// <summary>
        /// 帳號密文
        /// </summary>
        public string PassToken { get; set; }

        /// <summary>
        /// 新密碼
        /// </summary>
        public string DecryptionPass { get; set; }
    }
}
