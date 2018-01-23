namespace ConExpress.Utility.DataComponent
{
    partial class DataGridViewColumnHeaderEditor
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbTitle = new System.Windows.Forms.RichTextBox();
            // 
            // rtbTitle
            // 
            this.rtbTitle.Location = new System.Drawing.Point(0, 0);
            this.rtbTitle.Name = "rtbTitle";
            this.rtbTitle.Size = new System.Drawing.Size(100, 96);
            this.rtbTitle.TabIndex = 0;
            this.rtbTitle.Text = "";
            this.rtbTitle.Multiline = false;
            this.rtbTitle.TabStop = false;
            this.rtbTitle.Visible = false;
            this.rtbTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbTitle_KeyDown);
            this.rtbTitle.Leave += new System.EventHandler(this.rtbTitle_Leave);
        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbTitle;
    }
}
