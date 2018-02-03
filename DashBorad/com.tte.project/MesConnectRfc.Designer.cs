namespace DashBorad.com.tte.project
{
    partial class MesConnectRfc
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
            this.ConnectRfc = new System.Windows.Forms.Button();
            this.dgSAPData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgSAPData)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectRfc
            // 
            this.ConnectRfc.Location = new System.Drawing.Point(46, 30);
            this.ConnectRfc.Name = "ConnectRfc";
            this.ConnectRfc.Size = new System.Drawing.Size(75, 23);
            this.ConnectRfc.TabIndex = 0;
            this.ConnectRfc.Text = "查询";
            this.ConnectRfc.UseVisualStyleBackColor = true;
            this.ConnectRfc.Click += new System.EventHandler(this.ConnectRfc_Click);
            // 
            // dgSAPData
            // 
            this.dgSAPData.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgSAPData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSAPData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSAPData.Location = new System.Drawing.Point(46, 71);
            this.dgSAPData.Name = "dgSAPData";
            this.dgSAPData.RowHeadersVisible = false;
            this.dgSAPData.RowTemplate.Height = 23;
            this.dgSAPData.Size = new System.Drawing.Size(571, 304);
            this.dgSAPData.TabIndex = 1;
            // 
            // MesConnectRfc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 406);
            this.Controls.Add(this.dgSAPData);
            this.Controls.Add(this.ConnectRfc);
            this.Name = "MesConnectRfc";
            this.Text = "MesConnectRfc";
            ((System.ComponentModel.ISupportInitialize)(this.dgSAPData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ConnectRfc;
        private System.Windows.Forms.DataGridView dgSAPData;
    }
}