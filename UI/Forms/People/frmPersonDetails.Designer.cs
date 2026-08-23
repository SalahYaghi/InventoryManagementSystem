using UI.Forms.Refrences.Documents;

namespace UI.Forms.People
{
 
        partial class frmPersonDetails
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.FlowLayoutPanel flowBody;
            private System.Windows.Forms.Panel panelBasic;
            private System.Windows.Forms.Panel panelContact;
            private System.Windows.Forms.Panel panelAddress;
            private System.Windows.Forms.Panel panelDocument;
            private System.Windows.Forms.Panel panelFooter;

            private System.Windows.Forms.Label lblPersonName;
            private System.Windows.Forms.Label lblPersonSubTitle;
            private System.Windows.Forms.Label lblGenderBadge;

            private System.Windows.Forms.GroupBox groupBasic;
            private System.Windows.Forms.PictureBox picPersonImage;

            private System.Windows.Forms.Label lblNationalNo;
            private System.Windows.Forms.Label lblFullName;
            private System.Windows.Forms.Label lblDateOfBirth;
            private System.Windows.Forms.Label lblGender;

            private System.Windows.Forms.Label lblNationalNoValue;
            private System.Windows.Forms.Label lblFullNameValue;
            private System.Windows.Forms.Label lblDateOfBirthValue;
            private System.Windows.Forms.Label lblGenderValue;

            private UI.Forms.Refrences.Contacts.ctrlContactDetails ctrlContactDetails1;
            private UI.Forms.Refrences.Contacts.ctrlAddressDetails ctrlAddressDetails1;
            private  ctrlDocumentDetails ctrlDocumentDetails1;

