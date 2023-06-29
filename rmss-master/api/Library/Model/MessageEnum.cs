namespace Library.Model
{
    /// <summary>
    /// 訊息列舉
    /// </summary>
    public enum MessageEnum
    {
        Error,
        Success,
        Fail,
        UpdateFail,
        InsertFail,
        DeleteFail,
        CodeFail,
        None,
        NoneAccount,
        AccountStatus1,
        AccountStatus2,
        NonLoginUID,
        NonLoginPWD,     
        NonToken,
        NonLogin,
        NonFile,
        NonPath,
        NonType,
        NonParameters,
        DuplicateAccount,
        MailFail,
        TelFail,
        PwdSimple,
        NotVerifiedImage,
        NotVerifiedFile,
        ErrorSendMail,
        RegexFailMail
    }
}
