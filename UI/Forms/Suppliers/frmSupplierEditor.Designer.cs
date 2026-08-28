namespace UI.Forms.Suppliers
{
    
        partial class frmSupplierEditor
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
            this.components = new System.ComponentModel.Container();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelBody = new System.Windows.Forms.FlowLayoutPanel();
            this.groupIdentity = new System.Windows.Forms.GroupBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.lblSupplierCode = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.ctrlContactInfo1 = new UI.Forms.References.Contacts.ctrlContactInfo();
            this.ctrlAddressInfo1 = new UI.Forms.References.Contacts.ctrlAddressInfo();
            this.groupNotes = new System.Windows.Forms.GroupBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
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
            this.groupNotes.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(800, 760);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.groupIdentity);
            this.panelBody.Controls.Add(this.ctrlContactInfo1);
            this.panelBody.Controls.Add(this.ctrlAddressInfo1);
            this.panelBody.Controls.Add(this.groupNotes);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 100);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24);
            this.panelBody.Size = new System.Drawing.Size(800, 580);
            this.panelBody.TabIndex = 1;
            // 
            // groupIdentity
            // 
            this.groupIdentity.BackColor = System.Drawing.Color.White;
            this.groupIdentity.Controls.Add(this.chkStatus);
            this.groupIdentity.Controls.Add(this.txtSupplierCode);
            this.groupIdentity.Controls.Add(this.lblSupplierCode);
            this.groupIdentity.Controls.Add(this.txtSupplierName);
            this.groupIdentity.Controls.Add(this.lblSupplierName);
            this.groupIdentity.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupIdentity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupIdentity.Location = new System.Drawing.Point(27, 27);
            this.groupIdentity.Name = "groupIdentity";
            this.groupIdentity.Padding = new System.Windows.Forms.Padding(18);
            this.groupIdentity.Size = new System.Drawing.Size(728, 125);
            this.groupIdentity.TabIndex = 0;
            this.groupIdentity.TabStop = false;
            this.groupIdentity.Text = "Supplier Information";
            // 
            // chkStatus
            // 
            this.chkStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkStatus.Location = new System.Drawing.Point(585, 63);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(110, 28);
            this.chkStatus.TabIndex = 2;
            this.chkStatus.Text = "Active";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupplierCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSupplierCode.Location = new System.Drawing.Point(340, 62);
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(210, 30);
            this.txtSupplierCode.TabIndex = 1;
            this.txtSupplierCode.TextChanged += new System.EventHandler(this.txtSupplierCode_TextChanged);
            // 
            // lblSupplierCode
            // 
            this.lblSupplierCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSupplierCode.Location = new System.Drawing.Point(340, 35);
            this.lblSupplierCode.Name = "lblSupplierCode";
            this.lblSupplierCode.Size = new System.Drawing.Size(120, 23);
            this.lblSupplierCode.TabIndex = 3;
            this.lblSupplierCode.Text = "Supplier Code";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSupplierName.Location = new System.Drawing.Point(22, 62);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(290, 30);
            this.txtSupplierName.TabIndex = 0;
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSupplierName.Location = new System.Drawing.Point(22, 35);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(120, 23);
            this.lblSupplierName.TabIndex = 4;
            this.lblSupplierName.Text = "Supplier Name";
            // 
            // ctrlContactInfo1
            // 
            this.ctrlContactInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlContactInfo1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlContactInfo1.Location = new System.Drawing.Point(27, 158);
            this.ctrlContactInfo1.Name = "ctrlContactInfo1";
            this.ctrlContactInfo1.Size = new System.Drawing.Size(728, 209);
            this.ctrlContactInfo1.TabIndex = 5;
            this.ctrlContactInfo1.Load += new System.EventHandler(this.ctrlContactInfo1_Load);
            // 
            // ctrlAddressInfo1
            // 
            this.ctrlAddressInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlAddressInfo1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlAddressInfo1.Location = new System.Drawing.Point(27, 373);
            this.ctrlAddressInfo1.Name = "ctrlAddressInfo1";
            this.ctrlAddressInfo1.Size = new System.Drawing.Size(728, 275);
            this.ctrlAddressInfo1.TabIndex = 4;
            // 
            // groupNotes
            // 
            this.groupNotes.BackColor = System.Drawing.Color.White;
            this.groupNotes.Controls.Add(this.txtNotes);
            this.groupNotes.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupNotes.Location = new System.Drawing.Point(27, 654);
            this.groupNotes.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.groupNotes.Name = "groupNotes";
            this.groupNotes.Padding = new System.Windows.Forms.Padding(18);
            this.groupNotes.Size = new System.Drawing.Size(728, 152);
            this.groupNotes.TabIndex = 3;
            this.groupNotes.TabStop = false;
            this.groupNotes.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.Location = new System.Drawing.Point(22, 35);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(678, 96);
            this.txtNotes.TabIndex = 14;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 680);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(800, 80);
            this.panelFooter.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(24, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 24);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Ready";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(656, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(520, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 15;
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
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(27, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(700, 24);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Supplier form";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Supplier";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // frmSupplierEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 760);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmSupplierEditor";
            this.Text = "Supplier Editor";
            this.Load += new System.EventHandler(this.frmSupplierEditor_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.groupIdentity.ResumeLayout(false);
            this.groupIdentity.PerformLayout();
            this.groupNotes.ResumeLayout(false);
            this.groupNotes.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

            }

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblSubtitle;
            private System.Windows.Forms.FlowLayoutPanel panelBody;
            private System.Windows.Forms.GroupBox groupIdentity;
            private System.Windows.Forms.Label lblSupplierName;
            private System.Windows.Forms.TextBox txtSupplierName;
            private System.Windows.Forms.Label lblSupplierCode;
            private System.Windows.Forms.TextBox txtSupplierCode;
            private System.Windows.Forms.CheckBox chkStatus;
            private System.Windows.Forms.Panel panelFooter;
            private System.Windows.Forms.Button btnSave;
            private System.Windows.Forms.Button btnCancel;
            private System.Windows.Forms.Label lblStatus;
            private System.Windows.Forms.ErrorProvider errorProvider;
        private References.Contacts.ctrlContactInfo ctrlContactInfo1;
        private References.Contacts.ctrlAddressInfo ctrlAddressInfo1;
        private System.Windows.Forms.GroupBox groupNotes;
        private System.Windows.Forms.TextBox txtNotes;
    }
     
}
