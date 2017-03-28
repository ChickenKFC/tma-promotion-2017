namespace LogicalOperationSearch
{
    partial class DropPanelControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.flpDropPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.linkRemove = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Items.AddRange(new object[] {
            "AND",
            "OR",
            "NOT"});
            this.cbOperator.Location = new System.Drawing.Point(167, 4);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(121, 21);
            this.cbOperator.TabIndex = 4;
            this.cbOperator.SelectedIndexChanged += new System.EventHandler(this.cbOperator_SelectedIndexChanged);
            // 
            // flpDropPanel
            // 
            this.flpDropPanel.AllowDrop = true;
            this.flpDropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpDropPanel.AutoScroll = true;
            this.flpDropPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpDropPanel.Location = new System.Drawing.Point(4, 30);
            this.flpDropPanel.Name = "flpDropPanel";
            this.flpDropPanel.Size = new System.Drawing.Size(452, 38);
            this.flpDropPanel.TabIndex = 3;
            this.flpDropPanel.WrapContents = false;
            // 
            // linkRemove
            // 
            this.linkRemove.AutoSize = true;
            this.linkRemove.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.linkRemove.Location = new System.Drawing.Point(406, 6);
            this.linkRemove.Name = "linkRemove";
            this.linkRemove.Size = new System.Drawing.Size(47, 13);
            this.linkRemove.TabIndex = 5;
            this.linkRemove.TabStop = true;
            this.linkRemove.Text = "Remove";
            this.linkRemove.VisitedLinkColor = System.Drawing.Color.Red;
            // 
            // DropPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.linkRemove);
            this.Controls.Add(this.cbOperator);
            this.Controls.Add(this.flpDropPanel);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "DropPanelControl";
            this.Size = new System.Drawing.Size(460, 72);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DropPanelControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.FlowLayoutPanel flpDropPanel;
        private System.Windows.Forms.LinkLabel linkRemove;
    }
}
