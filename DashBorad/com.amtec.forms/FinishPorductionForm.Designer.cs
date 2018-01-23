namespace com.amtec.forms
{
    partial class FinishPorductionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinishPorductionForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCurTime = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pagerToolbar1 = new com.amtec.usercontrol.PagerToolbar();
            this.gridFGData = new System.Windows.Forms.DataGridView();
            this.FPNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FGSEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FPReceiveDate = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPInspectionDate = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPWoType = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPwoNumber = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPPartNumber = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPPartDEsc = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPMatDocNo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPQty = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPUnit = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPInspectionNo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.FPProcessState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FPInpsectionState = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFGData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridFGData, 0, 1);
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
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.88084F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.00781F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.17969F));
            this.tableLayoutPanel3.Controls.Add(this.lblCaption, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel6, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1024, 70);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // lblCaption
            // 
            this.lblCaption.BackColor = System.Drawing.Color.Black;
            this.lblCaption.Font = new System.Drawing.Font("方正舒体", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCaption.ForeColor = System.Drawing.Color.Yellow;
            this.lblCaption.Location = new System.Drawing.Point(339, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(331, 70);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "FG DashBoard";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBox1);
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
            this.panel6.Location = new System.Drawing.Point(673, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(351, 70);
            this.panel6.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(46, 43);
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
            this.label4.Location = new System.Drawing.Point(46, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "未捡";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Location = new System.Drawing.Point(14, 38);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 25);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 7);
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
            this.lblCurTime.Location = new System.Drawing.Point(92, 21);
            this.lblCurTime.Name = "lblCurTime";
            this.lblCurTime.Size = new System.Drawing.Size(238, 22);
            this.lblCurTime.TabIndex = 19;
            this.lblCurTime.Text = "2008-05-20 00:00:00";
            this.lblCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // gridFGData
            // 
            this.gridFGData.AllowUserToAddRows = false;
            this.gridFGData.AllowUserToDeleteRows = false;
            this.gridFGData.AllowUserToOrderColumns = true;
            this.gridFGData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridFGData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridFGData.BackgroundColor = System.Drawing.Color.Gray;
            this.gridFGData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridFGData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridFGData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFGData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FPNO,
            this.FGSEQ,
            this.FPReceiveDate,
            this.FPInspectionDate,
            this.FPWoType,
            this.FPwoNumber,
            this.FPPartNumber,
            this.FPPartDEsc,
            this.FPMatDocNo,
            this.FPQty,
            this.FPUnit,
            this.FPInspectionNo,
            this.FPProcessState,
            this.FPInpsectionState});
            this.gridFGData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFGData.EnableHeadersVisualStyles = false;
            this.gridFGData.GridColor = System.Drawing.Color.Black;
            this.gridFGData.Location = new System.Drawing.Point(3, 73);
            this.gridFGData.Name = "gridFGData";
            this.gridFGData.ReadOnly = true;
            this.gridFGData.RowHeadersVisible = false;
            this.gridFGData.RowTemplate.Height = 23;
            this.gridFGData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFGData.Size = new System.Drawing.Size(1018, 591);
            this.gridFGData.TabIndex = 1;
            this.gridFGData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridFGData_ColumnHeaderMouseClick);
            this.gridFGData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridFGData_DataBindingComplete);
            this.gridFGData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridFGData_KeyDown);
            // 
            // FPNO
            // 
            this.FPNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPNO.DataPropertyName = "FPNO";
            this.FPNO.HeaderText = "NO";
            this.FPNO.Name = "FPNO";
            this.FPNO.ReadOnly = true;
            this.FPNO.Width = 53;
            // 
            // FGSEQ
            // 
            this.FGSEQ.DataPropertyName = "FPSEQ";
            this.FGSEQ.HeaderText = "FPSEQ";
            this.FGSEQ.Name = "FGSEQ";
            this.FGSEQ.ReadOnly = true;
            this.FGSEQ.Visible = false;
            // 
            // FPReceiveDate
            // 
            this.FPReceiveDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPReceiveDate.DataPropertyName = "FPReceiveDate";
            this.FPReceiveDate.HeaderText = "收料日期";
            this.FPReceiveDate.Name = "FPReceiveDate";
            this.FPReceiveDate.ReadOnly = true;
            this.FPReceiveDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPReceiveDate.Width = 150;
            // 
            // FPInspectionDate
            // 
            this.FPInspectionDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPInspectionDate.DataPropertyName = "FPInspectionDate";
            this.FPInspectionDate.HeaderText = "检验日期";
            this.FPInspectionDate.Name = "FPInspectionDate";
            this.FPInspectionDate.ReadOnly = true;
            this.FPInspectionDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPInspectionDate.Width = 150;
            // 
            // FPWoType
            // 
            this.FPWoType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPWoType.DataPropertyName = "FPWoType";
            this.FPWoType.HeaderText = "工单单别";
            this.FPWoType.Name = "FPWoType";
            this.FPWoType.ReadOnly = true;
            this.FPWoType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPWoType.Width = 130;
            // 
            // FPwoNumber
            // 
            this.FPwoNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPwoNumber.DataPropertyName = "FPwoNumber";
            this.FPwoNumber.HeaderText = "工单号";
            this.FPwoNumber.Name = "FPwoNumber";
            this.FPwoNumber.ReadOnly = true;
            this.FPwoNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPwoNumber.Width = 150;
            // 
            // FPPartNumber
            // 
            this.FPPartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPPartNumber.DataPropertyName = "FPPartNumber";
            this.FPPartNumber.HeaderText = "品号";
            this.FPPartNumber.Name = "FPPartNumber";
            this.FPPartNumber.ReadOnly = true;
            this.FPPartNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPPartNumber.Width = 170;
            // 
            // FPPartDEsc
            // 
            this.FPPartDEsc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPPartDEsc.DataPropertyName = "FPPartDEsc";
            this.FPPartDEsc.HeaderText = "品名";
            this.FPPartDEsc.Name = "FPPartDEsc";
            this.FPPartDEsc.ReadOnly = true;
            this.FPPartDEsc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPPartDEsc.Width = 500;
            // 
            // FPMatDocNo
            // 
            this.FPMatDocNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPMatDocNo.DataPropertyName = "FPNumberNo";
            this.FPMatDocNo.HeaderText = "入库单号";
            this.FPMatDocNo.Name = "FPMatDocNo";
            this.FPMatDocNo.ReadOnly = true;
            this.FPMatDocNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPMatDocNo.Width = 150;
            // 
            // FPQty
            // 
            this.FPQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPQty.DataPropertyName = "FPQty";
            this.FPQty.HeaderText = "收料数量";
            this.FPQty.Name = "FPQty";
            this.FPQty.ReadOnly = true;
            this.FPQty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPQty.Width = 130;
            // 
            // FPUnit
            // 
            this.FPUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPUnit.DataPropertyName = "FPUnit";
            this.FPUnit.HeaderText = "单位";
            this.FPUnit.Name = "FPUnit";
            this.FPUnit.ReadOnly = true;
            this.FPUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // FPInspectionNo
            // 
            this.FPInspectionNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPInspectionNo.DataPropertyName = "FPInspectionNo";
            this.FPInspectionNo.HeaderText = "检验批号";
            this.FPInspectionNo.Name = "FPInspectionNo";
            this.FPInspectionNo.ReadOnly = true;
            this.FPInspectionNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPInspectionNo.Width = 150;
            // 
            // FPProcessState
            // 
            this.FPProcessState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPProcessState.DataPropertyName = "FPProcessState";
            this.FPProcessState.HeaderText = "检验状态";
            this.FPProcessState.Name = "FPProcessState";
            this.FPProcessState.ReadOnly = true;
            this.FPProcessState.Visible = false;
            this.FPProcessState.Width = 130;
            // 
            // FPInpsectionState
            // 
            this.FPInpsectionState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FPInpsectionState.DataPropertyName = "FPInpsectionState";
            this.FPInpsectionState.HeaderText = "检验状态";
            this.FPInpsectionState.Name = "FPInpsectionState";
            this.FPInpsectionState.ReadOnly = true;
            this.FPInpsectionState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FPInpsectionState.Width = 130;
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
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FPNO";
            this.dataGridViewTextBoxColumn1.HeaderText = "NO";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 53;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FPSEQ";
            this.dataGridViewTextBoxColumn2.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewAutoFilterTextBoxColumn1
            // 
            this.dataGridViewAutoFilterTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn1.DataPropertyName = "FPReceiveDate";
            this.dataGridViewAutoFilterTextBoxColumn1.HeaderText = "收料日期";
            this.dataGridViewAutoFilterTextBoxColumn1.Name = "dataGridViewAutoFilterTextBoxColumn1";
            this.dataGridViewAutoFilterTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn1.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn2
            // 
            this.dataGridViewAutoFilterTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn2.DataPropertyName = "FPInspectionDate";
            this.dataGridViewAutoFilterTextBoxColumn2.HeaderText = "检查日期";
            this.dataGridViewAutoFilterTextBoxColumn2.Name = "dataGridViewAutoFilterTextBoxColumn2";
            this.dataGridViewAutoFilterTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn2.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn3
            // 
            this.dataGridViewAutoFilterTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn3.DataPropertyName = "FPVenderName";
            this.dataGridViewAutoFilterTextBoxColumn3.HeaderText = "供应商";
            this.dataGridViewAutoFilterTextBoxColumn3.Name = "dataGridViewAutoFilterTextBoxColumn3";
            this.dataGridViewAutoFilterTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn3.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn4
            // 
            this.dataGridViewAutoFilterTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn4.DataPropertyName = "FPPartNumber";
            this.dataGridViewAutoFilterTextBoxColumn4.HeaderText = "品号";
            this.dataGridViewAutoFilterTextBoxColumn4.Name = "dataGridViewAutoFilterTextBoxColumn4";
            this.dataGridViewAutoFilterTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn4.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn5
            // 
            this.dataGridViewAutoFilterTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn5.DataPropertyName = "FPPartDEsc";
            this.dataGridViewAutoFilterTextBoxColumn5.HeaderText = "品名";
            this.dataGridViewAutoFilterTextBoxColumn5.Name = "dataGridViewAutoFilterTextBoxColumn5";
            this.dataGridViewAutoFilterTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn5.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn6
            // 
            this.dataGridViewAutoFilterTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn6.DataPropertyName = "FPPurchNo";
            this.dataGridViewAutoFilterTextBoxColumn6.HeaderText = "单号";
            this.dataGridViewAutoFilterTextBoxColumn6.Name = "dataGridViewAutoFilterTextBoxColumn6";
            this.dataGridViewAutoFilterTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn6.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn7
            // 
            this.dataGridViewAutoFilterTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn7.DataPropertyName = "FPUnit";
            this.dataGridViewAutoFilterTextBoxColumn7.HeaderText = "单位";
            this.dataGridViewAutoFilterTextBoxColumn7.Name = "dataGridViewAutoFilterTextBoxColumn7";
            this.dataGridViewAutoFilterTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewAutoFilterTextBoxColumn8
            // 
            this.dataGridViewAutoFilterTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn8.DataPropertyName = "FPQty";
            this.dataGridViewAutoFilterTextBoxColumn8.HeaderText = "收料数量";
            this.dataGridViewAutoFilterTextBoxColumn8.Name = "dataGridViewAutoFilterTextBoxColumn8";
            this.dataGridViewAutoFilterTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn8.Width = 110;
            // 
            // dataGridViewAutoFilterTextBoxColumn9
            // 
            this.dataGridViewAutoFilterTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn9.DataPropertyName = "FPMatDocNo";
            this.dataGridViewAutoFilterTextBoxColumn9.HeaderText = "物料文件";
            this.dataGridViewAutoFilterTextBoxColumn9.Name = "dataGridViewAutoFilterTextBoxColumn9";
            this.dataGridViewAutoFilterTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn9.Width = 150;
            // 
            // dataGridViewAutoFilterTextBoxColumn10
            // 
            this.dataGridViewAutoFilterTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn10.DataPropertyName = "FPBatchNo";
            this.dataGridViewAutoFilterTextBoxColumn10.HeaderText = "检验批号";
            this.dataGridViewAutoFilterTextBoxColumn10.Name = "dataGridViewAutoFilterTextBoxColumn10";
            this.dataGridViewAutoFilterTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn10.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "FPProcessState";
            this.dataGridViewTextBoxColumn3.HeaderText = "检验状态";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 130;
            // 
            // dataGridViewAutoFilterTextBoxColumn11
            // 
            this.dataGridViewAutoFilterTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewAutoFilterTextBoxColumn11.DataPropertyName = "FPInpsectionState";
            this.dataGridViewAutoFilterTextBoxColumn11.HeaderText = "检验状态";
            this.dataGridViewAutoFilterTextBoxColumn11.Name = "dataGridViewAutoFilterTextBoxColumn11";
            this.dataGridViewAutoFilterTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAutoFilterTextBoxColumn11.Width = 130;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 25);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // FinishPorductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 702);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FinishPorductionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FinishPorductionForm_FormClosing);
            this.Load += new System.EventHandler(this.FinishPorductionForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFGData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView gridFGData;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn FPNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FGSEQ;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPReceiveDate;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPInspectionDate;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPWoType;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPwoNumber;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPPartNumber;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPPartDEsc;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPMatDocNo;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPQty;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPUnit;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPInspectionNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FPProcessState;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn FPInpsectionState;
        private System.Windows.Forms.TextBox textBox1;
    }
}

