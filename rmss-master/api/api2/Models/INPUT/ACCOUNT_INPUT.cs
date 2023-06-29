using Library.Model;

namespace api2.Models.INPUT
{
    public class ACCOUNT_INPUT : BaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }
    }

    public class ACCOUNT_INPUT_ADD : BaseModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }
        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string? AC_USERNAME { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string? AC_PWD { get; set; }
        /// <summary>
        /// 職稱
        /// </summary>
        public string? AC_TITLE { get; set; }

        /// <summary>
        /// 國籍[0:本國 1:外籍 ]
        /// </summary>
        public int? AC_COUNTRY { get; set; }
        /// <summary>
        /// 性別[0:男 1:女 2:不揭露]
        /// </summary>
        public byte? AC_SEX { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public string? AC_TEL { get; set; }
        /// <summary>
        /// 手機
        /// </summary>
        public string? AC_MOBILE { get; set; }

        /// <summary>
        /// 備用信箱
        /// </summary>
        public string? AC_EMAIL2 { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        public string? AC_CONTENT { get; set; }
        /// <summary>
        /// 是否接收行銷活動訊息
        /// </summary>
        public byte? AC_ISRECEIVE { get; set; }
    }

    public class ACCOUNT_INPU_MAILL : BaseModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }
    }

    public class ACCOUNT_INPU_VERIFY_MAILL : BaseModel
    {
        /// <summary>
        /// 加密資料
        /// </summary>
        public string DATA { get; set; }
    }


}
