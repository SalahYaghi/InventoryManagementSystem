namespace UI.Forms.Customers
{
    partial class frmCustomerDetails
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.FlowLayoutPanel panelBody;
        private System.Windows.Forms.Panel panelFooter;

        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.Label lblStatus;

        private UI.Forms.References.Contacts.ctrlContactDetails ctrlContactDetails1;
        private UI.Forms.References.Contacts.ctrlAddressDetails ctrlAddressDetails1;

        private System.Windows.Forms.GroupBox groupNotes;
        private System.Windows.Forms.TextBox txtNotes;

        private System.Windows.Forms.Button btnEdit;
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
            this.panelBody = new System.Windows.Forms.FlowLayoutPanel();
            this.groupNotes = new System.Windows.Forms.GroupBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.ctrlAddressDetails1 = new UI.Forms.References.Contacts.ctrlAddressDetails();
            this.ctrlContactDetails1 = new UI.Forms.References.Contacts.ctrlContactDetails();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.groupNotes.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(760, 720);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.groupNotes);
            this.panelBody.Controls.Add(this.ctrlAddressDetails1);
            this.panelBody.Controls.Add(this.ctrlContactDetails1);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelBody.Location = new System.Drawing.Point(0, 120);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24);
            this.panelBody.Size = new System.Drawing.Size(760, 520);
            this.panelBody.TabIndex = 0;
            this.panelBody.WrapContents = false;
            // 
            // groupNotes
            // 
            this.groupNotes.BackColor = System.Drawing.Color.White;
            this.groupNotes.Controls.Add(this.txtNotes);
            this.groupNotes.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupNotes.Location = new System.Drawing.Point(27, 27);
            this.groupNotes.Name = "groupNotes";
            this.groupNotes.Size = new System.Drawing.Size(693, 95);
            this.groupNotes.TabIndex = 0;
            this.groupNotes.TabStop = false;
            this.groupNotes.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.txtNotes.Location = new System.Drawing.Point(22, 32);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ReadOnly = true;
            this.txtNotes.Size = new System.Drawing.Size(646, 42);
            this.txtNotes.TabIndex = 0;
            // 
            // ctrlAddressDetails1
            // 
            this.ctrlAddressDetails1.BackColor = System.Drawing.Color.White;
            this.ctrlAddressDetails1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlAddressDetails1.Location = new System.Drawing.Point(27, 128);
            this.ctrlAddressDetails1.Name = "ctrlAddressDetails1";
            this.ctrlAddressDetails1.Size = new System.Drawing.Size(690, 340);
            this.ctrlAddressDetails1.TabIndex = 1;
            // 
            // ctrlContactDetails1
            // 
            this.ctrlContactDetails1.BackColor = System.Drawing.Color.White;
            this.ctrlContactDetails1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlContactDetails1.Location = new System.Drawing.Point(27, 474);
            this.ctrlContactDetails1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 50);
            this.ctrlContactDetails1.Name = "ctrlContactDetails1";
            this.ctrlContactDetails1.Size = new System.Drawing.Size(690, 230);
            this.ctrlContactDetails1.TabIndex = 2;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnEdit);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 640);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(760, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(24, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(360, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(500, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(105, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(615, 20);
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
            this.panelHeader.Controls.Add(this.lblCustomerCode);
            this.panelHeader.Controls.Add(this.lblCustomerName);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(760, 120);
            this.panelHeader.TabIndex = 2;
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerCode.ForeColor = System.Drawing.Color.Gray;
            this.lblCustomerCode.Location = new System.Drawing.Point(28, 72);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(500, 25);
            this.lblCustomerCode.TabIndex = 1;
            this.lblCustomerCode.Text = "Code:";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblCustomerName.Location = new System.Drawing.Point(24, 22);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(570, 46);
            this.lblCustomerName.TabIndex = 2;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // frmCustomerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 720);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmCustomerDetails";
            this.Text = "Customer Details";
            this.Load += new System.EventHandler(this.frmCustomerDetails_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.groupNotes.ResumeLayout(false);
            this.groupNotes.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

