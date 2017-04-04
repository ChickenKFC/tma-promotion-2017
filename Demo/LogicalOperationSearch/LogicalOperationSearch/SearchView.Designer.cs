namespace LogicalOperationSearch
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tbSimpleSearch = new System.Windows.Forms.TabControl();
            this.tabSimpleSearch = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pnSearchText = new System.Windows.Forms.Panel();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.pnSearchDateTime = new System.Windows.Forms.Panel();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdScientific = new System.Windows.Forms.RadioButton();
            this.rdMathematics = new System.Windows.Forms.RadioButton();
            this.rdPhysical = new System.Windows.Forms.RadioButton();
            this.tabLogicalOperatorSearch = new System.Windows.Forms.TabPage();
            this.flpSearchConditionCreatorPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgDisplayResult = new System.Windows.Forms.DataGridView();
            this.pgbSearchProcess = new System.Windows.Forms.ProgressBar();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flpHistorySearch = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExport = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historySearchControlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.historySearchControlBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tbSimpleSearch.SuspendLayout();
            this.tabSimpleSearch.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.pnSearchText.SuspendLayout();
            this.pnSearchDateTime.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabLogicalOperatorSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplayResult)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.historySearchControlBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historySearchControlBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSimpleSearch
            // 
            this.tbSimpleSearch.Controls.Add(this.tabSimpleSearch);
            this.tbSimpleSearch.Controls.Add(this.tabLogicalOperatorSearch);
            this.tbSimpleSearch.Location = new System.Drawing.Point(12, 12);
            this.tbSimpleSearch.Name = "tbSimpleSearch";
            this.tbSimpleSearch.SelectedIndex = 0;
            this.tbSimpleSearch.Size = new System.Drawing.Size(493, 246);
            this.tbSimpleSearch.TabIndex = 0;
            this.tbSimpleSearch.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabSimpleSearch
            // 
            this.tabSimpleSearch.BackColor = System.Drawing.Color.Transparent;
            this.tabSimpleSearch.Controls.Add(this.groupBox4);
            this.tabSimpleSearch.Controls.Add(this.groupBox3);
            this.tabSimpleSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSimpleSearch.Name = "tabSimpleSearch";
            this.tabSimpleSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimpleSearch.Size = new System.Drawing.Size(485, 220);
            this.tabSimpleSearch.TabIndex = 0;
            this.tabSimpleSearch.Text = "Simple Search";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pnSearchText);
            this.groupBox4.Controls.Add(this.pnSearchDateTime);
            this.groupBox4.Controls.Add(this.cbSearchType);
            this.groupBox4.Location = new System.Drawing.Point(6, 67);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(439, 65);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search Conditions";
            // 
            // pnSearchText
            // 
            this.pnSearchText.Controls.Add(this.tbSearchText);
            this.pnSearchText.Location = new System.Drawing.Point(133, 24);
            this.pnSearchText.Name = "pnSearchText";
            this.pnSearchText.Size = new System.Drawing.Size(278, 25);
            this.pnSearchText.TabIndex = 3;
            // 
            // tbSearchText
            // 
            this.tbSearchText.Location = new System.Drawing.Point(3, 2);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(272, 20);
            this.tbSearchText.TabIndex = 3;
            this.tbSearchText.TextChanged += new System.EventHandler(this.tbSearchText_TextChanged);
            // 
            // pnSearchDateTime
            // 
            this.pnSearchDateTime.Controls.Add(this.dtFrom);
            this.pnSearchDateTime.Controls.Add(this.lblTo);
            this.pnSearchDateTime.Controls.Add(this.dtTo);
            this.pnSearchDateTime.Controls.Add(this.lblFrom);
            this.pnSearchDateTime.Location = new System.Drawing.Point(133, 24);
            this.pnSearchDateTime.Name = "pnSearchDateTime";
            this.pnSearchDateTime.Size = new System.Drawing.Size(278, 25);
            this.pnSearchDateTime.TabIndex = 3;
            this.pnSearchDateTime.Visible = false;
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "dd-MM-yyyy";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(49, 3);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(91, 20);
            this.dtFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(155, 6);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 13);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "To:";
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "dd-MM-yyyy";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(184, 3);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(91, 20);
            this.dtTo.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(10, 6);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(33, 13);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From:";
            // 
            // cbSearchType
            // 
            this.cbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Items.AddRange(new object[] {
            "BOOK NAME",
            "AUTHOR",
            "PUSHLISH DATE",
            "BARCODE"});
            this.cbSearchType.Location = new System.Drawing.Point(6, 25);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(121, 21);
            this.cbSearchType.TabIndex = 0;
            this.cbSearchType.SelectedIndexChanged += new System.EventHandler(this.cbSearchType_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdScientific);
            this.groupBox3.Controls.Add(this.rdMathematics);
            this.groupBox3.Controls.Add(this.rdPhysical);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 55);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Book type";
            // 
            // rdScientific
            // 
            this.rdScientific.AutoSize = true;
            this.rdScientific.Location = new System.Drawing.Point(245, 19);
            this.rdScientific.Name = "rdScientific";
            this.rdScientific.Size = new System.Drawing.Size(68, 17);
            this.rdScientific.TabIndex = 0;
            this.rdScientific.Text = "Scientific";
            this.rdScientific.UseVisualStyleBackColor = true;
            // 
            // rdMathematics
            // 
            this.rdMathematics.AutoSize = true;
            this.rdMathematics.Checked = true;
            this.rdMathematics.Location = new System.Drawing.Point(84, 19);
            this.rdMathematics.Name = "rdMathematics";
            this.rdMathematics.Size = new System.Drawing.Size(85, 17);
            this.rdMathematics.TabIndex = 0;
            this.rdMathematics.TabStop = true;
            this.rdMathematics.Text = "Mathematics";
            this.rdMathematics.UseVisualStyleBackColor = true;
            // 
            // rdPhysical
            // 
            this.rdPhysical.AutoSize = true;
            this.rdPhysical.Location = new System.Drawing.Point(175, 19);
            this.rdPhysical.Name = "rdPhysical";
            this.rdPhysical.Size = new System.Drawing.Size(64, 17);
            this.rdPhysical.TabIndex = 0;
            this.rdPhysical.Text = "Physical";
            this.rdPhysical.UseVisualStyleBackColor = true;
            // 
            // tabLogicalOperatorSearch
            // 
            this.tabLogicalOperatorSearch.Controls.Add(this.btnExport);
            this.tabLogicalOperatorSearch.Controls.Add(this.flpSearchConditionCreatorPanel);
            this.tabLogicalOperatorSearch.Location = new System.Drawing.Point(4, 22);
            this.tabLogicalOperatorSearch.Name = "tabLogicalOperatorSearch";
            this.tabLogicalOperatorSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogicalOperatorSearch.Size = new System.Drawing.Size(485, 220);
            this.tabLogicalOperatorSearch.TabIndex = 1;
            this.tabLogicalOperatorSearch.Text = "Searching With Logical Operation";
            this.tabLogicalOperatorSearch.UseVisualStyleBackColor = true;
            // 
            // flpSearchConditionCreatorPanel
            // 
            this.flpSearchConditionCreatorPanel.AutoScroll = true;
            this.flpSearchConditionCreatorPanel.BackColor = System.Drawing.Color.Transparent;
            this.flpSearchConditionCreatorPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSearchConditionCreatorPanel.Location = new System.Drawing.Point(3, 3);
            this.flpSearchConditionCreatorPanel.Name = "flpSearchConditionCreatorPanel";
            this.flpSearchConditionCreatorPanel.Size = new System.Drawing.Size(479, 181);
            this.flpSearchConditionCreatorPanel.TabIndex = 0;
            this.flpSearchConditionCreatorPanel.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgDisplayResult);
            this.groupBox1.Location = new System.Drawing.Point(12, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 238);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display Result";
            // 
            // dgDisplayResult
            // 
            this.dgDisplayResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDisplayResult.Location = new System.Drawing.Point(6, 19);
            this.dgDisplayResult.Name = "dgDisplayResult";
            this.dgDisplayResult.Size = new System.Drawing.Size(759, 213);
            this.dgDisplayResult.TabIndex = 0;
            // 
            // pgbSearchProcess
            // 
            this.pgbSearchProcess.Location = new System.Drawing.Point(12, 508);
            this.pgbSearchProcess.Name = "pgbSearchProcess";
            this.pgbSearchProcess.Size = new System.Drawing.Size(271, 15);
            this.pgbSearchProcess.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(679, 505);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flpHistorySearch);
            this.groupBox2.Location = new System.Drawing.Point(505, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 246);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "History";
            // 
            // flpHistorySearch
            // 
            this.flpHistorySearch.AllowDrop = true;
            this.flpHistorySearch.AutoScroll = true;
            this.flpHistorySearch.BackColor = System.Drawing.Color.Transparent;
            this.flpHistorySearch.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpHistorySearch.Location = new System.Drawing.Point(6, 19);
            this.flpHistorySearch.Name = "flpHistorySearch";
            this.flpHistorySearch.Size = new System.Drawing.Size(266, 221);
            this.flpHistorySearch.TabIndex = 0;
            this.flpHistorySearch.WrapContents = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(421, 190);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(58, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Add";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn1.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // historySearchControlBindingSource
            // 
            this.historySearchControlBindingSource.DataSource = typeof(HistorySearchCondition.HistorySearchControl);
            // 
            // historySearchControlBindingSource1
            // 
            this.historySearchControlBindingSource1.DataSource = typeof(HistorySearchCondition.HistorySearchControl);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(786, 531);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pgbSearchProcess);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbSimpleSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Searching With Logical Operator";
            this.tbSimpleSearch.ResumeLayout(false);
            this.tabSimpleSearch.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.pnSearchText.ResumeLayout(false);
            this.pnSearchText.PerformLayout();
            this.pnSearchDateTime.ResumeLayout(false);
            this.pnSearchDateTime.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabLogicalOperatorSearch.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplayResult)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.historySearchControlBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historySearchControlBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbSimpleSearch;
        private System.Windows.Forms.TabPage tabSimpleSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgDisplayResult;
        private System.Windows.Forms.ProgressBar pgbSearchProcess;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdScientific;
        private System.Windows.Forms.RadioButton rdMathematics;
        private System.Windows.Forms.RadioButton rdPhysical;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.Panel pnSearchDateTime;
        private System.Windows.Forms.Panel pnSearchText;
        private System.Windows.Forms.BindingSource historySearchControlBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.FlowLayoutPanel flpHistorySearch;
        private System.Windows.Forms.BindingSource historySearchControlBindingSource1;
        private System.Windows.Forms.TabPage tabLogicalOperatorSearch;
        private System.Windows.Forms.FlowLayoutPanel flpSearchConditionCreatorPanel;
    }
}

