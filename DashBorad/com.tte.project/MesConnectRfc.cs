using SAP.Middleware.Connector;
using System;
using System.Data;
using System.Windows.Forms;


namespace DashBorad.com.tte.project
{
    public partial class MesConnectRfc : Form
    {
        public MesConnectRfc()
        {
            InitializeComponent();
        }

        private void ConnectRfc_Click(object sender, EventArgs e)
        {
            getSAPData();
        }

        #region SAP
        public void getSAPData()
        {
            try
            {
                RfcConfigParameters rfcPar = new RfcConfigParameters();
                rfcPar.Add(RfcConfigParameters.Name, "DEV");
                rfcPar.Add(RfcConfigParameters.AppServerHost, "10.1.10.6");
                rfcPar.Add(RfcConfigParameters.Client, "560");
                rfcPar.Add(RfcConfigParameters.User, "IT_XM01");
                rfcPar.Add(RfcConfigParameters.Password, "/qaz456");
                rfcPar.Add(RfcConfigParameters.SystemNumber, "01");
                rfcPar.Add(RfcConfigParameters.Language, "ZF");

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcPar);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("ZPP_EXCEL_CO01");//SAP里面的函数名称
                myfun.SetValue("I_AUFNR_B", "11000086");            //SAP传入参数 Single
                myfun.SetValue("WERKS", "1201");   //SAP传入参数 Single
                                                   //IRfcStructure rfcstructSN = null;
                                                   //IRfcStructure rfcstructX = null;
                                                   //rfcstructSN = myfun.GetStructure("GENERALDATA");   //SAP传入参数 Structure
                                                   //rfcstructSN.SetValue("SERIAL_NO", "TestValue");   //SAP传入参数 Structure
                                                   //myfun.SetValue("GENERALDATA", rfcstructSN);
                myfun.Invoke(dest);
                //IRfcStructure rfcReturn = myfun.GetStructure("RETURN"); //此处返回类型为Structure 如果是Single类型 则直接调用myfun.GetString("RETURN");
                //string s = rfcReturn.GetString("TYPE");



                IRfcTable irfcTable = myfun.GetTable("E_BOMCOMP");
                //提前实例化一个空的表结构出来
                DataTable dt = new DataTable();
                dt.Columns.Add("GAMNG");
                dt.Columns.Add("MATNR");
                dt.Columns.Add("BDMNG");
                //循环把IRfcTable里面的数据放入Table里面，因为类型不同，不可直接使用。
                for (int i = 0; i < irfcTable.Count; i++)
                {
                    irfcTable.CurrentIndex = i;
                    DataRow dr = dt.NewRow();
                    dr["GAMNG"] = irfcTable.GetString("AUFNR");
                    dr["MATNR"] = irfcTable.GetString("Z_SYTABIX");
                    dr["BDMNG"] = irfcTable.GetString("BDMNG");
                    dt.Rows.Add(dr);
                }

                dgSAPData.DataSource = dt;
            }
            catch (Exception e)
            {

            }

        }
        #endregion

    }


}
