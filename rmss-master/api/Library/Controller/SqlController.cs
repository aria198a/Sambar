using Library.Functions;
using System.Configuration;
using System.Data.SqlClient;

namespace Library.Controller
{
    public class SqlController
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        private string ConnectionString { get => new AES().Decryption(ConfigurationManager.ConnectionStrings["DefaultDataSource"].ConnectionString); }

        /// <summary>
        /// 建構子
        /// </summary>
        public SqlController()
        {
            DataSource.SetConnectionString(ConnectionString);
        }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="conn"></param>
        public SqlController(string conn)
        {
            DataSource.SetConnectionString(conn);
        }
        /// <summary>
        /// 取得路徑
        /// </summary>
        /// <returns></returns>
        //public string GetFilePathDefault()
        //{
        //    SqlCommand cmd = new SqlCommand(@"
        //        SELECT SP_PATH FROM STORAGE_POOL
        //        WHERE SP_ISONLINE = 1");

        //    return DataSource.ScalarExample(cmd);
        //}

    }
}
