using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Library.Controller
{
    public class ReportExcel
    {
        public DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }

        //public static void ExportTable(string title, DataTable table, Microsoft.AspNetCore.Http.HttpContext context)
        //{
        //    HSSFWorkbook Workbook = new HSSFWorkbook();

        //    //標頭1
        //    ICellStyle StyleTitle = Workbook.CreateCellStyle();
        //    StyleTitle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        //    StyleTitle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        //    StyleTitle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        //    StyleTitle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        //    StyleTitle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        //    StyleTitle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
        //    StyleTitle.WrapText = true;
        //    IFont font_Title = Workbook.CreateFont();
        //    font_Title.FontName = "標楷體";
        //    font_Title.FontHeightInPoints = 12;
        //    font_Title.IsBold = true;
        //    StyleTitle.SetFont(font_Title);

        //    //內容
        //    ICellStyle StyleContent_Center = Workbook.CreateCellStyle();
        //    StyleContent_Center.CloneStyleFrom(StyleTitle);
        //    IFont font_Content = Workbook.CreateFont();
        //    font_Content.FontName = "標楷體";
        //    font_Content.FontHeightInPoints = 12;
        //    font_Content.IsBold = false;
        //    StyleContent_Center.SetFont(font_Content);

        //    ICellStyle StyleContent_text_Left = Workbook.CreateCellStyle();
        //    StyleContent_text_Left.CloneStyleFrom(StyleContent_Center);
        //    StyleContent_text_Left.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
        //    StyleContent_text_Left.SetFont(font_Content);

        //    ISheet Sheet0 = Workbook.CreateSheet(title);
        //    int columns = 0;
        //    int rows = 0;

        //    #region 處裡標題

        //    IRow Sheet0_row0 = Sheet0.CreateRow(rows++);

        //    for (int i = 0; i < table.Columns.Count; i++)
        //    {
        //        SetCell_Content(Sheet0_row0.CreateCell(columns), StyleTitle, table.Columns[i].ColumnName);
        //        Sheet0.SetColumnWidth(columns++, 20 * 256);
        //    }

        //    #endregion

        //    #region 處裡內容

        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        IRow row = Sheet0.CreateRow(rows++);
        //        columns = 0;

        //        for (int j = 0; j < table.Columns.Count; j++)
        //        {
        //            SetCell_Content(row.CreateCell(columns++), StyleContent_Center, (table.Rows[i][j] == DBNull.Value ? "-" : Convert.ToString(table.Rows[i][j])));
        //        }
        //    }

        //    #endregion

        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    Workbook.Write(ms);
        //    string wordFileName = title + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        //    string strContentDisposition = String.Format("{0}; filename=\"{1}\"", "attachment", wordFileName);
        //    context.Response.AddHeader("content-disposition", strContentDisposition);
        //    context.Response.ContentType = "application/vnd.ms-excel";
        //    context.Response.ContentEncoding = Encoding.UTF8;
        //    context.Response.BinaryWrite(ms.ToArray());
        //    Workbook = null;
        //    ms.Close();
        //    ms.Dispose();
        //}

        /// <summary>
        /// 欄位設定
        /// </summary>
        /// <param name="Cell">第幾欄</param>
        /// <param name="CellStyle">靠左、靠右、置中</param>
        /// <param name="SetCellValue">值</param>
        private static void SetCell_Content(ICell Cell, ICellStyle CellStyle, string SetCellValue)
        {
            if (CellStyle != null)
            {
                Cell.CellStyle = CellStyle;
            }

            Cell.SetCellValue(SetCellValue);
        }


        /// <summary>
        /// 設定欄位樣式[標題]
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static ICellStyle CellStyle(HSSFWorkbook workbook)
        {
            ICellStyle styleTitle = workbook.CreateCellStyle();
            styleTitle.BorderTop = BorderStyle.Thin;
            styleTitle.BorderBottom = BorderStyle.Thin;
            styleTitle.BorderLeft = BorderStyle.Thin;
            styleTitle.BorderRight = BorderStyle.Thin;
            styleTitle.Alignment = HorizontalAlignment.Center;
            styleTitle.VerticalAlignment = VerticalAlignment.Center;
            styleTitle.WrapText = true;
            IFont font_Title = workbook.CreateFont();
            font_Title.FontName = "標楷體";
            font_Title.FontHeightInPoints = 12;
            font_Title.IsBold = true;
            styleTitle.SetFont(font_Title);
            return styleTitle;
        }

        /// <summary>
        /// 設定欄位樣式[左]
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static ICellStyle CellStyle3(HSSFWorkbook workbook)
        {
            ICellStyle styleTitle = workbook.CreateCellStyle();
            styleTitle.BorderTop = BorderStyle.Thin;
            styleTitle.BorderBottom = BorderStyle.Thin;
            styleTitle.BorderLeft = BorderStyle.Thin;
            styleTitle.BorderRight = BorderStyle.Thin;
            styleTitle.Alignment = HorizontalAlignment.Left;
            styleTitle.VerticalAlignment = VerticalAlignment.Center;
            styleTitle.WrapText = true;
            IFont font = workbook.CreateFont();
            font.FontName = "標楷體";
            font.FontHeightInPoints = 12;
            font.IsBold = false;
            styleTitle.SetFont(font);
            return styleTitle;
        }

        /// <summary>
        /// 產生Excel檔案
        /// </summary>
        /// <param name="table"></param>
        public bool SaveExcel(DataTable table, string path, int PositiveTake)
        {

            HSSFWorkbook Workbook = new HSSFWorkbook();
            ICellStyle StyleTitle = CellStyle(Workbook);
            ICellStyle StyleContent_Center = CellStyle3(Workbook);
            ISheet Sheet0 = Workbook.CreateSheet("正取");
            ISheet Sheet1 = Workbook.CreateSheet("備取");

            ISheet Sheet2 = Workbook.CreateSheet("正取隱蔽");
            ISheet Sheet3 = Workbook.CreateSheet("備取隱蔽");

            int rows1 = 0;
            int rows2 = 0;
            int columns = 0;

            IRow row1 = Sheet0.CreateRow(rows1++);
            IRow row2 = Sheet1.CreateRow(rows2++);
            IRow row3 = Sheet2.CreateRow(0);
            IRow row4 = Sheet3.CreateRow(0);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                string ColumnName = "";

                switch (table.Columns[i].ColumnName)
                {
                    case "AP_ID":
                        ColumnName = "報名編號";
                        break;
                    case "AP_NAME":
                        ColumnName = "姓名";
                        break;
                    case "AP_ADDRESS":
                        ColumnName = "住址";
                        break;
                    case "AP_PHONE":
                        ColumnName = "手機號碼";
                        break;
                    case "AP_MAIL":
                        ColumnName = "電子郵件";
                        break;
                    case "AP_CONTACT":
                        ColumnName = "緊急聯絡人姓名";
                        break;
                    case "AP_C_PHONE":
                        ColumnName = "緊急聯絡人電話";
                        break;
                    case "AP_ISCOVID19":
                        ColumnName = "是否接種疫苗";
                        break;
                    case "AP_ISMARK":
                        ColumnName = "是否註記(0:否/1:是)";
                        break;
                    case "AP_ISDRAW":
                        ColumnName = "抽籤(0:否/1:正取/2:備取)";
                        break;
                    case "AP_ISDRAW_R":
                        ColumnName = "是否回覆參加(0:否/1:是)";
                        break;
                    case "AP_DATE":
                        ColumnName = "報名時間";
                        break;
                    case "AP_NID":
                        ColumnName = "身分證字號";
                        break;
                    default:
                        ColumnName = "流水號";
                        break;
                }

                SetCell_Content(row1.CreateCell(columns), StyleTitle, ColumnName);
                SetCell_Content(row2.CreateCell(columns), StyleTitle, ColumnName);
                columns++;
            }

            SetCell_Content(row3.CreateCell(0), StyleTitle, "姓名");
            SetCell_Content(row3.CreateCell(1), StyleTitle, "身分證字號");
            SetCell_Content(row3.CreateCell(2), StyleTitle, "電話");
            SetCell_Content(row3.CreateCell(3), StyleTitle, "流水號");

            SetCell_Content(row4.CreateCell(0), StyleTitle, "姓名");
            SetCell_Content(row4.CreateCell(1), StyleTitle, "身分證字號");
            SetCell_Content(row4.CreateCell(2), StyleTitle, "電話");
            SetCell_Content(row4.CreateCell(3), StyleTitle, "流水號");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                columns = 0;

                string AP_NAME1 = table.Rows[i]["AP_NAME"].ToString();
                string AP_NAME = "";
                int AP_NAME_L = AP_NAME1.Length;

                if (AP_NAME_L < 3)
                {
                    AP_NAME = AP_NAME1.Substring(0, 1) + "◯";
                }
                else
                {
                    AP_NAME = AP_NAME1.Substring(0, 1);

                    for (int x = 0; x < AP_NAME_L - 2; x++)
                    {
                        AP_NAME += "◯";
                    }

                    AP_NAME += AP_NAME1.Substring(AP_NAME_L - 1, 1);
                }

                string AP_NID1 = table.Rows[i]["AP_NID"].ToString();
                string AP_NID = AP_NID1.Substring(0, 1) + "◯◯◯◯◯◯" + AP_NID1.Substring(7, 3);

                string AP_PHONE1 = table.Rows[i]["AP_PHONE"].ToString();
                string AP_PHONE = AP_PHONE1.Substring(0, 2) + "◯◯◯◯" + AP_PHONE1.Substring(6, 4);


                if (i < PositiveTake)
                {
                    IRow row = Sheet0.CreateRow(rows1);
                    IRow row22 = Sheet2.CreateRow(rows1);

                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        SetCell_Content(row.CreateCell(columns++), StyleContent_Center, table.Rows[i][j].ToString());
                        SetCell_Content(row.CreateCell(13), StyleContent_Center, Convert.ToString(i + 1));
                    }

                    SetCell_Content(row22.CreateCell(0), StyleContent_Center, AP_NAME);
                    SetCell_Content(row22.CreateCell(1), StyleContent_Center, AP_NID);
                    SetCell_Content(row22.CreateCell(2), StyleContent_Center, AP_PHONE);
                    SetCell_Content(row22.CreateCell(3), StyleContent_Center, Convert.ToString(i + 1));

                    columns = 0;
                    rows1++;
                }
                else
                {
                    IRow row = Sheet1.CreateRow(rows2);
                    IRow row22 = Sheet3.CreateRow(rows2);
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        SetCell_Content(row.CreateCell(columns++), StyleContent_Center, table.Rows[i][j].ToString());
                        SetCell_Content(row.CreateCell(13), StyleContent_Center, Convert.ToString(i + 1));
                    }

                    SetCell_Content(row22.CreateCell(0), StyleContent_Center, AP_NAME);
                    SetCell_Content(row22.CreateCell(1), StyleContent_Center, AP_NID);
                    SetCell_Content(row22.CreateCell(2), StyleContent_Center, AP_PHONE);
                    SetCell_Content(row22.CreateCell(3), StyleContent_Center, Convert.ToString(i + 1));

                    columns = 0;
                    rows2++;
                }






                //for (int j = 0; j < table.Columns.Count; j++)
                //{
                //    GeneralTools.SetCell_Content(row.CreateCell(columns++), StyleContent_Center, FixTools.FixCrossSiteScripting(table.Rows[i][j]));
                //}
            }

            FileStream file = new FileStream(path, FileMode.Create);//產生檔案
            Workbook.Write(file);
            file.Close();

            return true;
        }
    }
}
