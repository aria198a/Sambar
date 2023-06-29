using Library.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library6.Model.PostgreSQL
{
    public class epa_newest // Missing PM2.5 and PM2.5_AVG fields
    {
        /// <summary>
        /// 測站名稱
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 縣市
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 空氣品質指標
        /// </summary>
        public int? AQI { get; set; }

        /// <summary>
        /// 空氣污染指標物
        /// </summary>
        public string Pollutant { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 二氧化硫(ppb)
        /// </summary>
        public string SO2 { get; set; }

        /// <summary>
        /// 一氧化碳(ppm)
        /// </summary>
        public string CO { get; set; }

        /// <summary>
        /// 一氧化碳8小時移動平均(ppm)
        /// </summary>
        public string CO_8hr { get; set; }

        /// <summary>
        /// 臭氧(ppb)
        /// </summary>
        public string O3 { get; set; }

        /// <summary>
        /// 臭氧8小時移動平均(ppb)
        /// </summary>
        public string O3_8hr { get; set; }

        /// <summary>
        /// 懸浮微粒(μg/m3)
        /// </summary>
        public string PM10 { get; set; }

        /// <summary>
        /// 懸浮微粒移動平均值(μg/m3)
        /// </summary>
        public string PM10_AVG { get; set; }

        /// <summary>
        /// 二氧化氮(ppb)
        /// </summary>
        public string NO2 { get; set; }

        /// <summary>
        /// 氮氧化物(ppb)
        /// </summary>
        public string NOx { get; set; }

        /// <summary>
        /// 一氧化氮(ppb)
        /// </summary>
        public string NO { get; set; }

        /// <summary>
        /// 風速(m/sec)
        /// </summary>
        public string WindSpeed { get; set; }

        /// <summary>
        /// 風向(degrees)
        /// </summary>
        public string WindDirec { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        //public string PublishTime { get; set; }

        /// <summary>
        /// 二氧化硫移動平均值(ppb)
        /// </summary>
        public string SO2_AVG { get; set; }

        /// <summary>
        /// 經度
        /// </summary>
        //public string Longitude { get; set; }

        /// <summary>
        /// 緯度
        /// </summary>
        //public string Latitude { get; set; }

        /// <summary>
        /// 測站編號
        /// </summary>
        public int? SiteId { get; set; }

        //=========================================================================================================

        public List<epa_newest> Get()
        {
            using (var sqlSugar = CustomizeSqlSugar.GetPostgreSQLInstance())
            {
                return sqlSugar.Queryable<epa_newest>()
                    .ToList();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public epa_newest InsertExecute(epa_newest model)
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
        public int InsertExecute(List<epa_newest> model)
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
        public epa_newest? UpdateExecute(epa_newest? model)
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
        public int UpdateExecute(List<epa_newest> model)
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
        public epa_newest DeleteExecute(epa_newest model)
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
        public int DeleteExecute(List<epa_newest> model)
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