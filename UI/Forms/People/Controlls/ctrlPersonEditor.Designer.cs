using UI.Forms.Refrences.Documents;

namespace UI.Forms.People.Controlls
{ 
        partial class ctrlPersonEditor
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.FlowLayoutPanel flowBody;

            private System.Windows.Forms.Panel panelBasic;
            private System.Windows.Forms.Panel panelContact;
            private System.Windows.Forms.Panel panelAddress;

            private System.Windows.Forms.GroupBox groupBasic;

            private System.Windows.Forms.Label lblNationalNo;
            private System.Windows.Forms.Label lblFirstName;
            private System.Windows.Forms.Label lblSecondName;
            private System.Windows.Forms.Label lblThirdName;
            private System.Windows.Forms.Label lblLastName;
            private System.Windows.Forms.Label lblGender;
            private System.Windows.Forms.Label lblDateOfBirth;
            private System.Windows.Forms.Label lblStatus;

            private System.Windows.Forms.TextBox txtNationalNo;
            private System.Windows.Forms.TextBox txtFirstName;
            private System.Windows.Forms.TextBox txtSecondName;
            private System.Windows.Forms.TextBox txtThirdName;
            private System.Windows.Forms.TextBox txtLastName;

            private System.Windows.Forms.ComboBox cmbGender;
            private System.Windows.Forms.DateTimePicker dtpDateOfBirth;

