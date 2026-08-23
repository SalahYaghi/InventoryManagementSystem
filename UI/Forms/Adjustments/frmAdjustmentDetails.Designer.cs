namespace UI.Forms.Adjustments
{
    partial class frmAdjustmentDetails
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelFooter;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTypeBadge;
        private System.Windows.Forms.Label lblStatusBadge;

        private System.Windows.Forms.FlowLayoutPanel flowBody;

        private System.Windows.Forms.Panel pnlOverview;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Panel pnlNotes;

        private System.Windows.Forms.Label lblOverviewTitle;
        private System.Windows.Forms.Label lblWarehouseCaption;
        private System.Windows.Forms.Label lblWarehouseValue;
        private System.Windows.Forms.Label lblReasonCaption;
        private System.Windows.Forms.Label lblReasonValue;
        private System.Windows.Forms.Label lblItemsCountCaption;
        private System.Windows.Forms.Label lblItemsCountValue;
        private System.Windows.Forms.Label lblTotalQuantityCaption;
        private System.Windows.Forms.Label lblTotalQuantityValue;

        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.FlowLayoutPanel flowDetails;

        private System.Windows.Forms.Label lblNotesTitle;
        private System.Windows.Forms.Label lblNotesValue;

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelBody = new System.Windows.Forms.Panel();
            this.flowBody = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlOverview = new System.Windows.Forms.Panel();
            this.lblOverviewTitle = new System.Windows.Forms.Label();
            this.lblWarehouseCaption = new System.Windows.Forms.Label();
            this.lblWarehouseValue = new System.Windows.Forms.Label();
            this.lblReasonCaption = new System.Windows.Forms.Label();
            this.lblReasonValue = new System.Windows.Forms.Label();
            this.lblItemsCountCaption = new System.Windows.Forms.Label();
            this.lblItemsCountValue = new System.Windows.Forms.Label();
            this.lblTotalQuantityCaption = new System.Windows.Forms.Label();
            this.lblTotalQuantityValue = new System.Windows.Forms.Label();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.flowDetails = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.pnlNotes = new System.Windows.Forms.Panel();
            this.lblNotesTitle = new System.Windows.Forms.Label();
            this.lblNotesValue = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTypeBadge = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.flowBody.SuspendLayout();
            this.pnlOverview.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlNotes.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelRoot.Controls.Add(this.panelBody);
            this.panelRoot.Controls.Add(this.panelFooter);
            this.panelRoot.Controls.Add(this.panelHeader);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Size = new System.Drawing.Size(980, 687);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.flowBody);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 120);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.panelBody.Size = new System.Drawing.Size(980, 487);
            this.panelBody.TabIndex = 0;
            // 
            // flowBody
            // 
            this.flowBody.AutoSize = true;
            this.flowBody.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowBody.Controls.Add(this.pnlOverview);
            this.flowBody.Controls.Add(this.pnlDetails);
            this.flowBody.Controls.Add(this.pnlNotes);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBody.Location = new System.Drawing.Point(24, 20);
            this.flowBody.Name = "flowBody";
            this.flowBody.Size = new System.Drawing.Size(932, 427);
            this.flowBody.TabIndex = 0;
            this.flowBody.WrapContents = false;
            // 
            // pnlOverview
            // 
            this.pnlOverview.BackColor = System.Drawing.Color.White;
            this.pnlOverview.Controls.Add(this.lblOverviewTitle);
            this.pnlOverview.Controls.Add(this.lblWarehouseCaption);
            this.pnlOverview.Controls.Add(this.lblWarehouseValue);
            this.pnlOverview.Controls.Add(this.lblReasonCaption);
            this.pnlOverview.Controls.Add(this.lblReasonValue);
            this.pnlOverview.Controls.Add(this.lblItemsCountCaption);
            this.pnlOverview.Controls.Add(this.lblItemsCountValue);
            this.pnlOverview.Controls.Add(this.lblTotalQuantityCaption);
            this.pnlOverview.Controls.Add(this.lblTotalQuantityValue);
            this.pnlOverview.Location = new System.Drawing.Point(0, 0);
            this.pnlOverview.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlOverview.Name = "pnlOverview";
            this.pnlOverview.Size = new System.Drawing.Size(910, 185);
            this.pnlOverview.TabIndex = 0;
            // 
            // lblOverviewTitle
            // 
            this.lblOverviewTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblOverviewTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblOverviewTitle.Location = new System.Drawing.Point(22, 18);
            this.lblOverviewTitle.Name = "lblOverviewTitle";
            this.lblOverviewTitle.Size = new System.Drawing.Size(250, 30);
            this.lblOverviewTitle.TabIndex = 0;
            this.lblOverviewTitle.Text = "Overview";
            // 
            // lblWarehouseCaption
            // 
            this.lblWarehouseCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehouseCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblWarehouseCaption.Location = new System.Drawing.Point(24, 62);
            this.lblWarehouseCaption.Name = "lblWarehouseCaption";
            this.lblWarehouseCaption.Size = new System.Drawing.Size(160, 22);
            this.lblWarehouseCaption.TabIndex = 1;
            this.lblWarehouseCaption.Text = "Warehouse";
            // 
            // lblWarehouseValue
            // 
            this.lblWarehouseValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblWarehouseValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWarehouseValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblWarehouseValue.Location = new System.Drawing.Point(24, 88);
            this.lblWarehouseValue.Name = "lblWarehouseValue";
            this.lblWarehouseValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblWarehouseValue.Size = new System.Drawing.Size(395, 36);
            this.lblWarehouseValue.TabIndex = 2;
            this.lblWarehouseValue.Text = "-";
            this.lblWarehouseValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReasonCaption
            // 
            this.lblReasonCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReasonCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblReasonCaption.Location = new System.Drawing.Point(445, 62);
            this.lblReasonCaption.Name = "lblReasonCaption";
            this.lblReasonCaption.Size = new System.Drawing.Size(160, 22);
            this.lblReasonCaption.TabIndex = 3;
            this.lblReasonCaption.Text = "Reason";
            // 
            // lblReasonValue
            // 
            this.lblReasonValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblReasonValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReasonValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblReasonValue.Location = new System.Drawing.Point(445, 88);
            this.lblReasonValue.Name = "lblReasonValue";
            this.lblReasonValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblReasonValue.Size = new System.Drawing.Size(230, 36);
            this.lblReasonValue.TabIndex = 4;
            this.lblReasonValue.Text = "-";
            this.lblReasonValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblItemsCountCaption
            // 
            this.lblItemsCountCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblItemsCountCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblItemsCountCaption.Location = new System.Drawing.Point(24, 138);
            this.lblItemsCountCaption.Name = "lblItemsCountCaption";
            this.lblItemsCountCaption.Size = new System.Drawing.Size(130, 22);
            this.lblItemsCountCaption.TabIndex = 5;
            this.lblItemsCountCaption.Text = "Items Count";
            // 
            // lblItemsCountValue
            // 
            this.lblItemsCountValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblItemsCountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblItemsCountValue.Location = new System.Drawing.Point(160, 128);
            this.lblItemsCountValue.Name = "lblItemsCountValue";
            this.lblItemsCountValue.Size = new System.Drawing.Size(100, 45);
            this.lblItemsCountValue.TabIndex = 6;
            this.lblItemsCountValue.Text = "0";
            // 
            // lblTotalQuantityCaption
            // 
            this.lblTotalQuantityCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalQuantityCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalQuantityCaption.Location = new System.Drawing.Point(310, 138);
            this.lblTotalQuantityCaption.Name = "lblTotalQuantityCaption";
            this.lblTotalQuantityCaption.Size = new System.Drawing.Size(150, 22);
            this.lblTotalQuantityCaption.TabIndex = 7;
            this.lblTotalQuantityCaption.Text = "Total Quantity";
            // 
            // lblTotalQuantityValue
            // 
            this.lblTotalQuantityValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalQuantityValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblTotalQuantityValue.Location = new System.Drawing.Point(465, 128);
            this.lblTotalQuantityValue.Name = "lblTotalQuantityValue";
            this.lblTotalQuantityValue.Size = new System.Drawing.Size(160, 45);
            this.lblTotalQuantityValue.TabIndex = 8;
            this.lblTotalQuantityValue.Text = "0";
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.flowDetails);
            this.pnlDetails.Controls.Add(this.lblDetailsTitle);
            this.pnlDetails.Location = new System.Drawing.Point(0, 199);
            this.pnlDetails.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(910, 80);
            this.pnlDetails.TabIndex = 1;
            // 
            // flowDetails
            // 
            this.flowDetails.AutoScroll = true;
            this.flowDetails.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowDetails.Location = new System.Drawing.Point(24, 60);
            this.flowDetails.Name = "flowDetails";
            this.flowDetails.Size = new System.Drawing.Size(860, 265);
            this.flowDetails.TabIndex = 0;
            this.flowDetails.WrapContents = false;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblDetailsTitle.Location = new System.Drawing.Point(22, 18);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(300, 30);
            this.lblDetailsTitle.TabIndex = 1;
            this.lblDetailsTitle.Text = "Adjusted Products";
            // 
            // pnlNotes
            // 
            this.pnlNotes.BackColor = System.Drawing.Color.White;
            this.pnlNotes.Controls.Add(this.lblNotesTitle);
            this.pnlNotes.Controls.Add(this.lblNotesValue);
            this.pnlNotes.Location = new System.Drawing.Point(0, 293);
            this.pnlNotes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlNotes.Name = "pnlNotes";
            this.pnlNotes.Size = new System.Drawing.Size(910, 120);
            this.pnlNotes.TabIndex = 2;
            // 
            // lblNotesTitle
            // 
            this.lblNotesTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNotesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblNotesTitle.Location = new System.Drawing.Point(22, 18);
            this.lblNotesTitle.Name = "lblNotesTitle";
            this.lblNotesTitle.Size = new System.Drawing.Size(300, 30);
            this.lblNotesTitle.TabIndex = 0;
            this.lblNotesTitle.Text = "Notes";
            // 
            // lblNotesValue
            // 
            this.lblNotesValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblNotesValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNotesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblNotesValue.Location = new System.Drawing.Point(24, 58);
            this.lblNotesValue.Name = "lblNotesValue";
            this.lblNotesValue.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblNotesValue.Size = new System.Drawing.Size(860, 40);
            this.lblNotesValue.TabIndex = 1;
            this.lblNotesValue.Text = "-";
            this.lblNotesValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnEdit);
            this.panelFooter.Controls.Add(this.btnRefresh);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 607);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(980, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(24, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(380, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(630, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(105, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(745, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 40);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(860, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTypeBadge);
            this.panelHeader.Controls.Add(this.lblStatusBadge);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(980, 120);
            this.panelHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(560, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Adjustment Details";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(28, 72);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(700, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Adjustment ID:";
            // 
            // lblTypeBadge
            // 
            this.lblTypeBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTypeBadge.Location = new System.Drawing.Point(730, 25);
            this.lblTypeBadge.Name = "lblTypeBadge";
            this.lblTypeBadge.Size = new System.Drawing.Size(100, 32);
            this.lblTypeBadge.TabIndex = 2;
            this.lblTypeBadge.Text = "Type";
            this.lblTypeBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.Location = new System.Drawing.Point(842, 25);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(110, 32);
            this.lblStatusBadge.TabIndex = 3;
            this.lblStatusBadge.Text = "Status";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAdjustmentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 687);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmAdjustmentDetails";
            this.Text = "Adjustment Details";
            this.Load += new System.EventHandler(this.frmAdjustmentDetails_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowBody.ResumeLayout(false);
            this.pnlOverview.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlNotes.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

