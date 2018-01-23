using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ConExpress.Utility.DataComponent
{
    /// <summary>
    /// Author: Alex Leo
    /// Email: alexleo321@hotmail.com
    /// Blog: http://www.cnblogs.com/conexpress/
    /// </summary>
    [DefaultProperty("TargetControl")]
    public partial class DataGridViewColumnHeaderEditor : Component, ISupportInitialize
    {
        public DataGridViewColumnHeaderEditor()
        {
            InitializeComponent();
        }

        public DataGridViewColumnHeaderEditor(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region 事件声明

        /// <summary>
        /// 开始编辑，可取消编辑
        /// </summary>
        [Description("在开始编辑列标题时发生的事件，可取消编辑。")]
        public event ColumnHeaderEditEventHandler BeginEdit;

        /// <summary>
        /// 准备结束编辑，可取消
        /// </summary>
        [Description("在即将结束编辑时发生的事件，可取消。")]
        public event ColumnHeaderEditEventHandler EndingEdit;

        /// <summary>
        /// 结束编辑
        /// </summary>
        [Description("在编辑结束后发生的事件。")]
        public event ColumnHeaderEditEventHandler EndEdit;

        #endregion 事件声明


        #region 字段和属性声明


        private int m_SelectedColumnIndex = -1;//选择的列序号
        private int m_ScrollValue = 0;//DataGridView控件水平滚动像素
        //按列的显示序号排序的列对象列表
        private SortedList<int, DataGridViewColumn> m_SortedColumnList = new SortedList<int, DataGridViewColumn>();


        private DataGridView m_TargetControl = null;
        /// <summary>
        /// 要编辑的目标 DataGridView 控件
        /// </summary>
        [Description("要编辑的目标 DataGridView 控件。")]
        public DataGridView TargetControl
        {
            get { return m_TargetControl; }
            set { m_TargetControl = value; }
        }


        private bool m_EnableEdit = true;
        /// <summary>
        /// 是否允许编辑
        /// </summary>
        [Description("是否允许编辑。"), DefaultValue(true)]
        public bool EnableEdit
        {
            get { return m_EnableEdit; }
            set { m_EnableEdit = value; }
        }

        private string selectText = string.Empty;
        /// <summary>
        /// 编辑的列名
        /// </summary>
        [Description("编辑的列名。"), DefaultValue(true)]
        public string EditColumnText
        {
            get { return selectText; }
            set { selectText = value; }
        }

        #endregion 字段和属性声明


        #region ISupportInitialize 成员

        public void BeginInit()
        {
            //无操作
        }

        public void EndInit()
        {
            if (m_TargetControl != null)
            {
                this.m_TargetControl.Parent.Controls.Add(this.rtbTitle);
                this.rtbTitle.BringToFront();//将RichTextBox控件前置
                this.ReloadSortedColumnList();//重新加载列对象列表
                m_TargetControl.ColumnHeaderMouseDoubleClick += new DataGridViewCellMouseEventHandler(TargetControl_ColumnHeaderMouseDoubleClick);
                m_TargetControl.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(TargetControl_ColumnDisplayIndexChanged);
                m_TargetControl.ColumnRemoved += new DataGridViewColumnEventHandler(TargetControl_ColumnRemoved);
                m_TargetControl.ColumnAdded += new DataGridViewColumnEventHandler(TargetControl_ColumnAdded);
                m_TargetControl.Scroll += new ScrollEventHandler(TargetControl_Scroll);
            }
        }

        #endregion ISupportInitialize 成员


        #region 目标控件的事件处理

        void TargetControl_Scroll(object sender, ScrollEventArgs e)
        {
            //只在操作水平滚动条时进行处理
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                this.m_ScrollValue = e.NewValue;//记录滚动条位置

                if (this.rtbTitle.Visible)
                    this.ShowHeaderEdit();//如果当前是编辑状态，则刷新显示编辑框的位置
            }
        }

        void TargetControl_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            this.ReloadSortedColumnList();//重新加载列对象列表
        }

        void TargetControl_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            this.ReloadSortedColumnList();
        }

        void TargetControl_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.ReloadSortedColumnList();
        }

        //双击列标题显示编辑状态
        void TargetControl_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.m_SelectedColumnIndex = this.m_TargetControl.Columns[e.ColumnIndex].DisplayIndex;
            if (this.m_EnableEdit)
                this.ShowHeaderEdit();//显示编辑状态
        }

        #endregion 目标控件的事件处理


        #region 文本框相关方法

        /// <summary>
        /// 文本框的键盘处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter://回车结束编辑
                    this.m_TargetControl.Focus();//让编辑框失去焦点而结束编辑并隐藏，下同
                    e.Handled = true;//必须设置为true，否则会有烦人的系统提示音，下同
                    break;
                case Keys.Right://向右
                    //判断光标是否移动到当前编辑字符串的末尾，光标移到末尾才移动编辑框
                    if (this.rtbTitle.SelectionStart >= this.rtbTitle.Text.Length)
                    {
                        //判断当前编辑列是否是最后一列
                        if (this.m_SelectedColumnIndex < this.m_TargetControl.Columns.Count - 1)
                        {
                            e.Handled = true;
                            this.m_TargetControl.Focus();
                            //获取下一个可见列的序号并设置为当前选中列序号
                            this.m_SelectedColumnIndex = this.GetNextVisibleColumnIndex(this.m_SelectedColumnIndex);
                            this.ShowHeaderEdit();//根据选中列显示编辑框
                        }
                    }
                    break;
                case Keys.Left://向左
                    //判断光标是否到达当前编辑字符串的最前，光标移动到最前才移动编辑框
                    if (this.rtbTitle.SelectionStart == 0)
                    {
                        //判断当前编辑列是否是第0列
                        if (this.m_SelectedColumnIndex > 0)
                        {
                            e.Handled = true;
                            this.m_TargetControl.Focus();
                            this.m_SelectedColumnIndex = this.GetPreVisibleColumnIndex(this.m_SelectedColumnIndex);
                            this.ShowHeaderEdit();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 文本框失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbTitle_Leave(object sender, EventArgs e)
        {
            DataGridViewColumn myColumn = this.m_SortedColumnList[this.m_SelectedColumnIndex];
            //定义事件参数
            ColumnHeaderEditEventArgs myArgs = new ColumnHeaderEditEventArgs(myColumn, this.rtbTitle.Text.Trim());

            if (this.EndingEdit != null)
            {
                this.EndingEdit(this, myArgs);//引发事件
                if (myArgs.Cancel)//如果取消标志为true
                {
                    this.rtbTitle.Focus();//保持编辑状态
                    return;
                }
            }

            this.rtbTitle.Visible = false;
            if (myArgs.NewHeaderText.Length > 0)//不允许用空字符串作为标题
            {
                if (myColumn.HeaderText != myArgs.NewHeaderText)
                {
                    //用事件参数里面的新标题，因为在事件处理程序里面可能修改新标题
                    myColumn.HeaderText = myArgs.NewHeaderText;
                }
            }

            if (this.EndEdit != null)
                this.EndEdit(this, myArgs);//引发事件
        }

        /// <summary>
        /// 显示标题编辑效果
        /// </summary>
        private void ShowHeaderEdit()
        {
            if (this.BeginEdit != null)
            {
                ColumnHeaderEditEventArgs myArgs = new ColumnHeaderEditEventArgs(this.m_SortedColumnList[this.m_SelectedColumnIndex], "");
                BeginEdit(this, myArgs);
                if (myArgs.Cancel)
                    return;
            }

            int intColumnRelativeLeft = 0;
            //第一列左边距，需要判断是否显示行标题
            int intFirstColumnLeft = (this.m_TargetControl.RowHeadersVisible ? this.m_TargetControl.RowHeadersWidth + 1 : 1);
            int intTargetX = this.m_TargetControl.Location.X, intTargetY = this.m_TargetControl.Location.Y, intTargetWidth = this.m_TargetControl.Width;

            intColumnRelativeLeft = GetColumnRelativeLeft(this.m_SelectedColumnIndex);

            if (intColumnRelativeLeft < this.m_ScrollValue)
            {
                this.rtbTitle.Location = new Point(intTargetX + intFirstColumnLeft, intTargetY + 1);
                if (intColumnRelativeLeft + this.m_SortedColumnList[this.m_SelectedColumnIndex].Width > this.m_ScrollValue)
                    this.rtbTitle.Width = intColumnRelativeLeft + this.m_SortedColumnList[this.m_SelectedColumnIndex].Width - this.m_ScrollValue;
                else
                    this.rtbTitle.Width = 0;
            }
            else
            {
                this.rtbTitle.Location = new Point(intColumnRelativeLeft + intTargetX - this.m_ScrollValue + intFirstColumnLeft, intTargetY + 1);

                if (this.rtbTitle.Location.X + this.rtbTitle.Width > intTargetX + intTargetWidth)
                {
                    int intWidth = intTargetX + intTargetWidth - this.rtbTitle.Location.X;
                    this.rtbTitle.Width = (intWidth >= 0 ? intWidth : 0);
                }
                else
                    this.rtbTitle.Width = this.m_SortedColumnList[this.m_SelectedColumnIndex].Width;
            }

            this.rtbTitle.Height = this.m_TargetControl.ColumnHeadersHeight - 1;
            this.rtbTitle.Text = this.m_SortedColumnList[this.m_SelectedColumnIndex].HeaderText;
            this.rtbTitle.SelectAll();
            this.rtbTitle.Visible = true;
            this.rtbTitle.Focus();
        }


        #endregion 文本框相关方法


        #region DataGridView列相关方法

        /// <summary>
        /// 重新加载列对象的列表
        /// </summary>
        private void ReloadSortedColumnList()
        {
            this.m_SortedColumnList.Clear();
            foreach (DataGridViewColumn column in this.m_TargetControl.Columns)
            {
                this.m_SortedColumnList.Add(column.DisplayIndex, column);
            }
        }

        /// <summary>
        /// 获取列的相对左边距
        /// </summary>
        /// <param name="ColumnIndex">列序号</param>
        /// <returns>列的左边距</returns>
        private int GetColumnRelativeLeft(int ColumnIndex)
        {
            int intLeft = 0;
            DataGridViewColumn Column = null;

            for (int intIndex = 0; intIndex < ColumnIndex; intIndex++)
            {
                if (this.m_SortedColumnList.ContainsKey(intIndex))
                {
                    Column = this.m_SortedColumnList[intIndex];
                    if (Column.Visible)
                        intLeft += Column.Width + Column.DividerWidth;
                }
            }

            return intLeft;
        }

        /// <summary>
        /// 获取上一个可见列的序号
        /// </summary>
        /// <param name="CurrentIndex">当前列序号</param>
        /// <returns></returns>
        private int GetPreVisibleColumnIndex(int CurrentIndex)
        {
            int intPreIndex = 0;

            for (int intIndex = CurrentIndex - 1; intIndex >= 0; intIndex--)
            {
                if (this.m_SortedColumnList.ContainsKey(intIndex) && this.m_SortedColumnList[intIndex].Visible)
                {
                    intPreIndex = intIndex;
                    break;
                }
            }

            return intPreIndex;
        }

        /// <summary>
        /// 获取下一个可见列的序号
        /// </summary>
        /// <param name="CurrentIndex">当前列序号</param>
        /// <returns></returns>
        private int GetNextVisibleColumnIndex(int CurrentIndex)
        {
            int intNextIndex = CurrentIndex;

            for (int intIndex = CurrentIndex + 1; intIndex <= this.m_SortedColumnList.Keys[this.m_SortedColumnList.Count - 1]; intIndex++)
            {
                if (this.m_SortedColumnList.ContainsKey(intIndex) && this.m_SortedColumnList[intIndex].Visible)
                {
                    intNextIndex = intIndex;
                    break;
                }
            }

            return intNextIndex;
        }


        #endregion DataGridView列相关方法


    }//class DataGridViewColumnHeaderEditor

    /// <summary>
    /// 列标题编辑事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ColumnHeaderEditEventHandler(object sender, ColumnHeaderEditEventArgs e);

    /// <summary>
    /// 列标题编辑事件参数
    /// </summary>
    public class ColumnHeaderEditEventArgs : EventArgs
    {
        private bool m_Cancel = false;
        /// <summary>
        /// 取消编辑
        /// </summary>
        public bool Cancel
        {
            get { return m_Cancel; }
            set { m_Cancel = value; }
        }

        private string m_NewHeaderText = "";
        /// <summary>
        /// 新的列标题
        /// </summary>
        public string NewHeaderText
        {
            get { return m_NewHeaderText; }
            set
            {
                if (!(string.IsNullOrEmpty(value) || value.Trim().Length == 0))
                    m_NewHeaderText = value;
            }
        }

        private DataGridViewColumn m_Column = null;
        /// <summary>
        /// 目标列
        /// </summary>
        public DataGridViewColumn Column
        {
            get { return m_Column; }
        }

        public ColumnHeaderEditEventArgs(DataGridViewColumn Column, string NewHeaderText)
        {
            //if (Column == null)
            //    throw new ArgumentNullException("Column", "要编辑的列不允许为空。");

            //this.m_Column = Column;

            //if (string.IsNullOrEmpty(NewHeaderText) || NewHeaderText.Trim().Length == 0)
            //    NewHeaderText = Column.HeaderText;

            //this.m_NewHeaderText = NewHeaderText.Trim();

            if (Column == null)
                throw new ArgumentNullException("Column", "要编辑的列不允许为空。");

            

            this.m_NewHeaderText = Column.HeaderText;

        }
    }//class ColumnHeaderEditEventArgs

}
