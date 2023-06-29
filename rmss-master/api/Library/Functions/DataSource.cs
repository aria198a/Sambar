using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Library.Functions
{
    public class DataSource
    {
        private static string ConnectionString = new AES().Decryption(ConfigurationManager.ConnectionStrings["DefaultDataSource"].ConnectionString);

        //設定連結位置
        public static void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        ///// <summary>
        ///// DataReader
        ///// </summary>
        ///// <param name="cmd"></param>
        //public static void DataReaderExample(SqlCommand cmd)
        //{
        //    LogCommand(cmd);
        //    SqlDataReader dr = null;

        //    try
        //    {
        //        cmd.Connection = new SqlConnection(ConnectionString);
        //        cmd.Connection.Open(); //開啟資料庫連線
        //        dr = cmd.ExecuteReader(); //執行命令

        //        while (dr.Read())
        //        {
        //            string aa = dr["xxx"].ToString();
        //        }
        //    }
        //    catch (Exception ex) 
        //    {
        //        new NLog().LogInformation(cmd.CommandText + Environment.NewLine + ex);
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            dr.Close();
        //        }

        //        cmd.Parameters.Clear();
        //        cmd.Connection.Close();
        //    }
        //}

        ///// <summary>
        ///// DataTable
        ///// </summary>
        ///// <param name="cmd"></param>
        ///// <returns></returns>
        //public static DataTable DataTableExample(SqlCommand cmd)
        //{
        //    LogCommand(cmd);
        //    SqlDataAdapter da = new SqlDataAdapter(); //宣告一個配接器(DataTable與DataSet必須)
        //    DataTable dt = new DataTable(); //宣告DataTable物件
            
        //    try
        //    {
        //        cmd.Connection = new SqlConnection(ConnectionString);
        //        cmd.Connection.Open(); //開啟資料庫連線

        //        da.SelectCommand = cmd; //執行
        //        da.Fill(dt); //結果存放至DataTable

        //        cmd.Connection.Close(); //關閉連線
        //    }
        //    catch (Exception ex) 
        //    {
        //        new NLog().LogInformation(cmd.CommandText + Environment.NewLine + ex);
        //    }
        //    finally
        //    {
        //        cmd.Parameters.Clear();

        //        if (cmd.Connection.State != ConnectionState.Closed)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }

        //    return dt;//回傳資料表
        //}

        ///// <summary>
        ///// Execute
        ///// </summary>
        ///// <param name="cmd"></param>
        ///// <returns></returns>
        //public static int ExecuteExample(SqlCommand cmd)
        //{
        //    LogCommand(cmd);
        //    int result = 0;

        //    try
        //    {
        //        cmd.Connection = new SqlConnection(ConnectionString);
        //        cmd.Connection.Open(); //開啟資料庫連線
        //        result = cmd.ExecuteNonQuery(); //執行並回傳受影響筆數
        //    }
        //    catch (Exception ex)
        //    {
        //        new NLog().LogInformation(cmd.CommandText + Environment.NewLine + ex);
        //    }
        //    finally
        //    {
        //        cmd.Parameters.Clear();
        //        cmd.Connection.Close();
        //    }

        //    return result;//回傳影響筆數
        //}

        ///// <summary>
        ///// ExecuteScalar
        ///// </summary>
        ///// <param name="cmd"></param>
        ///// <returns></returns>
        //public static string ScalarExample(SqlCommand cmd)
        //{
        //    LogCommand(cmd);
        //    string result = string.Empty;

        //    try
        //    {
        //        cmd.Connection = new SqlConnection(ConnectionString);
        //        cmd.Connection.Open(); //開啟資料庫連線
        //        result = cmd.ExecuteScalar().ToString(); //執行並取回資料
        //    }
        //    catch (Exception ex)
        //    {
        //        new NLog().LogInformation(cmd.CommandText + Environment.NewLine + ex);
        //    }
        //    finally
        //    {
        //        cmd.Parameters.Clear();
        //        cmd.Connection.Close();
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 批次新增
        ///// </summary>
        ///// <param name="table">新增內容</param>
        ///// <param name="tableName">表格名稱</param>
        ///// <returns></returns>
        //public static int InsertBath(DataTable table, string tableName)
        //{
        //    int result = 0;

        //    using (SqlConnection conn = new SqlConnection())
        //    {
        //        //打開連接
        //        conn.Open();
        //        //使用SqlBulkCopy
        //        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
        //        {
        //            try
        //            {
        //                //插入到數據庫的目標表 TbA：表名  
        //                bulkCopy.DestinationTableName = tableName;
        //                //內存表的字段 對應數據庫表的字段
        //                for (int i = 0; i < table.Columns.Count; i++)
        //                {
        //                    bulkCopy.ColumnMappings.Add(table.Columns[i].ColumnName, table.Columns[i].ColumnName);
        //                }

        //                bulkCopy.WriteToServer(table);
        //                result = table.Rows.Count;
        //            }
        //            catch (Exception ex)
        //            {
        //                new NLog().LogInformation(ex);
        //            }
        //        }
        //        //關閉連接
        //        conn.Close();
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 紀錄Command
        ///// </summary>
        ///// <param name="cmd"></param>
        //private static void LogCommand(SqlCommand cmd)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append(cmd.CommandText + Environment.NewLine);

        //        for (int i = 0; i < cmd.Parameters.Count; i++)
        //        {
        //            sb.Append(Environment.NewLine);
        //            sb.Append(cmd.Parameters[i].ToString());
        //            sb.Append("：");
        //            sb.Append(cmd.Parameters[i].Value);
        //        }

        //        new NLog().LogDetail("Functions", "DataSource", "LogCommand", sb.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        new NLog().LogInformation(ex);
        //    }
        //}

    }
}