            private System.Windows.Forms.Label lblStatus;
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
            this.flowBody = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBasic = new System.Windows.Forms.Panel();
            this.groupBasic = new System.Windows.Forms.GroupBox();
            this.picPersonImage = new System.Windows.Forms.PictureBox();
            this.lblNationalNo = new System.Windows.Forms.Label();
            this.lblNationalNoValue = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblFullNameValue = new System.Windows.Forms.Label();
            this.lblDateOfBirth = new System.Windows.Forms.Label();
            this.lblDateOfBirthValue = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblGenderValue = new System.Windows.Forms.Label();
            this.panelContact = new System.Windows.Forms.Panel();
            this.panelAddress = new System.Windows.Forms.Panel();
            this.panelDocument = new System.Windows.Forms.Panel();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblGenderBadge = new System.Windows.Forms.Label();
            this.lblPersonSubTitle = new System.Windows.Forms.Label();
            this.lblPersonName = new System.Windows.Forms.Label();
            this.lblUpdateImage = new System.Windows.Forms.LinkLabel();
            this.ctrlContactDetails1 = new UI.Forms.Refrences.Contacts.ctrlContactDetails();
            this.ctrlAddressDetails1 = new UI.Forms.Refrences.Contacts.ctrlAddressDetails();
            this.ctrlDocumentDetails1 = new UI.Forms.Refrences.Documents.ctrlDocumentDetails();
            this.panelRoot.SuspendLayout();
            this.flowBody.SuspendLayout();
            this.panelBasic.SuspendLayout();
            this.groupBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonImage)).BeginInit();
            this.panelContact.SuspendLayout();
            this.panelAddress.SuspendLayout();
            this.panelDocument.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelHeader.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(820, 820);
            this.panelRoot.TabIndex = 0;
            // 
            // flowBody
            // 
            this.flowBody.AutoScroll = true;
            this.flowBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.flowBody.Controls.Add(this.panelBasic);
            this.flowBody.Controls.Add(this.panelContact);
            this.flowBody.Controls.Add(this.panelAddress);
            this.flowBody.Controls.Add(this.panelDocument);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBody.Location = new System.Drawing.Point(0, 120);
            this.flowBody.Name = "flowBody";
            this.flowBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.flowBody.Size = new System.Drawing.Size(820, 620);
            this.flowBody.TabIndex = 0;
            this.flowBody.WrapContents = false;
            // 
            // panelBasic
            // 
            this.panelBasic.Controls.Add(this.groupBasic);
            this.panelBasic.Location = new System.Drawing.Point(24, 20);
            this.panelBasic.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelBasic.Name = "panelBasic";
            this.panelBasic.Size = new System.Drawing.Size(760, 216);
            this.panelBasic.TabIndex = 0;
            // 
            // groupBasic
            // 
            this.groupBasic.BackColor = System.Drawing.Color.White;
            this.groupBasic.Controls.Add(this.lblUpdateImage);
            this.groupBasic.Controls.Add(this.picPersonImage);
            this.groupBasic.Controls.Add(this.lblNationalNo);
            this.groupBasic.Controls.Add(this.lblNationalNoValue);
            this.groupBasic.Controls.Add(this.lblFullName);
            this.groupBasic.Controls.Add(this.lblFullNameValue);
            this.groupBasic.Controls.Add(this.lblDateOfBirth);
            this.groupBasic.Controls.Add(this.lblDateOfBirthValue);
            this.groupBasic.Controls.Add(this.lblGender);
            this.groupBasic.Controls.Add(this.lblGenderValue);
            this.groupBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBasic.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupBasic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupBasic.Location = new System.Drawing.Point(0, 0);
            this.groupBasic.Name = "groupBasic";
            this.groupBasic.Size = new System.Drawing.Size(760, 216);
            this.groupBasic.TabIndex = 0;
            this.groupBasic.TabStop = false;
            this.groupBasic.Text = "Personal Details";
            // 
            // picPersonImage
            // 
            this.picPersonImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.picPersonImage.Location = new System.Drawing.Point(575, 42);
            this.picPersonImage.Name = "picPersonImage";
            this.picPersonImage.Size = new System.Drawing.Size(120, 120);
            this.picPersonImage.TabIndex = 0;
            this.picPersonImage.TabStop = false;
            this.picPersonImage.Click += new System.EventHandler(this.picPersonImage_Click);
            // 
            // lblNationalNo
            // 
            this.lblNationalNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNationalNo.ForeColor = System.Drawing.Color.Gray;
            this.lblNationalNo.Location = new System.Drawing.Point(24, 34);
            this.lblNationalNo.Name = "lblNationalNo";
            this.lblNationalNo.Size = new System.Drawing.Size(200, 22);
            this.lblNationalNo.TabIndex = 1;
            this.lblNationalNo.Text = "National No";
            // 
            // lblNationalNoValue
            // 
            this.lblNationalNoValue.Location = new System.Drawing.Point(24, 58);
            this.lblNationalNoValue.Name = "lblNationalNoValue";
            this.lblNationalNoValue.Size = new System.Drawing.Size(245, 30);
            this.lblNationalNoValue.TabIndex = 2;
            this.lblNationalNoValue.Text = "-";
            // 
            // lblFullName
            // 
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFullName.ForeColor = System.Drawing.Color.Gray;
            this.lblFullName.Location = new System.Drawing.Point(300, 34);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(200, 22);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "Full Name";
            // 
            // lblFullNameValue
            // 
            this.lblFullNameValue.Location = new System.Drawing.Point(300, 58);
            this.lblFullNameValue.Name = "lblFullNameValue";
            this.lblFullNameValue.Size = new System.Drawing.Size(245, 30);
            this.lblFullNameValue.TabIndex = 4;
            this.lblFullNameValue.Text = "-";
            // 
            // lblDateOfBirth
            // 
            this.lblDateOfBirth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDateOfBirth.ForeColor = System.Drawing.Color.Gray;
            this.lblDateOfBirth.Location = new System.Drawing.Point(24, 105);
            this.lblDateOfBirth.Name = "lblDateOfBirth";
            this.lblDateOfBirth.Size = new System.Drawing.Size(200, 22);
            this.lblDateOfBirth.TabIndex = 5;
            this.lblDateOfBirth.Text = "Date Of Birth";
            // 
            // lblDateOfBirthValue
            // 
            this.lblDateOfBirthValue.Location = new System.Drawing.Point(24, 129);
            this.lblDateOfBirthValue.Name = "lblDateOfBirthValue";
            this.lblDateOfBirthValue.Size = new System.Drawing.Size(245, 30);
            this.lblDateOfBirthValue.TabIndex = 6;
            this.lblDateOfBirthValue.Text = "-";
            // 
            // lblGender
            // 
            this.lblGender.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGender.ForeColor = System.Drawing.Color.Gray;
            this.lblGender.Location = new System.Drawing.Point(300, 105);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(200, 22);
            this.lblGender.TabIndex = 7;
            this.lblGender.Text = "Gender";
            // 
            // lblGenderValue
            // 
            this.lblGenderValue.Location = new System.Drawing.Point(300, 129);
            this.lblGenderValue.Name = "lblGenderValue";
            this.lblGenderValue.Size = new System.Drawing.Size(245, 30);
            this.lblGenderValue.TabIndex = 8;
            this.lblGenderValue.Text = "-";
            // 
            // panelContact
            // 
            this.panelContact.Controls.Add(this.ctrlContactDetails1);
            this.panelContact.Location = new System.Drawing.Point(24, 250);
            this.panelContact.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelContact.Name = "panelContact";
            this.panelContact.Size = new System.Drawing.Size(760, 246);
            this.panelContact.TabIndex = 1;
            // 
            // panelAddress
            // 
            this.panelAddress.Controls.Add(this.ctrlAddressDetails1);
            this.panelAddress.Location = new System.Drawing.Point(24, 510);
            this.panelAddress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelAddress.Name = "panelAddress";
            this.panelAddress.Size = new System.Drawing.Size(760, 373);
            this.panelAddress.TabIndex = 2;
            // 
            // panelDocument
            // 
            this.panelDocument.Controls.Add(this.ctrlDocumentDetails1);
            this.panelDocument.Location = new System.Drawing.Point(24, 897);
            this.panelDocument.Margin = new System.Windows.Forms.Padding(0, 0, 0, 30);
            this.panelDocument.Name = "panelDocument";
            this.panelDocument.Size = new System.Drawing.Size(760, 156);
            this.panelDocument.TabIndex = 3;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnEdit);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 740);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.panelFooter.Size = new System.Drawing.Size(820, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(24, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(420, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(580, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(105, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(695, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblGenderBadge);
            this.panelHeader.Controls.Add(this.lblPersonSubTitle);
            this.panelHeader.Controls.Add(this.lblPersonName);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
            this.panelHeader.Size = new System.Drawing.Size(820, 120);
            this.panelHeader.TabIndex = 2;
            // 
            // lblGenderBadge
            // 
            this.lblGenderBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblGenderBadge.Location = new System.Drawing.Point(680, 26);
            this.lblGenderBadge.Name = "lblGenderBadge";
            this.lblGenderBadge.Size = new System.Drawing.Size(100, 30);
            this.lblGenderBadge.TabIndex = 0;
            this.lblGenderBadge.Text = "Gender";
            this.lblGenderBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPersonSubTitle
            // 
            this.lblPersonSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPersonSubTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblPersonSubTitle.Location = new System.Drawing.Point(28, 72);
            this.lblPersonSubTitle.Name = "lblPersonSubTitle";
            this.lblPersonSubTitle.Size = new System.Drawing.Size(500, 25);
            this.lblPersonSubTitle.TabIndex = 1;
            this.lblPersonSubTitle.Text = "National No:";
            // 
            // lblPersonName
            // 
            this.lblPersonName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblPersonName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblPersonName.Location = new System.Drawing.Point(24, 22);
            this.lblPersonName.Name = "lblPersonName";
            this.lblPersonName.Size = new System.Drawing.Size(570, 46);
            this.lblPersonName.TabIndex = 2;
            this.lblPersonName.Text = "Person Name";
            // 
            // lblUpdateImage
            // 
            this.lblUpdateImage.ActiveLinkColor = System.Drawing.Color.DarkGray;
            this.lblUpdateImage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUpdateImage.ForeColor = System.Drawing.Color.Gray;
            this.lblUpdateImage.LinkColor = System.Drawing.Color.Gray;
            this.lblUpdateImage.Location = new System.Drawing.Point(24, 180);
            this.lblUpdateImage.Name = "lblUpdateImage";
            this.lblUpdateImage.Size = new System.Drawing.Size(200, 22);
            this.lblUpdateImage.TabIndex = 9;
            this.lblUpdateImage.TabStop = true;
            this.lblUpdateImage.Text = "Click Here To Change Image";
            this.lblUpdateImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUpdateImage_LinkClicked);
            // 
            // ctrlContactDetails1
            // 
            this.ctrlContactDetails1.BackColor = System.Drawing.Color.White;
            this.ctrlContactDetails1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlContactDetails1.Location = new System.Drawing.Point(0, 0);
            this.ctrlContactDetails1.Name = "ctrlContactDetails1";
            this.ctrlContactDetails1.Size = new System.Drawing.Size(760, 244);
            this.ctrlContactDetails1.TabIndex = 0;
            // 
            // ctrlAddressDetails1
            // 
            this.ctrlAddressDetails1.BackColor = System.Drawing.Color.White;
            this.ctrlAddressDetails1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlAddressDetails1.Location = new System.Drawing.Point(0, 0);
            this.ctrlAddressDetails1.Name = "ctrlAddressDetails1";
            this.ctrlAddressDetails1.Size = new System.Drawing.Size(760, 370);
            this.ctrlAddressDetails1.TabIndex = 0;
            // 
            // ctrlDocumentDetails1
            // 
            this.ctrlDocumentDetails1.BackColor = System.Drawing.Color.White;
            this.ctrlDocumentDetails1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlDocumentDetails1.Location = new System.Drawing.Point(0, 0);
            this.ctrlDocumentDetails1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 50);
            this.ctrlDocumentDetails1.Name = "ctrlDocumentDetails1";
            this.ctrlDocumentDetails1.Size = new System.Drawing.Size(760, 150);
            this.ctrlDocumentDetails1.TabIndex = 0;
            // 
            // frmPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 820);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmPersonDetails";
            this.Text = "Person Details";
            this.Load += new System.EventHandler(this.frmPersonDetails_Load);
            this.panelRoot.ResumeLayout(false);
            this.flowBody.ResumeLayout(false);
            this.panelBasic.ResumeLayout(false);
            this.groupBasic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPersonImage)).EndInit();
            this.panelContact.ResumeLayout(false);
            this.panelAddress.ResumeLayout(false);
            this.panelDocument.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        private System.Windows.Forms.LinkLabel lblUpdateImage;
    }
    } 