            private UI.Forms.Refrences.Contacts.ctrlContactInfo ctrlContactInfo1;
            private UI.Forms.Refrences.Contacts.ctrlAddressInfo ctrlAddressInfo1;

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
            this.flowBody = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBasic = new System.Windows.Forms.Panel();
            this.groupBasic = new System.Windows.Forms.GroupBox();
            this.lblNationalNo = new System.Windows.Forms.Label();
            this.txtNationalNo = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblSecondName = new System.Windows.Forms.Label();
            this.txtSecondName = new System.Windows.Forms.TextBox();
            this.lblThirdName = new System.Windows.Forms.Label();
            this.txtThirdName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblDateOfBirth = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelContact = new System.Windows.Forms.Panel();
            this.panelAddress = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ctrlContactInfo1 = new UI.Forms.Refrences.Contacts.ctrlContactInfo();
            this.ctrlAddressInfo1 = new UI.Forms.Refrences.Contacts.ctrlAddressInfo();
            this.flowBody.SuspendLayout();
            this.panelBasic.SuspendLayout();
            this.groupBasic.SuspendLayout();
            this.panelContact.SuspendLayout();
            this.panelAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // flowBody
            // 
            this.flowBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.flowBody.Controls.Add(this.panelBasic);
            this.flowBody.Controls.Add(this.panelContact);
            this.flowBody.Controls.Add(this.panelAddress);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBody.Location = new System.Drawing.Point(0, 0);
            this.flowBody.Name = "flowBody";
            this.flowBody.Size = new System.Drawing.Size(760, 769);
            this.flowBody.TabIndex = 0;
            this.flowBody.WrapContents = false;
            // 
            // panelBasic
            // 
            this.panelBasic.Controls.Add(this.groupBasic);
            this.panelBasic.Location = new System.Drawing.Point(0, 0);
            this.panelBasic.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelBasic.Name = "panelBasic";
            this.panelBasic.Size = new System.Drawing.Size(740, 230);
            this.panelBasic.TabIndex = 0;
            // 
            // groupBasic
            // 
            this.groupBasic.BackColor = System.Drawing.Color.White;
            this.groupBasic.Controls.Add(this.lblNationalNo);
            this.groupBasic.Controls.Add(this.txtNationalNo);
            this.groupBasic.Controls.Add(this.lblFirstName);
            this.groupBasic.Controls.Add(this.txtFirstName);
            this.groupBasic.Controls.Add(this.lblSecondName);
            this.groupBasic.Controls.Add(this.txtSecondName);
            this.groupBasic.Controls.Add(this.lblThirdName);
            this.groupBasic.Controls.Add(this.txtThirdName);
            this.groupBasic.Controls.Add(this.lblLastName);
            this.groupBasic.Controls.Add(this.txtLastName);
            this.groupBasic.Controls.Add(this.lblGender);
            this.groupBasic.Controls.Add(this.cmbGender);
            this.groupBasic.Controls.Add(this.lblDateOfBirth);
            this.groupBasic.Controls.Add(this.dtpDateOfBirth);
            this.groupBasic.Controls.Add(this.lblStatus);
            this.groupBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBasic.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupBasic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupBasic.Location = new System.Drawing.Point(0, 0);
            this.groupBasic.Name = "groupBasic";
            this.groupBasic.Size = new System.Drawing.Size(740, 230);
            this.groupBasic.TabIndex = 0;
            this.groupBasic.TabStop = false;
            this.groupBasic.Text = "Personal Information";
            // 
            // lblNationalNo
            // 
            this.lblNationalNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNationalNo.ForeColor = System.Drawing.Color.Gray;
            this.lblNationalNo.Location = new System.Drawing.Point(24, 34);
            this.lblNationalNo.Name = "lblNationalNo";
            this.lblNationalNo.Size = new System.Drawing.Size(200, 22);
            this.lblNationalNo.TabIndex = 0;
            this.lblNationalNo.Text = "National No *";
            // 
            // txtNationalNo
            // 
            this.txtNationalNo.Location = new System.Drawing.Point(24, 58);
            this.txtNationalNo.Name = "txtNationalNo";
            this.txtNationalNo.Size = new System.Drawing.Size(310, 30);
            this.txtNationalNo.TabIndex = 0;
            // 
            // lblFirstName
            // 
            this.lblFirstName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFirstName.ForeColor = System.Drawing.Color.Gray;
            this.lblFirstName.Location = new System.Drawing.Point(370, 34);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(200, 22);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "First Name *";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(370, 58);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(310, 30);
            this.txtFirstName.TabIndex = 1;
            // 
            // lblSecondName
            // 
            this.lblSecondName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSecondName.ForeColor = System.Drawing.Color.Gray;
            this.lblSecondName.Location = new System.Drawing.Point(24, 96);
            this.lblSecondName.Name = "lblSecondName";
            this.lblSecondName.Size = new System.Drawing.Size(200, 22);
            this.lblSecondName.TabIndex = 2;
            this.lblSecondName.Text = "Second Name *";
            // 
            // txtSecondName
            // 
            this.txtSecondName.Location = new System.Drawing.Point(24, 120);
            this.txtSecondName.Name = "txtSecondName";
            this.txtSecondName.Size = new System.Drawing.Size(310, 30);
            this.txtSecondName.TabIndex = 2;
            // 
            // lblThirdName
            // 
            this.lblThirdName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThirdName.ForeColor = System.Drawing.Color.Gray;
            this.lblThirdName.Location = new System.Drawing.Point(370, 96);
            this.lblThirdName.Name = "lblThirdName";
            this.lblThirdName.Size = new System.Drawing.Size(200, 22);
            this.lblThirdName.TabIndex = 3;
            this.lblThirdName.Text = "Third Name";
            // 
            // txtThirdName
            // 
            this.txtThirdName.Location = new System.Drawing.Point(370, 120);
            this.txtThirdName.Name = "txtThirdName";
            this.txtThirdName.Size = new System.Drawing.Size(310, 30);
            this.txtThirdName.TabIndex = 3;
            // 
            // lblLastName
            // 
            this.lblLastName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLastName.ForeColor = System.Drawing.Color.Gray;
            this.lblLastName.Location = new System.Drawing.Point(24, 158);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(200, 22);
            this.lblLastName.TabIndex = 4;
            this.lblLastName.Text = "Last Name *";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(24, 182);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(210, 30);
            this.txtLastName.TabIndex = 4;
            // 
            // lblGender
            // 
            this.lblGender.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGender.ForeColor = System.Drawing.Color.Gray;
            this.lblGender.Location = new System.Drawing.Point(260, 158);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(120, 22);
            this.lblGender.TabIndex = 5;
            this.lblGender.Text = "Gender *";
            // 
            // cmbGender
            // 
            this.cmbGender.Location = new System.Drawing.Point(260, 182);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(160, 31);
            this.cmbGender.TabIndex = 5;
            // 
            // lblDateOfBirth
            // 
            this.lblDateOfBirth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDateOfBirth.ForeColor = System.Drawing.Color.Gray;
            this.lblDateOfBirth.Location = new System.Drawing.Point(450, 158);
            this.lblDateOfBirth.Name = "lblDateOfBirth";
            this.lblDateOfBirth.Size = new System.Drawing.Size(150, 22);
            this.lblDateOfBirth.TabIndex = 6;
            this.lblDateOfBirth.Text = "Date Of Birth *";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDateOfBirth.Location = new System.Drawing.Point(450, 182);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(230, 30);
            this.dtpDateOfBirth.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(480, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(200, 22);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Person information";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelContact
            // 
            this.panelContact.Controls.Add(this.ctrlContactInfo1);
            this.panelContact.Location = new System.Drawing.Point(0, 244);
            this.panelContact.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelContact.Name = "panelContact";
            this.panelContact.Size = new System.Drawing.Size(740, 208);
            this.panelContact.TabIndex = 1;
            // 
            // panelAddress
            // 
            this.panelAddress.Controls.Add(this.ctrlAddressInfo1);
            this.panelAddress.Location = new System.Drawing.Point(0, 466);
            this.panelAddress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelAddress.Name = "panelAddress";
            this.panelAddress.Size = new System.Drawing.Size(740, 282);
            this.panelAddress.TabIndex = 2;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ctrlContactInfo1
            // 
            this.ctrlContactInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlContactInfo1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlContactInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrlContactInfo1.Name = "ctrlContactInfo1";
            this.ctrlContactInfo1.Size = new System.Drawing.Size(740, 203);
            this.ctrlContactInfo1.TabIndex = 1;
            // 
            // ctrlAddressInfo1
            // 
            this.ctrlAddressInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlAddressInfo1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlAddressInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrlAddressInfo1.Name = "ctrlAddressInfo1";
            this.ctrlAddressInfo1.Size = new System.Drawing.Size(740, 282);
            this.ctrlAddressInfo1.TabIndex = 2;
            // 
            // ctrlPersonEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.flowBody);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ctrlPersonEditor";
            this.Size = new System.Drawing.Size(760, 769);
            this.flowBody.ResumeLayout(false);
            this.panelBasic.ResumeLayout(false);
            this.groupBasic.ResumeLayout(false);
            this.groupBasic.PerformLayout();
            this.panelContact.ResumeLayout(false);
            this.panelAddress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

            }
        }
    }
