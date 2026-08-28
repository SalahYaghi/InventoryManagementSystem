namespace UI.Forms.References.Contacts
{
  
        partial class ctrlAddressInfo
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.GroupBox groupAddress;

            private System.Windows.Forms.Label lblPostalCode;
            private System.Windows.Forms.Label lblBuildingNumber;
            private System.Windows.Forms.Label lblStreet;
            private System.Windows.Forms.Label lblDescription;
            private System.Windows.Forms.Label lblStatus;

            private System.Windows.Forms.TextBox txtStreet;
            private System.Windows.Forms.TextBox txtBuildingNumber;
            private System.Windows.Forms.TextBox txtPostalCode;
            private System.Windows.Forms.TextBox txtDescription;

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
            this.groupAddress = new System.Windows.Forms.GroupBox();
            this.ctrCountryCitySelector1 = new UI.Forms.References.Countries.ctrCountryCitySelector();
            this.lblPostalCode = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.lblBuildingNumber = new System.Windows.Forms.Label();
            this.txtBuildingNumber = new System.Windows.Forms.TextBox();
            this.lblStreet = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupAddress
            // 
            this.groupAddress.BackColor = System.Drawing.Color.White;
            this.groupAddress.Controls.Add(this.ctrCountryCitySelector1);
            this.groupAddress.Controls.Add(this.lblPostalCode);
            this.groupAddress.Controls.Add(this.txtStreet);
            this.groupAddress.Controls.Add(this.lblBuildingNumber);
            this.groupAddress.Controls.Add(this.txtBuildingNumber);
            this.groupAddress.Controls.Add(this.lblStreet);
            this.groupAddress.Controls.Add(this.txtPostalCode);
            this.groupAddress.Controls.Add(this.lblDescription);
            this.groupAddress.Controls.Add(this.txtDescription);
            this.groupAddress.Controls.Add(this.lblStatus);
            this.groupAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupAddress.Location = new System.Drawing.Point(0, 0);
            this.groupAddress.Name = "groupAddress";
            this.groupAddress.Padding = new System.Windows.Forms.Padding(18);
            this.groupAddress.Size = new System.Drawing.Size(691, 282);
            this.groupAddress.TabIndex = 0;
            this.groupAddress.TabStop = false;
            this.groupAddress.Text = "Address Information";
            this.groupAddress.Enter += new System.EventHandler(this.groupAddress_Enter);
            // 
            // ctrCountryCitySelector1
            // 
            this.ctrCountryCitySelector1.Location = new System.Drawing.Point(16, 25);
            this.ctrCountryCitySelector1.Margin = new System.Windows.Forms.Padding(6);
            this.ctrCountryCitySelector1.Name = "ctrCountryCitySelector1";
            this.ctrCountryCitySelector1.Size = new System.Drawing.Size(634, 53);
            this.ctrCountryCitySelector1.TabIndex = 0;
            this.ctrCountryCitySelector1.Load += new System.EventHandler(this.ctrCountryCitySelector1_Load);
            // 
            // lblPostalCode
            // 
            this.lblPostalCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPostalCode.ForeColor = System.Drawing.Color.Gray;
            this.lblPostalCode.Location = new System.Drawing.Point(26, 132);
            this.lblPostalCode.Name = "lblPostalCode";
            this.lblPostalCode.Size = new System.Drawing.Size(280, 22);
            this.lblPostalCode.TabIndex = 1;
            this.lblPostalCode.Text = "Postal Code";
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(27, 100);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(310, 30);
            this.txtStreet.TabIndex = 1;
            // 
            // lblBuildingNumber
            // 
            this.lblBuildingNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBuildingNumber.ForeColor = System.Drawing.Color.Gray;
            this.lblBuildingNumber.Location = new System.Drawing.Point(370, 78);
            this.lblBuildingNumber.Name = "lblBuildingNumber";
            this.lblBuildingNumber.Size = new System.Drawing.Size(280, 22);
            this.lblBuildingNumber.TabIndex = 2;
            this.lblBuildingNumber.Text = "Building Number";
            // 
            // txtBuildingNumber
            // 
            this.txtBuildingNumber.Location = new System.Drawing.Point(370, 100);
            this.txtBuildingNumber.Name = "txtBuildingNumber";
            this.txtBuildingNumber.Size = new System.Drawing.Size(310, 30);
            this.txtBuildingNumber.TabIndex = 2;
            this.txtBuildingNumber.TextChanged += new System.EventHandler(this.txtBuildingNumber_TextChanged);
            // 
            // lblStreet
            // 
            this.lblStreet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStreet.ForeColor = System.Drawing.Color.Gray;
            this.lblStreet.Location = new System.Drawing.Point(22, 78);
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.Size = new System.Drawing.Size(280, 22);
            this.lblStreet.TabIndex = 3;
            this.lblStreet.Text = "Street";
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.Location = new System.Drawing.Point(26, 154);
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(654, 30);
            this.txtPostalCode.TabIndex = 3;
            this.txtPostalCode.TextChanged += new System.EventHandler(this.txtStreet_TextChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescription.ForeColor = System.Drawing.Color.Gray;
            this.lblDescription.Location = new System.Drawing.Point(23, 187);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(280, 22);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(27, 209);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(470, 45);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(510, 222);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(170, 25);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Address information";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ctrlAddressInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupAddress);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ctrlAddressInfo";
            this.Size = new System.Drawing.Size(691, 282);
            this.groupAddress.ResumeLayout(false);
            this.groupAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

            }

        private Countries.ctrCountryCitySelector ctrCountryCitySelector1;
    } 
}

