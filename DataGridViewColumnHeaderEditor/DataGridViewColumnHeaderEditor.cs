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

        #region �¼�����

        /// <summary>
        /// ��ʼ�༭����ȡ���༭
        /// </summary>
        [Description("�ڿ�ʼ�༭�б���ʱ�������¼�����ȡ���༭��")]
        public event ColumnHeaderEditEventHandler BeginEdit;

        /// <summary>
        /// ׼�������༭����ȡ��
        /// </summary>
        [Description("�ڼ��������༭ʱ�������¼�����ȡ����")]
        public event ColumnHeaderEditEventHandler EndingEdit;

        /// <summary>
        /// �����༭
        /// </summary>
        [Description("�ڱ༭�����������¼���")]
        public event ColumnHeaderEditEventHandler EndEdit;

        #endregion �¼�����


        #region �ֶκ���������


        private int m_SelectedColumnIndex = -1;//ѡ��������
        private int m_ScrollValue = 0;//DataGridView�ؼ�ˮƽ��������
        //���е���ʾ���������ж����б�
        private SortedList<int, DataGridViewColumn> m_SortedColumnList = new SortedList<int, DataGridViewColumn>();


        private DataGridView m_TargetControl = null;
        /// <summary>
        /// Ҫ�༭��Ŀ�� DataGridView �ؼ�
        /// </summary>
        [Description("Ҫ�༭��Ŀ�� DataGridView �ؼ���")]
        public DataGridView TargetControl
        {
            get { return m_TargetControl; }
            set { m_TargetControl = value; }
        }


        private bool m_EnableEdit = true;
        /// <summary>
        /// �Ƿ�����༭
        /// </summary>
        [Description("�Ƿ�����༭��"), DefaultValue(true)]
        public bool EnableEdit
        {
            get { return m_EnableEdit; }
            set { m_EnableEdit = value; }
        }

        private string selectText = string.Empty;
        /// <summary>
        /// �༭������
        /// </summary>
        [Description("�༭��������"), DefaultValue(true)]
        public string EditColumnText
        {
            get { return selectText; }
            set { selectText = value; }
        }

        #endregion �ֶκ���������


        #region ISupportInitialize ��Ա

        public void BeginInit()
        {
            //�޲���
        }

        public void EndInit()
        {
            if (m_TargetControl != null)
            {
                this.m_TargetControl.Parent.Controls.Add(this.rtbTitle);
                this.rtbTitle.BringToFront();//��RichTextBox�ؼ�ǰ��
                this.ReloadSortedColumnList();//���¼����ж����б�
                m_TargetControl.ColumnHeaderMouseDoubleClick += new DataGridViewCellMouseEventHandler(TargetControl_ColumnHeaderMouseDoubleClick);
                m_TargetControl.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(TargetControl_ColumnDisplayIndexChanged);
                m_TargetControl.ColumnRemoved += new DataGridViewColumnEventHandler(TargetControl_ColumnRemoved);
                m_TargetControl.ColumnAdded += new DataGridViewColumnEventHandler(TargetControl_ColumnAdded);
                m_TargetControl.Scroll += new ScrollEventHandler(TargetControl_Scroll);
            }
        }

        #endregion ISupportInitialize ��Ա


        #region Ŀ��ؼ����¼�����

        void TargetControl_Scroll(object sender, ScrollEventArgs e)
        {
            //ֻ�ڲ���ˮƽ������ʱ���д���
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                this.m_ScrollValue = e.NewValue;//��¼������λ��

                if (this.rtbTitle.Visible)
                    this.ShowHeaderEdit();//�����ǰ�Ǳ༭״̬����ˢ����ʾ�༭���λ��
            }
        }

        void TargetControl_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            this.ReloadSortedColumnList();//���¼����ж����б�
        }

        void TargetControl_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            this.ReloadSortedColumnList();
        }

        void TargetControl_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.ReloadSortedColumnList();
        }

        //˫���б�����ʾ�༭״̬
        void TargetControl_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.m_SelectedColumnIndex = this.m_TargetControl.Columns[e.ColumnIndex].DisplayIndex;
            if (this.m_EnableEdit)
                this.ShowHeaderEdit();//��ʾ�༭״̬
        }

        #endregion Ŀ��ؼ����¼�����


        #region �ı�����ط���

        /// <summary>
        /// �ı���ļ��̴���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter://�س������༭
                    this.m_TargetControl.Focus();//�ñ༭��ʧȥ����������༭�����أ���ͬ
                    e.Handled = true;//��������Ϊtrue��������з��˵�ϵͳ��ʾ������ͬ
                    break;
                case Keys.Right://����
                    //�жϹ���Ƿ��ƶ�����ǰ�༭�ַ�����ĩβ������Ƶ�ĩβ���ƶ��༭��
                    if (this.rtbTitle.SelectionStart >= this.rtbTitle.Text.Length)
                    {
                        //�жϵ�ǰ�༭���Ƿ������һ��
                        if (this.m_SelectedColumnIndex < this.m_TargetControl.Columns.Count - 1)
                        {
                            e.Handled = true;
                            this.m_TargetControl.Focus();
                            //��ȡ��һ���ɼ��е���Ų�����Ϊ��ǰѡ�������
                            this.m_SelectedColumnIndex = this.GetNextVisibleColumnIndex(this.m_SelectedColumnIndex);
                            this.ShowHeaderEdit();//����ѡ������ʾ�༭��
                        }
                    }
                    break;
                case Keys.Left://����
                    //�жϹ���Ƿ񵽴ﵱǰ�༭�ַ�������ǰ������ƶ�����ǰ���ƶ��༭��
                    if (this.rtbTitle.SelectionStart == 0)
                    {
                        //�жϵ�ǰ�༭���Ƿ��ǵ�0��
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
        /// �ı���ʧȥ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbTitle_Leave(object sender, EventArgs e)
        {
            DataGridViewColumn myColumn = this.m_SortedColumnList[this.m_SelectedColumnIndex];
            //�����¼�����
            ColumnHeaderEditEventArgs myArgs = new ColumnHeaderEditEventArgs(myColumn, this.rtbTitle.Text.Trim());

            if (this.EndingEdit != null)
            {
                this.EndingEdit(this, myArgs);//�����¼�
                if (myArgs.Cancel)//���ȡ����־Ϊtrue
                {
                    this.rtbTitle.Focus();//���ֱ༭״̬
                    return;
                }
            }

            this.rtbTitle.Visible = false;
            if (myArgs.NewHeaderText.Length > 0)//�������ÿ��ַ�����Ϊ����
            {
                if (myColumn.HeaderText != myArgs.NewHeaderText)
                {
                    //���¼�����������±��⣬��Ϊ���¼����������������޸��±���
                    myColumn.HeaderText = myArgs.NewHeaderText;
                }
            }

            if (this.EndEdit != null)
                this.EndEdit(this, myArgs);//�����¼�
        }

        /// <summary>
        /// ��ʾ����༭Ч��
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
            //��һ����߾࣬��Ҫ�ж��Ƿ���ʾ�б���
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


        #endregion �ı�����ط���


        #region DataGridView����ط���

        /// <summary>
        /// ���¼����ж�����б�
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
        /// ��ȡ�е������߾�
        /// </summary>
        /// <param name="ColumnIndex">�����</param>
        /// <returns>�е���߾�</returns>
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
        /// ��ȡ��һ���ɼ��е����
        /// </summary>
        /// <param name="CurrentIndex">��ǰ�����</param>
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
        /// ��ȡ��һ���ɼ��е����
        /// </summary>
        /// <param name="CurrentIndex">��ǰ�����</param>
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


        #endregion DataGridView����ط���


    }//class DataGridViewColumnHeaderEditor

    /// <summary>
    /// �б���༭�¼�ί��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ColumnHeaderEditEventHandler(object sender, ColumnHeaderEditEventArgs e);

    /// <summary>
    /// �б���༭�¼�����
    /// </summary>
    public class ColumnHeaderEditEventArgs : EventArgs
    {
        private bool m_Cancel = false;
        /// <summary>
        /// ȡ���༭
        /// </summary>
        public bool Cancel
        {
            get { return m_Cancel; }
            set { m_Cancel = value; }
        }

        private string m_NewHeaderText = "";
        /// <summary>
        /// �µ��б���
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
        /// Ŀ����
        /// </summary>
        public DataGridViewColumn Column
        {
            get { return m_Column; }
        }

        public ColumnHeaderEditEventArgs(DataGridViewColumn Column, string NewHeaderText)
        {
            //if (Column == null)
            //    throw new ArgumentNullException("Column", "Ҫ�༭���в�����Ϊ�ա�");

            //this.m_Column = Column;

            //if (string.IsNullOrEmpty(NewHeaderText) || NewHeaderText.Trim().Length == 0)
            //    NewHeaderText = Column.HeaderText;

            //this.m_NewHeaderText = NewHeaderText.Trim();

            if (Column == null)
                throw new ArgumentNullException("Column", "Ҫ�༭���в�����Ϊ�ա�");

            

            this.m_NewHeaderText = Column.HeaderText;

        }
    }//class ColumnHeaderEditEventArgs

}
