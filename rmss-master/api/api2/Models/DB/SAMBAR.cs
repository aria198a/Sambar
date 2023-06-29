using Library.Functions;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api2.Models.DB
{
    /// <summary>
    /// 水鹿類別
    /// </summary>
    public class SAMBAR
    {
        /// <summary>
        /// 水鹿的ID
        /// </summary>
        public string SB_ID { get; set; }

        /// <summary>
        /// 水鹿名字
        /// </summary>
        public string SB_NAME { get; set; }

        /// <summary>
        /// 水鹿的年齡
        /// </summary>
        public string SB_AGE { get; set; }

        /// <summary>
        /// 水鹿的性別
        /// </summary>
        public string SB_GENDER { get; set; }

        /// <summary>
        /// 水鹿的毛色
        /// </summary>
        public string SB_COLOR { get; set; }

        /// <summary>
        /// 水鹿的棲息地
        /// </summary>
        public string SB_HABITAT { get; set; }

        /// <summary>
        /// 水鹿的健康狀態
        /// </summary>
        public string SB_HEALTH { get; set; }

        /// <summary>
        /// 水鹿的照片
        /// </summary>
        public string SB_PICTURE { get; set; }

        /// <summary>
        /// 水鹿的資料建立的日期
        /// </summary>
        public DateTime SB_CREATEDATE { get; set; }

        /// <summary>
        /// 水鹿的更新資料的日期
        /// </summary>
        public DateTime SB_UPDATEDATE { get; set; }

        /// <summary>
        /// 水鹿的鹿場編號
        /// </summary>
        public string SB_FARMID { get; set; }
    }

    /// <summary>
    /// 擴充方法
    /// </summary>
    public class SAMBAR_EXTEND : SAMBAR
    {
        /// <summary>
        /// 紀錄
        /// </summary>
        /// <param name="function">API</param>
        /// <param name="ip">IP</param>
        /// <param name="account">帳號</param>
        public void Sambar(string function, string ip, ACCOUNT account = null)
        {
            try
            {
                SAMBAR model = new SAMBAR();
                model.SB_ID = "SB" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.SB_NAME = account == null ? "" : account.AC_USERNAME;
                model.SB_AGE = account == null ? "" : account.AC_USERAGE;
                model.SB_GENDER = account == null ? "" : account.AC_USERGENDER;
                model.SB_COLOR = account == null ? "" : account.AC_USERCOLOR;
                model.SB_HABITAT = account == null ? "" : account.AC_USERHABITAT;
                model.SB_HEALTH = account == null ? "" : account.AC_USERHEALTH;
                model.SB_PICTURE = account == null ? "" : account.AC_USERPICTURE;
                model.SB_CREATEDATE = DateTime.Now;
                model.SB_UPDATEDATE = DateTime.Now;
                model.SB_FARMID = account == null ? "" : account.AC_USERFARMID;
                Insert(model);
            }
            catch (Exception ex)
            {
                new Library.Functions.NLog().LogInformation(ex);
            }
        }
    }
}