using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Data.OleDb;
using System.Net;
using com.amtec.action;
using com.amtec.configurations;
using System.Threading;
using com.itac.mes.imsapi.domain.container;

namespace DashBorad.com.tte.project
{
    public partial class InsertFujiTrax : Form
    {
        SocketClientHandler clientSocket;
        public ApplicationConfiguration config;
        public delegate void MyInvokeNoParm();
        IMSApiSessionContextStruct sessionContext;

        public string Uname = string.Empty;//用户名称
        string user_id = string.Empty;//用户ID

        DateTime selectTime;

        public InsertFujiTrax()
        {
            InitializeComponent();

            tbLog.AppendText("Begion......\n");

            //显示版本
            this.Text = "FujiTrax数据导入 ( V:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";

            //显示IP地址
            string MachineName = Dns.GetHostName();
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(MachineName);
            System.Net.IPAddress[] addr = ipEntry.AddressList;
            this.lblIpAddress.Text += MachineName + "/" + addr[2].ToString();
        }

        public InsertFujiTrax( IMSApiSessionContextStruct _sessionContext)
        {
            InitializeComponent();
            sessionContext = _sessionContext;
        }

        private void CreateLocator_Load(object sender, EventArgs e)
        {
            clientSocket = new SocketClientHandler();
            config = new ApplicationConfiguration();
            clientSocket.connect(config.IPAddress, config.Port);
        }

        private void CreateLocator_FormClosing(object sender, FormClosingEventArgs e)
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

