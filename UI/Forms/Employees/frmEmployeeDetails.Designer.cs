namespace UI.Forms.Employees
{
    partial class frmEmployeeDetails
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelFooter;

        private System.Windows.Forms.Label lblEmployeeName;
        private System.Windows.Forms.Label lblEmployeeSubTitle;

        private System.Windows.Forms.GroupBox groupEmployee;

        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Label lblNationalNo;
        private System.Windows.Forms.Label lblJobTitle;
        private System.Windows.Forms.Label lblHiringDate;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.Label lblWarehouseCode;

        private System.Windows.Forms.Label lblPersonValue;
        private System.Windows.Forms.Label lblNationalNoValue;
        private System.Windows.Forms.Label lblJobTitleValue;
        private System.Windows.Forms.Label lblHiringDateValue;
        private System.Windows.Forms.Label lblWarehouseValue;
        private System.Windows.Forms.Label lblWarehouseCodeValue;

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
            this.panelBody = new System.Windows.Forms.Panel();
            this.groupEmployee = new System.Windows.Forms.GroupBox();
            this.lblPerson = new System.Windows.Forms.Label();
            this.lblPersonValue = new System.Windows.Forms.Label();
            this.lblNationalNo = new System.Windows.Forms.Label();
            this.lblNationalNoValue = new System.Windows.Forms.Label();
            this.lblJobTitle = new System.Windows.Forms.Label();
            this.lblJobTitleValue = new System.Windows.Forms.Label();
            this.lblHiringDate = new System.Windows.Forms.Label();
            this.lblHiringDateValue = new System.Windows.Forms.Label();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.lblWarehouseValue = new System.Windows.Forms.Label();
            this.lblWarehouseCode = new System.Windows.Forms.Label();
            this.lblWarehouseCodeValue = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblEmployeeName = new System.Windows.Forms.Label();
            this.lblEmployeeSubTitle = new System.Windows.Forms.Label();
            this.btnShowPersonalData = new System.Windows.Forms.Button();
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.groupEmployee.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(760, 511);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.groupEmployee);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 120);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.panelBody.Size = new System.Drawing.Size(760, 311);
            this.panelBody.TabIndex = 0;
            // 
            // groupEmployee
            // 
            this.groupEmployee.BackColor = System.Drawing.Color.White;
            this.groupEmployee.Controls.Add(this.lblPerson);
            this.groupEmployee.Controls.Add(this.lblPersonValue);
            this.groupEmployee.Controls.Add(this.lblNationalNo);
            this.groupEmployee.Controls.Add(this.lblNationalNoValue);
            this.groupEmployee.Controls.Add(this.lblJobTitle);
            this.groupEmployee.Controls.Add(this.lblJobTitleValue);
            this.groupEmployee.Controls.Add(this.lblHiringDate);
            this.groupEmployee.Controls.Add(this.lblHiringDateValue);
            this.groupEmployee.Controls.Add(this.lblWarehouse);
            this.groupEmployee.Controls.Add(this.lblWarehouseValue);
            this.groupEmployee.Controls.Add(this.lblWarehouseCode);
            this.groupEmployee.Controls.Add(this.lblWarehouseCodeValue);
            this.groupEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupEmployee.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupEmployee.Location = new System.Drawing.Point(24, 20);
            this.groupEmployee.Name = "groupEmployee";
            this.groupEmployee.Size = new System.Drawing.Size(712, 271);
            this.groupEmployee.TabIndex = 0;
            this.groupEmployee.TabStop = false;
            this.groupEmployee.Text = "Employee Information";
            // 
            // lblPerson
            // 
            this.lblPerson.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPerson.ForeColor = System.Drawing.Color.Gray;
            this.lblPerson.Location = new System.Drawing.Point(24, 40);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(200, 22);
            this.lblPerson.TabIndex = 0;
            this.lblPerson.Text = "Person";
            // 
            // lblPersonValue
            // 
            this.lblPersonValue.Location = new System.Drawing.Point(24, 65);
            this.lblPersonValue.Name = "lblPersonValue";
            this.lblPersonValue.Size = new System.Drawing.Size(300, 30);
            this.lblPersonValue.TabIndex = 1;
            this.lblPersonValue.Text = "-";
            // 
            // lblNationalNo
            // 
            this.lblNationalNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNationalNo.ForeColor = System.Drawing.Color.Gray;
            this.lblNationalNo.Location = new System.Drawing.Point(360, 40);
            this.lblNationalNo.Name = "lblNationalNo";
            this.lblNationalNo.Size = new System.Drawing.Size(200, 22);
            this.lblNationalNo.TabIndex = 2;
            this.lblNationalNo.Text = "National No";
            // 
            // lblNationalNoValue
            // 
            this.lblNationalNoValue.Location = new System.Drawing.Point(360, 65);
            this.lblNationalNoValue.Name = "lblNationalNoValue";
            this.lblNationalNoValue.Size = new System.Drawing.Size(300, 30);
            this.lblNationalNoValue.TabIndex = 3;
            this.lblNationalNoValue.Text = "-";
            // 
            // lblJobTitle
            // 
            this.lblJobTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJobTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblJobTitle.Location = new System.Drawing.Point(24, 115);
            this.lblJobTitle.Name = "lblJobTitle";
            this.lblJobTitle.Size = new System.Drawing.Size(200, 22);
            this.lblJobTitle.TabIndex = 4;
            this.lblJobTitle.Text = "Job Title";
            // 
            // lblJobTitleValue
            // 
            this.lblJobTitleValue.Location = new System.Drawing.Point(24, 140);
            this.lblJobTitleValue.Name = "lblJobTitleValue";
            this.lblJobTitleValue.Size = new System.Drawing.Size(300, 30);
            this.lblJobTitleValue.TabIndex = 5;
            this.lblJobTitleValue.Text = "-";
            // 
            // lblHiringDate
            // 
            this.lblHiringDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHiringDate.ForeColor = System.Drawing.Color.Gray;
            this.lblHiringDate.Location = new System.Drawing.Point(360, 115);
            this.lblHiringDate.Name = "lblHiringDate";
            this.lblHiringDate.Size = new System.Drawing.Size(200, 22);
            this.lblHiringDate.TabIndex = 6;
            this.lblHiringDate.Text = "Hiring Date";
            // 
            // lblHiringDateValue
            // 
            this.lblHiringDateValue.Location = new System.Drawing.Point(360, 140);
            this.lblHiringDateValue.Name = "lblHiringDateValue";
            this.lblHiringDateValue.Size = new System.Drawing.Size(300, 30);
            this.lblHiringDateValue.TabIndex = 7;
            this.lblHiringDateValue.Text = "-";
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehouse.ForeColor = System.Drawing.Color.Gray;
            this.lblWarehouse.Location = new System.Drawing.Point(24, 190);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(200, 22);
            this.lblWarehouse.TabIndex = 8;
            this.lblWarehouse.Text = "Warehouse";
            // 
            // lblWarehouseValue
            // 
            this.lblWarehouseValue.Location = new System.Drawing.Point(24, 215);
            this.lblWarehouseValue.Name = "lblWarehouseValue";
            this.lblWarehouseValue.Size = new System.Drawing.Size(300, 30);
            this.lblWarehouseValue.TabIndex = 9;
            this.lblWarehouseValue.Text = "-";
            // 
            // lblWarehouseCode
            // 
            this.lblWarehouseCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehouseCode.ForeColor = System.Drawing.Color.Gray;
            this.lblWarehouseCode.Location = new System.Drawing.Point(360, 190);
            this.lblWarehouseCode.Name = "lblWarehouseCode";
            this.lblWarehouseCode.Size = new System.Drawing.Size(200, 22);
            this.lblWarehouseCode.TabIndex = 10;
            this.lblWarehouseCode.Text = "Warehouse Code";
            // 
            // lblWarehouseCodeValue
            // 
            this.lblWarehouseCodeValue.Location = new System.Drawing.Point(360, 215);
            this.lblWarehouseCodeValue.Name = "lblWarehouseCodeValue";
            this.lblWarehouseCodeValue.Size = new System.Drawing.Size(300, 30);
            this.lblWarehouseCodeValue.TabIndex = 11;
            this.lblWarehouseCodeValue.Text = "-";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnShowPersonalData);
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnEdit);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 431);
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
            this.btnEdit.Location = new System.Drawing.Point(515, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(105, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(630, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblEmployeeName);
            this.panelHeader.Controls.Add(this.lblEmployeeSubTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(760, 120);
            this.panelHeader.TabIndex = 2;
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblEmployeeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblEmployeeName.Location = new System.Drawing.Point(24, 22);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(570, 46);
            this.lblEmployeeName.TabIndex = 0;
            this.lblEmployeeName.Text = "Employee Name";
            // 
            // lblEmployeeSubTitle
            // 
            this.lblEmployeeSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmployeeSubTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblEmployeeSubTitle.Location = new System.Drawing.Point(28, 72);
            this.lblEmployeeSubTitle.Name = "lblEmployeeSubTitle";
            this.lblEmployeeSubTitle.Size = new System.Drawing.Size(500, 25);
            this.lblEmployeeSubTitle.TabIndex = 1;
            this.lblEmployeeSubTitle.Text = "Job Title:";
            // 
            // btnShowPersonalData
            // 
            this.btnShowPersonalData.Location = new System.Drawing.Point(367, 19);
            this.btnShowPersonalData.Name = "btnShowPersonalData";
            this.btnShowPersonalData.Size = new System.Drawing.Size(142, 40);
            this.btnShowPersonalData.TabIndex = 3;
            this.btnShowPersonalData.Text = "Personal Data";
            this.btnShowPersonalData.UseVisualStyleBackColor = false;
            this.btnShowPersonalData.Click += new System.EventHandler(this.btnShowPersonalData_Click);
            // 
            // frmEmployeeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 511);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmEmployeeDetails";
            this.Text = "Employee Details";
            this.Load += new System.EventHandler(this.frmEmployeeDetails_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.groupEmployee.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnShowPersonalData;
    }
}

