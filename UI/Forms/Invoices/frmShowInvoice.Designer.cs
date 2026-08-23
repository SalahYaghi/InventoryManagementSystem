namespace UI.Forms.Invoices
{ 
        partial class frmShowInvoice
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Panel panelBody;
            private System.Windows.Forms.Panel panelFooter;

            private System.Windows.Forms.Label lblInvoiceTitle;
            private System.Windows.Forms.Label lblInvoiceSubtitle;
            private System.Windows.Forms.Label lblInvoiceTypeBadge;
            private System.Windows.Forms.Label lblInvoiceStatusBadge;

            private System.Windows.Forms.FlowLayoutPanel flowBody;
            private System.Windows.Forms.Panel pnlLineItems;
            private System.Windows.Forms.Panel pnlTotals;

            private System.Windows.Forms.Label lblLineItemsTitle;
            private System.Windows.Forms.FlowLayoutPanel flowLineItems;

            private System.Windows.Forms.Label lblTotalsTitle;
            private System.Windows.Forms.Label lblItemsCountCaption;
            private System.Windows.Forms.Label lblItemsCountValue;
            private System.Windows.Forms.Label lblTotalQuantityCaption;
            private System.Windows.Forms.Label lblTotalQuantityValue;

            private System.Windows.Forms.Label lblSubTotalCaption;
            private System.Windows.Forms.Label lblSubTotalValue;
            private System.Windows.Forms.Label lblTaxCaption;
            private System.Windows.Forms.Label lblTaxValue;
            private System.Windows.Forms.Label lblDiscountCaption;
            private System.Windows.Forms.Label lblDiscountValue;
            private System.Windows.Forms.Label lblNetCaption;
            private System.Windows.Forms.Label lblNetValue;

            private System.Windows.Forms.Label lblStatus;
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
            this.lblPartieCaption = new System.Windows.Forms.Label();
            this.lblPartieValue = new System.Windows.Forms.Label();
            this.lblDueDateCaption = new System.Windows.Forms.Label();
            this.lblDueDateValue = new System.Windows.Forms.Label();
            this.pnlSourceCard = new System.Windows.Forms.Panel();
            this.lblSourceWarehouseCaption = new System.Windows.Forms.Label();
            this.lblSourceWarehouseValue = new System.Windows.Forms.Label();
            this.pnlLineItems = new System.Windows.Forms.Panel();
            this.flowLineItems = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLineItemsTitle = new System.Windows.Forms.Label();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblTotalsTitle = new System.Windows.Forms.Label();
            this.lblItemsCountCaption = new System.Windows.Forms.Label();
            this.lblItemsCountValue = new System.Windows.Forms.Label();
            this.lblTotalQuantityCaption = new System.Windows.Forms.Label();
            this.lblTotalQuantityValue = new System.Windows.Forms.Label();
            this.lblSubTotalCaption = new System.Windows.Forms.Label();
            this.lblSubTotalValue = new System.Windows.Forms.Label();
            this.lblTaxCaption = new System.Windows.Forms.Label();
            this.lblTaxValue = new System.Windows.Forms.Label();
            this.lblDiscountCaption = new System.Windows.Forms.Label();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.lblNetCaption = new System.Windows.Forms.Label();
            this.lblNetValue = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblInvoiceTitle = new System.Windows.Forms.Label();
            this.lblInvoiceSubtitle = new System.Windows.Forms.Label();
            this.lblInvoiceTypeBadge = new System.Windows.Forms.Label();
            this.lblInvoiceStatusBadge = new System.Windows.Forms.Label();
            this.btnDownloadAsPdf = new System.Windows.Forms.Button();
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.flowBody.SuspendLayout();
            this.pnlOverview.SuspendLayout();
            this.pnlSourceCard.SuspendLayout();
            this.pnlLineItems.SuspendLayout();
            this.pnlTotals.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(980, 760);
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
            this.panelBody.Size = new System.Drawing.Size(980, 560);
            this.panelBody.TabIndex = 0;
            // 
            // flowBody
            // 
            this.flowBody.AutoScroll = true;
            this.flowBody.AutoSize = true;
            this.flowBody.Controls.Add(this.pnlOverview);
            this.flowBody.Controls.Add(this.pnlSourceCard);
            this.flowBody.Controls.Add(this.pnlLineItems);
            this.flowBody.Controls.Add(this.pnlTotals);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBody.Location = new System.Drawing.Point(24, 20);
            this.flowBody.Name = "flowBody";
            this.flowBody.Size = new System.Drawing.Size(932, 520);
            this.flowBody.TabIndex = 0;
            this.flowBody.WrapContents = false;
            // 
            // pnlOverview
            // 
            this.pnlOverview.BackColor = System.Drawing.Color.White;
            this.pnlOverview.Controls.Add(this.lblOverviewTitle);
            this.pnlOverview.Controls.Add(this.lblPartieCaption);
            this.pnlOverview.Controls.Add(this.lblPartieValue);
            this.pnlOverview.Controls.Add(this.lblDueDateCaption);
            this.pnlOverview.Controls.Add(this.lblDueDateValue);
            this.pnlOverview.Location = new System.Drawing.Point(0, 0);
            this.pnlOverview.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlOverview.Name = "pnlOverview";
            this.pnlOverview.Size = new System.Drawing.Size(898, 145);
            this.pnlOverview.TabIndex = 3;
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
            // lblPartieCaption
            // 
            this.lblPartieCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPartieCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblPartieCaption.Location = new System.Drawing.Point(24, 62);
            this.lblPartieCaption.Name = "lblPartieCaption";
            this.lblPartieCaption.Size = new System.Drawing.Size(160, 22);
            this.lblPartieCaption.TabIndex = 1;
            this.lblPartieCaption.Text = "Supplier";
            // 
            // lblPartieValue
            // 
            this.lblPartieValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblPartieValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPartieValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblPartieValue.Location = new System.Drawing.Point(24, 88);
            this.lblPartieValue.Name = "lblPartieValue";
            this.lblPartieValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblPartieValue.Size = new System.Drawing.Size(395, 32);
            this.lblPartieValue.TabIndex = 2;
            this.lblPartieValue.Text = "-";
            this.lblPartieValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDueDateCaption
            // 
            this.lblDueDateCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDueDateCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblDueDateCaption.Location = new System.Drawing.Point(449, 62);
            this.lblDueDateCaption.Name = "lblDueDateCaption";
            this.lblDueDateCaption.Size = new System.Drawing.Size(160, 22);
            this.lblDueDateCaption.TabIndex = 5;
            this.lblDueDateCaption.Text = "Due Date";
            // 
            // lblDueDateValue
            // 
            this.lblDueDateValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblDueDateValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDueDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblDueDateValue.Location = new System.Drawing.Point(449, 88);
            this.lblDueDateValue.Name = "lblDueDateValue";
            this.lblDueDateValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblDueDateValue.Size = new System.Drawing.Size(291, 32);
            this.lblDueDateValue.TabIndex = 6;
            this.lblDueDateValue.Text = "-";
            this.lblDueDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSourceCard
            // 
            this.pnlSourceCard.BackColor = System.Drawing.Color.White;
            this.pnlSourceCard.Controls.Add(this.lblSourceWarehouseCaption);
            this.pnlSourceCard.Controls.Add(this.lblSourceWarehouseValue);
            this.pnlSourceCard.Location = new System.Drawing.Point(3, 162);
            this.pnlSourceCard.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.pnlSourceCard.Name = "pnlSourceCard";
            this.pnlSourceCard.Size = new System.Drawing.Size(895, 80);
            this.pnlSourceCard.TabIndex = 4;
            // 
            // lblSourceWarehouseCaption
            // 
            this.lblSourceWarehouseCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSourceWarehouseCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblSourceWarehouseCaption.Location = new System.Drawing.Point(18, 14);
            this.lblSourceWarehouseCaption.Name = "lblSourceWarehouseCaption";
            this.lblSourceWarehouseCaption.Size = new System.Drawing.Size(180, 22);
            this.lblSourceWarehouseCaption.TabIndex = 0;
            this.lblSourceWarehouseCaption.Text = "Source Warehouse";
            // 
            // lblSourceWarehouseValue
            // 
            this.lblSourceWarehouseValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSourceWarehouseValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblSourceWarehouseValue.Location = new System.Drawing.Point(18, 38);
            this.lblSourceWarehouseValue.Name = "lblSourceWarehouseValue";
            this.lblSourceWarehouseValue.Size = new System.Drawing.Size(390, 28);
            this.lblSourceWarehouseValue.TabIndex = 1;
            this.lblSourceWarehouseValue.Text = "-";
            // 
            // pnlLineItems
            // 
            this.pnlLineItems.BackColor = System.Drawing.Color.White;
            this.pnlLineItems.Controls.Add(this.flowLineItems);
            this.pnlLineItems.Controls.Add(this.lblLineItemsTitle);
            this.pnlLineItems.Location = new System.Drawing.Point(0, 262);
            this.pnlLineItems.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlLineItems.Name = "pnlLineItems";
            this.pnlLineItems.Size = new System.Drawing.Size(898, 275);
            this.pnlLineItems.TabIndex = 1;
            // 
            // flowLineItems
            // 
            this.flowLineItems.AutoScroll = true;
            this.flowLineItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLineItems.Location = new System.Drawing.Point(24, 62);
            this.flowLineItems.Name = "flowLineItems";
            this.flowLineItems.Size = new System.Drawing.Size(860, 188);
            this.flowLineItems.TabIndex = 0;
            this.flowLineItems.WrapContents = false;
            // 
            // lblLineItemsTitle
            // 
            this.lblLineItemsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblLineItemsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblLineItemsTitle.Location = new System.Drawing.Point(22, 18);
            this.lblLineItemsTitle.Name = "lblLineItemsTitle";
            this.lblLineItemsTitle.Size = new System.Drawing.Size(300, 30);
            this.lblLineItemsTitle.TabIndex = 1;
            this.lblLineItemsTitle.Text = "Line Items";
            // 
            // pnlTotals
            // 
            this.pnlTotals.BackColor = System.Drawing.Color.White;
            this.pnlTotals.Controls.Add(this.lblTotalsTitle);
            this.pnlTotals.Controls.Add(this.lblItemsCountCaption);
            this.pnlTotals.Controls.Add(this.lblItemsCountValue);
            this.pnlTotals.Controls.Add(this.lblTotalQuantityCaption);
            this.pnlTotals.Controls.Add(this.lblTotalQuantityValue);
            this.pnlTotals.Controls.Add(this.lblSubTotalCaption);
            this.pnlTotals.Controls.Add(this.lblSubTotalValue);
            this.pnlTotals.Controls.Add(this.lblTaxCaption);
            this.pnlTotals.Controls.Add(this.lblTaxValue);
            this.pnlTotals.Controls.Add(this.lblDiscountCaption);
            this.pnlTotals.Controls.Add(this.lblDiscountValue);
            this.pnlTotals.Controls.Add(this.lblNetCaption);
            this.pnlTotals.Controls.Add(this.lblNetValue);
            this.pnlTotals.Location = new System.Drawing.Point(0, 551);
            this.pnlTotals.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(898, 220);
            this.pnlTotals.TabIndex = 2;
            // 
            // lblTotalsTitle
            // 
            this.lblTotalsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotalsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTotalsTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTotalsTitle.Name = "lblTotalsTitle";
            this.lblTotalsTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTotalsTitle.TabIndex = 0;
            this.lblTotalsTitle.Text = "Invoice Summary";
            // 
            // lblItemsCountCaption
            // 
            this.lblItemsCountCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblItemsCountCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblItemsCountCaption.Location = new System.Drawing.Point(26, 72);
            this.lblItemsCountCaption.Name = "lblItemsCountCaption";
            this.lblItemsCountCaption.Size = new System.Drawing.Size(140, 22);
            this.lblItemsCountCaption.TabIndex = 1;
            this.lblItemsCountCaption.Text = "Items Count";
            // 
            // lblItemsCountValue
            // 
            this.lblItemsCountValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblItemsCountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblItemsCountValue.Location = new System.Drawing.Point(26, 98);
            this.lblItemsCountValue.Name = "lblItemsCountValue";
            this.lblItemsCountValue.Size = new System.Drawing.Size(140, 45);
            this.lblItemsCountValue.TabIndex = 2;
            this.lblItemsCountValue.Text = "0";
            // 
            // lblTotalQuantityCaption
            // 
            this.lblTotalQuantityCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalQuantityCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalQuantityCaption.Location = new System.Drawing.Point(220, 72);
            this.lblTotalQuantityCaption.Name = "lblTotalQuantityCaption";
            this.lblTotalQuantityCaption.Size = new System.Drawing.Size(160, 22);
            this.lblTotalQuantityCaption.TabIndex = 3;
            this.lblTotalQuantityCaption.Text = "Total Quantity";
            // 
            // lblTotalQuantityValue
            // 
            this.lblTotalQuantityValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalQuantityValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblTotalQuantityValue.Location = new System.Drawing.Point(220, 98);
            this.lblTotalQuantityValue.Name = "lblTotalQuantityValue";
            this.lblTotalQuantityValue.Size = new System.Drawing.Size(160, 45);
            this.lblTotalQuantityValue.TabIndex = 4;
            this.lblTotalQuantityValue.Text = "0";
            // 
            // lblSubTotalCaption
            // 
            this.lblSubTotalCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubTotalCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTotalCaption.Location = new System.Drawing.Point(610, 55);
            this.lblSubTotalCaption.Name = "lblSubTotalCaption";
            this.lblSubTotalCaption.Size = new System.Drawing.Size(120, 25);
            this.lblSubTotalCaption.TabIndex = 5;
            this.lblSubTotalCaption.Text = "Sub Total";
            // 
            // lblSubTotalValue
            // 
            this.lblSubTotalValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblSubTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblSubTotalValue.Location = new System.Drawing.Point(735, 55);
            this.lblSubTotalValue.Name = "lblSubTotalValue";
            this.lblSubTotalValue.Size = new System.Drawing.Size(135, 28);
            this.lblSubTotalValue.TabIndex = 6;
            this.lblSubTotalValue.Text = "0.00";
            this.lblSubTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTaxCaption
            // 
            this.lblTaxCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTaxCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblTaxCaption.Location = new System.Drawing.Point(610, 90);
            this.lblTaxCaption.Name = "lblTaxCaption";
            this.lblTaxCaption.Size = new System.Drawing.Size(120, 25);
            this.lblTaxCaption.TabIndex = 7;
            this.lblTaxCaption.Text = "Tax";
            // 
            // lblTaxValue
            // 
            this.lblTaxValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTaxValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTaxValue.Location = new System.Drawing.Point(735, 90);
            this.lblTaxValue.Name = "lblTaxValue";
            this.lblTaxValue.Size = new System.Drawing.Size(135, 28);
            this.lblTaxValue.TabIndex = 8;
            this.lblTaxValue.Text = "0.00";
            this.lblTaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountCaption
            // 
            this.lblDiscountCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscountCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblDiscountCaption.Location = new System.Drawing.Point(610, 125);
            this.lblDiscountCaption.Name = "lblDiscountCaption";
            this.lblDiscountCaption.Size = new System.Drawing.Size(120, 25);
            this.lblDiscountCaption.TabIndex = 9;
            this.lblDiscountCaption.Text = "Discount";
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblDiscountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblDiscountValue.Location = new System.Drawing.Point(735, 125);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(135, 28);
            this.lblDiscountValue.TabIndex = 10;
            this.lblDiscountValue.Text = "0.00";
            this.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNetCaption
            // 
            this.lblNetCaption.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblNetCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblNetCaption.Location = new System.Drawing.Point(610, 165);
            this.lblNetCaption.Name = "lblNetCaption";
            this.lblNetCaption.Size = new System.Drawing.Size(120, 35);
            this.lblNetCaption.TabIndex = 11;
            this.lblNetCaption.Text = "Net";
            // 
            // lblNetValue
            // 
            this.lblNetValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblNetValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblNetValue.Location = new System.Drawing.Point(735, 160);
            this.lblNetValue.Name = "lblNetValue";
            this.lblNetValue.Size = new System.Drawing.Size(135, 45);
            this.lblNetValue.TabIndex = 12;
            this.lblNetValue.Text = "0.00";
            this.lblNetValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnDownloadAsPdf);
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnRefresh);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 680);
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
            this.lblStatus.Size = new System.Drawing.Size(420, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(745, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 40);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(860, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblInvoiceTitle);
            this.panelHeader.Controls.Add(this.lblInvoiceSubtitle);
            this.panelHeader.Controls.Add(this.lblInvoiceTypeBadge);
            this.panelHeader.Controls.Add(this.lblInvoiceStatusBadge);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(980, 120);
            this.panelHeader.TabIndex = 2;
            // 
            // lblInvoiceTitle
            // 
            this.lblInvoiceTitle.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblInvoiceTitle.Location = new System.Drawing.Point(24, 18);
            this.lblInvoiceTitle.Name = "lblInvoiceTitle";
            this.lblInvoiceTitle.Size = new System.Drawing.Size(500, 52);
            this.lblInvoiceTitle.TabIndex = 0;
            this.lblInvoiceTitle.Text = "INVOICE";
            // 
            // lblInvoiceSubtitle
            // 
            this.lblInvoiceSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInvoiceSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblInvoiceSubtitle.Location = new System.Drawing.Point(28, 76);
            this.lblInvoiceSubtitle.Name = "lblInvoiceSubtitle";
            this.lblInvoiceSubtitle.Size = new System.Drawing.Size(620, 25);
            this.lblInvoiceSubtitle.TabIndex = 1;
            this.lblInvoiceSubtitle.Text = "Professional invoice preview for the selected order.";
            // 
            // lblInvoiceTypeBadge
            // 
            this.lblInvoiceTypeBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceTypeBadge.Location = new System.Drawing.Point(700, 28);
            this.lblInvoiceTypeBadge.Name = "lblInvoiceTypeBadge";
            this.lblInvoiceTypeBadge.Size = new System.Drawing.Size(120, 34);
            this.lblInvoiceTypeBadge.TabIndex = 2;
            this.lblInvoiceTypeBadge.Text = "Type";
            this.lblInvoiceTypeBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInvoiceStatusBadge
            // 
            this.lblInvoiceStatusBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceStatusBadge.Location = new System.Drawing.Point(835, 28);
            this.lblInvoiceStatusBadge.Name = "lblInvoiceStatusBadge";
            this.lblInvoiceStatusBadge.Size = new System.Drawing.Size(115, 34);
            this.lblInvoiceStatusBadge.TabIndex = 3;
            this.lblInvoiceStatusBadge.Text = "Status";
            this.lblInvoiceStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDownloadAsPdf
            // 
            this.btnDownloadAsPdf.Location = new System.Drawing.Point(570, 20);
            this.btnDownloadAsPdf.Name = "btnDownloadAsPdf";
            this.btnDownloadAsPdf.Size = new System.Drawing.Size(160, 40);
            this.btnDownloadAsPdf.TabIndex = 3;
            this.btnDownloadAsPdf.Text = "Download As Pdf";
            this.btnDownloadAsPdf.UseVisualStyleBackColor = false;
            this.btnDownloadAsPdf.Click += new System.EventHandler(this.btnDownloadAsPdf_Click);
            // 
            // frmShowInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 760);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmShowInvoice";
            this.Text = "Invoice Preview";
            this.Load += new System.EventHandler(this.frmShowInvoice_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowBody.ResumeLayout(false);
            this.pnlOverview.ResumeLayout(false);
            this.pnlSourceCard.ResumeLayout(false);
            this.pnlLineItems.ResumeLayout(false);
            this.pnlTotals.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

    }

        private System.Windows.Forms.Panel pnlSourceCard;
        private System.Windows.Forms.Label lblSourceWarehouseCaption;
        private System.Windows.Forms.Label lblSourceWarehouseValue;
        private System.Windows.Forms.Panel pnlOverview;
        private System.Windows.Forms.Label lblOverviewTitle;
        private System.Windows.Forms.Label lblPartieCaption;
        private System.Windows.Forms.Label lblPartieValue;
        private System.Windows.Forms.Label lblDueDateCaption;
        private System.Windows.Forms.Label lblDueDateValue;
        private System.Windows.Forms.Button btnDownloadAsPdf;
    }
}
