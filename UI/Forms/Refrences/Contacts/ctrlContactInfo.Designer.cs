namespace UI.Forms.Refrences.Contacts
{
 
        partial class ctrlContactInfo
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.GroupBox groupContact;
            private System.Windows.Forms.Label lblEmail;
            private System.Windows.Forms.Label lblPhoneNumber;
            private System.Windows.Forms.Label lblAlternativePhoneNumber;
            private System.Windows.Forms.Label lblFaxNumber;
            private System.Windows.Forms.Label lblWebsiteUrl;
            private System.Windows.Forms.Label lblStatus;

            private System.Windows.Forms.TextBox txtEmail;
            private System.Windows.Forms.TextBox txtPhoneNumber;
            private System.Windows.Forms.TextBox txtAlternativePhoneNumber;
            private System.Windows.Forms.TextBox txtFaxNumber;
            private System.Windows.Forms.TextBox txtWebsiteUrl;

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
            this.groupContact = new System.Windows.Forms.GroupBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblAlternativePhoneNumber = new System.Windows.Forms.Label();
            this.txtAlternativePhoneNumber = new System.Windows.Forms.TextBox();
            this.lblFaxNumber = new System.Windows.Forms.Label();
            this.txtFaxNumber = new System.Windows.Forms.TextBox();
            this.lblWebsiteUrl = new System.Windows.Forms.Label();
            this.txtWebsiteUrl = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupContact.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupContact
            // 
            this.groupContact.BackColor = System.Drawing.Color.White;
            this.groupContact.Controls.Add(this.lblEmail);
            this.groupContact.Controls.Add(this.txtEmail);
            this.groupContact.Controls.Add(this.lblPhoneNumber);
            this.groupContact.Controls.Add(this.txtPhoneNumber);
            this.groupContact.Controls.Add(this.lblAlternativePhoneNumber);
            this.groupContact.Controls.Add(this.txtAlternativePhoneNumber);
            this.groupContact.Controls.Add(this.lblFaxNumber);
            this.groupContact.Controls.Add(this.txtFaxNumber);
            this.groupContact.Controls.Add(this.lblWebsiteUrl);
            this.groupContact.Controls.Add(this.txtWebsiteUrl);
            this.groupContact.Controls.Add(this.lblStatus);
            this.groupContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupContact.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupContact.Location = new System.Drawing.Point(0, 0);
            this.groupContact.Name = "groupContact";
            this.groupContact.Padding = new System.Windows.Forms.Padding(18);
            this.groupContact.Size = new System.Drawing.Size(720, 209);
            this.groupContact.TabIndex = 0;
            this.groupContact.TabStop = false;
            this.groupContact.Text = "Contact Information";
            this.groupContact.Enter += new System.EventHandler(this.groupContact_Enter);
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.Gray;
            this.lblEmail.Location = new System.Drawing.Point(21, 32);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(280, 22);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email Address *";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(21, 55);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(310, 30);
            this.txtEmail.TabIndex = 0;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhoneNumber.ForeColor = System.Drawing.Color.Gray;
            this.lblPhoneNumber.Location = new System.Drawing.Point(369, 32);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(280, 22);
            this.lblPhoneNumber.TabIndex = 1;
            this.lblPhoneNumber.Text = "Phone Number *";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(369, 55);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(310, 30);
            this.txtPhoneNumber.TabIndex = 1;
            this.txtPhoneNumber.TextChanged += new System.EventHandler(this.txtPhoneNumber_TextChanged);
            // 
            // lblAlternativePhoneNumber
            // 
            this.lblAlternativePhoneNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAlternativePhoneNumber.ForeColor = System.Drawing.Color.Gray;
            this.lblAlternativePhoneNumber.Location = new System.Drawing.Point(21, 83);
            this.lblAlternativePhoneNumber.Name = "lblAlternativePhoneNumber";
            this.lblAlternativePhoneNumber.Size = new System.Drawing.Size(280, 22);
            this.lblAlternativePhoneNumber.TabIndex = 2;
            this.lblAlternativePhoneNumber.Text = "Alternative Phone";
            // 
            // txtAlternativePhoneNumber
            // 
            this.txtAlternativePhoneNumber.Location = new System.Drawing.Point(21, 105);
            this.txtAlternativePhoneNumber.Name = "txtAlternativePhoneNumber";
            this.txtAlternativePhoneNumber.Size = new System.Drawing.Size(310, 30);
            this.txtAlternativePhoneNumber.TabIndex = 2;
            // 
            // lblFaxNumber
            // 
            this.lblFaxNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFaxNumber.ForeColor = System.Drawing.Color.Gray;
            this.lblFaxNumber.Location = new System.Drawing.Point(369, 83);
            this.lblFaxNumber.Name = "lblFaxNumber";
            this.lblFaxNumber.Size = new System.Drawing.Size(280, 22);
            this.lblFaxNumber.TabIndex = 3;
            this.lblFaxNumber.Text = "Fax Number";
            // 
            // txtFaxNumber
            // 
            this.txtFaxNumber.Location = new System.Drawing.Point(369, 105);
            this.txtFaxNumber.Name = "txtFaxNumber";
            this.txtFaxNumber.Size = new System.Drawing.Size(310, 30);
            this.txtFaxNumber.TabIndex = 3;
            // 
            // lblWebsiteUrl
            // 
            this.lblWebsiteUrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWebsiteUrl.ForeColor = System.Drawing.Color.Gray;
            this.lblWebsiteUrl.Location = new System.Drawing.Point(21, 133);
            this.lblWebsiteUrl.Name = "lblWebsiteUrl";
            this.lblWebsiteUrl.Size = new System.Drawing.Size(280, 22);
            this.lblWebsiteUrl.TabIndex = 4;
            this.lblWebsiteUrl.Text = "Website URL";
            // 
            // txtWebsiteUrl
            // 
            this.txtWebsiteUrl.Location = new System.Drawing.Point(21, 156);
            this.txtWebsiteUrl.Name = "txtWebsiteUrl";
            this.txtWebsiteUrl.Size = new System.Drawing.Size(470, 30);
            this.txtWebsiteUrl.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(509, 154);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(170, 25);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Contact information";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ctrlContactInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupContact);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ctrlContactInfo";
            this.Size = new System.Drawing.Size(720, 209);
            this.groupContact.ResumeLayout(false);
            this.groupContact.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

            }
        }
    } 
