namespace UI.Forms.Warehouses
{
        partial class frmWarehouseEditor
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.FlowLayoutPanel panelBody;
            private System.Windows.Forms.Panel panelFooter;

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblSubtitle;

            private System.Windows.Forms.GroupBox groupIdentity;
            private System.Windows.Forms.Label lblWarehouseName;
            private System.Windows.Forms.TextBox txtWarehouseName;
            private System.Windows.Forms.Label lblWarehouseCode;
            private System.Windows.Forms.TextBox txtWarehouseCode;
            private System.Windows.Forms.CheckBox chkStatus;

            private UI.Forms.References.Contacts.ctrlAddressInfo ctrlAddressInfo1;

            private System.Windows.Forms.Label lblStatus;
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
            this.panelBody = new System.Windows.Forms.FlowLayoutPanel();
            this.groupIdentity = new System.Windows.Forms.GroupBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.txtWarehouseCode = new System.Windows.Forms.TextBox();
            this.lblWarehouseCode = new System.Windows.Forms.Label();
            this.txtWarehouseName = new System.Windows.Forms.TextBox();
            this.lblWarehouseName = new System.Windows.Forms.Label();
            this.ctrlAddressInfo1 = new UI.Forms.References.Contacts.ctrlAddressInfo();
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
            this.panelRoot.Size = new System.Drawing.Size(800, 635);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.groupIdentity);
            this.panelBody.Controls.Add(this.ctrlAddressInfo1);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 100);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24);
            this.panelBody.Size = new System.Drawing.Size(800, 455);
            this.panelBody.TabIndex = 0;
            // 
            // groupIdentity
            // 
            this.groupIdentity.BackColor = System.Drawing.Color.White;
            this.groupIdentity.Controls.Add(this.chkStatus);
            this.groupIdentity.Controls.Add(this.txtWarehouseCode);
            this.groupIdentity.Controls.Add(this.lblWarehouseCode);
            this.groupIdentity.Controls.Add(this.txtWarehouseName);
            this.groupIdentity.Controls.Add(this.lblWarehouseName);
            this.groupIdentity.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupIdentity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupIdentity.Location = new System.Drawing.Point(27, 27);
            this.groupIdentity.Name = "groupIdentity";
            this.groupIdentity.Padding = new System.Windows.Forms.Padding(18);
            this.groupIdentity.Size = new System.Drawing.Size(728, 125);
            this.groupIdentity.TabIndex = 0;
            this.groupIdentity.TabStop = false;
            this.groupIdentity.Text = "Warehouse Information";
            // 
            // chkStatus
            // 
            this.chkStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkStatus.Location = new System.Drawing.Point(585, 63);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(110, 28);
            this.chkStatus.TabIndex = 0;
            this.chkStatus.Text = "Active";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // txtWarehouseCode
            // 
            this.txtWarehouseCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWarehouseCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWarehouseCode.Location = new System.Drawing.Point(340, 62);
            this.txtWarehouseCode.Name = "txtWarehouseCode";
            this.txtWarehouseCode.Size = new System.Drawing.Size(210, 30);
            this.txtWarehouseCode.TabIndex = 1;
            // 
            // lblWarehouseCode
            // 
            this.lblWarehouseCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehouseCode.Location = new System.Drawing.Point(340, 35);
            this.lblWarehouseCode.Name = "lblWarehouseCode";
            this.lblWarehouseCode.Size = new System.Drawing.Size(140, 23);
            this.lblWarehouseCode.TabIndex = 2;
            this.lblWarehouseCode.Text = "Warehouse Code";
            // 
            // txtWarehouseName
            // 
            this.txtWarehouseName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWarehouseName.Location = new System.Drawing.Point(22, 62);
            this.txtWarehouseName.Name = "txtWarehouseName";
            this.txtWarehouseName.Size = new System.Drawing.Size(290, 30);
            this.txtWarehouseName.TabIndex = 0;
            // 
            // lblWarehouseName
            // 
            this.lblWarehouseName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehouseName.Location = new System.Drawing.Point(22, 35);
            this.lblWarehouseName.Name = "lblWarehouseName";
            this.lblWarehouseName.Size = new System.Drawing.Size(150, 23);
            this.lblWarehouseName.TabIndex = 3;
            this.lblWarehouseName.Text = "Warehouse Name";
            // 
            // ctrlAddressInfo1
            // 
            this.ctrlAddressInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlAddressInfo1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlAddressInfo1.Location = new System.Drawing.Point(27, 158);
            this.ctrlAddressInfo1.Name = "ctrlAddressInfo1";
            this.ctrlAddressInfo1.Size = new System.Drawing.Size(728, 275);
            this.ctrlAddressInfo1.TabIndex = 2;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 555);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(800, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(24, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 24);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(656, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(520, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
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
            this.panelHeader.Size = new System.Drawing.Size(800, 100);
            this.panelHeader.TabIndex = 2;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(27, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(700, 24);
            this.lblSubtitle.TabIndex = 0;
            this.lblSubtitle.Text = "Warehouse form";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 42);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Warehouse";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // frmWarehouseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 635);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmWarehouseEditor";
            this.Text = "Warehouse Editor";
            this.Load += new System.EventHandler(this.frmWarehouseEditor_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.groupIdentity.ResumeLayout(false);
            this.groupIdentity.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

            }
        }
    }