        /// <summary>
        /// 查找出OA数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectOAData_Click(object sender, EventArgs e)
        {
            try
            {
                //导出OA数据
                string userSql = string.Format(@"select  MATERIAL_BIN_NUMBER,PART_NUMBER,VENDOR_CODE,LOT_NR,DATE_CODE,CREATED_DATE from [OATOMES].[dbo].[mes_portal] where CREATED_DATE>'2017-07-26 00:00:00' and CREATED_DATE<='2017-07-26 12:00:00' and status in( '2','3','4') order by CREATED_DATE desc ");
                DataTable dtUidData = getOADataTable(userSql);
                if (dtUidData == null || dtUidData.Rows.Count == 0)
                {

                }
                else
                {
                    dtUidData.Columns.Add("qty");
                    dtUidData.Columns.Add("result");
                    gridExcelData.DataSource = null;
                    gridExcelData.DataSource = dtUidData;
                    this.lblResult.Text = "Total: " + dtUidData.Rows.Count.ToString();


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExcelHelper exhep = new ExcelHelper(openFileDialog1.FileName);
            exhep.GridToExcel(DateTime.Now.ToString("yyyyMMddHHmmss"),gridExcelData);
        }

        private void btnInsertFuji_Click(object sender, EventArgs e)
        {
            try
            {
                if(tbxUID.Text.Trim() != "")
                {
                    #region 将文本框的UID写入到FujiTrax

                    //查询OA数据
                    string userSql = string.Format(@"select MATERIAL_BIN_NUMBER,PART_NUMBER,VENDOR_CODE,LOT_NR,DATE_CODE,CREATED_DATE from [OATOMES].[dbo].[mes_portal] where MATERIAL_BIN_NUMBER ='{0}' and status in( '2','3','4') order by CREATED_DATE desc ", tbxUID.Text.Trim().ToUpper());
                    DataTable dtUidData = getOADataTable(userSql);
                    if (dtUidData == null || dtUidData.Rows.Count == 0)
                    {
                        tbLog.SelectionColor = Color.Red;
                        tbLog.AppendText("error--->" + tbxUID.Text.Trim() + "\n");
                        tbLog.ScrollToCaret();
                        tbLog.SelectionColor = Color.Black;
                    }
                    dtUidData.Columns.Add("qty");
                    dtUidData.Columns.Add("result");

                    gridExcelData.DataSource = dtUidData;
                    this.lblResult.Text = "Total: " + dtUidData.Rows.Count.ToString();
                    Application.DoEvents();

                    SetFujiTrax();

                    #endregion
                }
                else
                {
                    #region 通过时间段写入FujiTrax数据
                    string beginTime = minDate.Text;
                    string endTime = maxDate.Text;
                    selectTime = Convert.ToDateTime(minDate.Text.ToString());

                    string tempDate = string.Empty;

                    bool flag = true;

                    while (flag)
                    {
                        if (selectTime.AddDays(1) > Convert.ToDateTime(endTime))
                        {
                            tempDate = endTime;
                            flag = false;
                        }
                        else
                        {
                            tempDate = selectTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        //查询OA数据
                        string userSql = string.Format(@"select MATERIAL_BIN_NUMBER,PART_NUMBER,VENDOR_CODE,LOT_NR,DATE_CODE,CREATED_DATE from [OATOMES].[dbo].[mes_portal] where CREATED_DATE>='{0}' and CREATED_DATE<='{1}' and status in( '2','3','4') order by CREATED_DATE desc ", selectTime.ToString("yyyy-MM-dd HH:mm:ss"), tempDate);
                        DataTable dtUidData = getOADataTable(userSql);
                        if (dtUidData == null || dtUidData.Rows.Count == 0)
                        {
                            tbLog.SelectionColor = Color.Red;
                            tbLog.AppendText("error--->" + selectTime.ToString() + "\n");
                            tbLog.ScrollToCaret();
                            tbLog.SelectionColor = Color.Black;

                            selectTime = selectTime.AddDays(1);
                            continue;
                        }
                        dtUidData.Columns.Add("qty");
                        dtUidData.Columns.Add("result");

                        gridExcelData.DataSource = dtUidData;
                        this.lblResult.Text = "Total: " + dtUidData.Rows.Count.ToString();
                        Application.DoEvents();

                        SetFujiTrax();

                        #region 写入FUJITRAX
                        //int success = 0;
                        //int index = 0;
                        //StringBuilder sbMat = new StringBuilder();

                        //foreach (DataGridViewRow dgvr in gridExcelData.Rows)
                        //{
                        //    string uid = dgvr.Cells["MATERIAL_BIN_NUMBER"].FormattedValue.ToString().Trim();
                        //    string partNumber = dgvr.Cells["PART_NUMBER"].FormattedValue.ToString().Trim();
                        //    string VendorCode = dgvr.Cells["VENDOR_CODE"].FormattedValue.ToString().Trim();
                        //    string LotNr = dgvr.Cells["LOT_NR"].FormattedValue.ToString().Trim();
                        //    string DateCode = dgvr.Cells["DATE_CODE"].FormattedValue.ToString().Trim();
                        //    string CreatedDate = dgvr.Cells["CREATED_DATE"].FormattedValue.ToString().Trim();

                        //    sbMat.Append("'" + uid + "',");

                        //    if (index == 100)
                        //    {
                        //        index = 0;
                        //        success += 101;
                        //        this.lblResult.Text = success.ToString();
                        //        Application.DoEvents();

                        //        string sqlResultValue = clientSocket.SendData("{InsertFUJIBYPC;" + "1201;" + sbMat.ToString().TrimEnd(',') + "}");
                        //        sbMat.Clear();
                        //    }
                        //    else
                        //    {
                        //        index++;
                        //    }
                        //}
                        //string sqlResultValue2 = clientSocket.SendData("{InsertFUJIBYPC;" + "1201;" + sbMat.ToString().TrimEnd(',') + "}");

                        //tbLog.AppendText("Success--->" + selectTime.ToString() + "\n");

                        #endregion

                        selectTime = selectTime.AddDays(1);
                    }

                    tbLog.AppendText("END .......\n");
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetFujiTrax()
        {
            int success = 0;
            int index = 0;
            StringBuilder sbMat = new StringBuilder();

            foreach (DataGridViewRow dgvr in gridExcelData.Rows)
            {
                string uid = dgvr.Cells["MATERIAL_BIN_NUMBER"].FormattedValue.ToString().Trim();
                string partNumber = dgvr.Cells["PART_NUMBER"].FormattedValue.ToString().Trim();
                string VendorCode = dgvr.Cells["VENDOR_CODE"].FormattedValue.ToString().Trim();
                string LotNr = dgvr.Cells["LOT_NR"].FormattedValue.ToString().Trim();
                string DateCode = dgvr.Cells["DATE_CODE"].FormattedValue.ToString().Trim();
                string CreatedDate = dgvr.Cells["CREATED_DATE"].FormattedValue.ToString().Trim();

                sbMat.Append("'" + uid + "',");

                if (index == 100)
                {
                    index = 0;
                    success += 101;
                    this.lblResult.Text = success.ToString();
                    Application.DoEvents();

                    string sqlResultValue = clientSocket.SendData("{InsertFUJIBYPC;" + "1201;" + sbMat.ToString().TrimEnd(',') + "}");
                    sbMat.Clear();
                }
                else
                {
                    index++;
                }
            }
            //会有部分凑不齐101所以后面要记得补上这些数据       郑培聪     2017/10/18
            string sqlResultValue2 = clientSocket.SendData("{InsertFUJIBYPC;" + "1201;" + sbMat.ToString().TrimEnd(',') + "}");

            if(tbxUID.Text.Trim() == string.Empty)
            {
                tbLog.AppendText("Success--->" + selectTime.ToString() + "\n");
            }
            else
            {
                tbLog.AppendText("Success--->" + tbxUID.Text.Trim().ToUpper() + "\n");
            }
            tbLog.ScrollToCaret();


        }

        #region getData
        /// <summary>
        /// 通过socket获取DataTable数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable getDataTable(string sql)
        {
            try
            {
                DataTable sqlData_Table = new DataTable();

                //发送请求入库数据Socket
                string sqlResultValue = clientSocket.SendData("{getDataTable;" + "1201;" + sql + "}");
                string[] values = sqlResultValue.Split(new char[] { ';' });
                if (values.Length < 3)
                {
                    return sqlData_Table;
                }
                //给DataTable添加列名
                string[] columnValues = values[2].Split('!');
                foreach (string columnName in columnValues)
                {
                    sqlData_Table.Columns.Add(columnName, typeof(string));
                }
                //验证没有列名就返回NULL
                if (sqlData_Table.Columns.Count == 0)
                {
                    return sqlData_Table;
                }

                int iLoop = Convert.ToInt32(values[1]);

                if (values[3].ToString().Trim() != string.Empty)//如果查询出来的数据是空的直接返回一个带列名的DataTable
                {
                    string[] rowData = values[3].Split('!');
                    //给DataTable填充数据
                    for (int i = 0; i < rowData.Length; i += iLoop)
                    {
                        DataRow dr = sqlData_Table.NewRow();
                        for (int loop = 0; loop < iLoop; loop++)
                        {
                            //dr[columnValues[loop]] = rowData[i + loop];
                            dr[loop] = rowData[i + loop];
                        }
                        sqlData_Table.Rows.Add(dr);
                    }
                }

                return sqlData_Table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 通过socket执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>执行结果影响行数</returns>
        private int executeSql(string sql)
        {
            try
            {
                DataTable sqlData_Table = new DataTable();

                //发送请求入库数据Socket
                string sqlResultValue = clientSocket.SendData("{executeSql;" + "1201;" + sql + "}");
                string[] values = sqlResultValue.TrimStart('{').TrimEnd('}').Split(new char[] { ';' });
                if (values.Length != 2)
                {
                    return -1;//portal并没有正确的处理
                }

                return Convert.ToInt32(values[1].ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 通过socket获取DataTable数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable getOADataTable(string sql)
        {
            try
            {
                DataTable sqlData_Table = new DataTable();

                //发送请求入库数据Socket
                string sqlResultValue = clientSocket.SendData("{getOADataTable;" + "1201;" + sql + "}");
                string[] values = sqlResultValue.Split(new char[] { ';' });
                if (values.Length < 3)
                {
                    return sqlData_Table;
                }
                //给DataTable添加列名
                string[] columnValues = values[2].Split('!');
                foreach (string columnName in columnValues)
                {
                    sqlData_Table.Columns.Add(columnName, typeof(string));
                }
                //验证没有列名就返回NULL
                if (sqlData_Table.Columns.Count == 0)
                {
                    return sqlData_Table;
                }

                int iLoop = Convert.ToInt32(values[1]);

                if (values[3].ToString().Trim() != string.Empty)//如果查询出来的数据是空的直接返回一个带列名的DataTable
                {
                    string[] rowData = values[3].Split('!');
                    //给DataTable填充数据
                    for (int i = 0; i < rowData.Length; i += iLoop)
                    {
                        DataRow dr = sqlData_Table.NewRow();
                        for (int loop = 0; loop < iLoop; loop++)
                        {
                            //dr[columnValues[loop]] = rowData[i + loop];
                            dr[loop] = rowData[i + loop];
                        }
                        sqlData_Table.Rows.Add(dr);
                    }
                }

                return sqlData_Table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}


