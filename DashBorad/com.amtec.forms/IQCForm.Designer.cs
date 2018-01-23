namespace com.amtec.forms
{
    partial class IQCForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IQCForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCurTime = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pagerToolbar1 = new com.amtec.usercontrol.PagerToolbar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolHide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.timerCurrentTime = new System.Windows.Forms.Timer(this.components);
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn6 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn7 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn8 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn9 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn10 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn11 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.gridIQCData = new TTEDataGrid.TTEDataGridView();
            this.IQCNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IQCSEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IQCReceiveDate = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCInspectionDate = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCVenderName = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCPartNumber = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCPartDEsc = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCPurchNo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCUnit = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCQty = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCMatDocNo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCBatchNo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.IQCProcessState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IQCInpsectionState = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridIQCData)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridIQCData, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 702);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.91016F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.39844F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.69141F));
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel6, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblCaption, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1024, 70);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pictureBox3);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(228, 70);
            this.panel5.TabIndex = 1;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(0, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(225, 64);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.pictureBox2);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Controls.Add(this.lblCurTime);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(677, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(347, 70);
            this.panel6.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(42, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "已检";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(42, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "未捡";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 38);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 25);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // lblCurTime
            // 
            this.lblCurTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurTime.AutoSize = true;
            this.lblCurTime.Font = new System.Drawing.Font("黑体", 16F, System.Drawing.FontStyle.Bold);
            this.lblCurTime.ForeColor = System.Drawing.Color.Yellow;
            this.lblCurTime.Location = new System.Drawing.Point(88, 21);
            this.lblCurTime.Name = "lblCurTime";
            this.lblCurTime.Size = new System.Drawing.Size(238, 22);
            this.lblCurTime.TabIndex = 19;
            this.lblCurTime.Text = "2008-05-20 00:00:00";
            this.lblCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaption
            // 
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.Yellow;
            this.lblCaption.Location = new System.Drawing.Point(339, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(335, 70);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "IQC DashBoard";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 667);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 35);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.pagerToolbar1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1024, 35);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // pagerToolbar1
            // 
            this.pagerToolbar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pagerToolbar1.Font = new System.Drawing.Font("宋体", 9F);
            this.pagerToolbar1.ForeColor = System.Drawing.Color.White;
            this.pagerToolbar1.Location = new System.Drawing.Point(617, 3);
            this.pagerToolbar1.Name = "pagerToolbar1";
            this.pagerToolbar1.PageIndex = 1;
            this.pagerToolbar1.PageSize = 20;
            this.pagerToolbar1.RowCount = 0;
            this.pagerToolbar1.Size = new System.Drawing.Size(404, 24);
            this.pagerToolbar1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolHide,
            this.toolShowAll});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // toolHide
            // 
            this.toolHide.Name = "toolHide";
            this.toolHide.Size = new System.Drawing.Size(124, 22);
            this.toolHide.Text = "隐藏";
            this.toolHide.Click += new System.EventHandler(this.toolHide_Click);
            // 
            // toolShowAll
            // 
            this.toolShowAll.Name = "toolShowAll";
            this.toolShowAll.Size = new System.Drawing.Size(124, 22);
            this.toolShowAll.Text = "全部显示";
            this.toolShowAll.Click += new System.EventHandler(this.toolShowAll_Click);
            // 
            // timerCurrentTime
            // 
            this.timerCurrentTime.Interval = 1000;
            this.timerCurrentTime.Tick += new System.EventHandler(this.timerCurrentTime_Tick);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IQCNO";
            this.dataGridViewTextBoxColumn1.HeaderText = "NO";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 53;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "IQCSEQ";
            this.dataGridViewTextBoxColumn2.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewAutoFilterTextBoxColumn1
            // 
            this.dataGridViewAutoFilterTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn1.DataPropertyName = "IQCReceiveDate";
            this.dataGridViewAutoFilterTextBoxColumn1.HeaderText = "收料日期";
            this.dataGridViewAutoFilterTextBoxColumn1.Name = "dataGridViewAutoFilterTextBoxColumn1";
            this.dataGridViewAutoFilterTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn1.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn2
            // 
            this.dataGridViewAutoFilterTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn2.DataPropertyName = "IQCInspectionDate";
            this.dataGridViewAutoFilterTextBoxColumn2.HeaderText = "检查日期";
            this.dataGridViewAutoFilterTextBoxColumn2.Name = "dataGridViewAutoFilterTextBoxColumn2";
            this.dataGridViewAutoFilterTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn2.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn3
            // 
            this.dataGridViewAutoFilterTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn3.DataPropertyName = "IQCVenderName";
            this.dataGridViewAutoFilterTextBoxColumn3.HeaderText = "供应商";
            this.dataGridViewAutoFilterTextBoxColumn3.Name = "dataGridViewAutoFilterTextBoxColumn3";
            this.dataGridViewAutoFilterTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn3.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn4
            // 
            this.dataGridViewAutoFilterTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn4.DataPropertyName = "IQCPartNumber";
            this.dataGridViewAutoFilterTextBoxColumn4.HeaderText = "品号";
            this.dataGridViewAutoFilterTextBoxColumn4.Name = "dataGridViewAutoFilterTextBoxColumn4";
            this.dataGridViewAutoFilterTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn4.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn5
            // 
            this.dataGridViewAutoFilterTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn5.DataPropertyName = "IQCPartDEsc";
            this.dataGridViewAutoFilterTextBoxColumn5.HeaderText = "品名";
            this.dataGridViewAutoFilterTextBoxColumn5.Name = "dataGridViewAutoFilterTextBoxColumn5";
            this.dataGridViewAutoFilterTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn5.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn6
            // 
            this.dataGridViewAutoFilterTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn6.DataPropertyName = "IQCPurchNo";
            this.dataGridViewAutoFilterTextBoxColumn6.HeaderText = "单号";
            this.dataGridViewAutoFilterTextBoxColumn6.Name = "dataGridViewAutoFilterTextBoxColumn6";
            this.dataGridViewAutoFilterTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn6.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn7
            // 
            this.dataGridViewAutoFilterTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn7.DataPropertyName = "IQCUnit";
            this.dataGridViewAutoFilterTextBoxColumn7.HeaderText = "单位";
            this.dataGridViewAutoFilterTextBoxColumn7.Name = "dataGridViewAutoFilterTextBoxColumn7";
            this.dataGridViewAutoFilterTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewAutoFilterTextBoxColumn8
            // 
            this.dataGridViewAutoFilterTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn8.DataPropertyName = "IQCQty";
            this.dataGridViewAutoFilterTextBoxColumn8.HeaderText = "收料数量";
            this.dataGridViewAutoFilterTextBoxColumn8.Name = "dataGridViewAutoFilterTextBoxColumn8";
            this.dataGridViewAutoFilterTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn8.Width = 110;
            // 
            // dataGridViewAutoFilterTextBoxColumn9
            // 
            this.dataGridViewAutoFilterTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn9.DataPropertyName = "IQCMatDocNo";
            this.dataGridViewAutoFilterTextBoxColumn9.HeaderText = "物料文件";
            this.dataGridViewAutoFilterTextBoxColumn9.Name = "dataGridViewAutoFilterTextBoxColumn9";
            this.dataGridViewAutoFilterTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn9.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn10
            // 
            this.dataGridViewAutoFilterTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn10.DataPropertyName = "IQCBatchNo";
            this.dataGridViewAutoFilterTextBoxColumn10.HeaderText = "检验批号";
            this.dataGridViewAutoFilterTextBoxColumn10.Name = "dataGridViewAutoFilterTextBoxColumn10";
            this.dataGridViewAutoFilterTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn10.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "IQCProcessState";
            this.dataGridViewTextBoxColumn3.HeaderText = "检验状态";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 130;
            // 
            // dataGridViewAutoFilterTextBoxColumn11
            // 
            this.dataGridViewAutoFilterTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn11.DataPropertyName = "IQCInpsectionState";
            this.dataGridViewAutoFilterTextBoxColumn11.HeaderText = "检验状态";
            this.dataGridViewAutoFilterTextBoxColumn11.Name = "dataGridViewAutoFilterTextBoxColumn11";
            this.dataGridViewAutoFilterTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn11.Width = 130;
            // 
            // gridIQCData
            // 
            this.gridIQCData.AllowUserToAddRows = false;
            this.gridIQCData.AllowUserToDeleteRows = false;
            this.gridIQCData.AllowUserToOrderColumns = true;
            this.gridIQCData.AllowUserToResizeRows = false;
            this.gridIQCData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridIQCData.BackgroundColor = System.Drawing.Color.Black;
            this.gridIQCData.ButtonValue = 0;
            this.gridIQCData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridIQCData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IQCNO,
            this.IQCSEQ,
            this.IQCReceiveDate,
            this.IQCInspectionDate,
            this.IQCVenderName,
            this.IQCPartNumber,
            this.IQCPartDEsc,
            this.IQCPurchNo,
            this.IQCUnit,
            this.IQCQty,
            this.IQCMatDocNo,
            this.IQCBatchNo,
            this.IQCProcessState,
            this.IQCInpsectionState});
            this.gridIQCData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridIQCData.Location = new System.Drawing.Point(3, 73);
            this.gridIQCData.Name = "gridIQCData";
            this.gridIQCData.RowHeadersVisible = false;
            this.gridIQCData.RowTemplate.Height = 23;
            this.gridIQCData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridIQCData.Size = new System.Drawing.Size(1018, 591);
            this.gridIQCData.TabIndex = 5;
            this.gridIQCData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridIQCData_ColumnHeaderMouseClick);
            // 
            // IQCNO
            // 
            this.IQCNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.IQCNO.DataPropertyName = "IQCNO";
            this.IQCNO.HeaderText = "NO";
            this.IQCNO.Name = "IQCNO";
            this.IQCNO.Width = 53;
            // 
            // IQCSEQ
            // 
            this.IQCSEQ.DataPropertyName = "IQCSEQ";
            this.IQCSEQ.HeaderText = "IQCSEQ";
            this.IQCSEQ.Name = "IQCSEQ";
            this.IQCSEQ.Visible = false;
            // 
            // IQCReceiveDate
            // 
            this.IQCReceiveDate.DataPropertyName = "IQCReceiveDate";
            this.IQCReceiveDate.HeaderText = "收料日期";
            this.IQCReceiveDate.Name = "IQCReceiveDate";
            // 
            // IQCInspectionDate
            // 
            this.IQCInspectionDate.DataPropertyName = "IQCInspectionDate";
            this.IQCInspectionDate.HeaderText = "检查日期";
            this.IQCInspectionDate.Name = "IQCInspectionDate";
            // 
            // IQCVenderName
            // 
            this.IQCVenderName.DataPropertyName = "IQCVenderName";
            this.IQCVenderName.HeaderText = "供应商";
            this.IQCVenderName.Name = "IQCVenderName";
            // 
            // IQCPartNumber
            // 
            this.IQCPartNumber.DataPropertyName = "IQCPartNumber";
            this.IQCPartNumber.HeaderText = "品号";
            this.IQCPartNumber.Name = "IQCPartNumber";
            // 
            // IQCPartDEsc
            // 
            this.IQCPartDEsc.DataPropertyName = "IQCPartDEsc";
            this.IQCPartDEsc.HeaderText = "品名";
            this.IQCPartDEsc.Name = "IQCPartDEsc";
            // 
            // IQCPurchNo
            // 
            this.IQCPurchNo.DataPropertyName = "IQCPurchNo";
            this.IQCPurchNo.HeaderText = "单号";
            this.IQCPurchNo.Name = "IQCPurchNo";
            // 
            // IQCUnit
            // 
            this.IQCUnit.DataPropertyName = "IQCUnit";
            this.IQCUnit.HeaderText = "单位";
            this.IQCUnit.Name = "IQCUnit";
            // 
            // IQCQty
            // 
            this.IQCQty.DataPropertyName = "IQCQty";
            this.IQCQty.HeaderText = "收料数量";
            this.IQCQty.Name = "IQCQty";
            // 
            // IQCMatDocNo
            // 
            this.IQCMatDocNo.DataPropertyName = "IQCMatDocNo";
            this.IQCMatDocNo.HeaderText = "物料文件";
            this.IQCMatDocNo.Name = "IQCMatDocNo";
            // 
            // IQCBatchNo
            // 
            this.IQCBatchNo.DataPropertyName = "IQCBatchNo";
            this.IQCBatchNo.HeaderText = "检验批号";
            this.IQCBatchNo.Name = "IQCBatchNo";
            // 
            // IQCProcessState
            // 
            this.IQCProcessState.DataPropertyName = "IQCProcessState";
            this.IQCProcessState.HeaderText = "检验状态";
            this.IQCProcessState.Name = "IQCProcessState";
            this.IQCProcessState.Visible = false;
            // 
            // IQCInpsectionState
            // 
            this.IQCInpsectionState.DataPropertyName = "IQCInpsectionState";
            this.IQCInpsectionState.HeaderText = "检验状态";
            this.IQCInpsectionState.Name = "IQCInpsectionState";
            // 
            // IQCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 702);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "IQCForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IQC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IQCForm_FormClosing);
            this.Load += new System.EventHandler(this.IQCForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridIQCData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolHide;
        private System.Windows.Forms.ToolStripMenuItem toolShowAll;
        private System.Windows.Forms.Timer timerCurrentTime;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Panel panel2;
        private com.amtec.usercontrol.PagerToolbar pagerToolbar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCurTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn6;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn7;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn8;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn9;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn11;
        private TTEDataGrid.TTEDataGridView gridIQCData;
        private System.Windows.Forms.DataGridViewTextBoxColumn IQCNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IQCSEQ;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCReceiveDate;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCInspectionDate;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCVenderName;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCPartNumber;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCPartDEsc;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCPurchNo;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCUnit;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCQty;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCMatDocNo;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IQCProcessState;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn IQCInpsectionState;
    }
}

