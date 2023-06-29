using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Library.Functions
{
    public class Excel
    {
        /// <summary>
        /// 讀取EXCEL檔案
        /// </summary>
        /// <param name="filePath">路徑名稱</param>
        /// <param name="sheet">分頁名稱</param>
        /// <returns></returns>
        //public static DataTable SelectExcel(string filePath, string sheet)
        //{
        //    string tableName = "[" + sheet + "$]";//在頁簽名稱後加$，再用中括號[]包起來
        //    string sql = "SELECT * FROM " + tableName;//SQL查詢
        //    OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;Readonly=0'");
        //    OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        da.Fill(dt);//第一行為標頭
        //    }
        //    catch (Exception ex)
        //    {
        //        new NLog().LogInformation(ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return dt;
        //}

        /// <summary>
        /// 讀取Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable ReadExcelAsTableNPOI(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                HSSFWorkbook wb = new HSSFWorkbook(fs);
                ISheet sheet = wb.GetSheetAt(0);
                DataTable table = new DataTable();
                //由第一列取標題做為欄位名稱
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    table.Columns.Add(new DataColumn(headerRow.GetCell(i).StringCellValue));//以欄位文字為名新增欄位，此處全視為字串型別以求簡化

                //略過第零列(標題列)，一直處理至最後一列
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    DataRow dataRow = table.NewRow();
                    //依先前取得的欄位數逐一設定欄位內容
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                        if (row.GetCell(j) != null)
                            //如要針對不同型別做個別處理，可善用.CellType判斷型別
                            //再用.StringCellValue, .DateCellValue, .NumericCellValue...取值
                            //此處只簡單轉成字串
                            dataRow[j] = row.GetCell(j).ToString();
                    table.Rows.Add(dataRow);
                }
                return table;
            }
        }


    }
}
