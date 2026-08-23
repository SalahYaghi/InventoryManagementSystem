using UI.Forms.Refrences.Contacts;

namespace UI.Forms.Customers
{
    partial class frmCustomerEditor
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.FlowLayoutPanel flowBody;
        private System.Windows.Forms.Panel panelFooter;

        private System.Windows.Forms.GroupBox groupBasic;
        private System.Windows.Forms.GroupBox groupNotes;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.CheckBox chkStatus;

        private ctrlContactInfo ctrlContactInfo1;

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
            this.flowBody = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBasic = new System.Windows.Forms.GroupBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.ctrlContactInfo1 = new UI.Forms.Refrences.Contacts.ctrlContactInfo();
            this.groupNotes = new System.Windows.Forms.GroupBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ctrlAddressInfo1 = new UI.Forms.Refrences.Contacts.ctrlAddressInfo();
            this.panelRoot.SuspendLayout();
            this.flowBody.SuspendLayout();
            this.groupBasic.SuspendLayout();
            this.groupNotes.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(810, 790);
            this.panelRoot.TabIndex = 0;
            // 
            // flowBody
            // 
            this.flowBody.AutoScroll = true;
            this.flowBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.flowBody.Controls.Add(this.ctrlContactInfo1);
            this.flowBody.Controls.Add(this.ctrlAddressInfo1);
            this.flowBody.Controls.Add(this.groupNotes);
            this.flowBody.Controls.Add(this.groupBasic);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBody.Location = new System.Drawing.Point(0, 100);
            this.flowBody.Name = "flowBody";
            this.flowBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.flowBody.Size = new System.Drawing.Size(810, 610);
            this.flowBody.TabIndex = 0;
            // 
            // groupBasic
            // 
            this.groupBasic.BackColor = System.Drawing.Color.White;
            this.groupBasic.Controls.Add(this.lblCustomerName);
            this.groupBasic.Controls.Add(this.txtCustomerName);
            this.groupBasic.Controls.Add(this.lblCustomerCode);
            this.groupBasic.Controls.Add(this.txtCustomerCode);
            this.groupBasic.Controls.Add(this.chkStatus);
            this.groupBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBasic.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupBasic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupBasic.Location = new System.Drawing.Point(27, 653);
            this.groupBasic.Name = "groupBasic";
            this.groupBasic.Size = new System.Drawing.Size(740, 0);
            this.groupBasic.TabIndex = 0;
            this.groupBasic.TabStop = false;
            this.groupBasic.Text = "Customer Information";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerName.ForeColor = System.Drawing.Color.Gray;
            this.lblCustomerName.Location = new System.Drawing.Point(24, 36);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(200, 22);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Customer Name *";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(24, 62);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(310, 30);
            this.txtCustomerName.TabIndex = 1;
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerCode.ForeColor = System.Drawing.Color.Gray;
            this.lblCustomerCode.Location = new System.Drawing.Point(370, 36);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(200, 22);
            this.lblCustomerCode.TabIndex = 2;
            this.lblCustomerCode.Text = "Customer Code *";
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(370, 62);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(310, 30);
            this.txtCustomerCode.TabIndex = 3;
            // 
            // chkStatus
            // 
            this.chkStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.chkStatus.Location = new System.Drawing.Point(24, 105);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(150, 30);
            this.chkStatus.TabIndex = 4;
            this.chkStatus.Text = "Active";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // ctrlContactInfo1
            // 
            this.ctrlContactInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlContactInfo1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlContactInfo1.Location = new System.Drawing.Point(27, 23);
            this.ctrlContactInfo1.Name = "ctrlContactInfo1";
            this.ctrlContactInfo1.Size = new System.Drawing.Size(720, 200);
            this.ctrlContactInfo1.TabIndex = 0;
            // 
            // groupNotes
            // 
            this.groupNotes.BackColor = System.Drawing.Color.White;
            this.groupNotes.Controls.Add(this.lblNotes);
            this.groupNotes.Controls.Add(this.txtNotes);
            this.groupNotes.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupNotes.Location = new System.Drawing.Point(27, 450);
            this.groupNotes.Margin = new System.Windows.Forms.Padding(3, 3, 3, 70);
            this.groupNotes.Name = "groupNotes";
            this.groupNotes.Size = new System.Drawing.Size(720, 130);
            this.groupNotes.TabIndex = 0;
            this.groupNotes.TabStop = false;
            this.groupNotes.Text = "Notes";
            // 
            // lblNotes
            // 
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNotes.ForeColor = System.Drawing.Color.Gray;
            this.lblNotes.Location = new System.Drawing.Point(24, 34);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(200, 22);
            this.lblNotes.TabIndex = 0;
            this.lblNotes.Text = "Optional Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(24, 58);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(656, 55);
            this.txtNotes.TabIndex = 1;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 710);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(810, 80);
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(560, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(675, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 40);
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
            this.panelHeader.Size = new System.Drawing.Size(810, 100);
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
            this.lblTitle.Text = "Customer Editor";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(28, 62);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(720, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Create or update customer information.";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ctrlAddressInfo1
            // 
            this.ctrlAddressInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlAddressInfo1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlAddressInfo1.Location = new System.Drawing.Point(27, 184);
            this.ctrlAddressInfo1.Name = "ctrlAddressInfo1";
            this.ctrlAddressInfo1.Size = new System.Drawing.Size(720, 260);
            this.ctrlAddressInfo1.TabIndex = 0;
            // 
            // frmCustomerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 790);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmCustomerEditor";
            this.Text = "Customer Editor";
            this.Load += new System.EventHandler(this.frmCustomerEditor_Load);
            this.panelRoot.ResumeLayout(false);
            this.flowBody.ResumeLayout(false);
            this.groupBasic.ResumeLayout(false);
            this.groupBasic.PerformLayout();
            this.groupNotes.ResumeLayout(false);
            this.groupNotes.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        private ctrlAddressInfo ctrlAddressInfo1;
    }
}

