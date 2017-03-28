using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicalOperationSearch.Resource;

namespace LogicalOperationSearch
{
    public partial class DragItem : UserControl
    {
        /// <summary>
        /// Delete link click event handler.
        /// </summary>
        public EventHandler OnDeleteButtonClick;

        private SearchTypes SearchTypeField;
        /// <summary>
        /// Search type
        /// </summary>
        public SearchTypes SearchType
        {
            get { return SearchTypeField; }
            set
            {
                SearchTypeField = value;
            }
        }

        /// <summary>
        /// Search target tables.
        /// </summary>
        private SearchTargets SearchTargetField;
        public SearchTargets SearchTarget
        {
            get { return SearchTargetField; }
            set
            {
                SearchTargetField = value;
            }
        }

        /// <summary>
        /// Text search condition.
        /// </summary>
        private string TextSearchConditionField;
        public string TextSearchCondition
        {
            get { return TextSearchConditionField; }
            set
            {
                TextSearchConditionField = value;
            }
        }

        /// <summary>
        /// Start date time
        /// </summary>
        private DateTime? FromDateField;
        public DateTime? FromDate
        {
            get { return FromDateField; }
            set
            {
                FromDateField = value;
            }
        }

        /// <summary>
        /// Start date time
        /// </summary>
        private DateTime? ToDateField;
        public DateTime? ToDate
        {
            get { return ToDateField; }
            set
            {
                ToDateField = value;
            }
        }

        public DragItem()
        {
            InitializeComponent();
            this.pictureBox1.Click += (s, e) => this.OnDeleteButtonClick?.Invoke(this, e);
        }

        private int HistoryIdField;
        public int HistoryId
        {
            get { return HistoryIdField; }
            set
            {
                HistoryIdField = value;
                lblHistoryId.Text = string.Format(@"History ID: {0}", value);
            }
        }

        private void DragItem_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.LightSlateGray, ButtonBorderStyle.Dotted);
        }
    }
}
