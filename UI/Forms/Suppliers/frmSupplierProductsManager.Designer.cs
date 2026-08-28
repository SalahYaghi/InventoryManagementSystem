namespace UI.Forms.Suppliers
{
    partial class frmSupplierProductsManager
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.FlowLayoutPanel flowBody;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Panel panelFooter;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.GroupBox groupEditor;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblPurchasePrice;
        private System.Windows.Forms.NumericUpDown numPurchasePrice;
        private System.Windows.Forms.CheckBox chkIsActive;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRemove;

        private System.Windows.Forms.GroupBox groupProducts;
        private UI.Shared.Controllers.DgvCustom dgvSupplierProducts;

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;

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
            this.flowBody = new System.Windows.Forms.FlowLayoutPanel();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.groupEditor = new System.Windows.Forms.GroupBox();
            this.btnDetails = new System.Windows.Forms.Button();
            this.txtSelectedProduct = new System.Windows.Forms.TextBox();
            this.btnChooseProduct = new System.Windows.Forms.Button();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblPurchasePrice = new System.Windows.Forms.Label();
            this.numPurchasePrice = new System.Windows.Forms.NumericUpDown();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.groupProducts = new System.Windows.Forms.GroupBox();
            this.dgvSupplierProducts = new UI.Shared.Controllers.DgvCustom();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelRoot.SuspendLayout();
            this.flowBody.SuspendLayout();
            this.panelEditor.SuspendLayout();
            this.groupEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPurchasePrice)).BeginInit();
            this.panelGrid.SuspendLayout();
            this.groupProducts.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelRoot.Controls.Add(this.flowBody);
            this.panelRoot.Controls.Add(this.panelFooter);
            this.panelRoot.Controls.Add(this.panelHeader);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Size = new System.Drawing.Size(980, 720);
            this.panelRoot.TabIndex = 0;
            // 
            // flowBody
            // 
            this.flowBody.AutoScroll = true;
            this.flowBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.flowBody.Controls.Add(this.panelEditor);
            this.flowBody.Controls.Add(this.panelGrid);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBody.Location = new System.Drawing.Point(0, 100);
            this.flowBody.Name = "flowBody";
            this.flowBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.flowBody.Size = new System.Drawing.Size(980, 540);
            this.flowBody.TabIndex = 0;
            this.flowBody.WrapContents = false;
            // 
            // panelEditor
            // 
            this.panelEditor.Controls.Add(this.groupEditor);
            this.panelEditor.Location = new System.Drawing.Point(24, 20);
            this.panelEditor.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(930, 160);
            this.panelEditor.TabIndex = 0;
            // 
            // groupEditor
            // 
            this.groupEditor.BackColor = System.Drawing.Color.White;
            this.groupEditor.Controls.Add(this.btnDetails);
            this.groupEditor.Controls.Add(this.txtSelectedProduct);
            this.groupEditor.Controls.Add(this.btnChooseProduct);
            this.groupEditor.Controls.Add(this.lblProduct);
            this.groupEditor.Controls.Add(this.lblPurchasePrice);
            this.groupEditor.Controls.Add(this.numPurchasePrice);
            this.groupEditor.Controls.Add(this.chkIsActive);
            this.groupEditor.Controls.Add(this.btnAdd);
            this.groupEditor.Controls.Add(this.btnUpdate);
            this.groupEditor.Controls.Add(this.btnRemove);
            this.groupEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupEditor.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupEditor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupEditor.Location = new System.Drawing.Point(0, 0);
            this.groupEditor.Name = "groupEditor";
            this.groupEditor.Size = new System.Drawing.Size(930, 160);
            this.groupEditor.TabIndex = 0;
            this.groupEditor.TabStop = false;
            this.groupEditor.Text = "Product Information";
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(305, 108);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(130, 36);
            this.btnDetails.TabIndex = 10;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = false;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // txtSelectedProduct
            // 
            this.txtSelectedProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSelectedProduct.Location = new System.Drawing.Point(28, 64);
            this.txtSelectedProduct.Name = "txtSelectedProduct";
            this.txtSelectedProduct.Size = new System.Drawing.Size(290, 30);
            this.txtSelectedProduct.TabIndex = 9;
            this.txtSelectedProduct.Click += new System.EventHandler(this.txtSelectedProduct_Click);
            // 
            // btnChooseProduct
            // 
            this.btnChooseProduct.Location = new System.Drawing.Point(324, 62);
            this.btnChooseProduct.Name = "btnChooseProduct";
            this.btnChooseProduct.Size = new System.Drawing.Size(38, 32);
            this.btnChooseProduct.TabIndex = 8;
            this.btnChooseProduct.Text = "...";
            this.btnChooseProduct.UseVisualStyleBackColor = true;
            this.btnChooseProduct.Click += new System.EventHandler(this.btnChooseProduct_Click);
            // 
            // lblProduct
            // 
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProduct.ForeColor = System.Drawing.Color.Gray;
            this.lblProduct.Location = new System.Drawing.Point(24, 36);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(200, 22);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product";
            // 
            // lblPurchasePrice
            // 
            this.lblPurchasePrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPurchasePrice.ForeColor = System.Drawing.Color.Gray;
            this.lblPurchasePrice.Location = new System.Drawing.Point(440, 36);
            this.lblPurchasePrice.Name = "lblPurchasePrice";
            this.lblPurchasePrice.Size = new System.Drawing.Size(200, 22);
            this.lblPurchasePrice.TabIndex = 2;
            this.lblPurchasePrice.Text = "Purchase Price";
            // 
            // numPurchasePrice
            // 
            this.numPurchasePrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.numPurchasePrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numPurchasePrice.Location = new System.Drawing.Point(440, 62);
            this.numPurchasePrice.Name = "numPurchasePrice";
            this.numPurchasePrice.Size = new System.Drawing.Size(180, 30);
            this.numPurchasePrice.TabIndex = 3;
            // 
            // chkIsActive
            // 
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkIsActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.chkIsActive.Location = new System.Drawing.Point(650, 62);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(120, 30);
            this.chkIsActive.TabIndex = 4;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(24, 108);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 36);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "+ Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(168, 108);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(130, 36);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(441, 108);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(130, 36);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.groupProducts);
            this.panelGrid.Location = new System.Drawing.Point(24, 196);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(930, 373);
            this.panelGrid.TabIndex = 1;
            // 
            // groupProducts
            // 
            this.groupProducts.BackColor = System.Drawing.Color.White;
            this.groupProducts.Controls.Add(this.dgvSupplierProducts);
            this.groupProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupProducts.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupProducts.Location = new System.Drawing.Point(0, 0);
            this.groupProducts.Name = "groupProducts";
            this.groupProducts.Size = new System.Drawing.Size(930, 373);
            this.groupProducts.TabIndex = 0;
            this.groupProducts.TabStop = false;
            this.groupProducts.Text = "Products Supplied";
            // 
            // dgvSupplierProducts
            // 
            this.dgvSupplierProducts.BackColor = System.Drawing.Color.White;
            this.dgvSupplierProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSupplierProducts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvSupplierProducts.Location = new System.Drawing.Point(3, 26);
            this.dgvSupplierProducts.Name = "dgvSupplierProducts";
            this.dgvSupplierProducts.Size = new System.Drawing.Size(924, 344);
            this.dgvSupplierProducts.TabIndex = 0;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnRefresh);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 640);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
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
            this.btnRefresh.Location = new System.Drawing.Point(720, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 40);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(838, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
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
            this.lblTitle.Text = "Supplier Products";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(28, 62);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(760, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage products supplied by this supplier.";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // frmSupplierProductsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 720);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmSupplierProductsManager";
            this.Text = "Supplier Products";
            this.Load += new System.EventHandler(this.frmSupplierProductsManager_Load);
            this.panelRoot.ResumeLayout(false);
            this.flowBody.ResumeLayout(false);
            this.panelEditor.ResumeLayout(false);
            this.groupEditor.ResumeLayout(false);
            this.groupEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPurchasePrice)).EndInit();
            this.panelGrid.ResumeLayout(false);
            this.groupProducts.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnChooseProduct;
        private System.Windows.Forms.TextBox txtSelectedProduct;
        private System.Windows.Forms.Button btnDetails;
    }
}

