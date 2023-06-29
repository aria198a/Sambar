using Library.Model;

namespace Library.Functions
{
    public class SendMessage
    {
        /// <summary>
        /// 取得訊息
        /// </summary>
        /// <param name="messageEnum">訊息列舉</param>
        /// <returns></returns>
        public string GetMessage(MessageEnum messageEnum)
        {
            string _message = string.Empty;

            switch (messageEnum)
            {
                case MessageEnum.Error:
                    _message = "錯誤";
                    break;
                case MessageEnum.Fail:
                    _message = "失敗";
                    break;
                case MessageEnum.UpdateFail:
                    _message = "更新失敗";
                    break;
                case MessageEnum.InsertFail:
                    _message = "新增失敗";
                    break;
                case MessageEnum.DeleteFail:
                    _message = "刪除失敗";
                    break;
                case MessageEnum.None:
                    _message = "查無資料";
                    break;
                case MessageEnum.NoneAccount:
                    _message = "查無帳號";
                    break;
                case MessageEnum.AccountStatus1:
                    _message = "帳號已啟用";
                    break;
                case MessageEnum.AccountStatus2:
                    _message = "帳號遭停用";
                    break;
                case MessageEnum.NonLoginPWD:
                    _message = "請查看密碼";
                    break;
                case MessageEnum.NonLoginUID:
                    _message = "請查看帳號";
                    break;
                case MessageEnum.Success:
                    _message = "成功";
                    break;
                case MessageEnum.NonToken:
                    _message = "無效Token，請重新發送";
                    break;
                case MessageEnum.NonLogin:
                    _message = "無效Token，帳號尚未登入";
                    break;
                case MessageEnum.NonFile:
                    _message = "請確認檔案";
                    break;
                case MessageEnum.NonPath:
                    _message = "尚未設定檔案路徑";
                    break;
                case MessageEnum.NonParameters:
                    _message = "請確認參數";
                    break;
                case MessageEnum.DuplicateAccount:
                    _message = "已有帳號";
                    break;
                case MessageEnum.MailFail:
                    _message = "請確認信箱";
                    break;
                case MessageEnum.TelFail:
                    _message = "請確認電話";
                    break;
                case MessageEnum.PwdSimple:
                    _message = "密碼複雜度需至少有一個數字 / 至少有一個大寫或小寫英文字母 / 至少有一個特殊符號 / 字串長度在 12 ~ 30 個字母之間";
                    break;
                case MessageEnum.CodeFail:
                    _message = "請確認驗證碼";
                    break;
                case MessageEnum.NotVerifiedImage:
                    _message = "圖片格式僅支援.jpg, .jpeg, .png";
                    break;
                case MessageEnum.NotVerifiedFile:
                    _message = "檔案格式僅支援.doc, .docx, .pdf";
                    break;
                case MessageEnum.ErrorSendMail:
                    _message = "送信功能異常";
                    break;
                case MessageEnum.RegexFailMail:
                    _message = "信箱格式不符";
                    break;
            }

            return _message;
        }
    }
}
