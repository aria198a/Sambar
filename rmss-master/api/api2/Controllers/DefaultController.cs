using api2.Commons;
using api2.Services;
using api2.Models.DB;
using api2.Models.General;
using api2.Models.INPUT;
using Library.Extension;
using Library.Functions;
using Library.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using Microsoft.Win32;
using System;
using static api2.Models.General.LogHelper;

namespace api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        #region 登入相關

        /// <summary>
        /// 取得Token
        /// </summary>
        /// <returns></returns>
        [Route("Token")]
        [HttpPost]
        public string Token()
        {
            return new AES().Encryption(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
        }

        /// <summary>
        /// 取得 IP
        /// </summary>
        /// <returns></returns>
        private string GetClientIp()
        {
            return HttpContext.Connection.RemoteIpAddress != null ? HttpContext.Connection.RemoteIpAddress.ToString() : string.Empty;
        }
        /// <summary>
        /// 取得圖形碼
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>取得登入圖形驗證碼</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("GetImageCode")]
        [HttpPost]
        public ReturnModel GetImageCode()
        {
            ReturnModel returnModel = new ReturnModel();
            GetImageCode getImageCode = new GetImageCode();
            returnModel.SetReturnModel(true, getImageCode.GetImage(), MessageEnum.Success);
            return returnModel;
        }
        /// <summary>
        /// 自訂義API
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>注意事項</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("Login")]
        [HttpPost]
        public ReturnModel Login(Login Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                try
                {
                    if (Object.CheckCodePass())
                    {
                        string id = Object.CheckPass(GetClientIp());
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (id.Contains("§"))
                            {
                                var encodeid = new AES().Encryption(id);
                                returnModel.SetReturnModel(true, JsonConvert.SerializeObject(encodeid, Formatting.Indented), MessageEnum.Success);
                            }
                            else
                            {
                                returnModel.SetReturnModel(false, id, MessageEnum.Fail);
                            }
                        }
                        else
                        {
                            returnModel.SetReturnModel(false, "查無此帳號", MessageEnum.Fail);
                        }
                    }
                    else
                    {
                        returnModel.SetReturnModel(false, "圖形驗證失敗", MessageEnum.Fail);
                    }
                }
                catch (Exception ex)
                {
                    returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }

        #endregion

        #region 帳號相關

        /// <summary>
        /// 取得帳號的列表
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>取得所有未刪除帳號的列表依照帳號建立時間排序</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("GetAccountList")]
        [HttpPost]
        public ReturnModel GetAccountList(BaseModel Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                if (LogicFunc.IsVerifyId(Object.TokenID, "PJ202111030001"))
                {
                    try
                    {
                        ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                        var Model = ACCOUNT_EXTEND.GetListByGroupId();
                        returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                    }
                    catch (Exception ex)
                    {
                        returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "權限不足", MessageEnum.Fail);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }
        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>使用系統介面新增帳號</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("InsertAccount")]
        [HttpPost]
        public ReturnModel InsertAccount(InsertAccount Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                if (LogicFunc.IsVerifyId(Object.TokenID, "PJ202111030001"))
                {
                    try
                    {
                        ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                        var ACCOUNT = LogicFunc.IsVerifyMailReturnAccount(Object.AC_USERID);
                        if (ACCOUNT == null)
                        {
                            ACCOUNT = new ACCOUNT().Mapper(Object);
                            Regex rgx = new Regex(@"^(?:(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])|(?=.*[A-Z])(?=.*[a-z])(?=.*[^A-Za-z0-9])|(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])|(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9])).{6,}|(?:(?=.*[A-Z])(?=.*[a-z])|(?=.*[A-Z])(?=.*[0-9])|(?=.*[A-Z])(?=.*[^A-Za-z0-9])|(?=.*[a-z])(?=.*[0-9])|(?=.*[a-z])(?=.*[^A-Za-z0-9])|(?=.*[0-9])(?=.*[^A-Za-z0-9])|).{8,}");
                            if (rgx.IsMatch(ACCOUNT.AC_PWD))
                            {
                                ACCOUNT.AC_ID = "AC" + GuidHelper.Base64Guid(true);
                                ACCOUNT.AC_USERTYPE = "新增帳號";
                                ACCOUNT.AC_PWD = new AES().Encryption(ACCOUNT.AC_PWD);
                                ACCOUNT.AC_PWD3 = "";
                                ACCOUNT.AC_VCOUNT = 0;
                                ACCOUNT.AC_ORDER = 0;
                                ACCOUNT.AC_STATUS = 1;
                                ACCOUNT.AC_ISDEL = 0;
                                ACCOUNT.AC_MDATE = DateTime.Now;
                                var Model = ACCOUNT_EXTEND.InsertExecute(ACCOUNT);
                                returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                            }
                            else
                            {
                                returnModel.SetReturnModel(false, "密碼複雜度不足", MessageEnum.Fail);
                            }
                        }
                        else
                        {
                            returnModel.SetReturnModel(false, "此帳號已使用", MessageEnum.Fail);
                        }
                    }
                    catch (Exception ex)
                    {
                        returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "權限不足", MessageEnum.Fail);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }
        
        /// <summary>
        /// 取得帳號資訊 (單筆)
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>使用系統介面編輯帳號資訊(不含密碼)</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("GetAccount")]
        [HttpPost]
        public ReturnModel GetAccount(UpdateAccount Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                if (LogicFunc.IsVerifyId(Object.TokenID, "PJ202111030001"))
                {
                    try
                    {
                        ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                        var Model = ACCOUNT_EXTEND.Get(new ACCOUNT() { AC_ID = Object.AC_ID });
                        returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                    }
                    catch (Exception ex)
                    {
                        returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "權限不足", MessageEnum.Fail);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }
        /// <summary>
        /// 修改帳號資訊
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>使用系統介面編輯帳號資訊(不含密碼)</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("UpdateAccount")]
        [HttpPost]
        public ReturnModel UpdateAccount(UpdateAccount Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                if (LogicFunc.IsVerifyId(Object.TokenID, "PJ202111030001"))
                {
                    try
                    {
                        ACCOUNT EditACCOUNT = LogicFunc.IsVerifyIdReturnAccount(Object.TokenID);
                        ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                        var ACCOUNT = ACCOUNT_EXTEND.Get(new ACCOUNT() { AC_ID = Object.AC_ID });
                        if (ACCOUNT != null)
                        {
                            ACCOUNT = ACCOUNT.MapperNotNull(Object);
                            ACCOUNT.AC_EDATE = DateTime.Now;
                            ACCOUNT.AC_EUSER = EditACCOUNT.AC_ID;
                            var Model = ACCOUNT_EXTEND.UpdateExecute(ACCOUNT);
                            returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                        }
                        else
                        {
                            returnModel.SetReturnModel(false, "編輯失敗，查無此帳號", MessageEnum.Fail);
                        }
                    }
                    catch (Exception ex)
                    {
                        returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "權限不足", MessageEnum.Fail);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }
        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>使用系統介面刪除帳號</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("DeleteAccount")]
        [HttpPost]
        public ReturnModel DeleteAccount(UpdateAccount Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                if (LogicFunc.IsVerifyId(Object.TokenID, ""))
                {
                    try
                    {
                        ACCOUNT EditACCOUNT = LogicFunc.IsVerifyIdReturnAccount(Object.TokenID);
                        ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                        var ACCOUNT = ACCOUNT_EXTEND.Get(new ACCOUNT() { AC_ID = Object.AC_ID });
                        if (ACCOUNT != null)
                        {
                            ACCOUNT.AC_ISDEL = 1;
                            ACCOUNT.AC_EDATE = DateTime.Now;
                            ACCOUNT.AC_EUSER = EditACCOUNT.AC_ID;
                            var Model = ACCOUNT_EXTEND.UpdateExecute(ACCOUNT);
                            returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                        }
                        else
                        {
                            returnModel.SetReturnModel(false, "刪除失敗，查無此帳號", MessageEnum.Fail);
                        }
                    }
                    catch (Exception ex)
                    {
                        returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "權限不足", MessageEnum.Fail);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }

        /// <summary>
        /// 權限列表
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>取得權限列表</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("GetProjectList")]
        [HttpPost]
        public ReturnModel GetProjectList(BaseModel Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                if (LogicFunc.IsVerifyId(Object.TokenID, "PJ202111030001"))
                {
                    try
                    {
                        PROJECT_EXTEND PROJECT_EXTEND = new PROJECT_EXTEND();
                        var Model = PROJECT_EXTEND.GetList();
                        returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                    }
                    catch (Exception ex)
                    {
                        returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "權限不足", MessageEnum.Fail);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>註冊帳號</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("Register")]
        [HttpPost]
        public ReturnModel Register(ACCOUNT_INPUT_ADD Object)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                if (LogicFunc.IsVerifyDate(Object.Token))
                {
                    if (LogicFunc.IsVerifyId(Object.TokenID, "PJ202209270001"))
                    {
                        ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                        var ACCOUNT = LogicFunc.IsVerifyMailReturnAccount(Object.AC_USERID);
                        if (ACCOUNT == null)
                        {
                            ACCOUNT = new ACCOUNT().Mapper(Object);
                            Regex rgx = new Regex(@"^(?:(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])|(?=.*[A-Z])(?=.*[a-z])(?=.*[^A-Za-z0-9])|(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])|(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9])).{6,}|(?:(?=.*[A-Z])(?=.*[a-z])|(?=.*[A-Z])(?=.*[0-9])|(?=.*[A-Z])(?=.*[^A-Za-z0-9])|(?=.*[a-z])(?=.*[0-9])|(?=.*[a-z])(?=.*[^A-Za-z0-9])|(?=.*[0-9])(?=.*[^A-Za-z0-9])|).{8,}");
                            if (rgx.IsMatch(ACCOUNT.AC_PWD))
                            {
                                ACCOUNT.AC_ID = "AC" + GuidHelper.Base64Guid(true);
                                ACCOUNT.AC_USERTYPE = "註冊帳號";
                                ACCOUNT.AC_PWD = new AES().Encryption(ACCOUNT.AC_PWD);
                                ACCOUNT.AC_PWD3 = "";
                                ACCOUNT.AC_EMAIL = Object.AC_USERID;
                                ACCOUNT.AC_VCOUNT = 0;
                                ACCOUNT.AC_ORDER = 0;
                                ACCOUNT.AC_STATUS = 0;
                                ACCOUNT.AC_ISDEL = 0;
                                ACCOUNT.AC_MDATE = DateTime.Now;
                                //待修改  測試權限為0
                                ACCOUNT.AC_GROUP_ID = "0";
                                var Model = ACCOUNT_EXTEND.InsertExecute(ACCOUNT);

                                if (Model != null)
                                {
                                    string Token = new AES().Encryption(ACCOUNT.AC_ID + "§" + DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

                                    Token = HttpUtility.UrlEncode(Token);

                                    string SendBody = $"感測平台帳號認證連結： http://localhost:8080/#/AccountVerify?Token=" + Token + " </br>";
                                    SendBody += "此為系統自動寄送通知訊息，請勿直接回覆本郵件，謝謝！ </br>";

                                    GMail GMail = new GMail();
                                    MessageEnum res = GMail.SendMail("", "genzz415305@gmail.com", "感測平台", ACCOUNT.AC_USERID, ACCOUNT.AC_USERNAME, "", "感測平台帳號認證連結", SendBody, "");

                                    returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                                }
                                else
                                {
                                    returnModel.SetReturnModel(false, "", MessageEnum.None);
                                }

                            }
                            else
                            {
                                returnModel.SetReturnModel(false, "", MessageEnum.PwdSimple);
                            }
                        }
                        else
                        {
                            returnModel.SetReturnModel(false, "", MessageEnum.DuplicateAccount);
                        }
                    }
                    else
                    {
                        returnModel.SetReturnModel(false, "", MessageEnum.Fail);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "", MessageEnum.NonToken);
                }
            }
            catch (Exception e)
            {
                returnModel.SetReturnModel(false, "", MessageEnum.Fail);

                LogAPIModule logAPIModule = new LogAPIModule();
                logAPIModule.url = "Register";
                logAPIModule.data = JsonConvert.SerializeObject(Object);
                LogHelper.Info(e.ToString(), "LOGAPI", logAPIModule);
            }

           

            return returnModel;
        }


        /// <summary>
        /// 重新發送驗證信
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("ReSendVerifyMail")]
        [HttpPost]
        public ReturnModel ReSendVerifyMail(ACCOUNT_INPU_MAILL Object)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                if (LogicFunc.IsVerifyDate(Object.Token))
                {
                    if (LogicFunc.IsVerifyId(Object.TokenID, "PJ202209270001"))
                    {
                        ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                        var ACCOUNT = LogicFunc.IsVerifyMailReturnAccount(Object.AC_USERID);
                        if (ACCOUNT != null)
                        {
                            
                            if (ACCOUNT.AC_STATUS == 0)
                            {
                                string Token = new AES().Encryption(ACCOUNT.AC_ID + "§" + DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

                                Token = HttpUtility.UrlEncode(Token);

                                string SendBody = $"感測平台帳號認證連結： http://localhost:8080/#/AccountVerify?Token=" + Token + " </br>";
                                SendBody += "此為系統自動寄送通知訊息，請勿直接回覆本郵件，謝謝！ </br>";

                                GMail GMail = new GMail();
                                MessageEnum res = GMail.SendMail("", "genzz415305@gmail.com", "感測平台", ACCOUNT.AC_USERID, ACCOUNT.AC_USERNAME, "", "感測平台帳號認證連結", SendBody, "");

                                returnModel.SetReturnModel(true,"", MessageEnum.Success);

                            }
                            else if (ACCOUNT.AC_STATUS == 1)
                            {
                                returnModel.SetReturnModel(false, "", MessageEnum.AccountStatus1);
                            }
                            else if (ACCOUNT.AC_STATUS == 2)
                            {
                                returnModel.SetReturnModel(false, "", MessageEnum.AccountStatus2);
                            }
                        }
                        else
                        {
                            returnModel.SetReturnModel(false, "", MessageEnum.NoneAccount);
                        }
                    }
                    else
                    {
                        returnModel.SetReturnModel(false, "", MessageEnum.Fail);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "", MessageEnum.NonToken);
                }
            }
            catch (Exception e)
            {
                returnModel.SetReturnModel(false, "", MessageEnum.Fail);

                LogAPIModule logAPIModule = new LogAPIModule();
                logAPIModule.url = "ReSendVerifyMail";
                logAPIModule.data = JsonConvert.SerializeObject(Object);
                LogHelper.Info(e.ToString(), "LOGAPI", logAPIModule);
            }
            return returnModel;
        }

        /// <summary>
        /// 驗證帳號
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("VerifyAccount")]
        [HttpPost]
        public ReturnModel VerifyAccount(ACCOUNT_INPU_VERIFY_MAILL Object)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                if (LogicFunc.IsVerifyDate(Object.Token))
                {
                    if (LogicFunc.IsVerifyId(Object.TokenID, "PJ202209270001"))
                    {
                        string[] data = new AES().Decryption(Object.DATA).Split('§');

                        if(data.Length == 2)
                        {
                            if (Convert.ToDateTime(data[1]).AddHours(24) >= DateTime.Now)
                            {
                                var ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                                var ACCOUNT = ACCOUNT_EXTEND.SelectById(data[0]);

                                if(ACCOUNT != null)
                                {
                                    if (ACCOUNT.AC_STATUS == 0)
                                    {
                                        ACCOUNT.AC_STATUS = 1;

                                        var model = ACCOUNT_EXTEND.Update(ACCOUNT);

                                        if (model != null)
                                            returnModel.SetReturnModel(true, "", MessageEnum.Success);
                                        else
                                            returnModel.SetReturnModel(true, "", MessageEnum.Fail);
                                    }
                                    else if (ACCOUNT.AC_STATUS == 1)
                                    {
                                        returnModel.SetReturnModel(false, "", MessageEnum.AccountStatus1);
                                    }
                                    else if (ACCOUNT.AC_STATUS == 2)
                                    {
                                        returnModel.SetReturnModel(false, "", MessageEnum.AccountStatus2);
                                    }


                                }
                                else
                                {
                                    returnModel.SetReturnModel(false, "", MessageEnum.NoneAccount);
                                }

                            }
                            else
                            {
                                returnModel.SetReturnModel(false, "", MessageEnum.NonToken);
                            }
                        }
                        else
                        {
                            returnModel.SetReturnModel(false, "", MessageEnum.NonToken);
                        }

                    }
                    else
                    {
                        returnModel.SetReturnModel(false, "", MessageEnum.Fail);
                    }
                }
                else
                {
                    returnModel.SetReturnModel(false, "", MessageEnum.NonToken);
                }
            }
            catch (Exception e)
            {
                returnModel.SetReturnModel(false, "", MessageEnum.Fail);

                LogAPIModule logAPIModule = new LogAPIModule();
                logAPIModule.url = "ReSendVerifyMail";
                logAPIModule.data = JsonConvert.SerializeObject(Object);
                LogHelper.Info(e.ToString(), "LOGAPI", logAPIModule);
            }
            return returnModel;
        }

        /// <summary>
        /// 個人帳號資訊
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>取得登入者的帳號資訊利用token id</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("GetMyAccount")]
        [HttpPost]
        public ReturnModel GetMyAccount(BaseModel Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                try
                {
                    var Model = LogicFunc.IsVerifyIdReturnAccount(Object.TokenID);
                    if (Model != null)
                    {
                        returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                    }
                    else
                    {
                        returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
                    }
                }
                catch (Exception ex)
                {
                    returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }

        /// <summary>
        /// 編輯個人帳號資訊
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>編輯登入者的帳號資訊利用token id</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("UpdateMyAccount")]
        [HttpPost]
        public ReturnModel UpdateMyAccount(UpdateAccount Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                try
                {
                    var ACCOUNT = LogicFunc.IsVerifyIdReturnAccount(Object.TokenID);
                    if (ACCOUNT != null)
                    {
                        ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                        ACCOUNT = ACCOUNT.MapperNotNull(Object);
                        var Model = ACCOUNT_EXTEND.UpdateExecute(ACCOUNT);
                        returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                    }
                    else
                    {
                        returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
                    }
                }
                catch (Exception ex)
                {
                    returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }

        #endregion

        #region 忘記密碼流程

        private IConfiguration Configuration;

        public DefaultController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        /// <summary>
        /// 忘記密碼
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>使用系統介面點擊忘記密碼按鈕並使用帳號及信箱驗證，然後寄送驗證信件給使用者驗證</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("ForgetPassword")]
        [HttpPost]
        public ReturnModel ForgetPassword(ForgetPassword Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDate(Object.Token))
            {
                try
                {
                    ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                    var Account = ACCOUNT_EXTEND.Get(new ACCOUNT() { AC_USERID = Object.AC_USERID, AC_EMAIL = Object.AC_EMAIL });
                    if (Account != null)
                    {
                        string Token = new AES().Encryption(Account.AC_USERID + "§" + DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                        Token = HttpUtility.UrlEncode(Token);
                        string SendBody = string.Empty;
                        SendBody += "修改密碼連結：http://localhost:8080/#/ForgetPassword?Token=" + Token;
                        SendBody += "<br />";
                        SendBody += "此為系統自動寄送通知訊息，請勿直接回覆本郵件，謝謝！";

                        EmailOptions emailOptions = new EmailOptions();
                        this.Configuration.GetSection("EmailConfiguration").Bind(emailOptions);
                        IOptions<EmailOptions> myOptions = Options.Create(emailOptions);
                        EmailService EmailService = new EmailService(myOptions);
                        EmailService.Send(Account.AC_EMAIL, "忘記密碼", SendBody);

                        returnModel.SetReturnModel(true, "寄信成功", MessageEnum.Success);
                    }
                    else
                    {
                        returnModel.SetReturnModel(true, "查無此帳號", MessageEnum.Fail);
                    }
                }
                catch (Exception ex)
                {
                    returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }

        /// <summary>
        /// 修改密碼
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        /// <remarks>忘記密碼後重新修改密碼</remarks> 
        //[SwaggerResponse(HttpStatusCode.OK, "成功", typeof(List<PICHOME>))]
        //[EnableCors(origins: IP1, headers: "*", methods: "*")]
        [Route("ResetPassword")]
        [HttpPost]
        public ReturnModel ResetPassword(ResetPassword Object)
        {
            ReturnModel returnModel = new ReturnModel();

            if (LogicFunc.IsVerifyDateToo(Object.PassToken))
            {
                try
                {
                    var Tokens = LogicFunc.GetDecryptionId(Object.PassToken);
                    ACCOUNT_EXTEND ACCOUNT_EXTEND = new ACCOUNT_EXTEND();
                    ACCOUNT ACCOUNT = ACCOUNT_EXTEND.Get(new ACCOUNT() { AC_USERID = Tokens[0] });
                    if (ACCOUNT != null)
                    {
                        Regex rgx = new Regex(@"^(?:(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])|(?=.*[A-Z])(?=.*[a-z])(?=.*[^A-Za-z0-9])|(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])|(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9])).{6,}|(?:(?=.*[A-Z])(?=.*[a-z])|(?=.*[A-Z])(?=.*[0-9])|(?=.*[A-Z])(?=.*[^A-Za-z0-9])|(?=.*[a-z])(?=.*[0-9])|(?=.*[a-z])(?=.*[^A-Za-z0-9])|(?=.*[0-9])(?=.*[^A-Za-z0-9])|).{8,}");
                        if (rgx.IsMatch(Object.DecryptionPass))
                        {
                            if (ACCOUNT.AC_PWD3 == null)
                            {
                                ACCOUNT.AC_PWD3 = "";
                            }

                            if (ACCOUNT.AC_PWD3.IndexOf(ACCOUNT.AC_PWD) == -1)
                            {
                                var AC_PWD3 = ACCOUNT.AC_PWD3.Split(',');
                                if (AC_PWD3.Count() >= 3)
                                {
                                    foreach (var item in AC_PWD3.Select((value, i) => new { i, value }))
                                    {
                                        var value = item.value;
                                        var index = item.i;

                                        if (index != 0)
                                        {
                                            ACCOUNT.AC_PWD3 += value + ",";
                                        }
                                    }

                                    ACCOUNT.AC_PWD3 += ACCOUNT.AC_PWD + ",";
                                }
                                else
                                {
                                    ACCOUNT.AC_PWD3 += ACCOUNT.AC_PWD + ",";
                                }

                                ACCOUNT.AC_PWD3 = ACCOUNT.AC_PWD3.Remove(ACCOUNT.AC_PWD3.Length - 1, 1);
                                ACCOUNT.AC_PWD = new AES().Encryption(Object.DecryptionPass);
                                var Model = ACCOUNT_EXTEND.UpdateExecute(ACCOUNT);
                                returnModel.SetReturnModel(true, JsonConvert.SerializeObject(Model), MessageEnum.Success);
                            }
                            else
                            {
                                returnModel.SetReturnModel(false, "密碼與前三次相同", MessageEnum.Fail);
                            }
                        }
                        else
                        {
                            returnModel.SetReturnModel(false, "密碼複雜度不足", MessageEnum.Fail);
                        }
                    }
                    else
                    {
                        returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
                    }
                }
                catch (Exception ex)
                {
                    returnModel.SetReturnModel(true, ex.Message, MessageEnum.Error);
                }
            }
            else
            {
                returnModel.SetReturnModel(false, "Token驗證失敗", MessageEnum.NonToken);
            }

            return returnModel;
        }

        #endregion
    }
}
