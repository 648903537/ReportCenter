namespace DashBorad.com.tte.project
{
    partial class InsertFujiTrax
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbxUID = new System.Windows.Forms.TextBox();
            this.maxDate = new System.Windows.Forms.DateTimePicker();
            this.minDate = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnInsertFuji = new System.Windows.Forms.Button();
            this.selectOAData = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabData = new System.Windows.Forms.TabPage();
            this.gridExcelData = new System.Windows.Forms.DataGridView();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.tbLog = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExcelData)).BeginInit();
            this.tabLog.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Lime;
            this.progressBar1.Location = new System.Drawing.Point(110, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 16);
            this.progressBar1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 485);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tbxUID);
            this.panel1.Controls.Add(this.maxDate);
            this.panel1.Controls.Add(this.minDate);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnInsertFuji);
            this.panel1.Controls.Add(this.selectOAData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(767, 94);
            this.panel1.TabIndex = 0;
            // 
            // tbxUID
            // 
            this.tbxUID.Location = new System.Drawing.Point(240, 15);
            this.tbxUID.Name = "tbxUID";
            this.tbxUID.Size = new System.Drawing.Size(174, 21);
            this.tbxUID.TabIndex = 5;
            // 
            // maxDate
            // 
            this.maxDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.maxDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.maxDate.Location = new System.Drawing.Point(37, 42);
            this.maxDate.Name = "maxDate";
            this.maxDate.ShowUpDown = true;
            this.maxDate.Size = new System.Drawing.Size(172, 21);
            this.maxDate.TabIndex = 4;
            this.maxDate.Value = new System.DateTime(2017, 10, 19, 23, 59, 0, 0);
            // 
            // minDate
            // 
            this.minDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.minDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.minDate.Location = new System.Drawing.Point(37, 15);
            this.minDate.Name = "minDate";
            this.minDate.ShowUpDown = true;
            this.minDate.Size = new System.Drawing.Size(172, 21);
            this.minDate.TabIndex = 3;
            this.minDate.Value = new System.DateTime(2017, 10, 17, 0, 0, 0, 0);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(651, 43);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExcel.TabIndex = 2;
            this.btnExcel.Text = "导出EXCEL";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnInsertFuji
            // 
            this.btnInsertFuji.Location = new System.Drawing.Point(489, 43);
            this.btnInsertFuji.Name = "btnInsertFuji";
            this.btnInsertFuji.Size = new System.Drawing.Size(75, 23);
            this.btnInsertFuji.TabIndex = 1;
            this.btnInsertFuji.Text = "写入FUJI";
            this.btnInsertFuji.UseVisualStyleBackColor = true;
            this.btnInsertFuji.Click += new System.EventHandler(this.btnInsertFuji_Click);
            // 
            // selectOAData
            // 
            this.selectOAData.Location = new System.Drawing.Point(570, 43);
            this.selectOAData.Name = "selectOAData";
            this.selectOAData.Size = new System.Drawing.Size(75, 23);
            this.selectOAData.TabIndex = 0;
            this.selectOAData.Text = "查找OA数据";
            this.selectOAData.UseVisualStyleBackColor = true;
            this.selectOAData.Click += new System.EventHandler(this.selectOAData_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(767, 351);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabData);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(767, 351);
            this.tabControl1.TabIndex = 1;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.gridExcelData);
            this.tabData.Location = new System.Drawing.Point(4, 22);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(759, 325);
            this.tabData.TabIndex = 0;
            this.tabData.Text = "数据列表";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // gridExcelData
            // 
            this.gridExcelData.AllowUserToAddRows = false;
            this.gridExcelData.AllowUserToDeleteRows = false;
            this.gridExcelData.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridExcelData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridExcelData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridExcelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridExcelData.Location = new System.Drawing.Point(3, 3);
            this.gridExcelData.Name = "gridExcelData";
            this.gridExcelData.RowHeadersVisible = false;
            this.gridExcelData.RowTemplate.Height = 23;
            this.gridExcelData.Size = new System.Drawing.Size(753, 319);
            this.gridExcelData.TabIndex = 0;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.tbLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(759, 325);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "日志";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(3, 3);
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(753, 319);
            this.tbLog.TabIndex = 0;
            this.tbLog.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblResult);
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Controls.Add(this.lblIpAddress);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 460);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(767, 22);
            this.panel3.TabIndex = 2;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(216, 4);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 12);
            this.lblResult.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(4, 7);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 12);
            this.lblStatus.TabIndex = 6;
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Location = new System.Drawing.Point(600, 4);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(35, 12);
            this.lblIpAddress.TabIndex = 4;
            this.lblIpAddress.Text = "IP : ";
            // 
            // InsertFujiTrax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 485);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "InsertFujiTrax";
            this.Text = "储位导入";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateLocator_FormClosing);
            this.Load += new System.EventHandler(this.CreateLocator_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridExcelData)).EndInit();
            this.tabLog.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.DataGridView gridExcelData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnInsertFuji;
        private System.Windows.Forms.Button selectOAData;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DateTimePicker maxDate;
        private System.Windows.Forms.DateTimePicker minDate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox tbLog;
        private System.Windows.Forms.TextBox tbxUID;
    }
}