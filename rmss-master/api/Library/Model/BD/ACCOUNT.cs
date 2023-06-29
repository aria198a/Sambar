using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.BD
{
    /// <summary>
    /// 帳號表
    /// </summary>
    public class ACCOUNT
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
        /// 密碼
        /// </summary>
        public string AC_PWD { get; set; }
        /// <summary>
        /// 帳號類別
        /// </summary>
        public string AC_USERTYPE { get; set; }
        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public byte AC_STATUS { get; set; }
        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }
        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }
        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }
        /// <summary>
        /// 附加類別
        /// </summary>
        public string AC_KEYID { get; set; }
        /// <summary>
        /// 性別[0:男 1:女]
        /// </summary>
        public byte? AC_SEX { get; set; }
        /// <summary>
        /// 身分證字號
        /// </summary>
        public string AC_IDNUM { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? AC_BIRTHDAY { get; set; }
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
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }
        /// <summary>
        /// 圖片連結
        /// </summary>
        public string AC_IMG { get; set; }
        /// <summary>
        /// 顯示排序
        /// </summary>
        public int AC_ORDER { get; set; }
        /// <summary>
        /// 登入次數
        /// </summary>
        public int AC_VCOUNT { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime AC_MDATE { get; set; }
        /// <summary>
        /// 編輯日期
        /// </summary>
        public DateTime? AC_EDATE { get; set; }
        /// <summary>
        /// 編輯人編號
        /// </summary>
        public string AC_EUSER { get; set; }
        /// <summary>
        /// 上三次密碼
        /// </summary>
        public string AC_PWD3 { get; set; }
        /// <summary>
        /// 上次更改密碼時間
        /// </summary>
        public DateTime? AC_UPPWD_DATE { get; set; }
        /// <summary>
        /// 是否刪除[0:是 1:否]
        /// </summary>
        public int AC_ISDEL { get; set; }
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
    /// 擴充方法
    /// </summary>
    public class ACCOUNT_EXTEND : ACCOUNT
    {
        /// <summary>
        /// 取得單筆帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ACCOUNT Get(ACCOUNT model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Queryable<ACCOUNT>()
                                     .WhereIF(model.AC_ID != null, x => x.AC_ID == model.AC_ID)
                                     .WhereIF(model.AC_USERID != null, x => x.AC_USERID == model.AC_USERID)
                                     .WhereIF(model.AC_EMAIL != null, x => x.AC_EMAIL == model.AC_EMAIL)
                                     .WhereIF(model.AC_IDNUM != null, x => x.AC_IDNUM == model.AC_IDNUM)
                                     .Where(x => x.AC_ISDEL == 0)
                                     .Single();

                if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);

                return result;
            }
        }
        /// <summary>
        /// 取得單筆帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ACCOUNT> GetList(ACCOUNT model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Queryable<ACCOUNT>()
                                     .Where(x => x.AC_ISDEL == 0)
                                     .OrderBy(x => x.AC_MDATE)
                                     .ToList();

                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        item.AC_IDNUM = new AES().Decryption(item.AC_IDNUM);
                    }
                }

                return result;
            }
        }
        /// <summary>
        /// 取得帳號列表 ()
        /// </summary>
        /// <returns></returns>
        public List<OUT_ACCOUNT_GetList> GetListByGroupId()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {

                var result = sqlSugar.Queryable<ACCOUNT>()
                                     .OrderBy(x => x.AC_MDATE)
                                     .Where(x => x.AC_ISDEL == 0)
                                     .Select(x => new OUT_ACCOUNT_GetList())
                                     .ToList();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        var list = sqlSugar.Queryable<ACCOUNT_LOGIN>().Where(x => x.AL_ACID == item.AC_ID).First();
                        if (list != null)
                        {
                            item.AC_LASTSIGNINDATE = list.AL_MDATE;
                        }

                        var group = sqlSugar.Queryable<ACCOUNT_GROUP>().Where(s => s.AG_ID == item.AC_GROUP_ID).Single();
                        if (group != null)
                        {
                            item.AG_NAME = sqlSugar.Queryable<ACCOUNT_GROUP>().Where(s => s.AG_ID == item.AC_GROUP_ID).Single().AG_NAME;
                        }

                        item.AC_IDNUM = new AES().Decryption(item.AC_IDNUM);
                    }
                }

                return result;
            }
        }
        /// <summary>
        /// 依AC_ID + AC_PWD查詢帳號
        /// </summary>
        /// <returns></returns>
        public ACCOUNT SelectByIdAndPwd()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Queryable<ACCOUNT>()
                                     .Where(x => x.AC_ID == AC_ID && x.AC_PWD == AC_PWD)
                                     .Single();

                if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);

                return result;
            }
        }
        /// <summary>
        /// 依AC_ID + AC_PWD查詢帳號
        /// </summary>
        /// <returns></returns>
        public ACCOUNT SelectByMail()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                var result = sqlSugar.Queryable<ACCOUNT>()
                                     .Where(x => x.AC_USERID == AC_USERID)
                                     .Single();

                if (result != null) result.AC_IDNUM = new AES().Decryption(result.AC_IDNUM);

                return result;
            }
        }
        /// <summary>
        /// 新建帳號
        /// </summary>
        /// <returns></returns>
        public ACCOUNT InsertExecute(ACCOUNT model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                model.AC_IDNUM = new AES().Encryption(model.AC_IDNUM);

                var result = sqlSugar.Insertable(model).ExecuteCommand();

                model.AC_IDNUM = new AES().Decryption(model.AC_IDNUM);

                return result > 0 ? model : null;
            }
        }
        /// <summary>
        /// 更新帳號資料 (單筆)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ACCOUNT UpdateExecute(ACCOUNT model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                model.AC_IDNUM = new AES().Encryption(model.AC_IDNUM);

                var result = sqlSugar.Updateable(model).ExecuteCommand();

                model.AC_IDNUM = new AES().Decryption(model.AC_IDNUM);

                return result > 0 ? model : null;
            }
        }
        /// <summary>
        /// 更新帳號資料 (多筆)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public List<ACCOUNT> UpdateExecute(List<ACCOUNT> model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetInstance())
            {
                foreach (var item in model)
                {
                    item.AC_IDNUM = new AES().Encryption(item.AC_IDNUM);
                }

                var result = sqlSugar.Updateable(model).ExecuteCommand();

                foreach (var item in model)
                {
                    item.AC_IDNUM = new AES().Decryption(item.AC_IDNUM);
                }

                return result > 0 ? model : null;
            }
        }
    }
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
        /// 密碼
        /// </summary>
        public string AC_PWD { get; set; }
        /// <summary>
        /// 啟用狀態[0:已註冊未開通 1:啟用 2:停用]
        /// </summary>
        public int? AC_STATUS { get; set; }
        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_GROUP_ID { get; set; }
        /// <summary>
        /// 權限群組
        /// </summary>
        public string AC_PJID { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        public string AC_EMAIL { get; set; }
        /// <summary>
        /// 身分證字號
        /// </summary>
        public string AC_IDNUM { get; set; }
        /// <summary>
        /// 性別[0:男 1:女]
        /// </summary>
        public byte? AC_SEX { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public string AC_TEL { get; set; }
        /// <summary>
        /// 分機
        /// </summary>
        public string AC_TEL_EX { get; set; }
        /// <summary>
        /// 手機
        /// </summary>
        public string AC_MOBILE { get; set; }
        /// <summary>
        /// 傳真
        /// </summary>
        public string AC_FAX { get; set; }
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
        /// 單位
        /// </summary>
        public string AC_CONTENT { get; set; }
        /// <summary>
        /// 職稱
        /// </summary>
        public string AC_TITLE { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? AC_BIRTHDAY { get; set; }
        /// <summary>
        /// 附加類別
        /// </summary>
        public string AC_KEYID { get; set; }
        /// <summary>
        /// 圖片連結
        /// </summary>
        public string AC_IMG { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string AC_NOTES { get; set; }
    }
    /// <summary>
    /// 編輯帳號 INPUT class
    /// </summary>
    public class UpdateAccount : InsertAccount
    {
        /// <summary>
        /// 帳號編號
        /// </summary>
        public string AC_ID { get; set; }
    }
    /// <summary>
    /// 輸出帳號列表用 (含群組名稱)
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

        /// <summary>
        /// 帳號權限
        /// </summary>
        public string AC_PJID { get; set; }
    }
}