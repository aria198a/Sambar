using Microsoft.Extensions.Configuration;
using NLog;
using SqlSugar;
using System.Configuration;

namespace Library.Functions
{
    public class CustomizeSqlSugar
    {
        //private static string ConnectionString = new AES().Decryption(ConfigurationManager.ConnectionStrings["DefaultDataSource"].ConnectionString);
        //private static string ConnectionString = "Data Source=140.134.48.75;Initial Catalog=RMS;Persist Security Info=True;User ID=se;Password=se!";
        //private static string ConnectionString = "Data Source=140.134.48.75;Initial Catalog=RMS;Persist Security Info=True;User ID=se;Password=se!";
        private static string ConnectionString = "Data Source=FCUGISSTA-26937\\SQLEXPRESS;Initial Catalog=RMS;Persist Security Info=True;User ID=gis;Password=se!";
        //private static string PostgreSQLConnectionString = "PORT=5432;DATABASE=postgres;HOST=127.0.0.1;USER ID=thomaslin";
        private static string PostgreSQLConnectionString = "PORT=5455;DATABASE=00data_newest;HOST=103.124.72.11;PASSWORD=GIS@fcu@2022;USER ID=fcugis";
        protected static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 連線DB
        /// </summary>
        /// <param name="usingMasterSlave"></param>
        /// <returns></returns>
        public static SqlSugarClient GetInstance(bool usingMasterSlave = false)
        {
            SqlSugarClient db = new SqlSugarClient(
              new ConnectionConfig()
              {
                  ConnectionString = ConnectionString,
                  DbType = DbType.SqlServer,
                  IsAutoCloseConnection = true,
                  InitKeyType = InitKeyType.SystemTable, 
              });

            db.Aop.OnError = (exp) =>
            {
                //logger.Fatal(exp);
                new NLog().LogInformation(exp);
            };

            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                var execTime = (int)db.Ado.SqlExecutionTime.TotalMilliseconds;
                //logger.Warn("DAO WarningExecTime runtime = {0}ms, Sql = {1}", execTime, sql);
                new NLog().LogInformation(System.String.Format("DAO WarningExecTime runtime = {0}ms, Sql = {1}", execTime, sql));
                //Log.SetLog(execTime);
            };

            return db;
        }


        /// <summary>
        /// 連線DB
        /// </summary>
        /// <param name="usingMasterSlave"></param>
        /// <returns></returns>
        public static SqlSugarClient GetPostgreSQLInstance(bool usingMasterSlave = false)
        {
            SqlSugarClient db = new SqlSugarClient(
              new ConnectionConfig()
              {
                  ConnectionString = PostgreSQLConnectionString,
                  DbType = DbType.PostgreSQL,
                  IsAutoCloseConnection = true,
                  InitKeyType = InitKeyType.SystemTable, 
                  MoreSettings = new ConnMoreSettings()
                  {
                      PgSqlIsAutoToLower = false 
                  },
              });

            db.Aop.OnError = (exp) =>
            {
                //logger.Fatal(exp);
                new NLog().LogInformation(exp);
            };

            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                var execTime = (int)db.Ado.SqlExecutionTime.TotalMilliseconds;
                //logger.Warn("DAO WarningExecTime runtime = {0}ms, Sql = {1}", execTime, sql);
                new NLog().LogInformation(System.String.Format("DAO WarningExecTime runtime = {0}ms, Sql = {1}", execTime, sql));
                //Log.SetLog(execTime);
            };

            return db;
        }

    }
}
