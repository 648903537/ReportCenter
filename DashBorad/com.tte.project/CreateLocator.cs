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
using System.Collections.Generic;

namespace DashBorad.com.tte.project
{
    public partial class CreateLocator : Form
    {
        SocketClientHandler clientSocket;
        public ApplicationConfiguration config;
        public delegate void MyInvokeNoParm();
        IMSApiSessionContextStruct sessionContext;

        public  string Uname = string.Empty;//用户名称
        string user_id = string.Empty;//用户ID

        public CreateLocator()
        {
            InitializeComponent();
        }

        public CreateLocator(string userName, DateTime dTime, IMSApiSessionContextStruct _sessionContext, ApplicationConfiguration _config)
        {
            InitializeComponent();

            Uname = userName;

            //显示版本
            this.Text = "储位导入 ( V:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";

            //显示IP地址
            string MachineName = Dns.GetHostName();
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(MachineName);
            System.Net.IPAddress[] addr = ipEntry.AddressList;
            this.lblIpAddress.Text += MachineName + "/" + addr[1].ToString();
        }

        private void CreateLocator_Load(object sender, EventArgs e)
        {
            clientSocket = new SocketClientHandler();
            config = new ApplicationConfiguration();
            clientSocket.connect(config.IPAddress, config.Port);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int result_OK = 0;
                int result_Err = 0;

                Dictionary<string, string> dicLine = new Dictionary<string, string>();//存放单元对应的ID

                this.Cursor = Cursors.WaitCursor;//等待

                this.Invoke(new MethodInvoker(delegate
                {
                    #region 获取用户ID值
                    string userSql = string.Format(@"select PERS_ID from bde.pers_stamm where upper(NAME) = upper('{0}')", Uname);
                    DataTable dt_user = getDataTable(userSql);
                    if (dt_user != null && dt_user.Rows.Count == 1)
                    {
                        user_id = dt_user.Rows[0][1].ToString();
                    }
                    else
                    {
                        return;
                    }
                    #endregion

                    //依次执行DataGridView里面的数据
                    foreach (DataGridViewRow dgvr in gridExcelData.Rows)
                    {
                        Application.DoEvents();
                        progressBar1.Value++;

                        lblStatus.Text = "开始处理第" + progressBar1.Value + "条";

                        string lager_id = string.Empty;//仓库ID
                        string lager_grp_id = string.Empty;//分组ID
                        string line_id = string.Empty;//单元ID

                        string locationName = dgvr.Cells[0].FormattedValue.ToString().Trim();//仓库名称
                        string locationRemark = dgvr.Cells[1].FormattedValue.ToString().Trim();//仓库备注
                        string locationGroupName = dgvr.Cells[2].FormattedValue.ToString().Trim();//分组名称
                        string locationGroupRemark = dgvr.Cells[3].FormattedValue.ToString().Trim();//分组名称
                        string locationLineName = dgvr.Cells[4].FormattedValue.ToString().Trim();//单元名称

                        //这边不想去查询厂别了,直接用固定值写进去
                        string plantNO_ID = "3000000";


                        #region 验证单元
                        if (!dicLine.ContainsKey(locationLineName))
                        {
                            string VerifyLocationLineSQL = string.Format(@"select * from BDE.LINE where line_nr ='{0}'", locationLineName);
                            DataTable dt_VerifyLocationLine = getDataTable(VerifyLocationLineSQL);
                            if (dt_VerifyLocationLine != null && dt_VerifyLocationLine.Rows.Count == 1)
                            {
                                line_id = dt_VerifyLocationLine.Rows[0][1].ToString();
                                dicLine[locationLineName] = line_id;
                            }
                            else
                            {
                                //找不到对应的仓库单元
                                result_Err++;
                                dgvr.DefaultCellStyle.BackColor = Color.Red;
                                continue;
                            }
                        }
                        else
                        {
                            line_id = dicLine[locationLineName];
                        }
                        #endregion


                        #region 验证分组,并创建
                        string VerifyLocationGrpSQL = string.Format(@"select * from ml.lager_grp where lager_grp_nr = '{0}' and line_id ='{1}'", locationGroupName, line_id);
                        DataTable dt_VerifyLocationGrp = getDataTable(VerifyLocationGrpSQL);
                        if (dt_VerifyLocationGrp != null && dt_VerifyLocationGrp.Rows.Count == 1)
                        {
                            lager_grp_id = dt_VerifyLocationGrp.Rows[0][1].ToString();
                        }
                        else
                        {
                            #region 获取lager_grp_id
                            DataTable dt_Grp_ID = getDataTable("SELECT ml.seq_lager_grp.NEXTVAL FROM DUAL");
                            if (dt_Grp_ID != null && dt_Grp_ID.Rows.Count == 1)
                            {
                                lager_grp_id = dt_Grp_ID.Rows[0][1].ToString();
                            }
                            else
                            {
                                //生成不了lager_grp_id
                                result_Err++;
                                dgvr.DefaultCellStyle.BackColor = Color.Red;
                                continue;
                            }
                            #endregion

                            #region 创建分组
                            string Insert_grp_sql = string.Format(@"INSERT INTO ML.lager_grp
                                                                          (lager_grp_id,lager_grp_nr,lager_grp_typ,lager_grp_bez,parent_lager_grp_id,line_id,aktiv,created,user_id,stamp,client_id,company_id,werk_id,barcode)
                                                                        VALUES
                                                                          ('{0}',
                                                                           '{1}',
                                                                           'M',
                                                                           '{2}',
                                                                           '0',
                                                                           '{3}',
                                                                           '1',
                                                                           TO_TIMESTAMP(TO_CHAR(SYSDATE, 'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'),
                                                                           '{4}',
                                                                           TO_TIMESTAMP(TO_CHAR(SYSDATE, 'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'),
                                                                           '1',
                                                                           '1',
                                                                           '{5}',
                                                                           '')", lager_grp_id, locationGroupName, locationGroupRemark, line_id, user_id, plantNO_ID);
                            int GrpReuslt = executeSql(Insert_grp_sql);
                            if(GrpReuslt != 1)
                            {
                                //创建分组不成功
                                dgvr.DefaultCellStyle.BackColor = Color.Red;
                                continue;
                            }
                            #endregion
                        }
                        #endregion


                        #region 获取Lager_ID
                        //获取Lager_ID
                        DataTable dt = getDataTable("SELECT ml.seq_lagerort.NEXTVAL FROM DUAL");
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            lager_id = dt.Rows[0][1].ToString();
                        }
                        else
                        {
                            result_Err++;
                            dgvr.DefaultCellStyle.BackColor = Color.Red;
                            //Application.DoEvents();
                            continue;
                        }
                        #endregion

                        #region 插入仓库数据
                        //插入数据
                        string sql = string.Format(@"INSERT INTO ML.LAGERORT
                                                  (lager_id, lager_nr, lager_typ, lager_bez, lager_grp_id, aktiv, created, user_id, stamp, client_id, company_id, werk_id, barcode, inv_date, inv_start_date)VALUES
                                                  ('{0}',
                                                   '{1}',
                                                   'M',
                                                   '{2}',
                                                   '{3}',
                                                   '1',
                                                   TO_TIMESTAMP(TO_CHAR(SYSDATE, 'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'),
                                                   '{4}',
                                                   TO_TIMESTAMP(TO_CHAR(SYSDATE, 'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'),
                                                   '1',
                                                   '1',
                                                   '{5}',
                                                   '',
                                                   TO_TIMESTAMP('2070-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'),
                                                   '')", lager_id, locationName, locationRemark, lager_grp_id, user_id, plantNO_ID);
                        int reuslt = executeSql(sql);
                        #endregion


                        if (reuslt == 1)
                        {
                            result_OK++;
                            dgvr.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            result_Err++;
                            dgvr.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                    this.Cursor = Cursors.Default;//正常状态
                    lblStatus.Text = "导入完成";
                    Application.DoEvents();
                }));

                this.lblResult.Text += " OK : " + result_OK + " NG: " + result_Err;
                lblStatus.Text = "完成";

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;//正常状态
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 读取EXCEL文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(System.Environment.CurrentDirectory + "\\excel"))
                {
                    openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory + "\\excel";
                }

                openFileDialog1.Filter = "EXCEL文件|*.xlsx";


                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //tbxFilePath.Text = openFileDialog1.FileName;
                    ExcelHelper exhep = new ExcelHelper(openFileDialog1.FileName);
                    DataTable dt = exhep.ExcelToDataTable("Sheet1", true);
                    gridExcelData.DataSource = dt;

                    this.lblResult.Text = "Total: " + dt.Rows.Count.ToString();
                    this.lblStatus.Text = "";

                    progressBar1.Maximum = dt.Rows.Count;
                    progressBar1.Value = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
    }
}

