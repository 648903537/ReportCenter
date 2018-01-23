using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using com.amtec.action;
using com.amtec.configurations;

namespace DashBorad.com.tte.project
{
    public partial class PackBoxNumber : Form
    {
        public ApplicationConfiguration config;
        SocketClientHandler clientSocket;

        public PackBoxNumber()
        {
            InitializeComponent();
            this.Text = "最小包装数量查询 ( Version:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
            clientSocket = new SocketClientHandler();
        }

        private void PackBoxNumber_Load(object sender, EventArgs e)
        {
            config = new ApplicationConfiguration();
            clientSocket.connect(config.IPAddress, config.Port);
        }

        private void PackBoxNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void tbPartNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                lblStatus.Text = "正在查询.........";
                tbPackNumber.Text = string.Empty;
                btnSearch.Enabled = false;
                Application.DoEvents();

                tbPackNumber.Text = getPackBoxNumber(tbPartNumber.Text);
                btnSearch.Enabled = true;
                lblStatus.Text = "完成";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "正在查询.........";
            tbPackNumber.Text = string.Empty;
            btnSearch.Enabled = false;
            Application.DoEvents();

            tbPackNumber.Text = getPackBoxNumber(tbPartNumber.Text);
            btnSearch.Enabled = true;
            lblStatus.Text = "完成";
        }

        /// <summary>
        /// 通过品号查询最小包装数量
        /// </summary>
        /// <param name="partNumber"></param>
        /// <returns></returns>
        public string getPackBoxNumber(string partNumber)
        {
            try
            {
                string sql = string.Format(@"select menge
                          from glo.adis_ref
                         where object_id in (select OBJECT_ID from glo.adis where artikel = '{0}')
                           and object_id_ref = '52533'", partNumber);
                DataTable result_dt = getDataTable(sql);

                if (result_dt == null || result_dt.Rows.Count != 1)
                {
                    lblStatus.Text = "查询失败";
                }

                //返回最小包装数量
                return result_dt.Rows[0]["menge"].ToString();
            }
            catch(Exception ex)
            {
                return "";
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

        private void tbPartNumber_MouseUp(object sender, MouseEventArgs e)
        {
            //如果鼠标左键操作并且标记存在，则执行全选             
            if (e.Button == MouseButtons.Left)
            {
                tbPartNumber.SelectAll();
            }
        }
    }
}
