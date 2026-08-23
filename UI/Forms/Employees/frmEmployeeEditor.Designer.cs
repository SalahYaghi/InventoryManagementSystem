namespace UI.Forms.Employees
{
    partial class frmEmployeeEditor
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelFooter;

        private System.Windows.Forms.GroupBox groupEmployee;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Label lblJobTitle;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.Label lblHiringDate;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.TextBox txtPerson;
        private System.Windows.Forms.Button btnSelectPerson;
        private System.Windows.Forms.TextBox txtJobTitle;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.DateTimePicker dtpHiringDate;

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
            this.panelBody = new System.Windows.Forms.Panel();
            this.groupEmployee = new System.Windows.Forms.GroupBox();
            this.lblPerson = new System.Windows.Forms.Label();
            this.txtPerson = new System.Windows.Forms.TextBox();
            this.btnSelectPerson = new System.Windows.Forms.Button();
            this.lblJobTitle = new System.Windows.Forms.Label();
            this.txtJobTitle = new System.Windows.Forms.TextBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.lblHiringDate = new System.Windows.Forms.Label();
            this.dtpHiringDate = new System.Windows.Forms.DateTimePicker();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnShowPersonalData = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.groupEmployee.SuspendLayout();
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
            this.panelRoot.Size = new System.Drawing.Size(760, 511);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.groupEmployee);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 100);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.panelBody.Size = new System.Drawing.Size(760, 331);
            this.panelBody.TabIndex = 0;
            // 
            // groupEmployee
            // 
            this.groupEmployee.BackColor = System.Drawing.Color.White;
            this.groupEmployee.Controls.Add(this.lblPerson);
            this.groupEmployee.Controls.Add(this.txtPerson);
            this.groupEmployee.Controls.Add(this.btnSelectPerson);
            this.groupEmployee.Controls.Add(this.lblJobTitle);
            this.groupEmployee.Controls.Add(this.txtJobTitle);
            this.groupEmployee.Controls.Add(this.lblWarehouse);
            this.groupEmployee.Controls.Add(this.cmbWarehouse);
            this.groupEmployee.Controls.Add(this.lblHiringDate);
            this.groupEmployee.Controls.Add(this.dtpHiringDate);
            this.groupEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupEmployee.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupEmployee.Location = new System.Drawing.Point(24, 20);
            this.groupEmployee.Name = "groupEmployee";
            this.groupEmployee.Size = new System.Drawing.Size(712, 291);
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
            this.lblPerson.Text = "Person *";
            // 
            // txtPerson
            // 
            this.txtPerson.Location = new System.Drawing.Point(24, 65);
            this.txtPerson.Name = "txtPerson";
            this.txtPerson.Size = new System.Drawing.Size(560, 30);
            this.txtPerson.TabIndex = 1;
            // 
            // btnSelectPerson
            // 
            this.btnSelectPerson.Location = new System.Drawing.Point(595, 65);
            this.btnSelectPerson.Name = "btnSelectPerson";
            this.btnSelectPerson.Size = new System.Drawing.Size(42, 30);
            this.btnSelectPerson.TabIndex = 2;
            this.btnSelectPerson.Text = "...";
            this.btnSelectPerson.UseVisualStyleBackColor = false;
            this.btnSelectPerson.Click += new System.EventHandler(this.btnSelectPerson_Click);
            // 
            // lblJobTitle
            // 
            this.lblJobTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJobTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblJobTitle.Location = new System.Drawing.Point(24, 115);
            this.lblJobTitle.Name = "lblJobTitle";
            this.lblJobTitle.Size = new System.Drawing.Size(200, 22);
            this.lblJobTitle.TabIndex = 3;
            this.lblJobTitle.Text = "Job Title *";
            // 
            // txtJobTitle
            // 
            this.txtJobTitle.Location = new System.Drawing.Point(24, 140);
            this.txtJobTitle.Name = "txtJobTitle";
            this.txtJobTitle.Size = new System.Drawing.Size(300, 30);
            this.txtJobTitle.TabIndex = 4;
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehouse.ForeColor = System.Drawing.Color.Gray;
            this.lblWarehouse.Location = new System.Drawing.Point(360, 115);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(200, 22);
            this.lblWarehouse.TabIndex = 5;
            this.lblWarehouse.Text = "Warehouse *";
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.Location = new System.Drawing.Point(360, 140);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(300, 31);
            this.cmbWarehouse.TabIndex = 6;
            // 
            // lblHiringDate
            // 
            this.lblHiringDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHiringDate.ForeColor = System.Drawing.Color.Gray;
            this.lblHiringDate.Location = new System.Drawing.Point(24, 190);
            this.lblHiringDate.Name = "lblHiringDate";
            this.lblHiringDate.Size = new System.Drawing.Size(200, 22);
            this.lblHiringDate.TabIndex = 7;
            this.lblHiringDate.Text = "Hiring Date *";
            // 
            // dtpHiringDate
            // 
            this.dtpHiringDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHiringDate.Location = new System.Drawing.Point(24, 215);
            this.dtpHiringDate.Name = "dtpHiringDate";
            this.dtpHiringDate.Size = new System.Drawing.Size(300, 30);
            this.dtpHiringDate.TabIndex = 8;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnShowPersonalData);
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 431);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(760, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // btnShowPersonalData
            // 
            this.btnShowPersonalData.Location = new System.Drawing.Point(367, 20);
            this.btnShowPersonalData.Name = "btnShowPersonalData";
            this.btnShowPersonalData.Size = new System.Drawing.Size(142, 40);
            this.btnShowPersonalData.TabIndex = 4;
            this.btnShowPersonalData.Text = "Personal Data";
            this.btnShowPersonalData.UseVisualStyleBackColor = false;
            this.btnShowPersonalData.Click += new System.EventHandler(this.btnShowPersonalData_Click);
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
            this.btnSave.Location = new System.Drawing.Point(515, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(630, 20);
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
            this.panelHeader.Size = new System.Drawing.Size(760, 100);
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
            this.lblTitle.Text = "Employee Editor";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(28, 62);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(680, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Create or update employee information.";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // frmEmployeeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 511);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmEmployeeEditor";
            this.Text = "Employee Editor";
            this.Load += new System.EventHandler(this.frmEmployeeEditor_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.groupEmployee.ResumeLayout(false);
            this.groupEmployee.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnShowPersonalData;
    }
}

