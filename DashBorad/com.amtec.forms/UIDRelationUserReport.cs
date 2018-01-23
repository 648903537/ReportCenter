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

namespace com.amtec.forms
{
    public partial class UIDRelationUserReport : Form
    {
        public ApplicationConfiguration config;
        SocketClientHandler clientSocket;
        DataTable dtBind = new DataTable();

        public UIDRelationUserReport()
        {
            InitializeComponent();
            this.Text = "UID查询 ( Version:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
            clientSocket = new SocketClientHandler(this);

        }

        private void UIDRelationUserReport_Load(object sender, EventArgs e)
        {

            try
            {
                config = new ApplicationConfiguration();
                clientSocket.connect(config.IPAddress, config.Port);

                dtBind = getUidData("AND 1=2");

                if (dtBind == null)
                {
                    return;
                }

                DataRow nuldr = dtBind.NewRow();
                nuldr["ID"] = "0";
                dtBind.Rows.InsertAt(nuldr, 0);

                BindingSource dataSource = new BindingSource(dtBind, null);
                gridUidData.DataSource = dataSource;
                for (int i = 1; i < dtBind.Rows.Count - 1; i++)
                {
                    gridUidData.Rows[i].ReadOnly = true;
                }
                gridUidData.Rows[0].Cells[0].ReadOnly = true;

            }
            catch (Exception ex)
            {

            }




            //dtBind = getUidData("");

            //if (dtBind == null || dtBind.Rows.Count == 0)
            //{
            //    return;
            //}

            //DataRow nuldr = dtBind.NewRow();
            //nuldr["ID"] = "0";
            //dtBind.Rows.InsertAt(nuldr, 0);

            //BindingSource dataSource = new BindingSource(dtBind, null);
            //gridUidData.DataSource = dataSource;
            //for (int i = 1; i < dtBind.Rows.Count - 1; i++)
            //{
            //    gridUidData.Rows[i].ReadOnly = true;
            //}
            //gridUidData.Rows[0].Cells[0].ReadOnly = true;
        }

