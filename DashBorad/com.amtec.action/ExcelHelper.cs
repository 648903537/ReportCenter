using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace com.amtec.action
{
    public class ExcelHelper : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;

        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        /// <summary>
        /// 导出到EXCEL
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool DataTableToExcel(DataTable dt)
        {
            return DataTableToExcel(dt, "sheet1");
        }

        public bool DataTableToExcel(DataTable dt, string sheetName)
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet(sheetName);//创建一个名称为XX的表  
                    int rowCount = dt.Rows.Count;//行数  
                    int columnCount = dt.Columns.Count;//列数  

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].ColumnName);
                    }

                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据  
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }

                    SaveFileDialog openFileDialog1 = new SaveFileDialog();

                    if (Directory.Exists(System.Environment.CurrentDirectory + "\\excel"))
                    {
                        openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory + "\\excel";
                    }

                    openFileDialog1.Filter = "EXCEL文件|*.xls";
                    openFileDialog1.Title = "选择文件保存位置";
                    openFileDialog1.FileName = DateTime.Now.ToString("yyyyMMdd");

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        using (fs = File.OpenWrite(openFileDialog1.FileName))
                        {
                            workbook.Write(fs);//向打开的这个xls文件中写入数据  
                            result = true;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return false;
            }
        }

        public void GridToExcel(string fileName, DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel 2003格式|*.xls";
            sfd.FileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssms");
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //if (fileName.IndexOf(".xlsx") > 0) // 2007版本
            //    workbook = new XSSFWorkbook(fs);
            //else if (fileName.IndexOf(".xls") > 0) // 2003版本
            //    workbook = new HSSFWorkbook(fs);

            HSSFWorkbook wb = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)wb.CreateSheet(fileName);
            HSSFRow headRow = (HSSFRow)sheet.CreateRow(0);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                HSSFCell headCell = (HSSFCell)headRow.CreateCell(i, CellType.String);
                headCell.SetCellValue(dgv.Columns[i].HeaderText);
            }
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                HSSFRow row = (HSSFRow)sheet.CreateRow(i + 1);
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    HSSFCell cell = (HSSFCell)row.CreateCell(j);
                    if (dgv.Rows[i].Cells[j].Value == null)
                    {
                        cell.SetCellType(CellType.Blank);
                    }
                    else
                    {
                        if (dgv.Rows[i].Cells[j].ValueType.FullName.Contains("System.Int32"))
                        {
                            cell.SetCellValue(Convert.ToInt32(dgv.Rows[i].Cells[j].Value));
                        }
                        else if (dgv.Rows[i].Cells[j].ValueType.FullName.Contains("System.String"))
                        {
                            cell.SetCellValue(dgv.Rows[i].Cells[j].Value.ToString());
                        }
                        else if (dgv.Rows[i].Cells[j].ValueType.FullName.Contains("System.Single"))
                        {
                            cell.SetCellValue(Convert.ToSingle(dgv.Rows[i].Cells[j].Value));
                        }
                        else if (dgv.Rows[i].Cells[j].ValueType.FullName.Contains("System.Double"))
                        {
                            cell.SetCellValue(Convert.ToDouble(dgv.Rows[i].Cells[j].Value));
                        }
                        else if (dgv.Rows[i].Cells[j].ValueType.FullName.Contains("System.Decimal"))
                        {
                            cell.SetCellValue(Convert.ToDouble(dgv.Rows[i].Cells[j].Value));
                        }
                        else if (dgv.Rows[i].Cells[j].ValueType.FullName.Contains("System.DateTime"))
                        {
                            cell.SetCellValue(Convert.ToDateTime(dgv.Rows[i].Cells[j].Value).ToString("yyyy-MM-dd"));
                        }
                    }

                }

            }
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
            {
                wb.Write(fs);
            }
            MessageBox.Show("导出成功！", "导出提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                //获取Sheet
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 创建一个EXCEL文件
        /// </summary>
        /// <param name="ExcelFilePath"></param>
        public static void CreateExcel(string ExcelFilePath)
        {
            Excel.Application EApp = new Excel.Application();  //Excel应用程序
            Excel.Workbook EWBook;  //Excel文档
            Excel.Worksheet WSheet;

            object nothing = Missing.Value;
            EWBook = EApp.Workbooks.Add(nothing);
            WSheet = EWBook.Worksheets[1] as Excel.Worksheet;
            //WSheet.SaveAs(ExcelFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            WSheet.SaveAs(ExcelFilePath, 52, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            EWBook.Save();

            EWBook.Close(false, Type.Missing, Type.Missing);
            EApp.Quit();
            KillSpecialExcel(EApp);
        }

        public static void CopyFile(string sourceFilePath,string copyToFilePath)
        {
            string sourceFile = sourceFilePath;
            string destinationFile = copyToFilePath;
            bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
            System.IO.File.Copy(sourceFile, destinationFile, isrewrite);
        }

        /// <summary>
        /// 清理无用的Sheet(xlsx文件格式)
        /// </summary>
        /// <param name="xlsBook_trans"></param>
        public static void CleanSheet(Excel.Workbook xlsBook_trans)
        {
            Excel.Worksheet xlsSheet_trans = null;

            for (int i = xlsBook_trans.Sheets.Count; i >= 1; i--)
            {
                xlsSheet_trans = (Excel.Worksheet)xlsBook_trans.Sheets[i];
                if (xlsSheet_trans.Name.Contains("Sheet"))
                {
                    xlsSheet_trans.Delete();
                }
            }

            xlsBook_trans.Save();
        }

        /// <summary>
        /// 清理无用的Sheet(xlsm,xlsx文件格式)
        /// </summary>
        /// <param name="xlsBook_trans"></param>
        public static void CleanSheet(Excel.Application app, Excel.Workbook xlsBook_trans)
        {
            Excel.Worksheet xlsSheet_trans = null;
           
            for (int i = xlsBook_trans.Sheets.Count; i >= 1; i--)
            {
                xlsSheet_trans = (Excel.Worksheet)xlsBook_trans.Sheets[i];
                if (xlsSheet_trans.Name.Contains("Sheet"))
                {
                    app.DisplayAlerts = false; //注意一定要加上这句
                    xlsSheet_trans.Delete();
                    app.DisplayAlerts = true;//注意一定要加上这句
                }
            }

            xlsBook_trans.Save();
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="wb">工作簿</param>
        /// <param name="r1"></param>
        /// <param name="c1"></param>
        /// <param name="r2"></param>
        /// <param name="c2"></param>
        public static void MerageCell(Excel.Workbook wb, int r1, int c1, int r2, int c2)
        {
            try
            {
                Excel.Worksheet ws = (Excel.Worksheet)wb.ActiveSheet;  //取得当前活动的Worksheet
                Excel.Range r;
                //r = ws.get_Range(ws.Cells[r1, c1], ws.Cells[r2, c2]);     //取得合并的区域 
                r = ws.Range[ws.Cells[r1, c1], ws.Cells[r2, c2]];     //取得合并的区域 
                r.MergeCells = true;
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        private static void KillSpecialExcel(Excel.Application m_objExcel)
        {

            if (m_objExcel != null)
            {
                int lpdwProcessId;
                GetWindowThreadProcessId(new IntPtr(m_objExcel.Hwnd), out lpdwProcessId);
                System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
            }

        }

        #region 转换FUJI文件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <param name="startRow"></param>
        /// <param name="bottomRow">0:默认对应该EXCEL最大的行数</param>
        /// <param name="flag">up:上面的;down:下面的</param>
        /// <returns></returns>
        public DataTable ExcelFujiFileToDatatable(string sheetName, bool isFirstRowColumn, int startRow, int bottomRow, string flag)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                //获取Sheet
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }

                //表头
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(startRow);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow++;
                    }

                    //最后一列的标号DateTime.Parse("2017/01/01").AddDays(Convert.ToInt32("-42736")).ToString()
                    if (bottomRow == 0) bottomRow = sheet.LastRowNum;

                    for (int i = startRow; i <= bottomRow; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        //没有数据的行默认是null
                        if (row == null || row.FirstCellNum < 0) continue;

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                string value = row.GetCell(j).ToString();

                                if (flag.ToUpper() == "DOWN" && j == 0)
                                {
                                    DateTime dateStr = new DateTime();

                                    //因为原始文档也可能变成只显示天数的"42736"这样的方式(从1900-01-01到现在的天数)
                                    Match matchFGSO = Regex.Match(value, "\\d{5}");
                                    if (matchFGSO.Success)
                                    {
                                        //这边之所以要减掉2,是因为猜测excel的天数计算和DataTime的计算天数有问题
                                        value = DateTime.Parse("1900/01/01").AddDays(Convert.ToInt32(value) - 2).ToString();
                                    }
                                    if (DateTime.TryParse(value,out dateStr))
                                    {
                                        value = dateStr.ToString("M-d");
                                    }
                                }

                                dataRow[j] = value;
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }
    }
}
