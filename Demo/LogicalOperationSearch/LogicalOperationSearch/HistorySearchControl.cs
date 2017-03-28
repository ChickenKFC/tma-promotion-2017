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

namespace HistorySearchCondition
{
    [System.ComponentModel.DefaultBindingProperty("HistorySearchControl")]
    public partial class HistorySearchControl : UserControl
    {
        Point ptStartPosition;

        #region Delegate Handler
        public event EventHandler OnDeleteButtonClicked;
        #endregion Delegate Handler
        public HistorySearchControl()
        {
            InitializeComponent();
            btnDelete.Click += (s, e) => this.OnDeleteButtonClicked?.Invoke(this, e);
        }

        private int HistoryIdValue;
        /// <summary>
        /// History ID
        /// </summary>
        [System.ComponentModel.Bindable(true)]
        public int HistoryId
        {
            get { return HistoryIdValue; }
            set
            {
                HistoryIdValue = value;
                lblHistoryId.Text = HistoryIdValue.ToString();
            }
        }

        private SearchTypes SearchTypeValue;
        /// <summary>
        /// Search Type
        /// </summary>
        [System.ComponentModel.Bindable(true)]
        public SearchTypes SearchType
        {
            get { return SearchTypeValue; }
            set
            {
                SearchTypeValue = value;
                this.lblSearchType.Text = Enum.GetName(typeof(SearchTargets), SearchTypeValue); ;
            }
        }

        private string SearchContentValue;
        /// <summary>
        /// Search Condition
        /// </summary>
        [System.ComponentModel.Bindable(true)]
        public string SearchContent
        {
            get { return SearchContentValue; }
            set
            {
                SearchContentValue = value;
                if (value != null)
                {
                    lblSearchContent.Text = value;
                }
            }
        }


        private SearchTargets SearchTargetValue;
        /// <summary>
        /// Search Condition
        /// </summary>
        [System.ComponentModel.Bindable(true)]
        public SearchTargets SearchTarget
        {
            get { return SearchTargetValue; }
            set
            {
                SearchTargetValue = value;
                this.lblSearchTarget.Text = Enum.GetName(typeof(SearchTargets), SearchTargetValue);
            }
        }

        private DateTime? FromDateField;
        /// <summary>
        /// Datetime search condition
        /// </summary>
        public DateTime? FromDate
        {
            get { return FromDateField; }
            set
            {
                if (FromDateField != value)
                {
                    FromDateField = value;
                    if (ToDate != null)
                    {
                        lblSearchContent.Text 
                            = string.Format(@"{0} ~ {1}",
                                this.FromDate.Value.ToString("yyyy/MM/dd"),
                                this.ToDate.Value.ToString("yyyy/MM/dd"));
                    }
                }
            }
        }

        private DateTime? ToDateField;
        /// <summary>
        /// Datetime search condition
        /// </summary>
        public DateTime? ToDate
        {
            get { return ToDateField; }
            set
            {
                if (ToDateField != value)
                {
                    ToDateField = value;
                    if (FromDate != null)
                    {
                        lblSearchContent.Text
                            = string.Format(@"{0} ~ {1}",
                                this.FromDate.Value.ToString("yyyy/MM/dd"),
                                this.ToDate.Value.ToString("yyyy/MM/dd"));
                    }
                }
            }
        }

        private void HistorySearchControl_Paint_1(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.LightSlateGray, ButtonBorderStyle.Dotted);
        }

        private void HistorySearchControl_MouseDown(object sender, MouseEventArgs e)
        {
            HistorySearchControl btn = sender as HistorySearchControl;

            if (e.Button == MouseButtons.Left)
            {
                btn.DoDragDrop(btn, DragDropEffects.Copy);
            }
            else if (e.Button == MouseButtons.Right)
            {
                ptStartPosition = this.PointToScreen(e.Location);
            }
        }

        private void HistorySearchControl_MouseMove(object sender, MouseEventArgs e)
        {
            // Berechnet zu jeder Bewegung die neue Position des Buttons (nur im "Verschiebe" Modus, linke
            // Maustaste...
            HistorySearchControl btn = (HistorySearchControl)sender;
            if (e.Button == MouseButtons.Right)
            {
                Point ptEndPosition = btn.PointToScreen(e.Location);
                ptEndPosition.Offset(-ptStartPosition.X, -ptStartPosition.Y);
                btn.Location = ptEndPosition;
            }
        }
    }
}
