namespace HistorySearchCondition
{
    partial class HistorySearchControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistorySearchControl));
            this.btnDelete = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSearchContent = new System.Windows.Forms.Label();
            this.lblHistoryId = new System.Windows.Forms.Label();
            this.lblSearchTarget = new System.Windows.Forms.Label();
            this.lblSearchType = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.InitialImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.InitialImage")));
            this.btnDelete.Location = new System.Drawing.Point(203, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(25, 25);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Search Target:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Search Type:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "History ID:";
            // 
            // lblSearchContent
            // 
            this.lblSearchContent.AutoSize = true;
            this.lblSearchContent.Location = new System.Drawing.Point(94, 64);
            this.lblSearchContent.Name = "lblSearchContent";
            this.lblSearchContent.Size = new System.Drawing.Size(136, 13);
            this.lblSearchContent.TabIndex = 16;
            this.lblSearchContent.Text = "2017/03/20 ~ 2017/03/21";
            // 
            // lblHistoryId
            // 
            this.lblHistoryId.AutoSize = true;
            this.lblHistoryId.Location = new System.Drawing.Point(94, 8);
            this.lblHistoryId.Name = "lblHistoryId";
            this.lblHistoryId.Size = new System.Drawing.Size(13, 13);
            this.lblHistoryId.TabIndex = 13;
            this.lblHistoryId.Text = "1";
            // 
            // lblSearchTarget
            // 
            this.lblSearchTarget.AutoSize = true;
            this.lblSearchTarget.Location = new System.Drawing.Point(94, 45);
            this.lblSearchTarget.Name = "lblSearchTarget";
            this.lblSearchTarget.Size = new System.Drawing.Size(46, 13);
            this.lblSearchTarget.TabIndex = 14;
            this.lblSearchTarget.Text = "Physical";
            // 
            // lblSearchType
            // 
            this.lblSearchType.AutoSize = true;
            this.lblSearchType.Location = new System.Drawing.Point(94, 26);
            this.lblSearchType.Name = "lblSearchType";
            this.lblSearchType.Size = new System.Drawing.Size(53, 13);
            this.lblSearchType.TabIndex = 15;
            this.lblSearchType.Text = "AUTHOR";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Search Content:";
            // 
            // HistorySearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblSearchContent);
            this.Controls.Add(this.lblHistoryId);
            this.Controls.Add(this.lblSearchTarget);
            this.Controls.Add(this.lblSearchType);
            this.Controls.Add(this.label7);
            this.Name = "HistorySearchControl";
            this.Size = new System.Drawing.Size(235, 82);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HistorySearchControl_Paint_1);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HistorySearchControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HistorySearchControl_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnDelete;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSearchContent;
        private System.Windows.Forms.Label lblHistoryId;
        private System.Windows.Forms.Label lblSearchTarget;
        private System.Windows.Forms.Label lblSearchType;
        private System.Windows.Forms.Label label7;
    }
}
