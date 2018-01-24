using com.amtec.action;
using com.amtec.configurations;
using com.itac.mes.imsapi.client.dotnet;
using com.itac.mes.imsapi.domain.container;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DashBorad.com.tte.project
{
    public partial class TransFujiZfile : Form
    {
        private static IMSApiDotNet imsapi = IMSApiDotNet.loadLibrary();
        private IMSApiSessionContextStruct sessionContext;
        public ApplicationConfiguration config ;

        //文件夹监控
        FileSystemWatcher watcher = new FileSystemWatcher();

        //存储上部分和下半部分文档内容
        DataTable Up_dt = new DataTable();
        DataTable Down_dt = new DataTable();

        public bool isOver;

        public int totalPage = 0;

        bool transResult = true;

        public TransFujiZfile()
        {
            InitializeComponent();

            config = new ApplicationConfiguration();

            txtConsole.LanguageOption = RichTextBoxLanguageOptions.UIFonts;

            //显示版本
            this.Text = "富士文档转换 ( V:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
        }

        public TransFujiZfile(IMSApiSessionContextStruct _sessionContext)
        {
            InitializeComponent();
            sessionContext = _sessionContext;
            config = new ApplicationConfiguration();

            txtConsole.LanguageOption = RichTextBoxLanguageOptions.UIFonts;

            //显示版本
            this.Text = "富士文档转换 ( V:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";

            //添加文件夹监控       郑培聪
            ListenerFolder(config.ListenFolder);
        }

        private void TransFujiZfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("你确定要关闭程序吗？", "Quit Application", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ListenerFolder(string path)
        {
            errorHandler(0, "开始监控目录" + config.ListenFolder);

            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.Filter = "*.xlsx";
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            try
            {
                string filename = e.Name;
                string filepath = e.FullPath;
                string strTemp = Path.GetDirectoryName(filepath) + @"\";
                if (strTemp.Contains(config.SuccessFolder) || strTemp.Contains(config.ErrorFolder))
                {
                    return;
                }
                Thread.Sleep(Convert.ToInt32(1000));//等待10s

                ListenFile(filepath);
            }
            catch (Exception ex)
            {
                errorHandler(0, ex.Message);
            }
        }

        public void ListenFile(string filePath)
        {

            try
            {
                errorHandler(0, "开始解析:" + filePath);

                isOver = false;

                int upStartRow = 6;
                int upEndRow = 16;

                int downStartRow = 24;
                int downEndRow = 0;//这个值是0在ExcelFujiFileToDatatable会改成最大行数


                ExcelHelper exhep = new ExcelHelper(filePath);

                //验证一开始是否有图片
                DataTable formatDt = exhep.ExcelFujiFileToDatatable("Sheet1", true, 0, 1, "UP");
                if (formatDt == null || formatDt.Rows.Count <1 )
                {
                    #region 如果有图片(第一个单元格没有数据)

                    upStartRow += 2;
                    upEndRow += 2;

                    downStartRow += 2;

                    #endregion
                }


                #region 读取前面部分

                Up_dt = exhep.ExcelFujiFileToDatatable("Sheet1", true, upStartRow, upEndRow, "UP");

                #endregion

                #region 读取后半部分

                Down_dt = exhep.ExcelFujiFileToDatatable("Sheet1", true, downStartRow, downEndRow, "DOWN");

                #endregion

                if (!VerifyDatatable())
                {
                    errorHandler(2, filePath + "文件数据或格式有问题,请自行排查");
                    //处理失败
                    MoveFileToFolder(filePath, config.ErrorFolder, true);
                    return;
                }

                //按照行高的方式来卡控
                //ExceptTransFileExt(Up_dt, Down_dt, 1, true, null);

                bool transResult = true;
                //每页的行数
                int maxRow = Convert.ToInt32(config.MaxRow);

                if (Down_dt.Rows.Count > maxRow)//这个21是按照打印最大行来设置的
                {
                    DataSet ds = SplitDataTable(Down_dt, maxRow);
                    int totalPages = ds.Tables.Count;
                    int currentPage = 1;
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (currentPage == totalPages) isOver = true;
                        //只要循环有一次是错误的就要搬到错误文件夹路径
                        transResult = transResult == true ? ExceptTransFile(Up_dt, dt, currentPage, totalPages) : transResult;
                        currentPage++;
                    }
                }
                else
                {
                    isOver = true;
                    transResult = ExceptTransFile(Up_dt, Down_dt, 1, 1);
                }

                if (transResult)
                {
                    //处理成功
                    MoveFileToFolder(filePath, config.SuccessFolder, false);
                }
                else
                {
                    //处理失败
                    MoveFileToFolder(filePath, config.ErrorFolder, true);
                }
            }
            catch(Exception ex)
            {
                errorHandler(2, ex.Message);
                MoveFileToFolder(filePath, config.ErrorFolder, true);
            }
        }

        private void btnImportAllFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(System.Environment.CurrentDirectory + "\\Template"))
                {
                    openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory + "\\Template";
                }
                openFileDialog1.Filter = "EXCEL文件|*.xlsx";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ListenFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                errorHandler(2, ex.Message);
            }
        }

        #region Main
        /// <summary>
        /// 复制Sheet,填充Sheet
        /// </summary>
        public bool ExceptTransFile(DataTable Up_dt, DataTable Down_dt, int currentPage, int totalPages)
        {
            Excel.Application xlsApp = new Excel.Application();

            Excel.Workbook xlsBook = null;
            Excel.Worksheet xlsSheet = null;

            Excel.Workbook xlsBook_trans = null;
            Excel.Worksheet xlsSheet_trans = null;

            bool isCleanSheet = false;//是否清空没用的Sheet

            try
            {
                string partNumberExt = Up_dt.Rows[0][1].ToString();//品号_面次:DEC-21458-00-1A_B
                string partNumber = Up_dt.Rows[0][1].ToString().Substring(0, 15);//品号:DEC-21458-00
                string partDesc = mGetPartData(partNumber, imsapi, sessionContext);//品名:北京现代ADc倒车雷达SMT半成品
                string programName = partNumberExt.Replace("_", " ") + " " + Up_dt.Rows[1][1].ToString();//程序名称:DEC-21458-00-1A B V1.4-S1E
                string machineGroup = Up_dt.Rows[1][1].ToString().Substring(Up_dt.Rows[1][1].ToString().IndexOf('-') + 1);//机械别:S1E
                string todayDate = DateTime.Now.ToString("yyyy-M-d");
                string side = Up_dt.Rows[9][1].ToString().ToUpper().Substring(0, 3);//面次:TOP/BOT
                int totalPositionCount = 0;//合计点数
                string sheetName = machineGroup + "-" + side + ((totalPages == 1) ? "" : "(" + currentPage + ")");//Sheet名称
                string page = currentPage + "/" + totalPages;


                #region 创建excel文件
                //1,验证目录
                if (!Directory.Exists(config.GenerateFolder))
                {
                    Directory.CreateDirectory(config.GenerateFolder);
                    errorHandler(0, "创建目录:" + config.GenerateFolder);
                }
                //2,验证文件
                string filePath = config.GenerateFolder + partNumber + ".xlsm";
                if (!File.Exists(filePath))
                {
                    ExcelHelper.CopyFile(@"D:\Z_files\Template\Z_Template_02.xlsm", filePath);
                    errorHandler(0, "创建文件:" + filePath);
                    isCleanSheet = true;
                }
                #endregion


                #region 复制Sheet
                GC.Collect();
                //模板页Sheet
                xlsBook = xlsApp.Workbooks.Open(config.TemplateFolder, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlsSheet = (Excel.Worksheet)xlsBook.ActiveSheet;
                //生成文件Sheet
                xlsBook_trans = xlsApp.Workbooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlsSheet_trans = (Excel.Worksheet)xlsBook_trans.Sheets[1];
                //将模板的格式复制过来
                xlsSheet.Copy(xlsSheet_trans, Type.Missing);
                #endregion


                #region 填充Sheet

                //填充表头部分
                xlsSheet_trans = (Excel.Worksheet)xlsBook_trans.ActiveSheet;
                xlsSheet_trans.Cells[4, 2] = machineGroup;
                xlsSheet_trans.Cells[4, 9] = page;
                xlsSheet_trans.Cells[5, 3] = programName;
                xlsSheet_trans.Cells[6, 3] = partDesc;
                xlsSheet_trans.Cells[7, 3] = partNumber;
                xlsSheet_trans.Cells[7, 7] = todayDate;

                //表身部分
                int idx = 9;//表身开始行(这个值是根据原始数据文档出来的)
                foreach (DataRow row in Down_dt.Rows)
                {
                    string zAxisNumber = row["pos."].ToString();                                                    //Z轴编号
                    string partNumberSource = row["Part Num"].ToString();                                           //品号

                    //料架型号(FUJI8mm*16PH)    要求:1,只要大于"8mm"不需后面的直接设置为"FJ24mm";2,只要Z轴编号为6-A或6-B开头的料架型号为TRAY
                    string prefixRackType = "FUJI";//料架型号前缀
                    string suffixRackType = row["Width"].ToString() + "*" + row["Feed Pitch"].ToString() + "PH";    //料架型号后缀
                    string valueRackType = row["Width"].ToString().TrimEnd('m');
                    Match canTransInt = Regex.Match(valueRackType, "\\d");
                    if(canTransInt.Success)
                    {
                        if (Convert.ToInt32(valueRackType) > 8)
                        {
                            prefixRackType = "FJ";
                            suffixRackType = "24mm";
                        }
                    }
                    if (zAxisNumber.ToUpper().Trim().StartsWith("6-A") || zAxisNumber.ToUpper().Trim().StartsWith("6-B"))
                    {
                        prefixRackType = "TRAY";
                        suffixRackType = "";
                    }
                    string rackType = prefixRackType + suffixRackType; 

                    //材料规格(这个有点复杂)
                    string materialSpec = GenerateMaterialSpec(partNumberSource.Trim());

                    //部品贴装位置,点数
                    string position = string.Empty;
                    int positionCount = 0;
                    GeneratePosition(row["Reference"].ToString(), out position, out positionCount);
                    totalPositionCount += positionCount;

                    //插入数据(用固定的模板来做)
                    xlsSheet_trans.Cells[idx, 1] = zAxisNumber;
                    xlsSheet_trans.Cells[idx, 2] = rackType;
                    xlsSheet_trans.Cells[idx, 3] = partNumberSource;
                    xlsSheet_trans.Cells[idx, 4] = materialSpec;
                    xlsSheet_trans.Cells[idx, 5] = position;
                    xlsSheet_trans.Cells[idx, 8] = positionCount;

                    //部品贴装位置要合并单元格
                    ExcelHelper.MerageCell(xlsBook_trans, idx, 5, idx, 7);

                    //自动调衡行高(根据装贴位置数,4个一行)
                    Excel.Range range1 = (Excel.Range)xlsSheet_trans.Rows[idx, Missing.Value];
                    range1.RowHeight = position.Length < 21 ? 21 : Math.Ceiling((position.Length - Math.Ceiling(position.Length / 20.00)) / 19.00) * 15;

                    idx++;
                }

                //添加行"以下空白"
                if(isOver)
                {
                    xlsSheet_trans.Cells[idx, 2] = "以下空白";

                    //部品贴装位置要合并单元格
                    ExcelHelper.MerageCell(xlsBook_trans, idx, 5, idx, 7);

                    idx++;
                }

                //填充点数合并
                xlsSheet_trans.Cells[32, 8] = totalPositionCount;

                #endregion


                //修改Sheet名称之前先将先前同名的删除掉
                Excel.Worksheet xlsSheet_clean = null;
                for (int i = xlsBook_trans.Sheets.Count; i >= 1; i--)
                {
                    xlsSheet_clean = (Excel.Worksheet)xlsBook_trans.Sheets[i];
                    if (xlsSheet_clean.Name.Contains(sheetName))
                    {
                        errorHandler(0, "清除Sheet:" + xlsSheet_clean.Name);
                        xlsSheet_clean.Delete();
                    }
                }
                xlsBook_trans.Save();
                //修改Sheet名称
                ((Excel.Worksheet)xlsBook_trans.ActiveSheet).Name = sheetName;

                xlsBook_trans.Save();

                if (isCleanSheet)
                    ExcelHelper.CleanSheet(xlsApp, xlsBook_trans);

                errorHandler(0, "生成成功");
                return true;
            }
            catch (Exception ex)
            {
                errorHandler(2, ex.Message );
                return false;
            }
            finally
            {
                xlsBook.Close(false, Type.Missing, Type.Missing);
                xlsBook_trans.Close(false, Type.Missing, Type.Missing);
                xlsApp.Quit();
                KillSpecialExcel(xlsApp);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsBook_trans);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet_trans);

                xlsSheet = null;
                xlsBook = null;
                xlsApp = null;
                xlsSheet_trans = null;
                xlsBook_trans = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }





        public void ExceptTransFileExt(DataTable up_dt,DataTable down_dt,int currentPage,bool isFirstTimes,transExcel transExcel)
        {
            if(transExcel == null)
            {
                transExcel = new transExcel();
            }

            DataTable copyDown_dt = down_dt.Copy();

            bool isCleanSheet = false;//是否清空没用的Sheet

            double sumRowHeight =0 ;

            if(isFirstTimes)
            {
                totalPage = 0;
                transResult = true;
            }

            try
            {
                string partNumberExt = up_dt.Rows[0][1].ToString();//品号_面次:DEC-21458-00-1A_B
                string partNumber = up_dt.Rows[0][1].ToString().Substring(0, 15);//品号:DEC-21458-00
                string partDesc = mGetPartData(partNumber, imsapi, sessionContext);//品名:北京现代ADc倒车雷达SMT半成品
                string programName = partNumberExt.Replace("_", " ") + " " + up_dt.Rows[1][1].ToString();//程序名称:DEC-21458-00-1A B V1.4-S1E
                string machineGroup = up_dt.Rows[1][1].ToString().Substring(up_dt.Rows[1][1].ToString().IndexOf('-') + 1);//机械别:S1E
                string todayDate = DateTime.Now.ToString("yyyy-M-d");
                string side = up_dt.Rows[9][1].ToString().ToUpper().Substring(0, 3);//面次:TOP/BOT
                int totalPositionCount = 0;//合计点数
                

                #region 创建excel文件
                //1,验证目录
                if (!Directory.Exists(config.GenerateFolder))
                {
                    Directory.CreateDirectory(config.GenerateFolder);
                    errorHandler(0, "创建目录:" + config.GenerateFolder);
                }
                //2,验证文件
                string filePath = config.GenerateFolder + partNumber + ".xlsm";
                if (!File.Exists(filePath))
                {
                    //ExcelHelper.CreateExcel(filePath);
                    ExcelHelper.CopyFile(@"D:\Z_files\Template\Z_Template_02.xlsm", filePath);
                    errorHandler(0, "创建文件:" + filePath);
                    isCleanSheet = true;
                }
                #endregion


                #region 复制Sheet
                GC.Collect();

                //模板页Sheet
                transExcel.xlsBook = transExcel.xlsApp.Workbooks.Open(config.TemplateFolder, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                transExcel.xlsSheet = (Excel.Worksheet)transExcel.xlsBook.ActiveSheet;
                //生成文件Sheet
                transExcel.xlsBook2 = transExcel.xlsApp.Workbooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                transExcel.xlsSheet2 = (Excel.Worksheet)transExcel.xlsBook2.Sheets[1];
                //将模板的格式复制过来
                transExcel.xlsSheet.Copy(transExcel.xlsSheet2, Type.Missing);
                #endregion


                #region 填充Sheet

                //填充表头部分
                transExcel.xlsSheet2 = (Excel.Worksheet)transExcel.xlsBook2.ActiveSheet;
                transExcel.xlsSheet2.Cells[4, 2] = machineGroup;
                
                transExcel.xlsSheet2.Cells[5, 3] = programName;
                transExcel.xlsSheet2.Cells[6, 3] = partDesc;
                transExcel.xlsSheet2.Cells[7, 3] = partNumber;
                transExcel.xlsSheet2.Cells[7, 7] = todayDate;

                //表身部分
                int idx = 9;//表身开始行(这个值是根据原始数据文档出来的)
                foreach (DataRow row in down_dt.Rows)
                {
                    string zAxisNumber = row["pos."].ToString();                                                    //Z轴编号
                    string partNumberSource = row["Part Num"].ToString();                                           //品号

                    //料架型号(FUJI8mm*16PH)    要求:1,只要大于"8mm"不需后面的直接设置为"FJ24mm";2,只要Z轴编号为6-A或6-B开头的料架型号为TRAY
                    string prefixRackType = "FUJI";//料架型号前缀
                    string suffixRackType = row["Width"].ToString() + "*" + row["Feed Pitch"].ToString() + "PH";    //料架型号后缀
                    string valueRackType = row["Width"].ToString().TrimEnd('m');
                    Match canTransInt = Regex.Match(valueRackType, "\\d");
                    if (canTransInt.Success)
                    {
                        if (Convert.ToInt32(valueRackType) > 8)
                        {
                            prefixRackType = "FJ";
                            suffixRackType = "24mm";
                        }
                    }
                    if (zAxisNumber.ToUpper().Trim().StartsWith("6-A") || zAxisNumber.ToUpper().Trim().StartsWith("6-B"))
                    {
                        prefixRackType = "TRAY";
                        suffixRackType = "";
                    }
                    string rackType = prefixRackType + suffixRackType;

                    //材料规格(这个有点复杂)
                    string materialSpec = GenerateMaterialSpec(partNumberSource.Trim());

                    //部品贴装位置,点数
                    string position = string.Empty;
                    int positionCount = 0;
                    GeneratePosition(row["Reference"].ToString(), out position, out positionCount);
                    totalPositionCount += positionCount;

                    //自动调衡行高(根据装贴位置数,19个位置一行)
                    double rowHeight = position.Length < 21 ? 21 : Math.Ceiling((position.Length - Math.Ceiling(position.Length / 20.00)) / 19.00) * 15;
                    sumRowHeight += rowHeight;
                    if(rowHeight>Convert.ToDouble(config.MaxRow))
                    {
                        errorHandler(2, "设置的最大行高小于单行行高");
                    }
                    if(sumRowHeight>Convert.ToDouble(config.MaxRow))
                    {
                        transExcel.xlsBook2.Save();
                        //如果加上当前行后大于设置的最大行高,那么就用迭代
                        ExceptTransFileExt(up_dt, copyDown_dt, currentPage + 1, false, transExcel);
                        break;
                    }

                    //插入数据
                    Excel.Range range = (Excel.Range)transExcel.xlsSheet2.Rows[idx, Missing.Value];
                    range.Rows.Insert(Excel.XlDirection.xlDown, Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                    transExcel.xlsSheet2.Cells[idx, 1] = zAxisNumber;
                    transExcel.xlsSheet2.Cells[idx, 2] = rackType;
                    transExcel.xlsSheet2.Cells[idx, 3] = partNumberSource;
                    transExcel.xlsSheet2.Cells[idx, 4] = materialSpec;
                    transExcel.xlsSheet2.Cells[idx, 5] = position;
                    transExcel.xlsSheet2.Cells[idx, 8] = positionCount;
                    //部品贴装位置要合并单元格
                    ExcelHelper.MerageCell(transExcel.xlsBook2, idx, 5, idx, 7);

                    Excel.Range range1 = (Excel.Range)transExcel.xlsSheet2.Rows[idx, Missing.Value];
                    //设置行高
                    range1.RowHeight = rowHeight;

                    copyDown_dt.Rows.RemoveAt(0);
                    idx++;
                }


                //添加行"以下空白"
                if (copyDown_dt==null || copyDown_dt.Rows.Count <1)
                {
                    Excel.Range range = (Excel.Range)transExcel.xlsSheet2.Rows[idx, Missing.Value];
                    range.Rows.Insert(Excel.XlDirection.xlDown, Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                    transExcel.xlsSheet2.Cells[idx, 2] = "以下空白";

                    //部品贴装位置要合并单元格
                    ExcelHelper.MerageCell(transExcel.xlsBook2, idx, 5, idx, 7);
                    idx++;
                }

                //总页数
                totalPage = totalPage > currentPage ? totalPage : currentPage;
                string page = currentPage + "/" + totalPage;
                transExcel.xlsSheet2 = (Excel.Worksheet)transExcel.xlsBook2.Sheets[totalPage - currentPage + 1];

                //填充点数合并
                transExcel.xlsSheet2.Cells[idx + 1, 8] = totalPositionCount;
                //填充页数
                transExcel.xlsSheet2.Cells[4, 9] = page;

                #endregion


                #region 改Sheet名称
                string sheetName = machineGroup + "-" + side + ((totalPage == 1) ? "" : "(" + currentPage + ")");//Sheet名称
                //1,修改Sheet名称之前先将先前同名的删除掉
                Excel.Worksheet xlsSheet_clean = null;
                for (int i = transExcel.xlsBook2.Sheets.Count; i >= 1; i--)
                {
                    xlsSheet_clean = (Excel.Worksheet)transExcel.xlsBook2.Sheets[i];
                    if (xlsSheet_clean.Name.Contains(sheetName))
                    {
                        errorHandler(0, "清除Sheet:" + xlsSheet_clean.Name);
                        xlsSheet_clean.Delete();
                    }
                }
                transExcel.xlsBook2.Save();
                //2,修改Sheet名称
                ((Excel.Worksheet)transExcel.xlsBook2.Sheets[totalPage - currentPage + 1]).Name = sheetName;
                transExcel.xlsBook2.Save();
                #endregion


                if (isCleanSheet)
                    ExcelHelper.CleanSheet(transExcel.xlsApp, transExcel.xlsBook2);

                errorHandler(0, sheetName + "生成成功");
            }
            catch (Exception ex)
            {
                errorHandler(2, ex.Message);
                transResult = false;
            }
            finally
            {
                //如果是第一次就要关闭

                if(isFirstTimes)
                {

                    transExcel.closeExcel();
                }

                //transExcel.xlsBook.Close(false, Type.Missing, Type.Missing);
                //transExcel.xlsBook2.Close(false, Type.Missing, Type.Missing);
                //transExcel.xlsApp.Quit();
                //KillSpecialExcel(transExcel.xlsApp);

                //System.Runtime.InteropServices.Marshal.ReleaseComObject(transExcel.xlsApp);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(transExcel.xlsBook);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(transExcel.xlsSheet);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(transExcel.xlsBook2);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(transExcel.xlsSheet2);

                //transExcel.xlsSheet = null;
                //transExcel.xlsBook = null;
                //transExcel.xlsApp = null;
                //transExcel.xlsSheet2 = null;
                //transExcel.xlsBook2 = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// 验证数据是否读取
        /// </summary>
        /// <returns></returns>
        public bool VerifyDatatable()
        {
            if (Up_dt == null || Up_dt.Rows.Count < 1 || Down_dt == null || Down_dt.Rows.Count < 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 计算部品装贴位置和点数
        /// </summary>
        /// <param name="text">原始数据中的Reference列</param>
        /// <param name="positionText">部品装贴位置值</param>
        /// <param name="positionCount">点数</param>
        public void GeneratePosition(string text, out string positionText, out int positionCount)
        {
            if(text == "")
            {
                positionText = string.Empty;
                positionCount = 0;
                return;
            }

            string[] strM = text.Split(new string[] { " " }, StringSplitOptions.None);
            List<string> lsStr = new List<string>();
            foreach (string str in strM)
            {
                if (!lsStr.Contains(str.Substring(str.IndexOf(':') + 1)))
                {
                    lsStr.Add(str.Substring(str.IndexOf(':') + 1));
                }
            }

            positionText = string.Join(" ", lsStr.ToArray());
            positionCount = lsStr.Count;
        }

        /// <summary>
        /// 生成材料规格
        /// </summary>
        /// <param name="partNumber">品号</param>
        /// <returns></returns>
        public string GenerateMaterialSpec(string partNumber)
        {
            MaterialSpecItemEntity entity = new MaterialSpecItemEntity();

            #region 筛选出对应MaterialSpec.xml的配置公式
            Dictionary<string, List<MaterialSpecItemEntity>> dicTask = new Dictionary<string, List<MaterialSpecItemEntity>>();
            XDocument xdc = XDocument.Load(@"./Xml/MaterialSpec.xml");

            //匹配前3码
            var partNumberStarts = from item in xdc.Descendants("partNumberStart")
                                   where Regex.Match(partNumber, item.Attribute("value").Value).Success
                                   select item;
            XElement partNumberStart = partNumberStarts.FirstOrDefault();
            if (partNumberStart == null)
            {
                errorHandler(2, partNumber + "没有找到对应的材料规格生成规则");
                return string.Empty;
            }

            //匹配品号是否满足
            var partNumberXml = from item in partNumberStart.Descendants("partNumber")
                                where Regex.Match(partNumber, item.Attribute("value").Value).Success
                                select item;
            if (partNumberXml == null)
            {
                errorHandler(2, partNumber + "没有找到对应的材料规格生成规则");
                return string.Empty;
            }

            //匹配对应的Item公式
            var node = from item in partNumberXml.Descendants("Item")
                       select item;
            if (node == null)
            {
                errorHandler(2, partNumber + "没有找到对应的材料规格生成规则");
                return string.Empty;
            }

            foreach (XElement item in node.ToList())
            {
                entity.PartNumber = partNumber;
                entity.ErrorNumber = Convert.ToInt32(GetNoteAttributeValues(item, "ErrorNumber"));
                entity.ErrorValue = GetNoteAttributeValues(item, "ErrorValue");
                entity.BaseValue = GetNoteAttributeValues(item, "BaseValue");
                entity.LowValue = GetNoteAttributeValues(item, "LowValue");
                entity.MaxValue = GetNoteAttributeValues(item, "MaxValue");
                entity.Unit = GetNoteAttributeValues(item, "Unit");
                break;
            }
            #endregion

            #region 根据公式,得到材料规格
            return GenerateMaterialSpecByEntity(entity);
            #endregion
        }

        /// <summary>
        /// 显示XML属性值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributename"></param>
        /// <returns></returns>
        public string GetNoteAttributeValues(XElement node, string attributename)
        {
            string strValue = "";
            try
            {
                strValue = node.Attribute(attributename).Value;
            }
            catch (Exception ex)
            {
                errorHandler(2, ex.Message);
            }
            return strValue;
        }

        /// <summary>
        /// 根据实体类,算出对应的材料规格
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GenerateMaterialSpecByEntity(MaterialSpecItemEntity entity)
        {
            string partNumber = entity.PartNumber;//品号
            string singleNumberText = partNumber.Substring(entity.ErrorNumber - 1, 1);//截取的字符值
            string unit = string.Empty;//单位

            string[] strErrValue = entity.ErrorValue.Split(',');
            Dictionary<string, string> dicErrValue = new Dictionary<string, string>();
            foreach (string s in strErrValue)
            {
                string[] strTemp = s.Trim().Split('=');
                if (strTemp.Length != 2)
                    continue;
                if (!dicErrValue.ContainsKey(strTemp[0]))
                {
                    dicErrValue.Add(strTemp[0], strTemp[1]);
                }
            }
            if (!dicErrValue.ContainsKey(singleNumberText)) return "";

            string errorValue = dicErrValue[singleNumberText];//误差值

            //品号的替换对应Dictionary(如:A->1;B->a......)
            Dictionary<string, string> dicFormalReplace = GetFormalDictionary(partNumber);
            //计算基值
            string baseValue = ComputeValueByFormula(dicFormalReplace, entity.BaseValue);

            //为了计算负值和正值,要将误差值和基值放到替换Dictionary里面
            dicFormalReplace.Add("P", errorValue);
            dicFormalReplace.Add("Q", baseValue);

            //负值
            string lowValue = ComputeValueByFormula(dicFormalReplace, entity.LowValue);
            //正值
            string maxValue = ComputeValueByFormula(dicFormalReplace, entity.MaxValue);

            //单位
            string[] unitStr = entity.Unit.Split(',');

            /*长度处理(以正值的长度为准,
             * (1),0～3位,不用除1000(默认单位OHM);
             * (2),4～6,除以1次1000(K OHM);
             * (3),7以上,除以2次1000(M OHM))*/
            if (Convert.ToDouble(maxValue) >= Math.Pow(10, 3) && Convert.ToDouble(maxValue) < Math.Pow(10, 7))
            {
                lowValue = (Convert.ToDouble(lowValue) / Math.Pow(10, 3)).ToString("0.##");
                maxValue = (Convert.ToDouble(maxValue) / Math.Pow(10, 3)).ToString("0.##");
                unit = unitStr.Length >= 3 ? unitStr[1] : unitStr[0];
            }
            else if (Convert.ToDouble(maxValue) >= Math.Pow(10, 7))
            {
                lowValue = (Convert.ToDouble(lowValue) / Math.Pow(10, 6)).ToString("0.##");
                maxValue = (Convert.ToDouble(maxValue) / Math.Pow(10, 6)).ToString("0.##");
                unit = unitStr.Length >= 3 ? unitStr[2] : unitStr[0];
            }
            else
            {
                lowValue = Convert.ToDouble(lowValue).ToString("0.##");
                maxValue = Convert.ToDouble(maxValue).ToString("0.##");
                unit = unitStr[0];
            }

            return lowValue + "～" + maxValue + unit;
        }

        /// <summary>
        /// 为品号匹配指定的字符串(ABCDEFGHIJKLMNO)
        /// </summary>
        /// <param name="partNumber">品号</param>
        /// <returns>出来的结果是标准格式对应的是品号字母的小写</returns>
        public Dictionary<string, string> GetFormalDictionary(string partNumber)
        {
            if (string.IsNullOrWhiteSpace(partNumber)) return null;
            partNumber = partNumber.Trim().ToLower();
            if (partNumber.Length != 15) return null;

            Dictionary<string, string> formalDictionary = new Dictionary<string, string>();
            string formalString = "ABCDEFGHIJKLMNO";
            int idx = 0;
            foreach (char c in formalString)
            {
                formalDictionary.Add(c.ToString(), partNumber.Substring(idx, 1));
                idx++;
            }
            return formalDictionary;
        }

        /// <summary>
        /// 字符对比替换,然后根据公式算出对应的值
        /// </summary>
        /// <param name="dicStr"></param>
        /// <param name="formula">公式</param>
        /// <returns></returns>
        public string ComputeValueByFormula(Dictionary<string, string> dicStr, string formula)
        {
            //筛选幂运算("^10^3^"-->10*10*10)
            if (formula.Contains('^'))
            {
                int a = formula.IndexOf('^') + 1;
                int b = formula.Substring(a).IndexOf('^') + 1;
                int c = formula.Substring(a + b).IndexOf('^');
                if (b == -1 || c == -1)
                {

                }
                else
                {
                    string v1 = formula.Substring(a, b - 1);
                    string v2 = formula.Substring(a + b, c);

                    double d1 = Convert.ToDouble(ComputeValueByFormula(dicStr, v1));
                    double d2 = Convert.ToDouble(ComputeValueByFormula(dicStr, v2));

                    formula = formula.Substring(0, a - 1) + Math.Pow(d1, d2).ToString() + formula.Substring(a + b + c + 1);
                }
            }

            //具体的计算公式(值已经替换过了如:strCompute ="4+5")
            string strCompute = string.Join("", formula.AsEnumerable().Select(c => dicStr.ContainsKey(c.ToString()) ? dicStr[c.ToString()] : c.ToString()));

            Microsoft.JScript.Vsa.VsaEngine ve = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
            //计算结果(如:result = 9)
            var result = Microsoft.JScript.Eval.JScriptEvaluate(strCompute, ve);

            return result.ToString();
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        private static void KillSpecialExcel(Excel.Application m_objExcel)
        {
            try
            {
                if (m_objExcel != null)
                {
                    int lpdwProcessId;
                    GetWindowThreadProcessId(new IntPtr(m_objExcel.Hwnd), out lpdwProcessId);
                    System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 获取品名
        /// </summary>
        /// <param name="partNumber"></param>
        /// <param name="imsapi"></param>
        /// <param name="sessionContext"></param>
        /// <returns></returns>
        private string mGetPartData(string partNumber, IMSApiDotNet imsapi, IMSApiSessionContextStruct sessionContext)
        {
            //查找品号
            KeyValue[] partFilter = new KeyValue[] { new KeyValue("PART_NUMBER", partNumber) };
            String[] partDataResultKey = new String[] { "PART_DESC" };
            String[] partDataResultValues = new String[] { };
            int result = imsapi.mdataGetPartData(sessionContext, "XOFL01-002-01", partFilter, partDataResultKey, out partDataResultValues);
            if (result == 0)
                return partDataResultValues[0];
            else
                return string.Empty;
        }
        #endregion


        /// <summary>
        /// 转移文件
        /// </summary>
        /// <param name="filePath">原始文件路径</param>
        /// <param name="transPath">目标文件路径</param>
        /// <param name="isCreateFolder">是否添加时间文件夹形式保存</param>
        private void MoveFileToFolder(string filePath, string transPath, bool isCreateFolder)
        {
            try
            {
                //查看指定路径下是否有相应日期的文件夹
                string year = DateTime.Now.Year.ToString();
                string folderName = isCreateFolder ? transPath + "\\" + year + "\\" + DateTime.Now.ToString("MMdd") : transPath;
                //创建文件保存路径
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                    errorHandler(0, "成功创建文件路径:" + folderName);
                }

                //生成要搬运的文件路径
                FileInfo fInfo = new FileInfo(@"" + filePath);
                string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);
                string newFullPath = Path.Combine(folderName, fileNameOnly + "_" + DateTime.Now.ToString("HHmmss") + extension);
                if (File.Exists(newFullPath))
                {
                    File.Delete(newFullPath);
                }

                fInfo.MoveTo(@"" + newFullPath);
                errorHandler(2, filePath + "转已转移到" + newFullPath);
            }
            catch (Exception ex)
            {
                errorHandler(2, ex.Message);
            }
        }

        /// <summary>  
        /// 拆分DataTable  
        /// </summary>  
        /// <param name="originalTab">需要分解的表</param>  
        /// <param name="rowsNum">每个表包含的数据量</param>  
        /// <returns></returns>  
        public DataSet SplitDataTable(DataTable originalTab, int rowsNum)
        {
            //获取所需创建的表数量  
            int tableNum = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(originalTab.Rows.Count) / rowsNum));

            DataSet ds = new DataSet();
            if (tableNum == 1)
            {
                ds.Tables.Add(originalTab);
            }
            else
            {
                DataTable[] tableSlice = new DataTable[tableNum];

                //Import Rows  
                for (int i = 0; i < tableNum; i++)
                {
                    tableSlice[i] = originalTab.Clone();

                    for (int j = 0; j < rowsNum; j++)
                    {
                        if ((i * rowsNum + j) >= originalTab.Rows.Count)
                            break;
                        tableSlice[i].ImportRow(originalTab.Rows[i * rowsNum + j]);
                    }
                }

                //add all tables into a dataset                  
                foreach (DataTable dt in tableSlice)
                {
                    ds.Tables.Add(dt);
                }
            }
            return ds;
        }

        public delegate void errorHandlerDel(int typeOfError, String logMessagee);
        public void errorHandler(int typeOfError, String logMessage)
        {
            if (txtConsole.InvokeRequired)
            {
                errorHandlerDel errorDel = new errorHandlerDel(errorHandler);
                Invoke(errorDel, new object[] { typeOfError, logMessage });
            }
            else
            {
                String errorBuilder = null;
                String isSucces = null;
                switch (typeOfError)
                {
                    case 0:
                        isSucces = "SUCCESS";
                        txtConsole.SelectionColor = Color.Black;
                        errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                        break;
                    case 1:
                        isSucces = "SUCCESS";
                        txtConsole.SelectionColor = Color.Blue;
                        errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                        LogHelper.Info(logMessage);
                        break;
                    case 2:
                        isSucces = "FAIL";
                        txtConsole.SelectionColor = Color.Red;
                        errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                        LogHelper.Error(logMessage);
                        break;
                    case 3:
                        isSucces = "FAIL";
                        txtConsole.SelectionColor = Color.Red;
                        errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                        LogHelper.Error(logMessage);
                        break;
                    case 4:
                        isSucces = "SUCCESS";
                        txtConsole.SelectionColor = Color.Black;
                        errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                        LogHelper.Info(logMessage);
                        break;
                    default:
                        isSucces = "FAIL";
                        txtConsole.SelectionColor = Color.Red;
                        errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                        LogHelper.Error(logMessage);
                        break;
                }
                txtConsole.AppendText(errorBuilder);
                txtConsole.ScrollToCaret();
            }
        }
    }
}
