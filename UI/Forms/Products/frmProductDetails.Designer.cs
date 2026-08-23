namespace UI.Forms.Products
{
           partial class frmProductDetails
        {
            private System.ComponentModel.IContainer components = null;

            protected override void Dispose(bool disposing)
            {
                if (disposing && components != null)
                    components.Dispose();

                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
                this.panelRoot = new System.Windows.Forms.Panel();
                this.panelFooter = new System.Windows.Forms.Panel();
                this.lblStatus = new System.Windows.Forms.Label();
                this.btnClose = new System.Windows.Forms.Button();
                this.btnEdit = new System.Windows.Forms.Button();
                this.panelBody = new System.Windows.Forms.Panel();
                this.groupDescription = new System.Windows.Forms.GroupBox();
                this.txtDescription = new System.Windows.Forms.TextBox();
                this.groupMainInfo = new System.Windows.Forms.GroupBox();
                this.lblPriceValue = new System.Windows.Forms.Label();
                this.lblPriceTitle = new System.Windows.Forms.Label();
                this.lblUnitValue = new System.Windows.Forms.Label();
                this.lblUnitTitle = new System.Windows.Forms.Label();
                this.lblCategoryValue = new System.Windows.Forms.Label();
                this.lblCategoryTitle = new System.Windows.Forms.Label();
                this.lblBarcodeValue = new System.Windows.Forms.Label();
                this.lblBarcodeTitle = new System.Windows.Forms.Label();
                this.panelHeader = new System.Windows.Forms.Panel();
                this.lblStatusBadge = new System.Windows.Forms.Label();
                this.lblSku = new System.Windows.Forms.Label();
                this.lblProductName = new System.Windows.Forms.Label();
                this.panelRoot.SuspendLayout();
                this.panelFooter.SuspendLayout();
                this.panelBody.SuspendLayout();
                this.groupDescription.SuspendLayout();
                this.groupMainInfo.SuspendLayout();
                this.panelHeader.SuspendLayout();
                this.SuspendLayout();
                // 
                // panelRoot
                // 
                this.panelRoot.BackColor = System.Drawing.Color.FromArgb(243, 246, 249);
                this.panelRoot.Controls.Add(this.panelBody);
                this.panelRoot.Controls.Add(this.panelFooter);
                this.panelRoot.Controls.Add(this.panelHeader);
                this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panelRoot.Location = new System.Drawing.Point(0, 0);
                this.panelRoot.Name = "panelRoot";
                this.panelRoot.Size = new System.Drawing.Size(760, 560);
                this.panelRoot.TabIndex = 0;
                // 
                // panelFooter
                // 
                this.panelFooter.BackColor = System.Drawing.Color.White;
                this.panelFooter.Controls.Add(this.lblStatus);
                this.panelFooter.Controls.Add(this.btnClose);
                this.panelFooter.Controls.Add(this.btnEdit);
                this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.panelFooter.Location = new System.Drawing.Point(0, 480);
                this.panelFooter.Name = "panelFooter";
                this.panelFooter.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
                this.panelFooter.Size = new System.Drawing.Size(760, 80);
                this.panelFooter.TabIndex = 2;
                // 
                // lblStatus
                // 
                this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.lblStatus.ForeColor = System.Drawing.Color.Gray;
                this.lblStatus.Location = new System.Drawing.Point(24, 29);
                this.lblStatus.Name = "lblStatus";
                this.lblStatus.Size = new System.Drawing.Size(360, 23);
                this.lblStatus.TabIndex = 2;
                this.lblStatus.Text = "Ready";
                // 
                // btnClose
                // 
                this.btnClose.Location = new System.Drawing.Point(500, 20);
                this.btnClose.Name = "btnClose";
                this.btnClose.Size = new System.Drawing.Size(105, 40);
                this.btnClose.TabIndex = 1;
                this.btnClose.Text = "Close";
                this.btnClose.UseVisualStyleBackColor = false;
                this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
                // 
                // btnEdit
                // 
                this.btnEdit.Location = new System.Drawing.Point(615, 20);
                this.btnEdit.Name = "btnEdit";
                this.btnEdit.Size = new System.Drawing.Size(120, 40);
                this.btnEdit.TabIndex = 0;
                this.btnEdit.Text = "Edit Product";
                this.btnEdit.UseVisualStyleBackColor = false;
                this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
                // 
                // panelBody
                // 
                this.panelBody.BackColor = System.Drawing.Color.FromArgb(243, 246, 249);
                this.panelBody.Controls.Add(this.groupDescription);
                this.panelBody.Controls.Add(this.groupMainInfo);
                this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panelBody.Location = new System.Drawing.Point(0, 120);
                this.panelBody.Name = "panelBody";
                this.panelBody.Padding = new System.Windows.Forms.Padding(24);
                this.panelBody.Size = new System.Drawing.Size(760, 360);
                this.panelBody.TabIndex = 1;
                // 
                // groupDescription
                // 
                this.groupDescription.BackColor = System.Drawing.Color.White;
                this.groupDescription.Controls.Add(this.txtDescription);
                this.groupDescription.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
                this.groupDescription.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.groupDescription.Location = new System.Drawing.Point(24, 190);
                this.groupDescription.Name = "groupDescription";
                this.groupDescription.Padding = new System.Windows.Forms.Padding(18);
                this.groupDescription.Size = new System.Drawing.Size(712, 145);
                this.groupDescription.TabIndex = 1;
                this.groupDescription.TabStop = false;
                this.groupDescription.Text = "Description";
                // 
                // txtDescription
                // 
                this.txtDescription.BackColor = System.Drawing.Color.White;
                this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
                this.txtDescription.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
                this.txtDescription.Location = new System.Drawing.Point(22, 35);
                this.txtDescription.Multiline = true;
                this.txtDescription.Name = "txtDescription";
                this.txtDescription.ReadOnly = true;
                this.txtDescription.Size = new System.Drawing.Size(665, 88);
                this.txtDescription.TabIndex = 0;
                // 
                // groupMainInfo
                // 
                this.groupMainInfo.BackColor = System.Drawing.Color.White;
                this.groupMainInfo.Controls.Add(this.lblPriceValue);
                this.groupMainInfo.Controls.Add(this.lblPriceTitle);
                this.groupMainInfo.Controls.Add(this.lblUnitValue);
                this.groupMainInfo.Controls.Add(this.lblUnitTitle);
                this.groupMainInfo.Controls.Add(this.lblCategoryValue);
                this.groupMainInfo.Controls.Add(this.lblCategoryTitle);
                this.groupMainInfo.Controls.Add(this.lblBarcodeValue);
                this.groupMainInfo.Controls.Add(this.lblBarcodeTitle);
                this.groupMainInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
                this.groupMainInfo.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.groupMainInfo.Location = new System.Drawing.Point(24, 24);
                this.groupMainInfo.Name = "groupMainInfo";
                this.groupMainInfo.Padding = new System.Windows.Forms.Padding(18);
                this.groupMainInfo.Size = new System.Drawing.Size(712, 145);
                this.groupMainInfo.TabIndex = 0;
                this.groupMainInfo.TabStop = false;
                this.groupMainInfo.Text = "Product Information";
                // 
                // lblPriceValue
                // 
                this.lblPriceValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
                this.lblPriceValue.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.lblPriceValue.Location = new System.Drawing.Point(382, 95);
                this.lblPriceValue.Name = "lblPriceValue";
                this.lblPriceValue.Size = new System.Drawing.Size(280, 28);
                this.lblPriceValue.TabIndex = 7;
                this.lblPriceValue.Text = "-";
                // 
                // lblPriceTitle
                // 
                this.lblPriceTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.lblPriceTitle.ForeColor = System.Drawing.Color.Gray;
                this.lblPriceTitle.Location = new System.Drawing.Point(382, 72);
                this.lblPriceTitle.Name = "lblPriceTitle";
                this.lblPriceTitle.Size = new System.Drawing.Size(280, 22);
                this.lblPriceTitle.TabIndex = 6;
                this.lblPriceTitle.Text = "Selling Price";
                // 
                // lblUnitValue
                // 
                this.lblUnitValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
                this.lblUnitValue.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.lblUnitValue.Location = new System.Drawing.Point(22, 95);
                this.lblUnitValue.Name = "lblUnitValue";
                this.lblUnitValue.Size = new System.Drawing.Size(280, 28);
                this.lblUnitValue.TabIndex = 5;
                this.lblUnitValue.Text = "-";
                // 
                // lblUnitTitle
                // 
                this.lblUnitTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.lblUnitTitle.ForeColor = System.Drawing.Color.Gray;
                this.lblUnitTitle.Location = new System.Drawing.Point(22, 72);
                this.lblUnitTitle.Name = "lblUnitTitle";
                this.lblUnitTitle.Size = new System.Drawing.Size(280, 22);
                this.lblUnitTitle.TabIndex = 4;
                this.lblUnitTitle.Text = "Unit";
                // 
                // lblCategoryValue
                // 
                this.lblCategoryValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
                this.lblCategoryValue.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.lblCategoryValue.Location = new System.Drawing.Point(382, 40);
                this.lblCategoryValue.Name = "lblCategoryValue";
                this.lblCategoryValue.Size = new System.Drawing.Size(280, 28);
                this.lblCategoryValue.TabIndex = 3;
                this.lblCategoryValue.Text = "-";
                // 
                // lblCategoryTitle
                // 
                this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.lblCategoryTitle.ForeColor = System.Drawing.Color.Gray;
                this.lblCategoryTitle.Location = new System.Drawing.Point(382, 18);
                this.lblCategoryTitle.Name = "lblCategoryTitle";
                this.lblCategoryTitle.Size = new System.Drawing.Size(280, 22);
                this.lblCategoryTitle.TabIndex = 2;
                this.lblCategoryTitle.Text = "Category";
                // 
                // lblBarcodeValue
                // 
                this.lblBarcodeValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
                this.lblBarcodeValue.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.lblBarcodeValue.Location = new System.Drawing.Point(22, 40);
                this.lblBarcodeValue.Name = "lblBarcodeValue";
                this.lblBarcodeValue.Size = new System.Drawing.Size(280, 28);
                this.lblBarcodeValue.TabIndex = 1;
                this.lblBarcodeValue.Text = "-";
                // 
                // lblBarcodeTitle
                // 
                this.lblBarcodeTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.lblBarcodeTitle.ForeColor = System.Drawing.Color.Gray;
                this.lblBarcodeTitle.Location = new System.Drawing.Point(22, 18);
                this.lblBarcodeTitle.Name = "lblBarcodeTitle";
                this.lblBarcodeTitle.Size = new System.Drawing.Size(280, 22);
                this.lblBarcodeTitle.TabIndex = 0;
                this.lblBarcodeTitle.Text = "Barcode";
                // 
                // panelHeader
                // 
                this.panelHeader.BackColor = System.Drawing.Color.White;
                this.panelHeader.Controls.Add(this.lblStatusBadge);
                this.panelHeader.Controls.Add(this.lblSku);
                this.panelHeader.Controls.Add(this.lblProductName);
                this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
                this.panelHeader.Location = new System.Drawing.Point(0, 0);
                this.panelHeader.Name = "panelHeader";
                this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
                this.panelHeader.Size = new System.Drawing.Size(760, 120);
                this.panelHeader.TabIndex = 0;
                // 
                // lblStatusBadge
                // 
                this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
                this.lblStatusBadge.Location = new System.Drawing.Point(620, 26);
                this.lblStatusBadge.Name = "lblStatusBadge";
                this.lblStatusBadge.Size = new System.Drawing.Size(100, 30);
                this.lblStatusBadge.TabIndex = 2;
                this.lblStatusBadge.Text = "Status";
                this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // lblSku
                // 
                this.lblSku.Font = new System.Drawing.Font("Segoe UI", 10F);
                this.lblSku.ForeColor = System.Drawing.Color.Gray;
                this.lblSku.Location = new System.Drawing.Point(28, 72);
                this.lblSku.Name = "lblSku";
                this.lblSku.Size = new System.Drawing.Size(500, 25);
                this.lblSku.TabIndex = 1;
                this.lblSku.Text = "SKU:";
                // 
                // lblProductName
                // 
                this.lblProductName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
                this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.lblProductName.Location = new System.Drawing.Point(24, 22);
                this.lblProductName.Name = "lblProductName";
                this.lblProductName.Size = new System.Drawing.Size(570, 46);
                this.lblProductName.TabIndex = 0;
                this.lblProductName.Text = "Product Name";
                // 
                // frmProductDetails
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(760, 560);
                this.Controls.Add(this.panelRoot);
                this.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.Name = "frmProductDetails";
                this.Text = "Product Details";
                this.Load += new System.EventHandler(this.frmProductDetails_Load);
                this.panelRoot.ResumeLayout(false);
                this.panelFooter.ResumeLayout(false);
                this.panelBody.ResumeLayout(false);
                this.groupDescription.ResumeLayout(false);
                this.groupDescription.PerformLayout();
                this.groupMainInfo.ResumeLayout(false);
                this.panelHeader.ResumeLayout(false);
                this.ResumeLayout(false);
            }

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Label lblProductName;
            private System.Windows.Forms.Label lblSku;
            private System.Windows.Forms.Label lblStatusBadge;
            private System.Windows.Forms.Panel panelBody;
            private System.Windows.Forms.GroupBox groupMainInfo;
            private System.Windows.Forms.Label lblBarcodeTitle;
            private System.Windows.Forms.Label lblBarcodeValue;
            private System.Windows.Forms.Label lblCategoryTitle;
            private System.Windows.Forms.Label lblCategoryValue;
            private System.Windows.Forms.Label lblUnitTitle;
            private System.Windows.Forms.Label lblUnitValue;
            private System.Windows.Forms.Label lblPriceTitle;
            private System.Windows.Forms.Label lblPriceValue;
            private System.Windows.Forms.GroupBox groupDescription;
            private System.Windows.Forms.TextBox txtDescription;
            private System.Windows.Forms.Panel panelFooter;
            private System.Windows.Forms.Button btnEdit;
            private System.Windows.Forms.Button btnClose;
            private System.Windows.Forms.Label lblStatus;
        }
    }
