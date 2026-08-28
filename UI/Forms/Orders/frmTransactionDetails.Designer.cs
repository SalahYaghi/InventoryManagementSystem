namespace UI.Forms.Orders
{
        partial class frmTransactionDetails
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Panel panelBody;
            private System.Windows.Forms.Panel panelFooter;

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblTypeBadge;
            private System.Windows.Forms.Label lblStatusBadge;

            private System.Windows.Forms.FlowLayoutPanel flowBody;

            private System.Windows.Forms.Panel pnlOverview;
            private System.Windows.Forms.Panel pnlPartyWarehouse;
            private System.Windows.Forms.Panel pnlDetails;
            private System.Windows.Forms.Panel pnlSummary;
            private System.Windows.Forms.Panel pnlNotes;

            private System.Windows.Forms.Label lblOverviewTitle;
            private System.Windows.Forms.Label lblDueDateCaption;
            private System.Windows.Forms.Label lblDueDateValue;
            private System.Windows.Forms.Panel pnlSourceCard;
            private System.Windows.Forms.Label lblSourceWarehouseCaption;
            private System.Windows.Forms.Label lblSourceWarehouseValue;

            private System.Windows.Forms.Label lblDetailsTitle;
            private System.Windows.Forms.FlowLayoutPanel flowDetails;

            private System.Windows.Forms.Label lblSummaryTitle;
            private System.Windows.Forms.Label lblSubTotalCaption;
            private System.Windows.Forms.Label lblSubTotalValue;
            private System.Windows.Forms.Label lblDiscountCaption;
            private System.Windows.Forms.Label lblDiscountValue;
            private System.Windows.Forms.Label lblNetCaption;
            private System.Windows.Forms.Label lblNetValue;

            private System.Windows.Forms.Label lblItemsCountCaption;
            private System.Windows.Forms.Label lblItemsCountValue;
            private System.Windows.Forms.Label lblTotalQuantityCaption;
            private System.Windows.Forms.Label lblTotalQuantityValue;
            private System.Windows.Forms.Label lblActualQuantityCaption;
            private System.Windows.Forms.Label lblActualQuantityValue;

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
            this.lblPartieCaption = new System.Windows.Forms.Label();
            this.lblPartieValue = new System.Windows.Forms.Label();
            this.lblDueDateCaption = new System.Windows.Forms.Label();
            this.lblDueDateValue = new System.Windows.Forms.Label();
            this.pnlPartyWarehouse = new System.Windows.Forms.Panel();
            this.pnlSourceCard = new System.Windows.Forms.Panel();
            this.lblSourceWarehouseCaption = new System.Windows.Forms.Label();
            this.lblSourceWarehouseValue = new System.Windows.Forms.Label();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.flowDetails = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblSummaryTitle = new System.Windows.Forms.Label();
            this.lblSubTotalCaption = new System.Windows.Forms.Label();
            this.lblSubTotalValue = new System.Windows.Forms.Label();
            this.lblDiscountCaption = new System.Windows.Forms.Label();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.lblNetCaption = new System.Windows.Forms.Label();
            this.lblNetValue = new System.Windows.Forms.Label();
            this.lblItemsCountCaption = new System.Windows.Forms.Label();
            this.lblItemsCountValue = new System.Windows.Forms.Label();
            this.lblTotalQuantityCaption = new System.Windows.Forms.Label();
            this.lblTotalQuantityValue = new System.Windows.Forms.Label();
            this.lblActualQuantityCaption = new System.Windows.Forms.Label();
            this.lblActualQuantityValue = new System.Windows.Forms.Label();
            this.pnlNotes = new System.Windows.Forms.Panel();
            this.lblNotesTitle = new System.Windows.Forms.Label();
            this.lblNotesValue = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnShowInvoice = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnIssueInvoice = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblInvoiceIssued = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTypeBadge = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.flowBody.SuspendLayout();
            this.pnlOverview.SuspendLayout();
            this.pnlPartyWarehouse.SuspendLayout();
            this.pnlSourceCard.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSummary.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(980, 735);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.flowBody);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 93);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.panelBody.Size = new System.Drawing.Size(980, 562);
            this.panelBody.TabIndex = 0;
            // 
            // flowBody
            // 
            this.flowBody.AutoSize = true;
            this.flowBody.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowBody.Controls.Add(this.pnlOverview);
            this.flowBody.Controls.Add(this.pnlPartyWarehouse);
            this.flowBody.Controls.Add(this.pnlDetails);
            this.flowBody.Controls.Add(this.pnlSummary);
            this.flowBody.Controls.Add(this.pnlNotes);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBody.Location = new System.Drawing.Point(24, 20);
            this.flowBody.Name = "flowBody";
            this.flowBody.Size = new System.Drawing.Size(911, 777);
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
            this.pnlOverview.Size = new System.Drawing.Size(910, 145);
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
            // pnlPartyWarehouse
            // 
            this.pnlPartyWarehouse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.pnlPartyWarehouse.Controls.Add(this.pnlSourceCard);
            this.pnlPartyWarehouse.Location = new System.Drawing.Point(0, 159);
            this.pnlPartyWarehouse.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlPartyWarehouse.Name = "pnlPartyWarehouse";
            this.pnlPartyWarehouse.Size = new System.Drawing.Size(910, 80);
            this.pnlPartyWarehouse.TabIndex = 1;
            // 
            // pnlSourceCard
            // 
            this.pnlSourceCard.BackColor = System.Drawing.Color.White;
            this.pnlSourceCard.Controls.Add(this.lblSourceWarehouseCaption);
            this.pnlSourceCard.Controls.Add(this.lblSourceWarehouseValue);
            this.pnlSourceCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSourceCard.Location = new System.Drawing.Point(0, 0);
            this.pnlSourceCard.Name = "pnlSourceCard";
            this.pnlSourceCard.Size = new System.Drawing.Size(910, 80);
            this.pnlSourceCard.TabIndex = 2;
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
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.flowDetails);
            this.pnlDetails.Controls.Add(this.panel1);
            this.pnlDetails.Location = new System.Drawing.Point(0, 253);
            this.pnlDetails.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(22, 18, 22, 18);
            this.pnlDetails.Size = new System.Drawing.Size(910, 152);
            this.pnlDetails.TabIndex = 2;
            // 
            // flowDetails
            // 
            this.flowDetails.AutoScroll = true;
            this.flowDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDetails.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowDetails.Location = new System.Drawing.Point(22, 82);
            this.flowDetails.Name = "flowDetails";
            this.flowDetails.Size = new System.Drawing.Size(866, 52);
            this.flowDetails.TabIndex = 0;
            this.flowDetails.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDetailsTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(22, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 64);
            this.panel1.TabIndex = 2;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblDetailsTitle.Location = new System.Drawing.Point(1, 18);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(300, 30);
            this.lblDetailsTitle.TabIndex = 1;
            this.lblDetailsTitle.Text = "Products / Line Items";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.Controls.Add(this.lblSummaryTitle);
            this.pnlSummary.Controls.Add(this.lblSubTotalCaption);
            this.pnlSummary.Controls.Add(this.lblSubTotalValue);
            this.pnlSummary.Controls.Add(this.lblDiscountCaption);
            this.pnlSummary.Controls.Add(this.lblDiscountValue);
            this.pnlSummary.Controls.Add(this.lblNetCaption);
            this.pnlSummary.Controls.Add(this.lblNetValue);
            this.pnlSummary.Controls.Add(this.lblItemsCountCaption);
            this.pnlSummary.Controls.Add(this.lblItemsCountValue);
            this.pnlSummary.Controls.Add(this.lblTotalQuantityCaption);
            this.pnlSummary.Controls.Add(this.lblTotalQuantityValue);
            this.pnlSummary.Controls.Add(this.lblActualQuantityCaption);
            this.pnlSummary.Controls.Add(this.lblActualQuantityValue);
            this.pnlSummary.Location = new System.Drawing.Point(0, 419);
            this.pnlSummary.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(910, 210);
            this.pnlSummary.TabIndex = 3;
            // 
            // lblSummaryTitle
            // 
            this.lblSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblSummaryTitle.Location = new System.Drawing.Point(22, 18);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Size = new System.Drawing.Size(300, 30);
            this.lblSummaryTitle.TabIndex = 0;
            this.lblSummaryTitle.Text = "Financial Summary";
            // 
            // lblSubTotalCaption
            // 
            this.lblSubTotalCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubTotalCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTotalCaption.Location = new System.Drawing.Point(620, 55);
            this.lblSubTotalCaption.Name = "lblSubTotalCaption";
            this.lblSubTotalCaption.Size = new System.Drawing.Size(120, 25);
            this.lblSubTotalCaption.TabIndex = 1;
            this.lblSubTotalCaption.Text = "Sub Total";
            // 
            // lblSubTotalValue
            // 
            this.lblSubTotalValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblSubTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblSubTotalValue.Location = new System.Drawing.Point(735, 55);
            this.lblSubTotalValue.Name = "lblSubTotalValue";
            this.lblSubTotalValue.Size = new System.Drawing.Size(135, 28);
            this.lblSubTotalValue.TabIndex = 2;
            this.lblSubTotalValue.Text = "0.00";
            this.lblSubTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountCaption
            // 
            this.lblDiscountCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscountCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblDiscountCaption.Location = new System.Drawing.Point(620, 92);
            this.lblDiscountCaption.Name = "lblDiscountCaption";
            this.lblDiscountCaption.Size = new System.Drawing.Size(120, 25);
            this.lblDiscountCaption.TabIndex = 3;
            this.lblDiscountCaption.Text = "Discount";
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblDiscountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblDiscountValue.Location = new System.Drawing.Point(735, 92);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(135, 28);
            this.lblDiscountValue.TabIndex = 4;
            this.lblDiscountValue.Text = "0.00";
            this.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNetCaption
            // 
            this.lblNetCaption.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNetCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblNetCaption.Location = new System.Drawing.Point(620, 140);
            this.lblNetCaption.Name = "lblNetCaption";
            this.lblNetCaption.Size = new System.Drawing.Size(120, 35);
            this.lblNetCaption.TabIndex = 5;
            this.lblNetCaption.Text = "Net";
            // 
            // lblNetValue
            // 
            this.lblNetValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblNetValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblNetValue.Location = new System.Drawing.Point(735, 135);
            this.lblNetValue.Name = "lblNetValue";
            this.lblNetValue.Size = new System.Drawing.Size(135, 42);
            this.lblNetValue.TabIndex = 6;
            this.lblNetValue.Text = "0.00";
            this.lblNetValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblItemsCountCaption
            // 
            this.lblItemsCountCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblItemsCountCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblItemsCountCaption.Location = new System.Drawing.Point(26, 68);
            this.lblItemsCountCaption.Name = "lblItemsCountCaption";
            this.lblItemsCountCaption.Size = new System.Drawing.Size(140, 22);
            this.lblItemsCountCaption.TabIndex = 7;
            this.lblItemsCountCaption.Text = "Items Count";
            // 
            // lblItemsCountValue
            // 
            this.lblItemsCountValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblItemsCountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblItemsCountValue.Location = new System.Drawing.Point(26, 92);
            this.lblItemsCountValue.Name = "lblItemsCountValue";
            this.lblItemsCountValue.Size = new System.Drawing.Size(140, 42);
            this.lblItemsCountValue.TabIndex = 8;
            this.lblItemsCountValue.Text = "0";
            // 
            // lblTotalQuantityCaption
            // 
            this.lblTotalQuantityCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalQuantityCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalQuantityCaption.Location = new System.Drawing.Point(200, 68);
            this.lblTotalQuantityCaption.Name = "lblTotalQuantityCaption";
            this.lblTotalQuantityCaption.Size = new System.Drawing.Size(160, 22);
            this.lblTotalQuantityCaption.TabIndex = 9;
            this.lblTotalQuantityCaption.Text = "Ordered Quantity";
            // 
            // lblTotalQuantityValue
            // 
            this.lblTotalQuantityValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalQuantityValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblTotalQuantityValue.Location = new System.Drawing.Point(200, 92);
            this.lblTotalQuantityValue.Name = "lblTotalQuantityValue";
            this.lblTotalQuantityValue.Size = new System.Drawing.Size(160, 42);
            this.lblTotalQuantityValue.TabIndex = 10;
            this.lblTotalQuantityValue.Text = "0";
            // 
            // lblActualQuantityCaption
            // 
            this.lblActualQuantityCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblActualQuantityCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblActualQuantityCaption.Location = new System.Drawing.Point(390, 68);
            this.lblActualQuantityCaption.Name = "lblActualQuantityCaption";
            this.lblActualQuantityCaption.Size = new System.Drawing.Size(160, 22);
            this.lblActualQuantityCaption.TabIndex = 11;
            this.lblActualQuantityCaption.Text = "Actual Quantity";
            // 
            // lblActualQuantityValue
            // 
            this.lblActualQuantityValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblActualQuantityValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblActualQuantityValue.Location = new System.Drawing.Point(390, 92);
            this.lblActualQuantityValue.Name = "lblActualQuantityValue";
            this.lblActualQuantityValue.Size = new System.Drawing.Size(160, 42);
            this.lblActualQuantityValue.TabIndex = 12;
            this.lblActualQuantityValue.Text = "0";
            // 
            // pnlNotes
            // 
            this.pnlNotes.BackColor = System.Drawing.Color.White;
            this.pnlNotes.Controls.Add(this.lblNotesTitle);
            this.pnlNotes.Controls.Add(this.lblNotesValue);
            this.pnlNotes.Location = new System.Drawing.Point(0, 643);
            this.pnlNotes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlNotes.Name = "pnlNotes";
            this.pnlNotes.Size = new System.Drawing.Size(910, 120);
            this.pnlNotes.TabIndex = 4;
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
            this.panelFooter.Controls.Add(this.btnShowInvoice);
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnIssueInvoice);
            this.panelFooter.Controls.Add(this.btnEdit);
            this.panelFooter.Controls.Add(this.btnRefresh);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 655);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(980, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // btnShowInvoice
            // 
            this.btnShowInvoice.Location = new System.Drawing.Point(477, 19);
            this.btnShowInvoice.Name = "btnShowInvoice";
            this.btnShowInvoice.Size = new System.Drawing.Size(158, 40);
            this.btnShowInvoice.TabIndex = 6;
            this.btnShowInvoice.Text = "Show Invoice";
            this.btnShowInvoice.UseVisualStyleBackColor = false;
            this.btnShowInvoice.Click += new System.EventHandler(this.btnShowInvoice_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(24, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(408, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnIssueInvoice
            // 
            this.btnIssueInvoice.Location = new System.Drawing.Point(477, 19);
            this.btnIssueInvoice.Name = "btnIssueInvoice";
            this.btnIssueInvoice.Size = new System.Drawing.Size(158, 40);
            this.btnIssueInvoice.TabIndex = 5;
            this.btnIssueInvoice.Text = "Issue Invoice";
            this.btnIssueInvoice.UseVisualStyleBackColor = false;
            this.btnIssueInvoice.Click += new System.EventHandler(this.btnIssueInvoice_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(642, 19);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(95, 40);
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
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(860, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblInvoiceIssued);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblTypeBadge);
            this.panelHeader.Controls.Add(this.lblStatusBadge);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(980, 93);
            this.panelHeader.TabIndex = 2;
            // 
            // lblInvoiceIssued
            // 
            this.lblInvoiceIssued.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceIssued.Location = new System.Drawing.Point(621, 25);
            this.lblInvoiceIssued.Name = "lblInvoiceIssued";
            this.lblInvoiceIssued.Size = new System.Drawing.Size(100, 32);
            this.lblInvoiceIssued.TabIndex = 4;
            this.lblInvoiceIssued.Text = "Issued";
            this.lblInvoiceIssued.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(560, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Transaction Details";
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
            // frmTransactionDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 735);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmTransactionDetails";
            this.Text = "Transaction Details";
            this.Load += new System.EventHandler(this.frmTransactionDetails_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowBody.ResumeLayout(false);
            this.pnlOverview.ResumeLayout(false);
            this.pnlPartyWarehouse.ResumeLayout(false);
            this.pnlSourceCard.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            this.pnlNotes.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        private System.Windows.Forms.Label lblPartieCaption;
        private System.Windows.Forms.Label lblPartieValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInvoiceIssued;
        private System.Windows.Forms.Button btnIssueInvoice;
        private System.Windows.Forms.Button btnShowInvoice;
    }
    }
