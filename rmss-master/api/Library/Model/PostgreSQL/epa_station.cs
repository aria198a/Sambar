using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.PostgreSQL
{
    /// <summary>
    /// 國家空品測站_測站資訊
    /// </summary>
    public class epa_station
    {

        /// <summary>
        /// 測站編號
        /// </summary>
        public string station_id { get; set; }
        
        /// <summary>
        /// 測站名稱
        /// </summary>
        public string SiteName { get; set; }
        
        /// <summary>
        /// 測站英文名稱
        /// </summary>
        public string SiteEngName { get; set; }
        
        /// <summary>
        /// 測站空品區
        /// </summary>
        public string AreaName { get; set; }
        
        /// <summary>
        /// 縣市
        /// </summary>
        public string County { get; set; }
        
        /// <summary>
        /// 空氣品質測站所在之鄉鎮
        /// </summary>
        public string Township { get; set; }
        
        /// <summary>
        /// 空氣品質測站之地址
        /// </summary>
        public string SiteAddress { get; set; }
        
        /// <summary>
        /// 空氣品質測站之經度
        /// </summary>
        public Decimal? TWD97Lon { get; set; }
        
        /// <summary>
        /// 空氣品質測站之緯度
        /// </summary>
        public Decimal? TWD97Lat { get; set; }
        
        /// <summary>
        /// 測站類型
        /// </summary>
        public string SiteType { get; set; }


        //=========================================================================================================
        public List<epa_station> GetList()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<epa_station>()
                               .ToList();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public epa_station InsertExecute(epa_station model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }


        /// <summary>
        /// 批次新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertExecute(List<epa_station> model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Insertable(model).ExecuteCommand();
                return result;
            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public epa_station UpdateExecute(epa_station model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

        /// <summary>
        /// 批次更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateExecute(List<epa_station> model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Updateable(model).ExecuteCommand();
                return result;
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public epa_station DeleteExecute(epa_station model)
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Deleteable(model).ExecuteCommand();
                return result > 0 ? model : null;
            }
        }

        /// <summary>
        /// 批次刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteExecute(List<epa_station> model)
        {

            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                var result = sqlSugar.Deleteable(model).ExecuteCommand();
                return result;
            }
        }
        //=========================================================================================================
    }
}
