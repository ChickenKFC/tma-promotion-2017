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
using HistorySearchCondition;

namespace LogicalOperationSearch
{
    public partial class DropPanelControl : UserControl
    {
        public event EventHandler OnDeleteLinkClicked;

        public DropPanelControl()
        {
            InitializeComponent();

            // Allow drag & drop event for FlowLayoutPanel
            this.flpDropPanel.AllowDrop = true;
            this.flpDropPanel.DragDrop += new DragEventHandler(flpDropPanel_DragDropEvent);
            this.flpDropPanel.DragEnter += new DragEventHandler(flpDropPanel_DragEnterEvent);
            this.linkRemove.LinkClicked += (s, e) => this.OnDeleteLinkClicked?.Invoke(this, e);
        }
        
        /// <summary>
        /// Operator field.
        /// </summary>
        private LogicalOperator OperatorField;
        public LogicalOperator Operator
        {
            get { return OperatorField; }
            set
            {
                OperatorField = value;
            }
        }

        /// <summary>
        /// Search history ids
        /// </summary>
        private List<int> HistoryIdField = new List<int>();
        public List<int> HistoryIds
        {
            get { return HistoryIdField; }
            set { HistoryIdField = value; }
        }

        /// <summary>
        /// Is display operator or not
        /// </summary>
        private bool IsDisplayOperatorSelectionField = false;
        public bool IsDisplayOperatorSelection
        {
            get { return IsDisplayOperatorSelectionField; }
            set
            {
                IsDisplayOperatorSelectionField = value;
                this.cbOperator.Visible = value;
            }
        }

        /// <summary>
        /// History search control
        /// </summary>
        private List<HistorySearchControl> HistorySearchControlField = new List<HistorySearchControl>();
        public List<HistorySearchControl> HistorySearchControl
        {
            get { return HistorySearchControlField; }
        }

        /// <summary>
        /// Flow layout panel's drag and drop event.
        /// </summary>
        /// <param name="sender">History Search Control object</param>
        /// <param name="args">Drag event args</param>
        private void flpDropPanel_DragDropEvent(Object sender, DragEventArgs args)
        {
            // Get HistorySearchControl from the DragDropEvent.
            HistorySearchControl usrCtrl = args.Data.GetData(typeof(HistorySearchControl)) as HistorySearchControl;

            // Add item into HistorySearchControl list.
            HistorySearchControlField.Add(usrCtrl);

            // Create DragItem from gotten information.
            DragItem item = new DragItem();
            item.HistoryId = usrCtrl.HistoryId;
            item.OnDeleteButtonClick += new EventHandler(RemoveChildItems);

            // Add into flpDropPanel
            flpDropPanel.Controls.Add(item);

            // Add new HistoryIds into HistoryIds.
            this.HistoryIds.Add(usrCtrl.HistoryId);
        }

        /// <summary>
        /// Flow layout panel's drag and drop event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void flpDropPanel_DragEnterEvent(Object sender, DragEventArgs args)
        {
            if (args.Data.GetDataPresent(typeof(HistorySearchControl))
                && flpDropPanel.Controls.Count < 4)
                args.Effect = DragDropEffects.Copy;
            else
                args.Effect = DragDropEffects.None;
        }

        private void RemoveChildItems (Object sender, EventArgs args)
        {
            // Handle event from here
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                DragItem ctrl = sender as DragItem;
                flpDropPanel.Controls.Remove(ctrl);

                // Remove item from HistorySearchControlList
                HistorySearchControl hisCtrl = 
                    HistorySearchControlField.FirstOrDefault(x => x.HistoryId == ctrl.HistoryId); 

                if (hisCtrl != null)
                {
                    HistorySearchControlField.Remove(hisCtrl);
                }
            }
        }

        private void DropPanelControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.LightSlateGray, ButtonBorderStyle.Dotted);
        }

        private void cbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbOperator = sender as ComboBox;

            switch (cbOperator.SelectedIndex)
            {
                case 0:
                    this.Operator = LogicalOperator.AND;
                    break;
                case 1:
                    this.Operator = LogicalOperator.OR;
                    break;
                case 2:
                    this.Operator = LogicalOperator.NOT;
                    break;
                default:
                    this.Operator = LogicalOperator.NONE;
                    break;
            }
        }
    }
}
