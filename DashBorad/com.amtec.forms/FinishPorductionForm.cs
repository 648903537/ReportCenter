using com.amtec.action;
using com.amtec.configurations;
using com.itac.mes.imsapi.domain.container;
using DataGridViewAutoFilter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.amtec.forms
{
    public partial class FinishPorductionForm : Form
    {
        public ApplicationConfiguration config;
        IMSApiSessionContextStruct sessionContext;
        SocketClientHandler clientSocket;
        DataTable DTBind = null;
        private Hocy_Hook hook_Main = new Hocy_Hook();
        public FinishPorductionForm()
        {
            InitializeComponent();
        }

        public FinishPorductionForm(string userName, DateTime dTime, IMSApiSessionContextStruct _sessionContext, ApplicationConfiguration _config)
        {
            InitializeComponent();
            sessionContext = _sessionContext;
            config = _config;
            lblCaption.Text = "成 品 入 库";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI Symbol", float.Parse(config.lblCaptionsize), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hook_Main.OnKeyUp += new KeyEventHandler(hook_MainKeyUp);
            hook_Main.InstallHook("1");
            timerCurrentTime.Enabled = true;
            clientSocket = new SocketClientHandler(this);
        }

        private void FinishPorductionForm_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            clientSocket.connect(config.IPAddress, config.Port);
            DataTable dtBind = GetFPData();
            DTBind = dtBind;
            if (dtBind != null)
            {
                this.pagerToolbar1.PageSize = config.PageSizeCount;
                this.pagerToolbar1.RowCount = dtBind.Rows.Count;
                this.pagerToolbar1.OnPagerToolBarClick += new System.EventHandler(this.pagerToolbar_Click);
                this.LoadData(this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive);
            }
            SetGridStatus();
            gridFGData.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            timerRefresh.Interval = 1000 * config.RefreshTimeSpan;
            timerRefresh.Enabled = true;
        }

        private void FinishPorductionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("你确定要关闭程序吗？", "Quit Application", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.OK)
            {
                SaveDisplayIndex();
                this.hook_Main.UnInstallHook();
                LogHelper.Info("Application end...");
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void hook_MainKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape )//直接改成按Esc更简单     郑培聪     2017/07/20
            {
                DialogResult dr = MessageBox.Show("你确定要关闭程序吗？", "退出程序", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.OK)
                {
                    //SaveDisplayIndex();
                    //SaveHiddenColumn();
                    LogHelper.Info("Application end...");
                    System.Environment.Exit(0);
                }
                else
                {

                }
            }
        }

        private void pagerToolbar_Click(object sender, System.EventArgs e)
        {
            this.LoadData(this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive);
        }

        private System.Data.DataTable LoadData(int inclusive, int exclusive)
        {
            //DataTable dtBind = GetIQCData(inclusive, exclusive);
            DataRow[] rows = DTBind.Select(string.Format(@"FPSEQ <={0}", exclusive));
            DataTable dtBind = DTBind.Clone();
            if (rows != null)
            {
                if (rows.Count() < inclusive)
                { }
                else
                {
                    for (int i = inclusive - 1; i < rows.Count(); i++)
                    {
                        dtBind.ImportRow(rows[i]);
                    }
                }
            }
            BindingSource dataSource = new BindingSource(dtBind, null);
            gridFGData.DataSource = dataSource;
            gridFGData.ClearSelection();
            SetGridStatus();
            return dtBind;
        }

        public delegate void errorHandlerDel(int typeOfError, String logMessage, String labelMessage);
        public void errorHandler(int typeOfError, String logMessage, String labelMessage)
        {
            String errorBuilder = null;
            String isSucces = null;
            switch (typeOfError)
            {
                case 0:
                    isSucces = "SUCCESS";
                    //txtConsole.SelectionColor = Color.Black;
                    errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                    //SetTipMessage(MessageType.OK, logMessage);
                    LogHelper.Info(logMessage);
                    break;
                case 1:
                    isSucces = "SUCCESS";
                    //txtConsole.SelectionColor = Color.Blue;
                    errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                    //SetTipMessage(MessageType.OK, logMessage);
                    LogHelper.Info(logMessage);
                    break;
                case 2:
                    isSucces = "FAIL";
                    //txtConsole.SelectionColor = Color.Red;
                    errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                    //SetTipMessage(MessageType.Error, logMessage);
                    LogHelper.Error(logMessage);
                    break;
                case 3:
                    isSucces = "FAIL";
                    //txtConsole.SelectionColor = Color.Black;
                    errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                    //SetTipMessage(MessageType.Error, logMessage);
                    LogHelper.Error(logMessage);
                    break;
                default:
                    isSucces = "FAIL";
                    //txtConsole.SelectionColor = Color.Red;
                    errorBuilder = "# " + DateTime.Now.ToString("HH:mm:ss") + " >> " + isSucces + " >< " + logMessage + "\n";
                    break;
            }
        }

        /// <summary>
        /// 获取成品入库数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetFPData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("FPNO", typeof(string));
                dt.Columns.Add("FPSEQ", typeof(int));
                dt.Columns.Add("FPReceiveDate", typeof(string));
                dt.Columns.Add("FPInspectionDate", typeof(string));
                dt.Columns.Add("FPWoType", typeof(string));
                dt.Columns.Add("FPwoNumber", typeof(string));
                dt.Columns.Add("FPPartNumber", typeof(string));
                dt.Columns.Add("FPPartDEsc", typeof(string));
                dt.Columns.Add("FPNumberNo", typeof(string));
                dt.Columns.Add("FPQty", typeof(string));
                dt.Columns.Add("FPUnit", typeof(string));
                dt.Columns.Add("FPInspectionNo", typeof(string));
                dt.Columns.Add("FPProcessState", typeof(string));
                dt.Columns.Add("FPInpsectionState", typeof(string));

                CommonFunction commonHandler = new CommonFunction(sessionContext, this);
                //获取本厂厂别编码
                string plantNo = commonHandler.GetSiteNoByStationNo(config.StationNumber);
                //发送请求入库数据Socket
                string iqcValue = clientSocket.SendData("{getFPResultData;" + plantNo + "}");
                LogHelper.Info("Receive message :" + iqcValue);
                string[] values = iqcValue.Split(new char[] { ';' });
                if (values.Length < 4)
                {
                    LogHelper.Error("No Finish Production data");
                    return null;
                }
                int iCount = Convert.ToInt32(values[1]);
                int iLoop = 11;
                string strValue = values[3].TrimEnd(new char[] { '#' });
                string[] fpItems = strValue.TrimEnd(new char[] { '}' }).Split(new char[] { '!' });
                int iSEQ = 0;
                for (int i = 0; i < fpItems.Length; i += iLoop)
                {
                    iSEQ++;
                    DataRow row = dt.NewRow();
                    LogHelper.Debug("Row index:" + iSEQ);
                    //LogHelper.Debug("Row data:" + fpItems[i] + ";" + fpItems[i + 1] + ";" + fpItems[i + 2] + ";" + fpItems[i + 3] + fpItems[i + 4] + ";" + fpItems[i + 5] + ";" + fpItems[i + 6] + ";" + fpItems[i + 7] + fpItems[i + 8]);
                    row["FPNO"] = iSEQ;
                    row["FPReceiveDate"] = GetDateTimeStringValue(fpItems[i]);
                    row["FPInspectionDate"] = GetDateTimeStringValue(fpItems[i + 1]);
                    row["FPwoNumber"] = fpItems[i + 2];
                    row["FPPartNumber"] = fpItems[i + 3];
                    row["FPPartDEsc"] = fpItems[i + 4];
                    row["FPNumberNo"] = fpItems[i + 5];
                    row["FPQty"] = fpItems[i + 6];
                    row["FPUnit"] = fpItems[i + 7];
                    row["FPInspectionNo"] = fpItems[i + 8];
                    row["FPInpsectionState"] = string.IsNullOrWhiteSpace(row["FPInspectionDate"].ToString()) ? "未检验" : "检验完成";
                    row["FPSEQ"] = GetFPSEQValue(fpItems[i + 9]);
                    row["FPWoType"] = fpItems[i + 10];
                    dt.Rows.Add(row);
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        private string GetDateTimeStringValue(string text)
        {
            string strValue = "";
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    if (Convert.ToDateTime(text) == DateTime.MinValue)
                    {
                        strValue = "";
                    }
                    else
                    {
                        strValue = Convert.ToDateTime(text).ToString("yyyy/MM/dd");
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("Text:" + text);
                    LogHelper.Error(ex);
                }
            }
            return strValue;
        }

        private string GetInspectionState(string text)
        {
            string value = "";
            switch (text)
            {
                case "A":
                    if (config.STATUS_MAP.ContainsKey("A"))
                    {
                        value = config.STATUS_MAP["A"];
                    }
                    else
                        value = "检验";
                    break;
                case "AS":
                    if (config.STATUS_MAP.ContainsKey("A"))
                    {
                        value = config.STATUS_MAP["AS"];
                    }
                    else
                        value = "特殊检验";
                    break;
                case "R":
                    value = "拒收";//no show
                    break;
                case "N":
                    if (config.STATUS_MAP.ContainsKey("N"))
                    {
                        value = config.STATUS_MAP["N"];
                    }
                    else
                        value = "未检";
                    break;
                default:
                    break;
            }
            return value;
        }

        /// <summary>
        /// 工单是否被点检,更新看板行颜色
        /// </summary>
        private void SetGridStatus()
        {
            int iSEQ = 0;
            foreach (DataGridViewRow row in gridFGData.Rows)
            {
                iSEQ++;
                row.Cells["FPNO"].Value = iSEQ;
                string strState = row.Cells["FPInspectionDate"].Value.ToString();
                if (strState.Trim() == "")
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else 
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(153, 217, 234);
                }
                //if (strState == "N")
                //{
                //    row.DefaultCellStyle.BackColor = Color.Yellow;
                //}
                //else if (strState == "A" || strState == "AS")
                //{
                //    row.DefaultCellStyle.BackColor = Color.FromArgb(153, 217, 234);
                //}
            }
            SetGridFont();
            gridFGData.ClearSelection();
        }

        private int GetFPSEQValue(string text)
        {
            int iValue = 0;
            try
            {
                iValue = Convert.ToInt32(Convert.ToDecimal(text));
                return iValue;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Text:" + text);
                LogHelper.Error(ex);
                return iValue;
            }
        }

        private void SetGridStatusExt()
        {
            foreach (DataGridViewRow row in gridFGData.Rows)
            {
                string strState = row.Cells["FPProcessState"].Value.ToString();
                if (strState == "N")
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (strState == "A" || strState == "AS")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(153, 217, 234);
                }
            }
            SetGridFont();
        }

        Dictionary<string, int> dicColumnIndex = new Dictionary<string, int>();
        int iIndexItem = -1;
        private void gridFGData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (gridFGData == null || gridFGData.Rows.Count == 0)
                return;
            if (e.Button == MouseButtons.Right)
            {
                if (this.gridFGData.Rows.Count == 0)
                    return;
                this.gridFGData.ContextMenuStrip = contextMenuStrip1;
                iIndexItem = e.ColumnIndex;
            }
            if (gridFGData.Columns[e.ColumnIndex].Name != "FPNO")
            {
                SetGridStatus();
            }
            else
            {
                SetGridStatusExt();
            }
            gridFGData.ClearSelection();
        }

        private void toolHide_Click(object sender, EventArgs e)
        {
            if (iIndexItem != -1)
            {
                gridFGData.Columns[iIndexItem].Visible = false;
                this.gridFGData.ContextMenuStrip = null;
            }
        }

        private void toolShowAll_Click(object sender, EventArgs e)
        {
            if (iIndexItem != -1)
            {
                foreach (DataGridViewColumn item in this.gridFGData.Columns)
                {
                    string columnName = item.Name;
                    if (columnName == "IQCProcessState")
                    {
                        item.Visible = false;
                    }
                    else
                    {
                        item.Visible = true;
                    }
                }
                SetMTOGridDisplayIndex();
                this.gridFGData.ContextMenuStrip = null;
            }
        }

        private void InitGridDisplayIndex()
        {
            foreach (DataGridViewColumn item in this.gridFGData.Columns)
            {
                string columnName = item.Name;
                int iDisplayIndex = item.DisplayIndex;
                dicColumnIndex[columnName] = iDisplayIndex;
            }
            ReadDisplayIndexFromFile();
        }

        private void InitGridDisplayHidden()
        {
            ReadHiddenColumnFromFile();
            foreach (DataGridViewColumn item in this.gridFGData.Columns)
            {
                string columnName = item.Name;
                if (hiddenColumnList.Contains(columnName))
                {
                    item.Visible = false;
                }
            }
        }

        private void SetMTOGridDisplayIndex()
        {
            foreach (DataGridViewColumn item in gridFGData.Columns)
            {
                string columnName = item.Name;
                item.DisplayIndex = dicColumnIndex[columnName];
                LogHelper.Debug(columnName + ";" + item.DisplayIndex);
            }
            gridFGData.Refresh();
        }

        private void SaveDisplayIndex()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataGridViewColumn item in this.gridFGData.Columns)
                {
                    string columnName = item.Name;
                    int iDisplayIndex = item.DisplayIndex;
                    sb.AppendLine(columnName + ";" + iDisplayIndex);
                }
                string filePath = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
                string _appDir = Path.GetDirectoryName(filePath);
                FileStream fs = new FileStream(_appDir + @"/fggrid.txt", FileMode.OpenOrCreate);
                byte[] bt = Encoding.UTF8.GetBytes(sb.ToString());
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(bt, 0, bt.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void ReadDisplayIndexFromFile()
        {
            string filePath = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string _appDir = Path.GetDirectoryName(filePath);
            string fullFilePath = _appDir + @"/fggrid.txt";
            if (!File.Exists(fullFilePath))
                return;
            string[] lines = File.ReadAllLines(fullFilePath);
            if (lines != null && lines.Length > 0)
            {
                foreach (var line in lines)
                {
                    string[] values = line.Split(new char[] { ';' });
                    if (string.IsNullOrEmpty(line) || values.Length == 1)
                        continue;
                    dicColumnIndex[values[0]] = Convert.ToInt32(values[1]);
                }
            }
        }

        List<string> hiddenColumnList = new List<string>();
        private void ReadHiddenColumnFromFile()
        {
            hiddenColumnList.Clear();
            string filePath = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string _appDir = Path.GetDirectoryName(filePath);
            string fullFilePath = _appDir + @"/fghidden.txt";
            if (!File.Exists(fullFilePath))
                return;
            string[] lines = File.ReadAllLines(fullFilePath);
            if (lines != null && lines.Length > 0)
            {
                foreach (var line in lines)
                {
                    hiddenColumnList.Add(line.Trim());
                }
            }
        }

        private void timerCurrentTime_Tick(object sender, EventArgs e)
        {
            this.lblCurTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            DataTable dtBind = GetFPData();
            if (dtBind != null)
            {
                bool isSame = CompareDataTable(DTBind, dtBind);
                if (isSame)
                {
                    LogHelper.Debug("==");
                    return;
                }
                else
                {
                    DTBind = dtBind.Copy();
                    this.pagerToolbar1.PageSize = config.PageSizeCount;
                    this.pagerToolbar1.RowCount = dtBind.Rows.Count;
                    this.pagerToolbar1.OnPagerToolBarClick += new System.EventHandler(this.pagerToolbar_Click);
                    this.LoadData(this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive);
                }
            }
        }

        private void SetGridFont()
        {
            gridFGData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            gridFGData.RowsDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
        }

        private bool CompareDataTable(DataTable dtA, DataTable dtB)
        {
            if (dtA == null && dtB != null)
                return false;
            else if (dtB == null)
                return true;
            if (dtA.Rows.Count == dtB.Rows.Count)
            {
                if (CompareColumn(dtA.Columns, dtB.Columns))
                {
                    //比内容 
                    for (int i = 0; i < dtA.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtA.Columns.Count; j++)
                        {
                            if (!dtA.Rows[i][j].Equals(dtB.Rows[i][j]))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CompareColumn(System.Data.DataColumnCollection dcA, System.Data.DataColumnCollection dcB)
        {
            if (dcA.Count == dcB.Count)
            {
                foreach (DataColumn dc in dcA)
                {
                    //找相同字段名称 
                    if (dcB.IndexOf(dc.ColumnName) > -1)
                    {
                        //测试数据类型 
                        if (dc.DataType != dcB[dcB.IndexOf(dc.ColumnName)].DataType)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void gridFGData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell =
                    gridFGData.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        private void gridFGData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //String filterStatus = DataGridViewAutoFilterColumnHeaderCell
            //   .GetFilterStatus(gridIQCData);
            //if (String.IsNullOrEmpty(filterStatus))
            //{
            //    showAllLabel.Visible = false;
            //    filterStatusLabel.Visible = false;
            //}
            //else
            //{
            //    //showAllLabel.Visible = true;
            //    //filterStatusLabel.Visible = true;
            //    //filterStatusLabel.Text = filterStatus;
            //}
            if (gridFGData == null || gridFGData.Rows.Count == 0)
                return;
            SetGridStatus();
        }

        /// <summary>
        /// 查找看板的单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (textBox1.Text.Trim() == string.Empty)
                    {
                        this.LoadData(this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive);
                    }
                    else
                    {
                        DataTable dt = DTBind.Clone();

                        var drValues = from p in DTBind.AsEnumerable()
                                       where p.Field<string>("FPNumberNo") == textBox1.Text
                                       select p;

                        foreach (DataRow dr in drValues)
                        {
                            dt.ImportRow(dr);
                        }

                        BindingSource dataSource = new BindingSource(dt, null);
                        gridFGData.DataSource = dataSource;
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
    }
}
