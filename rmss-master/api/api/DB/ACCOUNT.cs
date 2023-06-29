using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace api.DB
{
    #region 舊的帳號Model

    ///// <summary>
    ///// 帳號表
    ///// </summary>
    //public class ACCOUNT
    //{
    //    /// <summary>
    //    /// 帳號編號
    //    /// </summary>
    //    public string AC_ID { get; set; }

    //    /// <summary>
    //    /// 帳號
    //    /// </summary>
    //    public string AC_USERID { get; set; }

    //    /// <summary>
    //    /// 帳號名稱
    //    /// </summary>
    //    public string AC_USERNAME { get; set; }

    //    /// <summary>
    //    /// 密碼
    //    /// </summary>
    //    public string AC_PWD { get; set; }

    //    /// <summary>
    //    /// 帳號類別
    //    /// </summary>
    //    public string AC_USERTYPE { get; set; }

    //    /// <summary>
    //    /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
    //    /// </summary>
    //    public int AC_STATUS { get; set; }

    //    /// <summary>
    //    /// 權限群組
    //    /// </summary>
    //    public string AC_GROUP_ID { get; set; }

    //    /// <summary>
    //    /// 職稱
    //    /// </summary>
    //    public string AC_TITLE { get; set; }

    //    /// <summary>
    //    /// 分機
    //    /// </summary>
    //    public string AC_TEL_EX { get; set; }

    //    /// <summary>
    //    /// 附加類別
    //    /// </summary>
    //    public string AC_KEYID { get; set; }

    //    /// <summary>
    //    /// 性別[0:男 1:女]
    //    /// </summary>
    //    public int? AC_SEX { get; set; }

    //    /// <summary>
    //    /// 身分證字號
    //    /// </summary>
    //    public string AC_IDNUM { get; set; }

    //    /// <summary>
    //    /// 生日
    //    /// </summary>
    //    public DateTime? AC_BIRTHDAY { get; set; }

    //    /// <summary>
    //    /// 聯絡人
    //    /// </summary>
    //    public string AC_CONTACT { get; set; }

    //    /// <summary>
    //    /// 聯絡人郵遞區號
    //    /// </summary>
    //    public string AC_POST { get; set; }

    //    /// <summary>
    //    /// 聯絡人縣市
    //    /// </summary>
    //    public string AC_CITYID { get; set; }

    //    /// <summary>
    //    /// 聯絡人鄉鎮
    //    /// </summary>
    //    public string AC_TOWNID { get; set; }

    //    /// <summary>
    //    /// 聯絡人地址
    //    /// </summary>
    //    public string AC_ADDRESS { get; set; }

    //    /// <summary>
    //    /// 電話
    //    /// </summary>
    //    public string AC_TEL { get; set; }

    //    /// <summary>
    //    /// 手機
    //    /// </summary>
    //    public string AC_MOBILE { get; set; }

    //    /// <summary>
    //    /// 傳真
    //    /// </summary>
    //    public string AC_FAX { get; set; }

    //    /// <summary>
    //    /// 信箱
    //    /// </summary>
    //    public string AC_EMAIL { get; set; }

    //    /// <summary>
    //    /// 單位
    //    /// </summary>
    //    public string AC_CONTENT { get; set; }

    //    /// <summary>
    //    /// 圖片連結
    //    /// </summary>
    //    public string AC_IMG { get; set; }

    //    /// <summary>
    //    /// 顯示排序
    //    /// </summary>
    //    public int AC_ORDER { get; set; }

    //    /// <summary>
    //    /// 登入次數
    //    /// </summary>
    //    public int AC_VCOUNT { get; set; }

    //    /// <summary>
    //    /// 建立日期
    //    /// </summary>
    //    public DateTime AC_MDATE { get; set; }

    //    /// <summary>
    //    /// 編輯日期
    //    /// </summary>
    //    public DateTime? AC_EDATE { get; set; }

    //    /// <summary>
    //    /// 編輯人編號
    //    /// </summary>
    //    public string AC_EUSER { get; set; }

    //    /// <summary>
    //    /// 上三次密碼
    //    /// </summary>
    //    public string AC_PWD3 { get; set; }

    //    /// <summary>
    //    /// 上次更改密碼時間
    //    /// </summary>
    //    public DateTime? AC_UPPWD_DATE { get; set; }

    //    /// <summary>
    //    /// 是否刪除[0:是 1:否]
    //    /// </summary>
    //    public int AC_ISDEL { get; set; }

    //    /// <summary>
    //    /// 帳號備註
    //    /// </summary>
    //    public string AC_NOTES { get; set; }
    //}


    /// <summary>
    /// 擴充方法
    /// </summary>
    //public class ACCOUNT_EXTEND : ACCOUNT
    //{
    //    /// <summary>
    //    /// 新建帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT Insert(ACCOUNT model)
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            model.AC_IDNUM = new AES().Encryption(model.AC_IDNUM);
    //            var result = sqlSugar.Insertable(model).ExecuteCommand();
    //            model.AC_IDNUM = new AES().Decryption(model.AC_IDNUM);
    //            return result > 0 ? model : null;
    //        }
    //    }

    //    /// <summary>
    //    /// 取得單筆帳號
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <returns></returns>
    //    public ACCOUNT Get(ACCOUNT model)
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .WhereIF(model.AC_EMAIL != null, x => x.AC_EMAIL == model.AC_EMAIL)
    //                           .WhereIF(model.AC_ID != null, x => x.AC_ID == model.AC_ID)
    //                           .WhereIF(model.AC_IDNUM != null, x => x.AC_IDNUM == model.AC_IDNUM)
    //                           .Where(x => x.AC_ISDEL == 0)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 取得單筆帳號
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <returns></returns>
    //    public ACCOUNT GetByUserId(string _AC_USERID)
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            return sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == _AC_USERID)
    //                           .Where(x => x.AC_ISDEL == 0)
    //                           .Single();
    //        }
    //    }

    //    /// <summary>
    //    /// 取得通知AC_ID
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<string> GetUserIdForNotify()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            return sqlSugar.Queryable<ACCOUNT>()
    //                           .WhereIF(!string.IsNullOrEmpty(AC_GROUP_ID), x => x.AC_GROUP_ID == AC_GROUP_ID)
    //                           .Select(x => x.AC_ID)
    //                           .ToList();
    //        }
    //    }

    //    /// <summary>
    //    /// 取得通知帳號清單
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<ACCOUNT> GetUserIdForNotify2()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .WhereIF(!string.IsNullOrEmpty(AC_GROUP_ID), x => x.AC_GROUP_ID == AC_GROUP_ID)
    //                           .ToList();
    //            if (result != null)
    //            {
    //                foreach (var item in result)
    //                {
    //                    item.AC_IDNUM = new AES().Decryption(item.AC_IDNUM);
    //                }
    //            }
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 取得帳號列表
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<OUT_ACCOUNT_GetList> GetList()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            //return sqlSugar.Queryable<ACCOUNT>()
    //            //    .OrderBy(x => x.AC_MDATE)
    //            //    .Select(x => new OUT_ACCOUNT_GetList()
    //            //    {
    //            //        AC_CONTENT = x.AC_CONTENT,
    //            //        AC_GROUP_ID = x.AC_GROUP_ID,
    //            //        AC_ID = x.AC_ID,
    //            //        AC_STATUS = x.AC_STATUS,
    //            //        AC_TEL = x.AC_TEL,
    //            //        AC_TEL_EX = x.AC_TEL_EX,
    //            //        AC_TITLE = x.AC_TITLE,
    //            //        AC_USERID = x.AC_USERID,
    //            //        AC_USERNAME = x.AC_USERNAME,
    //            //        AC_USERTYPE = x.AC_USERTYPE,
    //            //        //AG_NAME = SqlFunc.Subqueryable<ACCOUNT_GROUP>().Where(s => s.AG_ID == x.AC_GROUP_ID).Select(s => s.AG_NAME)
    //            //    })

    //            //    .ToList();
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //               .OrderBy(x => x.AC_MDATE)
    //               .Where(x => x.AC_ISDEL == 0)
    //               .Select(x => new OUT_ACCOUNT_GetList())
    //               .ToList();

    //            if (result != null)
    //            {
    //                foreach (var item in result)
    //                {
    //                    var list = sqlSugar.Queryable<ACCOUNT_LOGIN>().Where(x => x.AL_ACID == item.AC_ID).First();

    //                    if (list != null)
    //                    {
    //                        item.AC_LASTSIGNINDATE = list.AL_MDATE;
    //                    }


    //                    item.AG_NAME = sqlSugar.Queryable<ACCOUNT_GROUP>().Where(s => s.AG_ID == item.AC_GROUP_ID).Single().AG_NAME;
    //                    item.AC_IDNUM = new AES().Decryption(item.AC_IDNUM);


    //                }
    //            }
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依照權限ID取得帳號列表
    //    /// </summary>
    //    /// <returns></returns>


    //    /// <summary>
    //    /// 取得目前上線帳號列表
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<OUT_ACCOUNT_GetList> GetListIsOnLine()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                .Where(x => x.AC_STATUS == 1)
    //                .Where(x => x.AC_ISDEL == 0)
    //                .Select(x => new OUT_ACCOUNT_GetList())
    //                           .ToList();
    //            if (result != null)
    //            {
    //                foreach (var item in result)
    //                {
    //                    item.AC_IDNUM = new AES().Decryption(item.AC_IDNUM);
    //                }
    //            }
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依帳密查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT Select()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == AC_USERID && x.AC_PWD == AC_PWD && AC_STATUS == 1 && AC_ISDEL == 0)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_USERID查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public OUT_ACCOUNT SelectByACUSERID()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == AC_USERID)
    //                           .Where(x => x.AC_ISDEL == 0)
    //                           .Select(x => new OUT_ACCOUNT())
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_USERID查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByACUSERID2()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == AC_USERID && x.AC_STATUS == 1)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_IDNUM查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByACIDNUM()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == AC_USERID && x.AC_STATUS == 1)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_ID查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByACID()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_ID == AC_ID && x.AC_STATUS == 1)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_ID查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public GetAccountInformation SelectByACIDToGetAccountInformation()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            return sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_ID == AC_ID && x.AC_STATUS == 1)
    //                           .Select(x => new GetAccountInformation())
    //                           .Single();
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_ID查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByACID2()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_ID == AC_ID)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_ID查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByIdAndType()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_ID == AC_ID && x.AC_USERTYPE == AC_USERTYPE)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_ID查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<ACCOUNT> SelectByACUSERTYPE()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERTYPE == AC_USERTYPE)
    //                           .ToList();
    //            if (result != null)
    //            {
    //                foreach (var item in result)
    //                {
    //                    item.AC_IDNUM = new AES().Decryption(item.AC_IDNUM);
    //                }
    //            }
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_ID + AC_PWD查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByIdAndPwd()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_ID == AC_ID && x.AC_PWD == AC_PWD)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_ID + AC_PWD查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByMail()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == AC_USERID)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 依AC_USERID + AC_EMAIL查詢帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<ACCOUNT> SelectMailList()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            return sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == AC_USERID)
    //                           .Where(x => x.AC_EMAIL == AC_EMAIL)
    //                           .ToList();
    //        }
    //    }

    //    /// <summary>
    //    /// 查詢帳號[忘記密碼]
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByGroup()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == AC_USERID && x.AC_STATUS == AC_STATUS && x.AC_USERTYPE == AC_USERTYPE && x.AC_BIRTHDAY == AC_BIRTHDAY)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 查詢帳號[忘記密碼]
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT SelectByAdmin()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .Where(x => x.AC_USERID == AC_USERID && x.AC_STATUS == AC_STATUS && x.AC_EMAIL == AC_EMAIL)
    //                           .Single();
    //            if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 查詢參數
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<ACCOUNT> SelectByParameters()
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            var result = sqlSugar.Queryable<ACCOUNT>()
    //                           .WhereIF(!string.IsNullOrEmpty(AC_USERID), x => x.AC_USERID.Contains(AC_USERID))
    //                           .WhereIF(!string.IsNullOrEmpty(AC_USERNAME), x => x.AC_USERNAME.Contains(AC_USERNAME))
    //                           .WhereIF(!string.IsNullOrEmpty(AC_USERTYPE), x => x.AC_USERTYPE == AC_USERTYPE)
    //                           .WhereIF(AC_GROUP_ID != null, x => x.AC_GROUP_ID == AC_GROUP_ID)
    //                           .WhereIF(!string.IsNullOrEmpty(AC_CITYID), x => x.AC_CITYID == AC_CITYID)
    //                           .WhereIF(!string.IsNullOrEmpty(AC_TOWNID), x => x.AC_TOWNID == AC_TOWNID)
    //                           .WhereIF(AC_STATUS != null, x => x.AC_STATUS == AC_STATUS)
    //                           .Where(x => x.AC_STATUS == AC_STATUS)
    //                           .ToList();
    //            if (result != null)
    //            {
    //                foreach (var item in result)
    //                {
    //                    item.AC_IDNUM = new AES().Decryption(item.AC_IDNUM);
    //                }
    //            }
    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// 更新帳號資料
    //    /// </summary>
    //    /// <param name="account"></param>
    //    /// <returns></returns>
    //    public ACCOUNT UpdateExecute(ACCOUNT model)
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            model.AC_IDNUM = new AES().Encryption(model.AC_IDNUM);
    //            var result = sqlSugar.Updateable(model).ExecuteCommand();
    //            model.AC_IDNUM = new AES().Decryption(model.AC_IDNUM);
    //            return result > 0 ? model : null;
    //        }
    //    }

    //    /// <summary>
    //    /// 更新帳號資料
    //    /// </summary>
    //    /// <param name="account"></param>
    //    /// <returns></returns>
    //    public List<ACCOUNT> UpdateExecute(List<ACCOUNT> model)
    //    {
    //        using (var sqlSugar = CustomizeSqlSugar.GetInstance())
    //        {
    //            foreach (var item in model)
    //            {
    //                item.AC_IDNUM = new AES().Encryption(item.AC_IDNUM);
    //            }
    //            var result = sqlSugar.Updateable(model).ExecuteCommand();
    //            return result > 0 ? model : null;
    //        }
    //    }

    //    /// <summary>
    //    /// 刪除帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT Delete()
    //    {
    //        ACCOUNT account = SelectByACID2();
    //        account.AC_STATUS = 3;
    //        return UpdateExecute(account);
    //    }

    //    /// <summary>
    //    /// 禁用帳號
    //    /// </summary>
    //    /// <returns></returns>
    //    public ACCOUNT Delete2()
    //    {
    //        ACCOUNT account = SelectByACID2();
    //        account.AC_STATUS = 2;
    //        return UpdateExecute(account);
    //    }

    //    /// <summary>
    //    /// 修改密碼
    //    /// </summary>
    //    /// <returns></returns>
    //    public int UpdateChangePwd(ACCOUNT account)
    //    {
    //        account.AC_PWD = AC_PWD;
    //        return UpdateExecute(account) != null ? 1 : 0;
    //    }
    //}

    #endregion

    /// <summary>
    /// 新增帳號 INPUT class
    /// </summary>
    public class InsertAccount : BaseModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public int? AC_STATUS { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string AC_PWD { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }
    }

    /// <summary>
    /// 新增帳號 INPUT class
    /// </summary>
    public class InsertAccount2
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public int? AC_STATUS { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string AC_PWD { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InsertAccountBath : BaseModel
    {
        public List<InsertAccount> InsertAccount { get; set; }
    }

    /// <summary>
    /// 編輯帳號 INPUT class
    /// </summary>
    public class UpdateAccount : BaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public int? AC_STATUS { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }


    }

    /// <summary>
    /// 管理帳號資訊 INPUT class
    /// </summary>
    public class GetAdminInformationRequestBody : BaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }

        /// <summary>
        /// 查詢人員
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OUT_ADMIN_INFO Get()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var data1 = sqlSugar.Queryable<ACCOUNT>()
                               .WhereIF(string.IsNullOrEmpty(AC_ID) == false, x => x.AC_ID == AC_ID)
                               .WhereIF(string.IsNullOrEmpty(AC_EMAIL) == false, x => x.AC_EMAIL == AC_EMAIL)
                               .Select(x => new OUT_ADMIN_INFO())
                               .ToList();
                if (data1.Count == 0)
                {
                    return null;
                }
                else
                {
                    return data1[0];
                }

            }
        }
    }

    /// <summary>
    /// 新增帳號 INPUT class
    /// </summary>
    public class InsertAdminRequestBody : BaseModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        public string AC_IDNUM { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? AC_BIRTHDAY { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string AC_PWD { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }
    }

    /// <summary>
    /// 刪除管理帳號 INPUT class
    /// </summary>
    public class DeleteAdminRequestBody : BaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 查詢人員列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ACCOUNT Get(string _AC_ID)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT>()
                               .Where(x => x.AC_ID == _AC_ID)
                               .Single();
            }
        }

        /// <summary>
        /// 刪除管理帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Delete()
        {
            string msg = "";

            ACCOUNT_EXTEND extend = new ACCOUNT_EXTEND();
            ACCOUNT model = Get(AC_ID);

            if (model != null)
            {
                model.AC_ISDEL = 1;

                if (extend.UpdateExecute(model) != null)
                    msg = "刪除成功";
                else
                    msg = "刪除失敗";
            }
            else
                msg = "查無帳號";

            return msg;
        }
    }

    public class UpdateAdminRequestBody : BaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public int? AC_STATUS { get; set; }

        /// <summary>
        /// 帳號備註
        /// </summary>
        public string AC_NOTES { get; set; }

        /// <summary>
        /// 帳號權限
        /// </summary>
        public string AC_PJID { get; set; }

        /// <summary>
        /// 查詢人員列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ACCOUNT Get()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Queryable<ACCOUNT>()
                               .Where(x => x.AC_ID == AC_ID)
                               .Single();
                if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);

                return result;
            }
        }

        /// <summary>
        /// 查詢人員列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Update(ACCOUNT model)
        {
            string msg = "";
            ACCOUNT_EXTEND extend = new ACCOUNT_EXTEND();


            if (extend.UpdateExecute(model) != null)
                msg = "更新成功";
            else
                msg = "更新失敗";

            return msg;
        }
    }


    /// <summary>
    /// 帳號資料維護 修改密碼 用INPUT class 
    /// </summary>
    public class UpdateAccountPassword : BaseModel
    {
        /// <summary>
        /// 新密碼
        /// </summary>
        public string DecryptionPass { get; set; }

        /// <summary>
        /// 舊密碼
        /// </summary>
        public string DecryptionPass_O { get; set; }


    }


    /// <summary>
    /// 修改帳號資訊
    /// </summary>
    public class UpdateAccountInfo : BaseModel
    {
        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 性別[0:男 1:女]
        /// </summary>
        public byte? AC_SEX { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? AC_BIRTHDAY { get; set; }

        /// <summary>
        /// 聯絡人地址
        /// </summary>
        public string AC_ADDRESS { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        public string AC_MOBILE { get; set; }

        /// <summary>
        /// 編輯日期
        /// </summary>
        public DateTime? AC_EDATE { get; set; }

        /// <summary>
        /// 編輯人編號
        /// </summary>
        public string AC_EUSER { get; set; }

        /// <summary>
        /// 聯絡人縣市
        /// </summary>
        public string AC_CITYID { get; set; }

        /// <summary>
        /// 聯絡人鄉鎮
        /// </summary>
        public string AC_TOWNID { get; set; }

        /// <summary>
        /// 查詢人員列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Update(ACCOUNT model)
        {
            string msg = "";
            ACCOUNT_EXTEND extend = new ACCOUNT_EXTEND();

            if (extend.UpdateExecute(model) != null)
                msg = "更新成功";
            else
                msg = "更新失敗";

            return msg;
        }
    }

    /// <summary>
    /// 編輯自己帳號 INPUT class
    /// </summary>
    public class UpdateMyAccount : BaseModel
    {
        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }
    }

    /// <summary>
    /// 取得自己帳號資訊 INPUT class
    /// </summary>
    public class GetMyAccount : BaseModel
    {
    }

    /// <summary>
    /// 取得自己帳號資訊 INPUT class
    /// </summary>
    public class GetAccountLog : BaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }
    }

    /// <summary>
    /// 查詢歷史紀錄 INPUT class
    /// </summary>
    public class GetAccountLogDetail : BaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public DateTime? AC_STIME { get; set; }

        /// <summary>
        /// 帳號編號
        /// </summary>
        public DateTime? AC_ETIME { get; set; }

        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_WORD { get; set; }
    }

    /// <summary>
    /// 取得帳號資訊 INPUT class
    /// </summary>
    public class GetAccountInformationINPUT : BaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }
    }

    public class OUT_ACCOUNT
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 帳號類別[會員身份]
        /// </summary>
        public string AC_USERTYPE { get; set; }

        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public int? AC_STATUS { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 附加類別
        /// </summary>
        public string AC_KEYID { get; set; }

        /// <summary>
        /// 性別[0:男 1:女]
        /// </summary>
        public string AC_SEX { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        public string AC_IDNUM { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime AC_BIRTHDAY { get; set; }

        /// <summary>
        /// 聯絡人
        /// </summary>
        public string AC_CONTACT { get; set; }

        /// <summary>
        /// 聯絡人郵遞區號
        /// </summary>
        public string AC_POST { get; set; }

        /// <summary>
        /// 聯絡人縣市
        /// </summary>
        public string AC_CITYID { get; set; }

        /// <summary>
        /// 聯絡人鄉鎮
        /// </summary>
        public string AC_TOWNID { get; set; }

        /// <summary>
        /// 聯絡人地址
        /// </summary>
        public string AC_ADDRESS { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        public string AC_MOBILE { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        public string AC_FAX { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }

        /// <summary>
        /// 帳號介紹
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 圖片連結
        /// </summary>
        public string AC_IMG { get; set; }

        /// <summary>
        /// 顯示排序
        /// </summary>
        public string AC_ORDER { get; set; }

        /// <summary>
        /// 登入次數
        /// </summary>
        public int AC_VCOUNT { get; set; }

    }

    public class OUT_GetMyAccount
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AG_NAME { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        public string AC_IDNUM { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }
    }

    /// <summary>
    /// 輸出帳號列表用
    /// </summary>
    public class OUT_ACCOUNT_GetList
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 帳號類別[會員身份]
        /// </summary>
        public string AC_USERTYPE { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        public string AC_IDNUM { get; set; }

        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public int? AC_STATUS { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AC_GROUP_NAME { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AG_NAME { get; set; }

        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime AC_MDATE { get; set; }

        /// <summary>
        /// 最後登入時間
        /// </summary>
        public DateTime AC_LASTSIGNINDATE { get; set; }

        /// <summary>
        /// 部門名稱
        /// </summary>
        public string AC_ORGNAME { get; set; }

        /// <summary>
        /// 離退單位
        /// </summary>
        public string AC_ACTUALSERVICEUNIT { get; set; }
    }

    /// <summary>
    /// 輸出管理帳號
    /// </summary>
    public class OUT_ADMIN_INFO
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string AC_USERID { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }

        /// <summary>
        /// 帳號類別[會員身份]
        /// </summary>
        public string AC_USERTYPE { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        public string AC_IDNUM { get; set; }

        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public int? AC_STATUS { get; set; }

        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AC_GROUP_NAME { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }

        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string AG_NAME { get; set; }

        /// <summary>
        /// 帳號備註
        /// </summary>
        public string AC_NOTES { get; set; }

        /// <summary>
        /// 帳號權限
        /// </summary>
        public string AC_PJID { get; set; }
    }

    /// <summary>
    /// 查詢名稱跟電話
    /// </summary>
    public class GetAccountInformation
    {
        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }

        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string AC_USERNAME { get; set; }
    }


    public class AccountBaseModel : BaseModel
    {
        /// <summary>
        /// 身分證號 (std_idno, emp_idno)
        /// </summary>
        public string PER_IDENTITY { get; set; }

        /// <summary>
        /// 查詢人員列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ACCOUNT> GetList()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT>()
                               .Where(x => x.AC_IDNUM == PER_IDENTITY)
                               .Where(x => x.AC_ISDEL == 0)
                               .Select(x => new ACCOUNT()
                               {
                                   AC_ID = x.AC_ID,
                                   AC_IDNUM = x.AC_IDNUM,
                                   AC_NOTES = x.AC_NOTES,
                                   AC_STATUS = x.AC_STATUS
                               })
                               .ToList();
            }
        }

        /// <summary>
        /// 查詢人員列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ACCOUNT Get(string _AC_ID)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT>()
                               .Where(x => x.AC_ID == _AC_ID)
                               .Single();
            }
        }
    }

    public class UpdateAccountRequestBody : AccountBaseModel
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public byte AC_STATUS { get; set; }

        /// <summary>
        /// 帳號備註
        /// </summary>
        public string AC_NOTES { get; set; }

        /// <summary>
        /// 查詢人員列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Update()
        {
            string msg = "";

            ACCOUNT_EXTEND extend = new ACCOUNT_EXTEND();
            ACCOUNT model = Get(AC_ID);

            if (model != null)
            {
                model.AC_STATUS = AC_STATUS;
                model.AC_NOTES = AC_NOTES;

                if (extend.UpdateExecute(model) != null)
                    msg = "更新成功";
                else
                    msg = "更新失敗";
            }
            else
                msg = "查無帳號";

            return msg;
        }
    }

    public class DeleteAccountRequestBody : AccountBaseModel
    {
        /// <summary>
        /// 人員ID
        /// </summary>
        public string PER_ID { get; set; }

        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }

        /// <summary>
        /// 查詢人員列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Delete()
        {
            string msg = "";

            ACCOUNT_EXTEND extend = new ACCOUNT_EXTEND();
            ACCOUNT model = Get(AC_ID);

            if (model != null)
            {
                model.AC_ISDEL = 1;

                if (extend.UpdateExecute(model) != null)
                    msg = "刪除成功";
                else
                    msg = "刪除失敗";
            }
            else
                msg = "查無帳號";

            return msg;
        }
    }

    public class GetMailRequestBody : BaseModel
    {
        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }

        public ACCOUNT Get()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT>()
                               .Where(x => x.AC_EMAIL == AC_EMAIL)
                               .Where(x => x.AC_ISDEL == 0)
                               .Where(x => x.AC_STATUS == 1)
                               .Single();
            }
        }
    }

    public class MailLoginRequestBody : BaseModel
    {
        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }
        public string ID { get; set; }
        public string TYPE { get; set; }

        public ACCOUNT Get()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                return sqlSugar.Queryable<ACCOUNT>()
                               .Where(x => x.AC_EMAIL == AC_EMAIL)
                               .Where(x => x.AC_ISDEL == 0)
                               .Where(x => x.AC_STATUS == 1)
                               .Single();
            }
        }
    }
}