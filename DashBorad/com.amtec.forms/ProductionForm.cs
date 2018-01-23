using com.amtec.action;
using com.amtec.configurations;
using com.amtec.model;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace com.amtec.forms
{
    public partial class ProductionForm : Form
    {
        public ApplicationConfiguration config;
        IMSApiSessionContextStruct sessionContext;
        SocketClientHandler clientSocket;
        DataTable DTBind = null;
        private Hocy_Hook hook_Main = new Hocy_Hook();
        private System.Timers.Timer AutoRefreshTimer = null;
        public ProductionForm()
        {
            InitializeComponent();
        }

        public ProductionForm(string userName, DateTime dTime, IMSApiSessionContextStruct _sessionContext, ApplicationConfiguration _config)
        {
            InitializeComponent();
            System.Drawing.Size mSize = SystemInformation.WorkingArea.Size;
            screenHeight = mSize.Height;
            screenWidth = mSize.Width;

            sessionContext = _sessionContext;
            config = _config;
            SystemVariable.CurrentLangaugeCode = "ZHS";
            InitCintrolLanguage(this);
            //lblCaption.Text = "     "+config.LINE_DESCRIPTION;//"收  料  看  板";
            this.lblCaption.Font = new System.Drawing.Font("Calibri", float.Parse(config.lblCaptionsize) * (screenWidth / 1024), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hook_Main.OnKeyUp += new KeyEventHandler(hook_MainKeyUp);
            hook_Main.InstallHook("1");
            timerCurrentTime.Enabled = true;
            //clientSocket = new SocketClientHandler(this);
        }

        int screenHeight = 0;
        int screenWidth = 0;
        private void IQCForm_Load(object sender, EventArgs e)
        {
            Application.DoEvents();

            GetCurrentWO();
            this.gridIQCData.Font = new System.Drawing.Font("Calibri", float.Parse(config.lblGridsize) * (screenWidth / 1024), FontStyle.Bold);
            this.gridIQCData.ForeColor = Color.White;
            this.gridIQCData.RowsDefaultCellStyle.BackColor = Color.Black;
            this.gridIQCData.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;//Color.Gainsboro;
            this.gridIQCData.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;//Color.Black;
            this.gridIQCData.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", float.Parse(config.lblGridHeadersize) * (screenWidth / 1024), FontStyle.Bold);

            //clientSocket.connect(config.IPAddress, config.Port);

            DataTable dtBind = GetIQCData();
            DTBind = dtBind;
            if (dtBind != null)
            {
                this.pagerToolbar1.PageSize = config.PageSizeCount;
                this.pagerToolbar1.RowCount = dtBind.Rows.Count;
                //this.pagerToolbar1.OnPagerToolBarClick += new System.EventHandler(this.pagerToolbar_Click);
                this.LoadData(this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive);//this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive
            }
            //InitGridDisplayIndex();
            //InitGridDisplayHidden();
            //SetMTOGridDisplayIndex();
            //SetGridStatus();
            gridIQCData.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            timerRefresh.Interval = 1000 * config.RefreshTimeSpan;
            timerRefresh.Enabled = true;
            //AutoRefreshTimerStart();
        }

        #region ADD BY QY
        Dictionary<string, string> dicStation = new Dictionary<string, string>();
        Dictionary<string, string> dicStationdesc = new Dictionary<string, string>();
        Dictionary<string, string> dicStationdescbyWP = new Dictionary<string, string>();
        private void GetCurrentWO()
        {

            GetCurrentWorkorder currentWorkorder = new GetCurrentWorkorder(sessionContext, this);
            GetStationSettingModel stationsetting = currentWorkorder.GetCurrentWorkorderResultCall(config.StationNumber);
            if (stationsetting != null)
            {
                if (lblWorkorder.Text != stationsetting.workorderNumber)
                {
                    dicStation = new Dictionary<string, string>();
                    dicStationdescbyWP = new Dictionary<string, string>();
                    this.lblWorkorder.Text = stationsetting.workorderNumber;
                    this.lblPartNumber.Text = stationsetting.partNumber;
                    this.lblPartDesc.Text = stationsetting.partdesc;
                    this.lblQuantity.Text = stationsetting.QuantityMO.ToString();
                    string[] machineAssetStructureValues = currentWorkorder.GetMachineStructrueData(config.LINE_DESCRIPTION, config.StationNumber);
                    if (machineAssetStructureValues != null)
                    {
                        lblCaption.Text = "" + machineAssetStructureValues[2];
                        for (int j = 0; j < machineAssetStructureValues.Length / 3; j++)
                        {
                            string stationdesc = machineAssetStructureValues[j * 3 + 1];
                            string station = machineAssetStructureValues[j * 3];
                            dicStationdesc[station] = stationdesc;
                            LogHelper.Debug("line station number:" + station);
                        }
                    }
                    string[] workplanDataResultValues = currentWorkorder.GetProcessLayerByWO(this.lblWorkorder.Text, config.StationNumber);
                    if (workplanDataResultValues != null)
                    {
                        for (int j = 0; j < workplanDataResultValues.Length / 2; j++)
                        {
                            string erpgroupnumber = workplanDataResultValues[j * 2 + 1];
                            string processlayer = workplanDataResultValues[j * 2];
                            dicStation[erpgroupnumber + "," + processlayer] = erpgroupnumber;
                        }
                    }

                    string[] workplanDataResultValues2 = currentWorkorder.GetstationdescByWO(this.lblWorkorder.Text, config.StationNumber);
                    if (workplanDataResultValues2 != null)
                    {
                        for (int j = 0; j < workplanDataResultValues2.Length / 3; j++)
                        {
                            string stationdesc = workplanDataResultValues2[j * 3 + 1];
                            string stationid = workplanDataResultValues2[j * 3];
                            string processlayer = workplanDataResultValues2[j * 3 + 2];
                            dicStationdescbyWP[stationid + ";" + processlayer] = stationdesc;
                        }
                    }
                }

            }


        }
        public void InitCintrolLanguage(Form form)
        {
            MutiLanguages lang = new MutiLanguages();
            foreach (Control ctl in this.Controls)
            {
                lang.InitLangauge(ctl);
                if (ctl is TabControl)
                {
                    lang.InitLangaugeForTabControl((TabControl)ctl);
                }
            }

            //Controls不包含ContextMenuStrip，可用以下方法获得
            System.Reflection.FieldInfo[] fieldInfo = this.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            for (int i = 0; i < fieldInfo.Length; i++)
            {
                switch (fieldInfo[i].FieldType.Name)
                {
                    case "ContextMenuStrip":
                        ContextMenuStrip contextMenuStrip = (ContextMenuStrip)fieldInfo[i].GetValue(this);
                        lang.InitLangauge(contextMenuStrip);
                        break;
                }
            }
        }
        private System.Data.DataTable LoadData()
        {
            BindingSource dataSource = new BindingSource(DTBind, null);
            gridIQCData.DataSource = dataSource;
            gridIQCData.ClearSelection();
            foreach (DataGridViewRow row in this.gridIQCData.Rows)
            {
                row.Cells["pStation"].Style.ForeColor = Color.Gainsboro;
                row.Cells["pPass"].Style.ForeColor = Color.Green;
                row.Cells["pYield"].Style.ForeColor = Color.White;
                row.Cells["pFail"].Style.ForeColor = Color.Red;
                row.Cells["pScrap"].Style.ForeColor = Color.Red;
            }
            //SetGridStatus();
            return DTBind;
        }

        public void AutoRefreshTimerStart()
        {
            AutoRefreshTimer = new System.Timers.Timer();
            if (AutoRefreshTimer.Enabled)
                return;
            // 循环间隔时间(1分钟)
            AutoRefreshTimer.Interval = Convert.ToInt32(config.PageNextTimer) * 1000;
            // 允许Timer执行
            AutoRefreshTimer.Enabled = true;
            // 定义回调
            AutoRefreshTimer.Elapsed += new ElapsedEventHandler(AutoRefreshTimer_Elapsed);
            // 定义多次循环
            AutoRefreshTimer.AutoReset = true;
        }
        private void AutoRefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!isInitData)
            {
                if (DTBind.Rows.Count <= config.PageSizeCount)
                {
                    pageindex = 1;
                }
                else
                {
                    pageindex++;
                }
                LogHelper.Info("PageNextIndex:" + pageindex);
                pagerToolbar_Click(null, null);
            }

        }
        public void AutoRefreshTimerStop()
        {
            if (AutoRefreshTimer == null)
                return;
            AutoRefreshTimer.Stop();
        }
        #endregion
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

        int pageindex = 1;
        private void pagerToolbar_Click(object sender, System.EventArgs e)
        {
            int start = (pageindex - 1) * config.PageSizeCount + 1;
            int end = start + config.PageSizeCount - 1;
            this.LoadData(start, end);//this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive
        }

        private System.Data.DataTable LoadData(int inclusive, int exclusive)
        {
            try
            {
                //DataTable dtBind = GetIQCData(inclusive, exclusive);
                DataRow[] rows = DTBind.Select(string.Format(@"IQCSEQ <{0}", exclusive));
                DataTable dtBind = DTBind.Clone();
                if (rows != null)
                {
                    if (rows.Count() < inclusive)
                    {
                    }
                    else
                    {
                        //if (rows.Count() - inclusive < exclusive)
                        if (rows.Count() - inclusive < config.PageSizeCount)
                        {
                            pageindex = 0;
                            exclusive = inclusive + rows.Count() - inclusive;
                            //exclusive = exclusive - config.PageSizeCount+3;
                        }

                        //for (int i = inclusive - 1; i < inclusive + exclusive - 1; i++)//rows.Count()
                        for (int i = inclusive - 1; i < exclusive; i++)
                        {
                            dtBind.ImportRow(rows[i]);
                        }
                    }
                }
                BindingSource dataSource = new BindingSource(dtBind, null);
                this.Invoke(new MethodInvoker(delegate
                {
                    gridIQCData.DataSource = dataSource;
                    gridIQCData.ClearSelection();
                    //SetGridStatus();
                    foreach (DataGridViewRow row in this.gridIQCData.Rows)
                    {
                        row.Cells["pStation"].Style.ForeColor = Color.Gainsboro;
                        row.Cells["pPass"].Style.ForeColor = Color.Lime;
                        row.Cells["pYield"].Style.ForeColor = Color.White;
                        row.Cells["pFail"].Style.ForeColor = Color.Red;
                        row.Cells["pScrap"].Style.ForeColor = Color.Red;
                    }
                }));
                return dtBind;
            }
            catch (Exception ex)
            {
                return null;
            }
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

        private DataTable GetIQCData1()
        {
            try
            {
                AutoRefreshTimerStop();
                pageindex = 1;
                //gridIQCData.Rows.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("pStation", typeof(string));
                dt.Columns.Add("pPass", typeof(string));
                dt.Columns.Add("pFail", typeof(string));
                dt.Columns.Add("pScrap", typeof(string));
                dt.Columns.Add("pYield", typeof(string));
                dt.Columns.Add("IQCSEQ", typeof(string));

                CommonFunction commonHandler = new CommonFunction(sessionContext, this);
                string plantNo = commonHandler.GetSiteNoByStationNo(config.StationNumber);
                string iqcValue = clientSocket.SendData("{getSNBookedForWorkOrder;" + lblWorkorder.Text + "}"); //clientSocket.SendData("#10;" + plantNo + ";IQC request data#");
                LogHelper.Info("Receive message :" + iqcValue);

                //if (values.Length < 3)
                //{
                //    LogHelper.Error("No data");
                //    return null;
                //}
                if (iqcValue != "" && iqcValue != null)
                {
                    string[] values = iqcValue.TrimEnd(';').Replace("{", "").Replace("}", "").Replace("#", "").Split(new string[] { ";" }, StringSplitOptions.None);
                    string status = values[1];
                    if (status == "0")//“0” , or “-1” (error)  
                    {
                        string itemregular = @"\[[^\[\]]+\]";
                        MatchCollection match = Regex.Matches(iqcValue, itemregular);
                        Dictionary<string, string> dicMatch = new Dictionary<string, string>();
                        for (int i = 0; i < match.Count; i++)
                        {
                            string data = match[i].ToString().TrimStart('[').TrimEnd(']');
                            string[] datas = data.Split(',');
                            dicMatch[datas[0] + ";" + datas[1]] = datas[0];
                            LogHelper.Debug("Portal stationdesc:" + dicMatch[datas[0] + ";" + datas[1]]);
                        }

                        if (dicStation != null && dicStation.Keys.Count > 0)
                        {
                            foreach (var keys in dicStation.Keys)
                            {
                                int count = 0;
                                DataRow row = dt.NewRow();
                                bool isMatch = false;
                                string stationdesc = "";
                                foreach (var key in new List<string>(dicStationdescbyWP.Keys))
                                {
                                    string station_number = key.Split(';')[0];
                                    string station_layer = key.Split(';')[1];
                                    if (match.Count == 0)
                                    {
                                        if (station_number.Contains(keys.Split(',')[0]) && station_layer == keys.Split(',')[1])
                                        {
                                            if (dicStationdesc.Keys.Contains(station_number))
                                            {
                                                stationdesc = dicStationdesc[station_number];
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //LogHelper.Debug("stationdesc:" + dicStationdescbyWP[key] + ";" + keys.Split(',')[1]);
                                        if (dicMatch.Keys.Contains(dicStationdescbyWP[key] + ";" + keys.Split(',')[1]))
                                        {
                                            stationdesc = dicStationdescbyWP[key];
                                            dicMatch.Remove(dicStationdescbyWP[key] + ";" + keys.Split(',')[1]);
                                            break;
                                        }
                                    }
                                }
                                if (stationdesc == "")
                                {
                                    foreach (var key in new List<string>(dicStationdescbyWP.Keys))
                                    {
                                        string station_number = key.Split(';')[0];
                                        string station_layer = key.Split(';')[1];
                                        if (station_number.Contains(keys.Split(',')[0]) && station_layer == keys.Split(',')[1])
                                        {
                                            LogHelper.Debug("work plan station_number:" + station_number);
                                            if (dicStationdesc.Keys.Contains(station_number))
                                            {
                                                stationdesc = dicStationdesc[station_number];
                                                break;
                                            }
                                        }
                                    }
                                    if (stationdesc == "")
                                    {
                                        foreach (var key in new List<string>(dicStationdescbyWP.Keys))
                                        {
                                            string station_number = key.Split(';')[0];
                                            string station_layer = key.Split(';')[1];
                                            if (station_number.Contains(keys.Split(',')[0]) && station_layer == keys.Split(',')[1])
                                            {
                                                stationdesc = dicStationdescbyWP[key];
                                                break;
                                            }
                                        }
                                    }
                                }
                                for (int i = 0; i < match.Count; i++)
                                {
                                    string data = match[i].ToString().TrimStart('[').TrimEnd(']');
                                    string[] datas = data.Split(',');

                                    if (datas[0] == stationdesc && datas[1] == keys.Split(',')[1])
                                    {
                                        count++;
                                        LogHelper.Debug("Row data:" + datas[0] + ";" + datas[1] + ";" + datas[2] + ";" + datas[3] + ";" + datas[4]);
                                        row["pStation"] = stationdesc;
                                        row["pPass"] = datas[2];
                                        row["pFail"] = datas[3];
                                        row["pScrap"] = datas[4];
                                        int totalQty = Convert.ToInt32(datas[2]) + Convert.ToInt32(datas[3]) + Convert.ToInt32(datas[4]);
                                        row["pYield"] = Math.Round(Convert.ToDecimal(datas[2]) / Convert.ToDecimal(totalQty) * 100, 2) + "%";
                                        row["IQCSEQ"] = count;
                                        isMatch = true;
                                        break;
                                    }
                                }
                                if (!isMatch)
                                {
                                    count++;
                                    row["pStation"] = stationdesc;
                                    row["pPass"] = "0";
                                    row["pFail"] = "0";
                                    row["pScrap"] = "0";
                                    row["pYield"] = "0.00%";
                                    row["IQCSEQ"] = count;
                                }
                                dt.Rows.Add(row);
                            }
                            AutoRefreshTimerStart();
                        }
                    }
                }
                else
                {
                    //clientSocket.connect(config.IPAddress, config.Port);
                    iqcValue = clientSocket.SendData("{getSNBookedForWorkOrder;" + lblWorkorder.Text + "}"); //clientSocket.SendData("#10;" + plantNo + ";IQC request data#");
                    LogHelper.Info("Receive message :" + iqcValue);
                    string[] values = iqcValue.TrimEnd(';').Replace("{", "").Replace("}", "").Replace("#", "").Split(new string[] { ";" }, StringSplitOptions.None);
                    //string[] values = iqcValue.Split(new char[] { ';' });
                    values = iqcValue.TrimEnd(';').Replace("{", "").Replace("}", "").Replace("#", "").Split(new string[] { ";" }, StringSplitOptions.None);
                    //if (values.Length < 3)
                    //{
                    //    LogHelper.Error("No data");
                    //    return null;
                    //}
                    if (iqcValue != "" && iqcValue != null)
                    {
                        string status = values[1];
                        if (status == "0")//“0” , or “-1” (error)  
                        {
                            string itemregular = @"\[[^\[\]]+\]";
                            MatchCollection match = Regex.Matches(iqcValue, itemregular);
                            Dictionary<string, string> dicMatch = new Dictionary<string, string>();
                            for (int i = 0; i < match.Count; i++)
                            {
                                string data = match[i].ToString().TrimStart('[').TrimEnd(']');
                                string[] datas = data.Split(',');
                                dicMatch[datas[0] + ";" + datas[1]] = datas[0];
                                LogHelper.Debug("Portal stationdesc:" + dicMatch[datas[0] + ";" + datas[1]]);
                            }

                            if (dicStation != null && dicStation.Keys.Count > 0)
                            {
                                foreach (var keys in dicStation.Keys)
                                {
                                    int count = 0;
                                    DataRow row = dt.NewRow();
                                    bool isMatch = false;
                                    string stationdesc = "";
                                    foreach (var key in new List<string>(dicStationdescbyWP.Keys))
                                    {
                                        string station_number = key.Split(';')[0];
                                        string station_layer = key.Split(';')[1];
                                        if (match.Count == 0)
                                        {
                                            if (station_number.Contains(keys.Split(',')[0]) && station_layer == keys.Split(',')[1])
                                            {
                                                stationdesc = dicStationdescbyWP[key];
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            LogHelper.Debug("stationdesc:" + dicStationdescbyWP[key] + ";" + keys.Split(',')[1]);
                                            if (dicMatch.Keys.Contains(dicStationdescbyWP[key] + ";" + keys.Split(',')[1]))
                                            {
                                                stationdesc = dicStationdescbyWP[key];
                                                dicMatch.Remove(dicStationdescbyWP[key] + ";" + keys.Split(',')[1]);
                                                break;
                                            }
                                        }
                                    }
                                    if (stationdesc == "")
                                    {
                                        foreach (var key in new List<string>(dicStationdescbyWP.Keys))
                                        {
                                            string station_number = key.Split(';')[0];
                                            string station_layer = key.Split(';')[1];
                                            if (station_number.Contains(keys.Split(',')[0]) && station_layer == keys.Split(',')[1])
                                            {
                                                stationdesc = dicStationdescbyWP[key];
                                                break;
                                            }
                                        }
                                    }
                                    for (int i = 0; i < match.Count; i++)
                                    {
                                        string data = match[i].ToString().TrimStart('[').TrimEnd(']');
                                        string[] datas = data.Split(',');

                                        if (datas[0] == stationdesc && datas[1] == keys.Split(',')[1])
                                        {
                                            count++;
                                            LogHelper.Debug("Row data:" + datas[0] + ";" + datas[1] + ";" + datas[2] + ";" + datas[3] + ";" + datas[4]);
                                            row["pStation"] = stationdesc;
                                            row["pPass"] = datas[2];
                                            row["pFail"] = datas[3];
                                            row["pScrap"] = datas[4];
                                            int totalQty = Convert.ToInt32(datas[2]) + Convert.ToInt32(datas[3]) + Convert.ToInt32(datas[4]);
                                            row["pYield"] = Math.Round(Convert.ToDecimal(datas[2]) / Convert.ToDecimal(totalQty) * 100, 2) + "%";
                                            row["IQCSEQ"] = count;
                                            isMatch = true;
                                            break;
                                        }
                                    }
                                    if (!isMatch)
                                    {
                                        count++;
                                        row["pStation"] = stationdesc;
                                        row["pPass"] = "0";
                                        row["pFail"] = "0";
                                        row["pScrap"] = "0";
                                        row["pYield"] = "0.00%";
                                        row["IQCSEQ"] = count;
                                    }
                                    dt.Rows.Add(row);
                                }
                                AutoRefreshTimerStart();
                            }
                        }
                    }
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
        bool isInitData = false;
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            isInitData = true;
            GetCurrentWO();
            DataTable dtBind = GetIQCData();
            if (dtBind != null)
            {
                bool isSame = CompareDataTable(DTBind, dtBind);
                if (isSame)
                {
                    LogHelper.Debug("==");
                    isInitData = false;
                    return;
                }
                else
                {
                    DTBind = dtBind.Copy();
                    this.pagerToolbar1.PageSize = config.PageSizeCount;
                    this.pagerToolbar1.RowCount = dtBind.Rows.Count;
                    //this.pagerToolbar1.OnPagerToolBarClick += new System.EventHandler(this.pagerToolbar_Click);
                    this.LoadData(this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive);//this.pagerToolbar1.Inclusive, this.pagerToolbar1.Exclusive
                }
            }
            isInitData = false;
        }

        private void SetGridFont()
        {
            gridIQCData.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Regular);
            gridIQCData.RowsDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Regular);
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

        private void gridIQCData_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Gainsboro, new Rectangle(0, 0, this.gridIQCData.Width - 1, this.gridIQCData.Height - 1));
        }

        private DataTable GetIQCData()
        {
            try
            {
                AutoRefreshTimerStop();
                pageindex = 1;
                DataTable dt = new DataTable();
                dt.Columns.Add("pStation", typeof(string));
                dt.Columns.Add("pPass", typeof(string));
                dt.Columns.Add("pFail", typeof(string));
                dt.Columns.Add("pScrap", typeof(string));
                dt.Columns.Add("pYield", typeof(string));
                dt.Columns.Add("IQCSEQ", typeof(string));

                GetProductQuantity productHander = new GetProductQuantity(sessionContext, this);
                //CommonFunction commonHandler = new CommonFunction(sessionContext, this);
                //string plantNo = commonHandler.GetSiteNoByStationNo(config.StationNumber);
                //string iqcValue = clientSocket.SendData("{getSNBookedForWorkOrder;" + lblWorkorder.Text + "}"); //clientSocket.SendData("#10;" + plantNo + ";IQC request data#");
                //LogHelper.Info("Receive message :" + iqcValue);

                if (dicStation != null && dicStation.Keys.Count > 0)
                {


                    foreach (var keys in dicStation.Keys)
                    {
                        int count = 0;
                        DataRow row = dt.NewRow();
                        bool isMatch = false;
                        string stationdesc = "";
                        string station_number = "";
                        string station_layer = "";
                        foreach (var key in new List<string>(dicStationdescbyWP.Keys))
                        {
                            station_number = key.Split(';')[0];
                            station_layer = key.Split(';')[1];
                            if (station_number.Contains(keys.Split(',')[0]) && station_layer == keys.Split(',')[1])
                            {
                                if (dicStationdesc.Keys.Contains(station_number))
                                {
                                    stationdesc = dicStationdesc[station_number];
                                    break;
                                }
                            }
                        }
                        if (stationdesc == "")
                        {
                            foreach (var key in new List<string>(dicStationdescbyWP.Keys))
                            {
                                station_number = key.Split(';')[0];
                                station_layer = key.Split(';')[1];
                                if (station_number.Contains(keys.Split(',')[0]) && station_layer == keys.Split(',')[1])
                                {
                                    LogHelper.Debug("work plan station_number:" + station_number);
                                    if (dicStationdesc.Keys.Contains(station_number))
                                    {
                                        stationdesc = dicStationdesc[station_number];
                                        break;
                                    }
                                }
                            }
                            if (stationdesc == "")
                            {
                                foreach (var key in new List<string>(dicStationdescbyWP.Keys))
                                {
                                    station_number = key.Split(';')[0];
                                    station_layer = key.Split(';')[1];
                                    if (station_number.Contains(keys.Split(',')[0]) && station_layer == keys.Split(',')[1])
                                    {
                                        stationdesc = dicStationdescbyWP[key];
                                        break;
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(this.lblWorkorder.Text))
                        {
                            ProductEntity entity = productHander.GetProductQty(Convert.ToInt32(station_layer), lblWorkorder.Text, station_number);
                            if (entity != null)
                            {
                                count++;
                                LogHelper.Debug("Row data:" + stationdesc + ";" + entity.QUANTITY_PASS + ";" + entity.QUANTITY_FAIL + ";" + entity.QUANTITY_SCRAP);
                                row["pStation"] = stationdesc;
                                row["pPass"] = entity.QUANTITY_PASS;
                                row["pFail"] = entity.QUANTITY_FAIL;
                                row["pScrap"] = entity.QUANTITY_SCRAP;
                                int totalQty = Convert.ToInt32(entity.QUANTITY_PASS) + Convert.ToInt32(entity.QUANTITY_FAIL) + Convert.ToInt32(entity.QUANTITY_SCRAP);
                                string yield = "0.00%";
                                if (totalQty > 0)
                                {
                                    yield = Math.Round(Convert.ToDecimal(entity.QUANTITY_PASS) / Convert.ToDecimal(totalQty) * 100, 2) + "%";
                                }
                                row["pYield"] = yield;
                                row["IQCSEQ"] = count;
                                isMatch = true;
                                //break;
                            }
                        }
                        dt.Rows.Add(row);
                    }
                    AutoRefreshTimerStart();
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
    }
}
