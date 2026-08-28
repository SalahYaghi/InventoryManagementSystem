namespace UI.Forms.Products
{ 
        partial class frmProductEditor
        {
            private System.ComponentModel.IContainer components = null;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                    components.Dispose();

                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
            this.components = new System.ComponentModel.Container();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelBody = new System.Windows.Forms.Panel();
            this.groupDescription = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.groupPricing = new System.Windows.Forms.GroupBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.numSellingPrice = new System.Windows.Forms.NumericUpDown();
            this.lblSellingPrice = new System.Windows.Forms.Label();
            this.groupIdentity = new System.Windows.Forms.GroupBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.lblSku = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.groupDescription.SuspendLayout();
            this.groupPricing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSellingPrice)).BeginInit();
            this.groupIdentity.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(760, 650);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.groupDescription);
            this.panelBody.Controls.Add(this.groupPricing);
            this.panelBody.Controls.Add(this.groupIdentity);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 100);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24);
            this.panelBody.Size = new System.Drawing.Size(760, 470);
            this.panelBody.TabIndex = 1;
            // 
            // groupDescription
            // 
            this.groupDescription.BackColor = System.Drawing.Color.White;
            this.groupDescription.Controls.Add(this.txtDescription);
            this.groupDescription.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupDescription.Location = new System.Drawing.Point(24, 314);
            this.groupDescription.Name = "groupDescription";
            this.groupDescription.Padding = new System.Windows.Forms.Padding(18);
            this.groupDescription.Size = new System.Drawing.Size(712, 130);
            this.groupDescription.TabIndex = 2;
            this.groupDescription.TabStop = false;
            this.groupDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(22, 35);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(665, 74);
            this.txtDescription.TabIndex = 0;
            // 
            // groupPricing
            // 
            this.groupPricing.BackColor = System.Drawing.Color.White;
            this.groupPricing.Controls.Add(this.chkIsActive);
            this.groupPricing.Controls.Add(this.cmbUnit);
            this.groupPricing.Controls.Add(this.lblUnit);
            this.groupPricing.Controls.Add(this.numSellingPrice);
            this.groupPricing.Controls.Add(this.lblSellingPrice);
            this.groupPricing.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupPricing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupPricing.Location = new System.Drawing.Point(24, 196);
            this.groupPricing.Name = "groupPricing";
            this.groupPricing.Padding = new System.Windows.Forms.Padding(18);
            this.groupPricing.Size = new System.Drawing.Size(712, 102);
            this.groupPricing.TabIndex = 1;
            this.groupPricing.TabStop = false;
            this.groupPricing.Text = "Pricing && Unit";
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkIsActive.Location = new System.Drawing.Point(497, 50);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(95, 27);
            this.chkIsActive.TabIndex = 4;
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // cmbUnit
            // 
            this.cmbUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(263, 46);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(190, 31);
            this.cmbUnit.TabIndex = 3;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUnit.ForeColor = System.Drawing.Color.Gray;
            this.lblUnit.Location = new System.Drawing.Point(259, 24);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(36, 20);
            this.lblUnit.TabIndex = 2;
            this.lblUnit.Text = "Unit";
            // 
            // numSellingPrice
            // 
            this.numSellingPrice.DecimalPlaces = 2;
            this.numSellingPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSellingPrice.Location = new System.Drawing.Point(22, 47);
            this.numSellingPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numSellingPrice.Name = "numSellingPrice";
            this.numSellingPrice.Size = new System.Drawing.Size(200, 30);
            this.numSellingPrice.TabIndex = 1;
            // 
            // lblSellingPrice
            // 
            this.lblSellingPrice.AutoSize = true;
            this.lblSellingPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSellingPrice.ForeColor = System.Drawing.Color.Gray;
            this.lblSellingPrice.Location = new System.Drawing.Point(18, 24);
            this.lblSellingPrice.Name = "lblSellingPrice";
            this.lblSellingPrice.Size = new System.Drawing.Size(90, 20);
            this.lblSellingPrice.TabIndex = 0;
            this.lblSellingPrice.Text = "Selling Price";
            // 
            // groupIdentity
            // 
            this.groupIdentity.BackColor = System.Drawing.Color.White;
            this.groupIdentity.Controls.Add(this.cmbCategory);
            this.groupIdentity.Controls.Add(this.lblCategory);
            this.groupIdentity.Controls.Add(this.txtBarcode);
            this.groupIdentity.Controls.Add(this.lblBarcode);
            this.groupIdentity.Controls.Add(this.txtSku);
            this.groupIdentity.Controls.Add(this.lblSku);
            this.groupIdentity.Controls.Add(this.txtProductName);
            this.groupIdentity.Controls.Add(this.lblProductName);
            this.groupIdentity.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupIdentity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupIdentity.Location = new System.Drawing.Point(24, 24);
            this.groupIdentity.Name = "groupIdentity";
            this.groupIdentity.Padding = new System.Windows.Forms.Padding(18);
            this.groupIdentity.Size = new System.Drawing.Size(712, 156);
            this.groupIdentity.TabIndex = 0;
            this.groupIdentity.TabStop = false;
            this.groupIdentity.Text = "Product Identity";
            // 
            // cmbCategory
            // 
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(382, 103);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(305, 31);
            this.cmbCategory.TabIndex = 7;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCategory.ForeColor = System.Drawing.Color.Gray;
            this.lblCategory.Location = new System.Drawing.Point(378, 80);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(69, 20);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.Text = "Category";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBarcode.Location = new System.Drawing.Point(22, 103);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(305, 30);
            this.txtBarcode.TabIndex = 5;
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBarcode.ForeColor = System.Drawing.Color.Gray;
            this.lblBarcode.Location = new System.Drawing.Point(18, 80);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(64, 20);
            this.lblBarcode.TabIndex = 4;
            this.lblBarcode.Text = "Barcode";
            // 
            // txtSku
            // 
            this.txtSku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSku.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSku.Location = new System.Drawing.Point(382, 47);
            this.txtSku.Name = "txtSku";
            this.txtSku.Size = new System.Drawing.Size(305, 30);
            this.txtSku.TabIndex = 3;
            // 
            // lblSku
            // 
            this.lblSku.AutoSize = true;
            this.lblSku.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSku.ForeColor = System.Drawing.Color.Gray;
            this.lblSku.Location = new System.Drawing.Point(378, 24);
            this.lblSku.Name = "lblSku";
            this.lblSku.Size = new System.Drawing.Size(36, 20);
            this.lblSku.TabIndex = 2;
            this.lblSku.Text = "SKU";
            // 
            // txtProductName
            // 
            this.txtProductName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtProductName.Location = new System.Drawing.Point(22, 47);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(305, 30);
            this.txtProductName.TabIndex = 1;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProductName.ForeColor = System.Drawing.Color.Gray;
            this.lblProductName.Location = new System.Drawing.Point(18, 24);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(104, 20);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Product Name";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 570);
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
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(500, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(615, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Product";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
            this.panelHeader.Size = new System.Drawing.Size(760, 100);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(27, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(650, 24);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Product form";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Product";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // frmProductEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 650);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmProductEditor";
            this.Text = "Product Editor";
            this.Load += new System.EventHandler(this.frmProductEditor_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.groupDescription.ResumeLayout(false);
            this.groupDescription.PerformLayout();
            this.groupPricing.ResumeLayout(false);
            this.groupPricing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSellingPrice)).EndInit();
            this.groupIdentity.ResumeLayout(false);
            this.groupIdentity.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

            }

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblSubtitle;
            private System.Windows.Forms.Panel panelBody;
            private System.Windows.Forms.GroupBox groupIdentity;
            private System.Windows.Forms.Label lblProductName;
            private System.Windows.Forms.TextBox txtProductName;
            private System.Windows.Forms.Label lblSku;
            private System.Windows.Forms.TextBox txtSku;
            private System.Windows.Forms.Label lblBarcode;
            private System.Windows.Forms.TextBox txtBarcode;
            private System.Windows.Forms.Label lblCategory;
            private System.Windows.Forms.ComboBox cmbCategory;
            private System.Windows.Forms.GroupBox groupPricing;
            private System.Windows.Forms.Label lblSellingPrice;
            private System.Windows.Forms.NumericUpDown numSellingPrice;
            private System.Windows.Forms.Label lblUnit;
            private System.Windows.Forms.ComboBox cmbUnit;
            private System.Windows.Forms.CheckBox chkIsActive;
            private System.Windows.Forms.GroupBox groupDescription;
            private System.Windows.Forms.TextBox txtDescription;
            private System.Windows.Forms.Panel panelFooter;
            private System.Windows.Forms.Button btnSave;
            private System.Windows.Forms.Button btnCancel;
            private System.Windows.Forms.Label lblStatus;
            private System.Windows.Forms.ErrorProvider errorProvider;
        }
    } 
