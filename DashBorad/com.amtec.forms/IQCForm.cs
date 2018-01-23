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
    public partial class IQCForm : Form
    {
        public ApplicationConfiguration config;
        IMSApiSessionContextStruct sessionContext;
        SocketClientHandler clientSocket;
        DataTable DTBind = null;
        private Hocy_Hook hook_Main = new Hocy_Hook();
        public IQCForm()
        {
            InitializeComponent();
        }

        public IQCForm(string userName, DateTime dTime, IMSApiSessionContextStruct _sessionContext, ApplicationConfiguration _config)
        {
            InitializeComponent();
            sessionContext = _sessionContext;
            config = _config;
            lblCaption.Text = "收  料  看  板";
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI Symbol", float.Parse(config.lblCaptionsize), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hook_Main.OnKeyUp += new KeyEventHandler(hook_MainKeyUp);
            hook_Main.InstallHook("1");
            timerCurrentTime.Enabled = true;
            clientSocket = new SocketClientHandler(this);
        }

        private void IQCForm_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            clientSocket.connect(config.IPAddress, config.Port);
            DataTable dtBind = GetIQCData();
            DTBind = dtBind;
            if (dtBind != null)
            {
                this.pagerToolbar1.PageSize = config.PageSizeCount;
                this.pagerToolbar1.RowCount = dtBind.Rows.Count;
                this.pagerToolbar1.OnPagerToolBarClick += new System.EventHandler(this.pagerToolbar_Click);
                this.LoadData(this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive);
            }
            //InitGridDisplayIndex();
            //InitGridDisplayHidden();
            //SetMTOGridDisplayIndex();
            SetGridStatus();
            gridIQCData.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            timerRefresh.Interval = 1000 * config.RefreshTimeSpan;
            timerRefresh.Enabled = true;
        }

        private void IQCForm_FormClosing(object sender, FormClosingEventArgs e)
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
            if (e.KeyCode == Keys.Escape && Control.ModifierKeys == Keys.Shift)
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
            DataRow[] rows = DTBind.Select(string.Format(@"IQCSEQ <{0}", exclusive));
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
            gridIQCData.DataSource = dataSource;
            gridIQCData.ClearSelection();
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

        private DataTable GetIQCData(int inclusive, int exclusive)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IQCNO", typeof(string));
                dt.Columns.Add("IQCReceiveDate", typeof(string));
                dt.Columns.Add("IQCInspectionDate", typeof(string));
                dt.Columns.Add("IQCVenderName", typeof(string));
                dt.Columns.Add("IQCPartNumber", typeof(string));
                dt.Columns.Add("IQCPartDEsc", typeof(string));
                dt.Columns.Add("IQCPurchNo", typeof(string));
                dt.Columns.Add("IQCUnit", typeof(string));
                dt.Columns.Add("IQCQty", typeof(string));
                dt.Columns.Add("IQCMatDocNo", typeof(string));
                dt.Columns.Add("IQCBatchNo", typeof(string));
                dt.Columns.Add("IQCProcessState", typeof(string));
                //dt.Columns.Add("IQCProcessStateDesc", typeof(string));
                dt.Columns.Add("IQCInpsectionState", typeof(string));

                CommonFunction commonHandler = new CommonFunction(sessionContext, this);
                string plantNo = commonHandler.GetSiteNoByStationNo(config.StationNumber);
                string iqcValue = clientSocket.SendData("#11;" + plantNo + ";" + inclusive + ";" + exclusive + "#");
                LogHelper.Info("Receive message :" + iqcValue);
                string[] values = iqcValue.Split(new char[] { ';' });
                if (values.Length < 4)
                {
                    LogHelper.Error("No IQC data");
                    return null;
                }
                int iCount = Convert.ToInt32(values[1]);
                int iLoop = Convert.ToInt32(values[2]);
                string strValue = values[3].TrimEnd(new char[] { '#' });
                string[] iqcItems = strValue.Split(new char[] { ',' });
                int iSEQ = 0;
                for (int i = 0; i < iqcItems.Length; i += iLoop)
                {
                    iSEQ++;
                    DataRow row = dt.NewRow();
                    row["IQCNO"] = iSEQ;
                    row["IQCReceiveDate"] = Convert.ToDateTime(iqcItems[i]).ToString("yyyy/MM/dd HH:mm:ss");
                    row["IQCInspectionDate"] = Convert.ToDateTime(iqcItems[i + 1]).ToString("yyyy/MM/dd HH:mm:ss");
                    row["IQCVenderName"] = iqcItems[i + 2];
                    row["IQCPartNumber"] = iqcItems[i + 3];
                    row["IQCPartDEsc"] = iqcItems[i + 4];
                    row["IQCPurchNo"] = iqcItems[i + 5];
                    row["IQCUnit"] = iqcItems[i + 6];
                    row["IQCQty"] = iqcItems[i + 7];
                    row["IQCMatDocNo"] = iqcItems[i + 8];
                    row["IQCBatchNo"] = iqcItems[i + 9];
                    row["IQCProcessState"] = iqcItems[i + 11];
                    //row["IQCProcessStateDesc"] = iqcItems[i + 10] == "0" ? "未检验" : "已检验";
                    row["IQCInpsectionState"] = GetInspectionState(iqcItems[i + 11]);
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

        private DataTable GetIQCData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IQCNO", typeof(string));
                dt.Columns.Add("IQCReceiveDate", typeof(string));
                dt.Columns.Add("IQCInspectionDate", typeof(string));
                dt.Columns.Add("IQCVenderName", typeof(string));
                dt.Columns.Add("IQCPartNumber", typeof(string));
                dt.Columns.Add("IQCPartDEsc", typeof(string));
                dt.Columns.Add("IQCPurchNo", typeof(string));
                dt.Columns.Add("IQCUnit", typeof(string));
                dt.Columns.Add("IQCQty", typeof(string));
                dt.Columns.Add("IQCMatDocNo", typeof(string));
                dt.Columns.Add("IQCBatchNo", typeof(string));
                dt.Columns.Add("IQCProcessState", typeof(string));
                //dt.Columns.Add("IQCProcessStateDesc", typeof(string));
                dt.Columns.Add("IQCInpsectionState", typeof(string));
                dt.Columns.Add("IQCSEQ", typeof(int));

                CommonFunction commonHandler = new CommonFunction(sessionContext, this);
                string plantNo = commonHandler.GetSiteNoByStationNo(config.StationNumber);
                string iqcValue = clientSocket.SendData("{getIQCResultData;" + plantNo + "}"); //clientSocket.SendData("#10;" + plantNo + ";IQC request data#");
                LogHelper.Info("Receive message :" + iqcValue);
                string[] values = iqcValue.Split(new char[] { ';' });
                if (values.Length < 4)
                {
                    LogHelper.Error("No IQC data");
                    return null;
                }
                int iCount = Convert.ToInt32(values[1]);
                int iLoop = Convert.ToInt32(values[2]);
                string strValue = values[3].TrimEnd(new char[] { '#' });
                string[] iqcItems = strValue.TrimEnd(new char[] { '}' }).Split(new char[] { '!' });
                int iSEQ = 0;
                for (int i = 0; i < iqcItems.Length; i += iLoop)
                {
                    iSEQ++;
                    DataRow row = dt.NewRow();
                    LogHelper.Debug("Row index:" + iSEQ);
                    LogHelper.Debug("Row data:" + iqcItems[i] + ";" + iqcItems[i + 1] + ";" + iqcItems[i + 2] + ";" + iqcItems[i + 3] + iqcItems[i + 4] + ";" + iqcItems[i + 5] + ";" + iqcItems[i + 6] + ";" + iqcItems[i + 7] + iqcItems[i + 8] + ";" + iqcItems[i + 9] + ";" + iqcItems[i + 10] + ";" + iqcItems[i + 11] + ";" + iqcItems[i + 12] + ";" + iqcItems[i + 13]);
                    row["IQCNO"] = iSEQ;
                    row["IQCReceiveDate"] = GetDateTimeStringValue(iqcItems[i]);
                    row["IQCInspectionDate"] = GetDateTimeStringValue(iqcItems[i + 1]);
                    row["IQCVenderName"] = iqcItems[i + 2];
                    row["IQCPartNumber"] = iqcItems[i + 3];
                    row["IQCPartDEsc"] = iqcItems[i + 4];
                    row["IQCPurchNo"] = iqcItems[i + 5];
                    row["IQCUnit"] = iqcItems[i + 6];
                    row["IQCQty"] = iqcItems[i + 7];
                    row["IQCMatDocNo"] = iqcItems[i + 8];
                    row["IQCBatchNo"] = iqcItems[i + 13]; //iqcItems[i + 9];
                    row["IQCProcessState"] = iqcItems[i + 11];
                    //row["IQCProcessStateDesc"] = iqcItems[i + 10] == "0" ? "未检验" : "已检验";
                    row["IQCInpsectionState"] = GetInspectionState(iqcItems[i + 11]);
                    row["IQCSEQ"] = GetIQCSEQValue(iqcItems[i + 12]);
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

        private int GetIQCSEQValue(string text)
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

        private void SetGridStatus()
        {
            int iSEQ = 0;
            foreach (DataGridViewRow row in gridIQCData.Rows)
            {
                iSEQ++;
                row.Cells["IQCNO"].Value = iSEQ;
                string strState = row.Cells["IQCProcessState"].Value.ToString();
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
            gridIQCData.ClearSelection();
        }

        private void SetGridStatusExt()
        {
            foreach (DataGridViewRow row in gridIQCData.Rows)
            {
                string strState = row.Cells["IQCProcessState"].Value.ToString();
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
        private void gridIQCData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (gridIQCData.Columns[e.ColumnIndex].Name != "IQCNO")
            {
                SetGridStatus();
            }
            else
            {
                SetGridStatusExt();
            }
            gridIQCData.ClearSelection();
        }

        private void toolHide_Click(object sender, EventArgs e)
        {
            if (iIndexItem != -1)
            {
                gridIQCData.Columns[iIndexItem].Visible = false;
                this.gridIQCData.ContextMenuStrip = null;
            }
        }

        private void toolShowAll_Click(object sender, EventArgs e)
        {
            if (iIndexItem != -1)
            {
                foreach (DataGridViewColumn item in this.gridIQCData.Columns)
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
                this.gridIQCData.ContextMenuStrip = null;
            }
        }

        private void InitGridDisplayIndex()
        {
            foreach (DataGridViewColumn item in this.gridIQCData.Columns)
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
            foreach (DataGridViewColumn item in this.gridIQCData.Columns)
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
            foreach (DataGridViewColumn item in gridIQCData.Columns)
            {
                string columnName = item.Name;
                item.DisplayIndex = dicColumnIndex[columnName];
                LogHelper.Debug(columnName + ";" + item.DisplayIndex);
            }
            gridIQCData.Refresh();
        }

        private void SaveDisplayIndex()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataGridViewColumn item in this.gridIQCData.Columns)
                {
                    string columnName = item.Name;
                    int iDisplayIndex = item.DisplayIndex;
                    sb.AppendLine(columnName + ";" + iDisplayIndex);
                }
                string filePath = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
                string _appDir = Path.GetDirectoryName(filePath);
                FileStream fs = new FileStream(_appDir + @"/iqcgrid.txt", FileMode.OpenOrCreate);
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

        private void SaveHiddenColumn()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataGridViewColumn item in this.gridIQCData.Columns)
                {
                    string columnName = item.Name;
                    if (item.Visible == false)
                    {
                        sb.AppendLine(columnName);
                    }
                }
                string filePath = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
                string _appDir = Path.GetDirectoryName(filePath);
                FileStream fs = new FileStream(_appDir + @"/iqchidden.txt", FileMode.OpenOrCreate);
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
            string fullFilePath = _appDir + @"/iqcgrid.txt";
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
            string fullFilePath = _appDir + @"/iqchidden.txt";
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
            DataTable dtBind = GetIQCData();
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
            gridIQCData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            gridIQCData.RowsDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
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

        private void gridIQCData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell =
                    gridIQCData.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        private void gridIQCData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
            SetGridStatus();
        }

        private void showAllLabel_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(gridIQCData);
        }
    }
}