        private void UIDRelationUserReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        /// <summary>
        /// Uid关联用户查询SQL
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private DataTable getUidData(string sqlWhere)
        {
            string strSql = @"select * from (select lot.snr_mat_ext as snr_mat_ext, --UID
                                    g.artikel as PartNumber,
                                    g.artbez as PartNumberDesc,
                                    lot.CUSTOMER_PN as CUSTOMER_PN,
                                    lot.data as MTO_NUM,
                                    lot.movement_type as MTO_MOVE_TYPE,
                                    lot.name as username,
                                    lot.vorname as vorname,
                                    lot.stamp as stamp,
                                    la.line_nr || '-' || lb.lager_grp_nr || '-' || lc.lager_nr as storageInfo
                                from (SELECT x.snr_mat_ext,
                                            x.LAGER_ID, --库位ID
                                            x.object_id, --品号ID
                                            --客户件号
                                            cust.data as CUSTOMER_PN,
                                            umto.data,
                                            umto.movement_type,
                                            umto.name,
                                            umto.vorname,
                                            --操作时间
                                            umto.stamp
                                        FROM ml.charge_snr_Mat x
                                        left join (select c.a_code,
                                                        a.snr_mat_id,
                                                        a.data,
                                                        a.stamp,
                                                        m.movement_type,
                                                        U.NAME,
                                                        U.VORNAME,
                                                        A.ATTRIB_NR
                                                    from ml.snr_mat_attrib A
                                                    left join ml.snr_mat_attrib_code c
                                                    ON (c.ID = A.ATTRIB_NR)
                                                    left join (select distinct mat_doc_number,
                                                                            movement_type
                                                                from cust.material_transfer_order
                                                                where CREATED >TO_TIMESTAMP(TO_CHAR(SYSDATE - 30,  'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS')) m
                                                    on a.data = m.mat_doc_number
                                                    left join bde.pers_stamm u
                                                    ON A.USER_ID = U.pers_id
                                                    where c.a_code in ('PICK_LIST_NO', 'MTO_NUM')) umto
                                        ON (x.snr_mat_id = umto.snr_mat_id)
                                        left join (select snr_mat_id, data
                                                    from ml.snr_mat_attrib sna
                                                    join ml.snr_mat_attrib_code snc
                                                    ON (snc.ID = sna.ATTRIB_NR and
                                                        A_CODE = 'CUSTOMER_PN')) cust
                                        on cust.snr_mat_id = x.snr_mat_id
                                        where x.created > TO_TIMESTAMP(TO_CHAR(SYSDATE - 10, 'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')
                                        --and x.snr_mat_ext = 'X110663580001'
                                        ) lot
                            --库位
                                left join ML.LAGERORT lc
                                on lc.LAGER_ID = lot.LAGER_ID
                                left join ML.LAGER_GRP lb
                                on lb.LAGER_GRP_ID = lc.LAGER_GRP_ID
                                left join BDE.LINE la
                                on la.LINE_ID = lb.LINE_ID
                            --品号
                                left join glo.adis g
                                on lot.object_id = g.object_id
                                order by stamp desc nulls last) where 1=1 {0}";

            return getDataTable(string.Format(strSql, sqlWhere));
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

        private void gridUidData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {

                    int column = gridUidData.Rows[0].Cells.Count;

                    DataRow dr = dtBind.Rows[0];

                    for (int i = 1; i < column; i++)
                    {
                        dr[i] = gridUidData.Rows[0].Cells[i].EditedFormattedValue.ToString();
                    }

                    //数据过滤
                    DataTable new_dt = dtBind;
                    string sqlStr = string.Empty;

                    //进行筛选
                    for (int j = 1; j < column; j++)
                    {
                        if (dr[j].ToString().Trim() != string.Empty)
                        {
                            sqlStr += " AND " + dr.Table.Columns[j].ColumnName + "='" + dr[j] + "'";
                        }
                    }

                    new_dt = getUidData(sqlStr);

                    DataRow new_dr = new_dt.NewRow();
                    for (int z = 0; z < column; z++)
                    {
                        new_dr[z] = dr[z];
                    }
                    new_dt.Rows.InsertAt(new_dr, 0);

                    if (new_dt != null && new_dt.Rows.Count > 1)
                    {
                        BindingSource dataSource = new BindingSource(new_dt, null);
                        gridUidData.DataSource = dataSource;
                        for (int i = 1; i < new_dt.Rows.Count - 1; i++)
                        {
                            gridUidData.Rows[i].ReadOnly = true;
                        }
                        gridUidData.Rows[0].Cells[0].ReadOnly = true;
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {

            try
            {
                picNet.BackgroundImage = global::DashBorad.Properties.Resources.NetWorkDisconnectedRed24x24;

                Application.DoEvents();

                dtBind = getUidData("");

                if (dtBind == null || dtBind.Rows.Count == 0)
                {
                    return;
                }

                DataRow nuldr = dtBind.NewRow();
                nuldr["ID"] = "0";
                dtBind.Rows.InsertAt(nuldr, 0);

                BindingSource dataSource = new BindingSource(dtBind, null);
                gridUidData.DataSource = dataSource;
                for (int i = 1; i < dtBind.Rows.Count - 1; i++)
                {
                    gridUidData.Rows[i].ReadOnly = true;
                }
                gridUidData.Rows[0].Cells[0].ReadOnly = true;

                picNet.BackgroundImage = global::DashBorad.Properties.Resources.NetWorkConnectedGreen24x24;

            }
            catch (Exception ex)
            {

            }


        }

        private void panel3_Click(object sender, EventArgs e)
        {
            try
            {

                int column = gridUidData.Rows[0].Cells.Count;

                DataRow dr = dtBind.Rows[0];

                for (int i = 1; i < column; i++)
                {
                    dr[i] = gridUidData.Rows[0].Cells[i].EditedFormattedValue.ToString();
                }

                //数据过滤
                DataTable new_dt = dtBind;
                string sqlStr = string.Empty;

                //进行筛选
                for (int j = 1; j < column; j++)
                {
                    if (dr[j].ToString().Trim() != string.Empty)
                    {
                        sqlStr += " AND " + dr.Table.Columns[j].ColumnName + "='" + dr[j] + "'";
                    }
                }

                if (sqlStr == "") return;

                new_dt = getUidData(sqlStr);

                DataRow new_dr = new_dt.NewRow();
                for (int z = 0; z < column; z++)
                {
                    new_dr[z] = dr[z];
                }
                new_dt.Rows.InsertAt(new_dr, 0);

                if (new_dt != null && new_dt.Rows.Count > 1)
                {
                    BindingSource dataSource = new BindingSource(new_dt, null);
                    gridUidData.DataSource = dataSource;
                    for (int i = 1; i < new_dt.Rows.Count - 1; i++)
                    {
                        gridUidData.Rows[i].ReadOnly = true;
                    }
                    gridUidData.Rows[0].Cells[0].ReadOnly = true;
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
