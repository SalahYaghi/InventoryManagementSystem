namespace UI.Forms.Orders
{
        partial class frmTransactionEditor
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Panel panelBody;
            private System.Windows.Forms.Panel panelFooter;

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblSubtitle;
            private System.Windows.Forms.Label lblStatus;

            private System.Windows.Forms.FlowLayoutPanel flowBody;

            private System.Windows.Forms.GroupBox groupHeader;
            private System.Windows.Forms.GroupBox groupDetails;
            private System.Windows.Forms.GroupBox groupSummary;

            private System.Windows.Forms.Panel panelHeaderFields;
            private System.Windows.Forms.FlowLayoutPanel flowSelections;

            private System.Windows.Forms.Label lblOrderType;
            private System.Windows.Forms.ComboBox cmbOrderType;

            private System.Windows.Forms.Label lblDueDate;
            private System.Windows.Forms.DateTimePicker dtpDueDate;

            private System.Windows.Forms.Label lblHint;

            private System.Windows.Forms.Panel pnlSupplier;
            private System.Windows.Forms.Label lblSupplier;
            private System.Windows.Forms.TextBox txtSupplier;
            private System.Windows.Forms.Button btnSelectSupplier;

            private System.Windows.Forms.Panel pnlCustomer;
            private System.Windows.Forms.Label lblCustomer;
            private System.Windows.Forms.TextBox txtCustomer;
            private System.Windows.Forms.Button btnSelectCustomer;

            private System.Windows.Forms.Panel pnlSourceWarehouse;
            private System.Windows.Forms.Label lblSourceWarehouse;
            private System.Windows.Forms.TextBox txtSourceWarehouse;
            private System.Windows.Forms.Button btnSelectSourceWarehouse;

            private System.Windows.Forms.Panel pnlDestinationWarehouse;
            private System.Windows.Forms.Label lblDestinationWarehouse;
            private System.Windows.Forms.TextBox txtDestinationWarehouse;
            private System.Windows.Forms.Button btnSelectDestinationWarehouse;

            private System.Windows.Forms.Panel panelDetailInputs;
            private System.Windows.Forms.Label lblQuantity;
            private System.Windows.Forms.TextBox txtQuantity;
            private System.Windows.Forms.Button btnAddDetail;
            private System.Windows.Forms.Button btnRemoveDetail;

            private UI.Shared.Controllers.DgvCustom dgvDetails;

            private System.Windows.Forms.Label lblDiscount;
            private System.Windows.Forms.TextBox txtDiscount;

            private System.Windows.Forms.Label lblNotes;
            private System.Windows.Forms.TextBox txtNotes;

            private System.Windows.Forms.Label lblSubTotal;
            private System.Windows.Forms.Label lblSubTotalValue;
            private System.Windows.Forms.Label lblDiscountValueTitle;
            private System.Windows.Forms.Label lblDiscountValue;
            private System.Windows.Forms.Label lblNet;
            private System.Windows.Forms.Label lblNetValue;

            private System.Windows.Forms.Button btnSave;
            private System.Windows.Forms.Button btnCancel;

            private System.Windows.Forms.ErrorProvider errorProvider;

            protected override void Dispose(bool disposing)
            {
                if (disposing && components != null)
                    components.Dispose();

                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
            this.components = new System.ComponentModel.Container();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelBody = new System.Windows.Forms.Panel();
            this.flowBody = new System.Windows.Forms.FlowLayoutPanel();
            this.groupHeader = new System.Windows.Forms.GroupBox();
            this.flowSelections = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSupplier = new System.Windows.Forms.Panel();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.btnSelectSupplier = new System.Windows.Forms.Button();
            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.btnSelectCustomer = new System.Windows.Forms.Button();
            this.pnlSourceWarehouse = new System.Windows.Forms.Panel();
            this.lblSourceWarehouse = new System.Windows.Forms.Label();
            this.txtSourceWarehouse = new System.Windows.Forms.TextBox();
            this.btnSelectSourceWarehouse = new System.Windows.Forms.Button();
            this.pnlDestinationWarehouse = new System.Windows.Forms.Panel();
            this.lblDestinationWarehouse = new System.Windows.Forms.Label();
            this.txtDestinationWarehouse = new System.Windows.Forms.TextBox();
            this.btnSelectDestinationWarehouse = new System.Windows.Forms.Button();
            this.panelHeaderFields = new System.Windows.Forms.Panel();
            this.lblOrderType = new System.Windows.Forms.Label();
            this.cmbOrderType = new System.Windows.Forms.ComboBox();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblHint = new System.Windows.Forms.Label();
            this.groupDetails = new System.Windows.Forms.GroupBox();
            this.dgvDetails = new UI.Shared.Controllers.DgvCustom();
            this.panelDetailInputs = new System.Windows.Forms.Panel();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAddDetail = new System.Windows.Forms.Button();
            this.btnRemoveDetail = new System.Windows.Forms.Button();
            this.groupSummary = new System.Windows.Forms.GroupBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblSubTotalValue = new System.Windows.Forms.Label();
            this.lblDiscountValueTitle = new System.Windows.Forms.Label();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.lblNet = new System.Windows.Forms.Label();
            this.lblNetValue = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnUpdateQuantity = new System.Windows.Forms.Button();
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.flowBody.SuspendLayout();
            this.groupHeader.SuspendLayout();
            this.flowSelections.SuspendLayout();
            this.pnlSupplier.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.pnlSourceWarehouse.SuspendLayout();
            this.pnlDestinationWarehouse.SuspendLayout();
            this.panelHeaderFields.SuspendLayout();
            this.groupDetails.SuspendLayout();
            this.panelDetailInputs.SuspendLayout();
            this.groupSummary.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.panelRoot.Size = new System.Drawing.Size(980, 820);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.flowBody);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 100);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(980, 640);
            this.panelBody.TabIndex = 0;
            // 
            // flowBody
            // 
            this.flowBody.AutoScroll = true;
            this.flowBody.AutoSize = true;
            this.flowBody.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowBody.Controls.Add(this.groupHeader);
            this.flowBody.Controls.Add(this.groupDetails);
            this.flowBody.Controls.Add(this.groupSummary);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBody.Location = new System.Drawing.Point(0, 0);
            this.flowBody.Name = "flowBody";
            this.flowBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.flowBody.Size = new System.Drawing.Size(980, 640);
            this.flowBody.TabIndex = 0;
            this.flowBody.WrapContents = false;
            // 
            // groupHeader
            // 
            this.groupHeader.BackColor = System.Drawing.Color.White;
            this.groupHeader.Controls.Add(this.flowSelections);
            this.groupHeader.Controls.Add(this.panelHeaderFields);
            this.groupHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupHeader.Location = new System.Drawing.Point(24, 20);
            this.groupHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.groupHeader.Name = "groupHeader";
            this.groupHeader.Size = new System.Drawing.Size(907, 283);
            this.groupHeader.TabIndex = 0;
            this.groupHeader.TabStop = false;
            this.groupHeader.Text = "Transaction Header";
            // 
            // flowSelections
            // 
            this.flowSelections.Controls.Add(this.pnlSupplier);
            this.flowSelections.Controls.Add(this.pnlCustomer);
            this.flowSelections.Controls.Add(this.pnlSourceWarehouse);
            this.flowSelections.Controls.Add(this.pnlDestinationWarehouse);
            this.flowSelections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSelections.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowSelections.Location = new System.Drawing.Point(3, 121);
            this.flowSelections.Name = "flowSelections";
            this.flowSelections.Padding = new System.Windows.Forms.Padding(20, 5, 20, 10);
            this.flowSelections.Size = new System.Drawing.Size(901, 159);
            this.flowSelections.TabIndex = 0;
            this.flowSelections.WrapContents = false;
            // 
            // pnlSupplier
            // 
            this.pnlSupplier.Controls.Add(this.lblSupplier);
            this.pnlSupplier.Controls.Add(this.txtSupplier);
            this.pnlSupplier.Controls.Add(this.btnSelectSupplier);
            this.pnlSupplier.Location = new System.Drawing.Point(20, 5);
            this.pnlSupplier.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlSupplier.Name = "pnlSupplier";
            this.pnlSupplier.Size = new System.Drawing.Size(850, 50);
            this.pnlSupplier.TabIndex = 0;
            // 
            // lblSupplier
            // 
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSupplier.ForeColor = System.Drawing.Color.Gray;
            this.lblSupplier.Location = new System.Drawing.Point(0, 0);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(180, 22);
            this.lblSupplier.TabIndex = 0;
            this.lblSupplier.Text = "Supplier *";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(0, 22);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(760, 30);
            this.txtSupplier.TabIndex = 1;
            // 
            // btnSelectSupplier
            // 
            this.btnSelectSupplier.Location = new System.Drawing.Point(770, 22);
            this.btnSelectSupplier.Name = "btnSelectSupplier";
            this.btnSelectSupplier.Size = new System.Drawing.Size(45, 27);
            this.btnSelectSupplier.TabIndex = 2;
            this.btnSelectSupplier.Text = "...";
            this.btnSelectSupplier.Click += new System.EventHandler(this.btnSelectSupplier_Click);
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.Controls.Add(this.lblCustomer);
            this.pnlCustomer.Controls.Add(this.txtCustomer);
            this.pnlCustomer.Controls.Add(this.btnSelectCustomer);
            this.pnlCustomer.Location = new System.Drawing.Point(20, 63);
            this.pnlCustomer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(850, 50);
            this.pnlCustomer.TabIndex = 1;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomer.ForeColor = System.Drawing.Color.Gray;
            this.lblCustomer.Location = new System.Drawing.Point(0, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(180, 22);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer *";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(0, 22);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(760, 30);
            this.txtCustomer.TabIndex = 1;
            // 
            // btnSelectCustomer
            // 
            this.btnSelectCustomer.Location = new System.Drawing.Point(770, 22);
            this.btnSelectCustomer.Name = "btnSelectCustomer";
            this.btnSelectCustomer.Size = new System.Drawing.Size(45, 27);
            this.btnSelectCustomer.TabIndex = 2;
            this.btnSelectCustomer.Text = "...";
            this.btnSelectCustomer.Click += new System.EventHandler(this.btnSelectCustomer_Click);
            // 
            // pnlSourceWarehouse
            // 
            this.pnlSourceWarehouse.Controls.Add(this.lblSourceWarehouse);
            this.pnlSourceWarehouse.Controls.Add(this.txtSourceWarehouse);
            this.pnlSourceWarehouse.Controls.Add(this.btnSelectSourceWarehouse);
            this.pnlSourceWarehouse.Location = new System.Drawing.Point(20, 121);
            this.pnlSourceWarehouse.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlSourceWarehouse.Name = "pnlSourceWarehouse";
            this.pnlSourceWarehouse.Size = new System.Drawing.Size(850, 50);
            this.pnlSourceWarehouse.TabIndex = 2;
            // 
            // lblSourceWarehouse
            // 
            this.lblSourceWarehouse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSourceWarehouse.ForeColor = System.Drawing.Color.Gray;
            this.lblSourceWarehouse.Location = new System.Drawing.Point(0, 0);
            this.lblSourceWarehouse.Name = "lblSourceWarehouse";
            this.lblSourceWarehouse.Size = new System.Drawing.Size(180, 22);
            this.lblSourceWarehouse.TabIndex = 0;
            this.lblSourceWarehouse.Text = "Source Warehouse *";
            // 
            // txtSourceWarehouse
            // 
            this.txtSourceWarehouse.Location = new System.Drawing.Point(0, 22);
            this.txtSourceWarehouse.Name = "txtSourceWarehouse";
            this.txtSourceWarehouse.Size = new System.Drawing.Size(760, 30);
            this.txtSourceWarehouse.TabIndex = 1;
            // 
            // btnSelectSourceWarehouse
            // 
            this.btnSelectSourceWarehouse.Location = new System.Drawing.Point(770, 22);
            this.btnSelectSourceWarehouse.Name = "btnSelectSourceWarehouse";
            this.btnSelectSourceWarehouse.Size = new System.Drawing.Size(45, 27);
            this.btnSelectSourceWarehouse.TabIndex = 2;
            this.btnSelectSourceWarehouse.Text = "...";
            this.btnSelectSourceWarehouse.Click += new System.EventHandler(this.btnSelectSourceWarehouse_Click);
            // 
            // pnlDestinationWarehouse
            // 
            this.pnlDestinationWarehouse.Controls.Add(this.lblDestinationWarehouse);
            this.pnlDestinationWarehouse.Controls.Add(this.txtDestinationWarehouse);
            this.pnlDestinationWarehouse.Controls.Add(this.btnSelectDestinationWarehouse);
            this.pnlDestinationWarehouse.Location = new System.Drawing.Point(20, 179);
            this.pnlDestinationWarehouse.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlDestinationWarehouse.Name = "pnlDestinationWarehouse";
            this.pnlDestinationWarehouse.Size = new System.Drawing.Size(850, 50);
            this.pnlDestinationWarehouse.TabIndex = 3;
            // 
            // lblDestinationWarehouse
            // 
            this.lblDestinationWarehouse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDestinationWarehouse.ForeColor = System.Drawing.Color.Gray;
            this.lblDestinationWarehouse.Location = new System.Drawing.Point(0, 0);
            this.lblDestinationWarehouse.Name = "lblDestinationWarehouse";
            this.lblDestinationWarehouse.Size = new System.Drawing.Size(220, 22);
            this.lblDestinationWarehouse.TabIndex = 0;
            this.lblDestinationWarehouse.Text = "Destination Warehouse *";
            // 
            // txtDestinationWarehouse
            // 
            this.txtDestinationWarehouse.Location = new System.Drawing.Point(0, 22);
            this.txtDestinationWarehouse.Name = "txtDestinationWarehouse";
            this.txtDestinationWarehouse.Size = new System.Drawing.Size(760, 30);
            this.txtDestinationWarehouse.TabIndex = 1;
            // 
            // btnSelectDestinationWarehouse
            // 
            this.btnSelectDestinationWarehouse.Location = new System.Drawing.Point(770, 22);
            this.btnSelectDestinationWarehouse.Name = "btnSelectDestinationWarehouse";
            this.btnSelectDestinationWarehouse.Size = new System.Drawing.Size(45, 27);
            this.btnSelectDestinationWarehouse.TabIndex = 2;
            this.btnSelectDestinationWarehouse.Text = "...";
            this.btnSelectDestinationWarehouse.Click += new System.EventHandler(this.btnSelectDestinationWarehouse_Click);
            // 
            // panelHeaderFields
            // 
            this.panelHeaderFields.Controls.Add(this.lblOrderType);
            this.panelHeaderFields.Controls.Add(this.cmbOrderType);
            this.panelHeaderFields.Controls.Add(this.lblDueDate);
            this.panelHeaderFields.Controls.Add(this.dtpDueDate);
            this.panelHeaderFields.Controls.Add(this.lblHint);
            this.panelHeaderFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeaderFields.Location = new System.Drawing.Point(3, 26);
            this.panelHeaderFields.Name = "panelHeaderFields";
            this.panelHeaderFields.Size = new System.Drawing.Size(901, 95);
            this.panelHeaderFields.TabIndex = 1;
            // 
            // lblOrderType
            // 
            this.lblOrderType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOrderType.ForeColor = System.Drawing.Color.Gray;
            this.lblOrderType.Location = new System.Drawing.Point(24, 28);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(180, 22);
            this.lblOrderType.TabIndex = 0;
            this.lblOrderType.Text = "Transaction Type *";
            // 
            // cmbOrderType
            // 
            this.cmbOrderType.Location = new System.Drawing.Point(24, 52);
            this.cmbOrderType.Name = "cmbOrderType";
            this.cmbOrderType.Size = new System.Drawing.Size(260, 31);
            this.cmbOrderType.TabIndex = 1;
            this.cmbOrderType.SelectedIndexChanged += new System.EventHandler(this.cmbOrderType_SelectedIndexChanged);
            // 
            // lblDueDate
            // 
            this.lblDueDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDueDate.ForeColor = System.Drawing.Color.Gray;
            this.lblDueDate.Location = new System.Drawing.Point(320, 28);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(180, 22);
            this.lblDueDate.TabIndex = 2;
            this.lblDueDate.Text = "Due Date *";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Location = new System.Drawing.Point(320, 52);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(264, 30);
            this.dtpDueDate.TabIndex = 3;
            // 
            // lblHint
            // 
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(590, 45);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(280, 42);
            this.lblHint.TabIndex = 4;
            this.lblHint.Text = "Transaction hint";
            // 
            // groupDetails
            // 
            this.groupDetails.BackColor = System.Drawing.Color.White;
            this.groupDetails.Controls.Add(this.dgvDetails);
            this.groupDetails.Controls.Add(this.panelDetailInputs);
            this.groupDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupDetails.Location = new System.Drawing.Point(24, 317);
            this.groupDetails.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.groupDetails.Name = "groupDetails";
            this.groupDetails.Size = new System.Drawing.Size(910, 330);
            this.groupDetails.TabIndex = 1;
            this.groupDetails.TabStop = false;
            this.groupDetails.Text = "Transaction Details";
            // 
            // dgvDetails
            // 
            this.dgvDetails.BackColor = System.Drawing.Color.White;
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvDetails.Location = new System.Drawing.Point(3, 102);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.Size = new System.Drawing.Size(904, 225);
            this.dgvDetails.TabIndex = 0;
            // 
            // panelDetailInputs
            // 
            this.panelDetailInputs.Controls.Add(this.btnUpdateQuantity);
            this.panelDetailInputs.Controls.Add(this.lblQuantity);
            this.panelDetailInputs.Controls.Add(this.txtQuantity);
            this.panelDetailInputs.Controls.Add(this.btnAddDetail);
            this.panelDetailInputs.Controls.Add(this.btnRemoveDetail);
            this.panelDetailInputs.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetailInputs.Location = new System.Drawing.Point(3, 26);
            this.panelDetailInputs.Name = "panelDetailInputs";
            this.panelDetailInputs.Size = new System.Drawing.Size(904, 76);
            this.panelDetailInputs.TabIndex = 1;
            // 
            // lblQuantity
            // 
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuantity.ForeColor = System.Drawing.Color.Gray;
            this.lblQuantity.Location = new System.Drawing.Point(24, 15);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(120, 22);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Quantity";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(24, 38);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(160, 30);
            this.txtQuantity.TabIndex = 1;
            this.txtQuantity.Text = "1";
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.Location = new System.Drawing.Point(206, 30);
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Size = new System.Drawing.Size(145, 36);
            this.btnAddDetail.TabIndex = 4;
            this.btnAddDetail.Text = "Add Product";
            this.btnAddDetail.UseVisualStyleBackColor = false;
            this.btnAddDetail.Click += new System.EventHandler(this.btnAddDetail_Click);
            // 
            // btnRemoveDetail
            // 
            this.btnRemoveDetail.Location = new System.Drawing.Point(357, 30);
            this.btnRemoveDetail.Name = "btnRemoveDetail";
            this.btnRemoveDetail.Size = new System.Drawing.Size(145, 36);
            this.btnRemoveDetail.TabIndex = 5;
            this.btnRemoveDetail.Text = "Remove";
            this.btnRemoveDetail.UseVisualStyleBackColor = false;
            this.btnRemoveDetail.Click += new System.EventHandler(this.btnRemoveDetail_Click);
            // 
            // groupSummary
            // 
            this.groupSummary.BackColor = System.Drawing.Color.White;
            this.groupSummary.Controls.Add(this.lblDiscount);
            this.groupSummary.Controls.Add(this.txtDiscount);
            this.groupSummary.Controls.Add(this.lblNotes);
            this.groupSummary.Controls.Add(this.txtNotes);
            this.groupSummary.Controls.Add(this.lblSubTotal);
            this.groupSummary.Controls.Add(this.lblSubTotalValue);
            this.groupSummary.Controls.Add(this.lblDiscountValueTitle);
            this.groupSummary.Controls.Add(this.lblDiscountValue);
            this.groupSummary.Controls.Add(this.lblNet);
            this.groupSummary.Controls.Add(this.lblNetValue);
            this.groupSummary.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupSummary.Location = new System.Drawing.Point(24, 661);
            this.groupSummary.Margin = new System.Windows.Forms.Padding(0, 0, 0, 45);
            this.groupSummary.Name = "groupSummary";
            this.groupSummary.Size = new System.Drawing.Size(910, 170);
            this.groupSummary.TabIndex = 2;
            this.groupSummary.TabStop = false;
            this.groupSummary.Text = "Summary";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiscount.ForeColor = System.Drawing.Color.Gray;
            this.lblDiscount.Location = new System.Drawing.Point(24, 38);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(120, 22);
            this.lblDiscount.TabIndex = 0;
            this.lblDiscount.Text = "Discount";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(24, 62);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(180, 30);
            this.txtDiscount.TabIndex = 1;
            this.txtDiscount.Text = "0";
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            // 
            // lblNotes
            // 
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNotes.ForeColor = System.Drawing.Color.Gray;
            this.lblNotes.Location = new System.Drawing.Point(24, 98);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(120, 22);
            this.lblNotes.TabIndex = 2;
            this.lblNotes.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(24, 122);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(540, 30);
            this.txtNotes.TabIndex = 3;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubTotal.Location = new System.Drawing.Point(640, 42);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(120, 25);
            this.lblSubTotal.TabIndex = 4;
            this.lblSubTotal.Text = "Sub Total:";
            // 
            // lblSubTotalValue
            // 
            this.lblSubTotalValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblSubTotalValue.Location = new System.Drawing.Point(770, 42);
            this.lblSubTotalValue.Name = "lblSubTotalValue";
            this.lblSubTotalValue.Size = new System.Drawing.Size(110, 25);
            this.lblSubTotalValue.TabIndex = 5;
            this.lblSubTotalValue.Text = "0.00";
            this.lblSubTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountValueTitle
            // 
            this.lblDiscountValueTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscountValueTitle.Location = new System.Drawing.Point(640, 78);
            this.lblDiscountValueTitle.Name = "lblDiscountValueTitle";
            this.lblDiscountValueTitle.Size = new System.Drawing.Size(120, 25);
            this.lblDiscountValueTitle.TabIndex = 6;
            this.lblDiscountValueTitle.Text = "Discount:";
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblDiscountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblDiscountValue.Location = new System.Drawing.Point(770, 78);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(110, 25);
            this.lblDiscountValue.TabIndex = 7;
            this.lblDiscountValue.Text = "0.00";
            this.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNet
            // 
            this.lblNet.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNet.Location = new System.Drawing.Point(640, 118);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(120, 30);
            this.lblNet.TabIndex = 8;
            this.lblNet.Text = "Net:";
            // 
            // lblNetValue
            // 
            this.lblNetValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNetValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblNetValue.Location = new System.Drawing.Point(770, 118);
            this.lblNetValue.Name = "lblNetValue";
            this.lblNetValue.Size = new System.Drawing.Size(110, 30);
            this.lblNetValue.TabIndex = 9;
            this.lblNetValue.Text = "0.00";
            this.lblNetValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 740);
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(735, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(850, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(980, 100);
            this.panelHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Transaction Editor";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(28, 62);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(760, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Create or update inventory transaction.";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // btnUpdateQuantity
            // 
            this.btnUpdateQuantity.Location = new System.Drawing.Point(508, 30);
            this.btnUpdateQuantity.Name = "btnUpdateQuantity";
            this.btnUpdateQuantity.Size = new System.Drawing.Size(210, 36);
            this.btnUpdateQuantity.TabIndex = 6;
            this.btnUpdateQuantity.Text = "Update Quantity";
            this.btnUpdateQuantity.UseVisualStyleBackColor = false;
            this.btnUpdateQuantity.Click += new System.EventHandler(this.btnUpdateQuantity_Click);
            // 
            // frmTransactionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 820);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmTransactionEditor";
            this.Text = "Transaction Editor";
            this.Load += new System.EventHandler(this.frmTransactionEditor_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowBody.ResumeLayout(false);
            this.groupHeader.ResumeLayout(false);
            this.flowSelections.ResumeLayout(false);
            this.pnlSupplier.ResumeLayout(false);
            this.pnlSupplier.PerformLayout();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.pnlSourceWarehouse.ResumeLayout(false);
            this.pnlSourceWarehouse.PerformLayout();
            this.pnlDestinationWarehouse.ResumeLayout(false);
            this.pnlDestinationWarehouse.PerformLayout();
            this.panelHeaderFields.ResumeLayout(false);
            this.groupDetails.ResumeLayout(false);
            this.panelDetailInputs.ResumeLayout(false);
            this.panelDetailInputs.PerformLayout();
            this.groupSummary.ResumeLayout(false);
            this.groupSummary.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

            }

        private System.Windows.Forms.Button btnUpdateQuantity;
    }
    

}
