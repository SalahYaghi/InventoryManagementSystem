namespace UI.Forms.Refrences.Contacts
{
    partial class ctrlContactDetails
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupContact;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAlternativePhone;
        private System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.Label lblWebsite;

        private System.Windows.Forms.Label lblEmailValue;
        private System.Windows.Forms.Label lblPhoneValue;
        private System.Windows.Forms.Label lblAlternativePhoneValue;
        private System.Windows.Forms.Label lblFaxValue;
        private System.Windows.Forms.Label lblWebsiteValue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupContact = new System.Windows.Forms.GroupBox();

            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAlternativePhone = new System.Windows.Forms.Label();
            this.lblFax = new System.Windows.Forms.Label();
            this.lblWebsite = new System.Windows.Forms.Label();

            this.lblEmailValue = new System.Windows.Forms.Label();
            this.lblPhoneValue = new System.Windows.Forms.Label();
            this.lblAlternativePhoneValue = new System.Windows.Forms.Label();
            this.lblFaxValue = new System.Windows.Forms.Label();
            this.lblWebsiteValue = new System.Windows.Forms.Label();

            this.groupContact.SuspendLayout();
            this.SuspendLayout();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupContact);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ctrlContactDetails";
            this.Size = new System.Drawing.Size(690, 220);

            this.groupContact.BackColor = System.Drawing.Color.White;
            this.groupContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupContact.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupContact.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
            this.groupContact.Text = "Contact Details";
            this.groupContact.Controls.Add(this.lblEmail);
            this.groupContact.Controls.Add(this.lblEmailValue);
            this.groupContact.Controls.Add(this.lblPhone);
            this.groupContact.Controls.Add(this.lblPhoneValue);
            this.groupContact.Controls.Add(this.lblAlternativePhone);
            this.groupContact.Controls.Add(this.lblAlternativePhoneValue);
            this.groupContact.Controls.Add(this.lblFax);
            this.groupContact.Controls.Add(this.lblFaxValue);
            this.groupContact.Controls.Add(this.lblWebsite);
            this.groupContact.Controls.Add(this.lblWebsiteValue);

            this.lblEmail.Text = "Email";
            this.lblEmail.ForeColor = System.Drawing.Color.Gray;
            this.lblEmail.Location = new System.Drawing.Point(22, 35);
            this.lblEmail.Size = new System.Drawing.Size(200, 22);

            this.lblEmailValue.Location = new System.Drawing.Point(22, 58);
            this.lblEmailValue.Size = new System.Drawing.Size(300, 30);
            this.lblEmailValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblPhone.Text = "Phone Number";
            this.lblPhone.ForeColor = System.Drawing.Color.Gray;
            this.lblPhone.Location = new System.Drawing.Point(360, 35);
            this.lblPhone.Size = new System.Drawing.Size(200, 22);

            this.lblPhoneValue.Location = new System.Drawing.Point(360, 58);
            this.lblPhoneValue.Size = new System.Drawing.Size(300, 30);
            this.lblPhoneValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblAlternativePhone.Text = "Alternative Phone";
            this.lblAlternativePhone.ForeColor = System.Drawing.Color.Gray;
            this.lblAlternativePhone.Location = new System.Drawing.Point(22, 100);
            this.lblAlternativePhone.Size = new System.Drawing.Size(200, 22);

            this.lblAlternativePhoneValue.Location = new System.Drawing.Point(22, 123);
            this.lblAlternativePhoneValue.Size = new System.Drawing.Size(300, 30);
            this.lblAlternativePhoneValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblFax.Text = "Fax Number";
            this.lblFax.ForeColor = System.Drawing.Color.Gray;
            this.lblFax.Location = new System.Drawing.Point(360, 100);
            this.lblFax.Size = new System.Drawing.Size(200, 22);

            this.lblFaxValue.Location = new System.Drawing.Point(360, 123);
            this.lblFaxValue.Size = new System.Drawing.Size(300, 30);
            this.lblFaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblWebsite.Text = "Website";
            this.lblWebsite.ForeColor = System.Drawing.Color.Gray;
            this.lblWebsite.Location = new System.Drawing.Point(22, 165);
            this.lblWebsite.Size = new System.Drawing.Size(200, 22);

            this.lblWebsiteValue.Location = new System.Drawing.Point(22, 188);
            this.lblWebsiteValue.Size = new System.Drawing.Size(638, 30);
            this.lblWebsiteValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.groupContact.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

