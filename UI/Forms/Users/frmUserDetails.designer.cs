using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI.Forms.Users
{
    public partial class frmUserDetails
    {
        private Panel panelRoot;
        private Panel panelHeader;
        private Panel panelBody;
        private Panel panelFooter;

        private Label lblUserName;
        private Label lblSubTitle;
        private Label lblStatusBadge;
        private Label lblStatus;

        private GroupBox groupUser;

        private Label lblUsernameValue;
        private Label lblEmailValue;
        private Label lblRoleValue;
        private Label lblEmployeeValue;
        private Label lblJobTitleValue;
        private Label lblWarehouseValue;
        private Label lblLastLoginValue;

        private Button btnEdit;
        private Button btnResetPassword;
        private Button btnClose;

        private void InitializeComponent()
        {
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnEmployeeData = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelBody = new System.Windows.Forms.Panel();
            this.groupUser = new System.Windows.Forms.GroupBox();
            this.lblUsernameCaption = new System.Windows.Forms.Label();
            this.lblUsernameValue = new System.Windows.Forms.Label();
            this.lblEmailCaption = new System.Windows.Forms.Label();
            this.lblEmailValue = new System.Windows.Forms.Label();
            this.lblRoleCaption = new System.Windows.Forms.Label();
            this.lblRoleValue = new System.Windows.Forms.Label();
            this.lblEmployeeCaption = new System.Windows.Forms.Label();
            this.lblEmployeeValue = new System.Windows.Forms.Label();
            this.lblJobTitleCaption = new System.Windows.Forms.Label();
            this.lblJobTitleValue = new System.Windows.Forms.Label();
            this.lblWarehouseCaption = new System.Windows.Forms.Label();
            this.lblWarehouseValue = new System.Windows.Forms.Label();
            this.lblLastLoginCaption = new System.Windows.Forms.Label();
            this.lblLastLoginValue = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.groupUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelRoot.Controls.Add(this.panelBody);
            this.panelRoot.Controls.Add(this.panelHeader);
            this.panelRoot.Controls.Add(this.panelFooter);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Size = new System.Drawing.Size(793, 598);
            this.panelRoot.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblUserName);
            this.panelHeader.Controls.Add(this.lblSubTitle);
            this.panelHeader.Controls.Add(this.lblStatusBadge);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(793, 120);
            this.panelHeader.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblUserName.Location = new System.Drawing.Point(24, 22);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(520, 46);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Username";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTitle.Location = new System.Drawing.Point(28, 72);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(520, 25);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "User details";
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.Location = new System.Drawing.Point(620, 26);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(100, 30);
            this.lblStatusBadge.TabIndex = 2;
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnEmployeeData);
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnEdit);
            this.panelFooter.Controls.Add(this.btnResetPassword);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 518);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(793, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // btnEmployeeData
            // 
            this.btnEmployeeData.Location = new System.Drawing.Point(296, 20);
            this.btnEmployeeData.Name = "btnEmployeeData";
            this.btnEmployeeData.Size = new System.Drawing.Size(139, 40);
            this.btnEmployeeData.TabIndex = 4;
            this.btnEmployeeData.Text = "Employee Data";
            this.btnEmployeeData.Click += new System.EventHandler(this.btnEmployeeData_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(24, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(280, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(441, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(95, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Location = new System.Drawing.Point(546, 20);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(120, 40);
            this.btnResetPassword.TabIndex = 2;
            this.btnResetPassword.Text = "Password";
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(676, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.groupUser);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 120);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.panelBody.Size = new System.Drawing.Size(793, 398);
            this.panelBody.TabIndex = 2;
            // 
            // groupUser
            // 
            this.groupUser.BackColor = System.Drawing.Color.White;
            this.groupUser.Controls.Add(this.lblUsernameCaption);
            this.groupUser.Controls.Add(this.lblUsernameValue);
            this.groupUser.Controls.Add(this.lblEmailCaption);
            this.groupUser.Controls.Add(this.lblEmailValue);
            this.groupUser.Controls.Add(this.lblRoleCaption);
            this.groupUser.Controls.Add(this.lblRoleValue);
            this.groupUser.Controls.Add(this.lblEmployeeCaption);
            this.groupUser.Controls.Add(this.lblEmployeeValue);
            this.groupUser.Controls.Add(this.lblJobTitleCaption);
            this.groupUser.Controls.Add(this.lblJobTitleValue);
            this.groupUser.Controls.Add(this.lblWarehouseCaption);
            this.groupUser.Controls.Add(this.lblWarehouseValue);
            this.groupUser.Controls.Add(this.lblLastLoginCaption);
            this.groupUser.Controls.Add(this.lblLastLoginValue);
            this.groupUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupUser.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupUser.Location = new System.Drawing.Point(24, 20);
            this.groupUser.Name = "groupUser";
            this.groupUser.Size = new System.Drawing.Size(745, 358);
            this.groupUser.TabIndex = 0;
            this.groupUser.TabStop = false;
            this.groupUser.Text = "User Account Information";
            // 
            // lblUsernameCaption
            // 
            this.lblUsernameCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsernameCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblUsernameCaption.Location = new System.Drawing.Point(24, 40);
            this.lblUsernameCaption.Name = "lblUsernameCaption";
            this.lblUsernameCaption.Size = new System.Drawing.Size(300, 22);
            this.lblUsernameCaption.TabIndex = 0;
            this.lblUsernameCaption.Text = "Username";
            // 
            // lblUsernameValue
            // 
            this.lblUsernameValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblUsernameValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsernameValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblUsernameValue.Location = new System.Drawing.Point(24, 65);
            this.lblUsernameValue.Name = "lblUsernameValue";
            this.lblUsernameValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblUsernameValue.Size = new System.Drawing.Size(300, 30);
            this.lblUsernameValue.TabIndex = 1;
            this.lblUsernameValue.Text = "-";
            this.lblUsernameValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEmailCaption
            // 
            this.lblEmailCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmailCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblEmailCaption.Location = new System.Drawing.Point(360, 40);
            this.lblEmailCaption.Name = "lblEmailCaption";
            this.lblEmailCaption.Size = new System.Drawing.Size(300, 22);
            this.lblEmailCaption.TabIndex = 2;
            this.lblEmailCaption.Text = "Email";
            // 
            // lblEmailValue
            // 
            this.lblEmailValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblEmailValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmailValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblEmailValue.Location = new System.Drawing.Point(360, 65);
            this.lblEmailValue.Name = "lblEmailValue";
            this.lblEmailValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblEmailValue.Size = new System.Drawing.Size(300, 30);
            this.lblEmailValue.TabIndex = 3;
            this.lblEmailValue.Text = "-";
            this.lblEmailValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRoleCaption
            // 
            this.lblRoleCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRoleCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblRoleCaption.Location = new System.Drawing.Point(24, 115);
            this.lblRoleCaption.Name = "lblRoleCaption";
            this.lblRoleCaption.Size = new System.Drawing.Size(300, 22);
            this.lblRoleCaption.TabIndex = 4;
            this.lblRoleCaption.Text = "Role";
            // 
            // lblRoleValue
            // 
            this.lblRoleValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblRoleValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoleValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblRoleValue.Location = new System.Drawing.Point(24, 140);
            this.lblRoleValue.Name = "lblRoleValue";
            this.lblRoleValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblRoleValue.Size = new System.Drawing.Size(300, 30);
            this.lblRoleValue.TabIndex = 5;
            this.lblRoleValue.Text = "-";
            this.lblRoleValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEmployeeCaption
            // 
            this.lblEmployeeCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmployeeCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblEmployeeCaption.Location = new System.Drawing.Point(360, 115);
            this.lblEmployeeCaption.Name = "lblEmployeeCaption";
            this.lblEmployeeCaption.Size = new System.Drawing.Size(300, 22);
            this.lblEmployeeCaption.TabIndex = 6;
            this.lblEmployeeCaption.Text = "Employee";
            // 
            // lblEmployeeValue
            // 
            this.lblEmployeeValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblEmployeeValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmployeeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblEmployeeValue.Location = new System.Drawing.Point(360, 140);
            this.lblEmployeeValue.Name = "lblEmployeeValue";
            this.lblEmployeeValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblEmployeeValue.Size = new System.Drawing.Size(300, 30);
            this.lblEmployeeValue.TabIndex = 7;
            this.lblEmployeeValue.Text = "-";
            this.lblEmployeeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblJobTitleCaption
            // 
            this.lblJobTitleCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJobTitleCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblJobTitleCaption.Location = new System.Drawing.Point(24, 190);
            this.lblJobTitleCaption.Name = "lblJobTitleCaption";
            this.lblJobTitleCaption.Size = new System.Drawing.Size(300, 22);
            this.lblJobTitleCaption.TabIndex = 8;
            this.lblJobTitleCaption.Text = "Job Title";
            // 
            // lblJobTitleValue
            // 
            this.lblJobTitleValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblJobTitleValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblJobTitleValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblJobTitleValue.Location = new System.Drawing.Point(24, 215);
            this.lblJobTitleValue.Name = "lblJobTitleValue";
            this.lblJobTitleValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblJobTitleValue.Size = new System.Drawing.Size(300, 30);
            this.lblJobTitleValue.TabIndex = 9;
            this.lblJobTitleValue.Text = "-";
            this.lblJobTitleValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWarehouseCaption
            // 
            this.lblWarehouseCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehouseCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblWarehouseCaption.Location = new System.Drawing.Point(360, 190);
            this.lblWarehouseCaption.Name = "lblWarehouseCaption";
            this.lblWarehouseCaption.Size = new System.Drawing.Size(300, 22);
            this.lblWarehouseCaption.TabIndex = 10;
            this.lblWarehouseCaption.Text = "Warehouse";
            // 
            // lblWarehouseValue
            // 
            this.lblWarehouseValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblWarehouseValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWarehouseValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblWarehouseValue.Location = new System.Drawing.Point(360, 215);
            this.lblWarehouseValue.Name = "lblWarehouseValue";
            this.lblWarehouseValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblWarehouseValue.Size = new System.Drawing.Size(300, 30);
            this.lblWarehouseValue.TabIndex = 11;
            this.lblWarehouseValue.Text = "-";
            this.lblWarehouseValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastLoginCaption
            // 
            this.lblLastLoginCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLastLoginCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblLastLoginCaption.Location = new System.Drawing.Point(24, 265);
            this.lblLastLoginCaption.Name = "lblLastLoginCaption";
            this.lblLastLoginCaption.Size = new System.Drawing.Size(636, 22);
            this.lblLastLoginCaption.TabIndex = 12;
            this.lblLastLoginCaption.Text = "Last Login";
            // 
            // lblLastLoginValue
            // 
            this.lblLastLoginValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblLastLoginValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLastLoginValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblLastLoginValue.Location = new System.Drawing.Point(24, 290);
            this.lblLastLoginValue.Name = "lblLastLoginValue";
            this.lblLastLoginValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblLastLoginValue.Size = new System.Drawing.Size(636, 30);
            this.lblLastLoginValue.TabIndex = 13;
            this.lblLastLoginValue.Text = "-";
            this.lblLastLoginValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmUserDetails
            // 
            this.ClientSize = new System.Drawing.Size(793, 598);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Details";
            this.Load += new System.EventHandler(this.frmUserDetails_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.groupUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Button btnEmployeeData;
        private Label lblUsernameCaption;
        private Label lblEmailCaption;
        private Label lblRoleCaption;
        private Label lblEmployeeCaption;
        private Label lblJobTitleCaption;
        private Label lblWarehouseCaption;
        private Label lblLastLoginCaption;
    }
}
