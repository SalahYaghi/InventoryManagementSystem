using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Forms.Users
{
    public partial class frmShowUsers
    {
        private Panel panelContent;
        private Panel panelActions;
        private Panel panelFilters;
        private Panel panelFooter;

        private Button btnAdd;
        private Button btnEdit;
        private Button btnView;
        private Button btnResetPassword;

        private Label lblSearch;
        private TextBox txtSearch;

        private Label lblStatus;
        private void InitializeComponent()
        {
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvUsers = new UI.Shared.Controllers.DgvCustom();
            this.cmbOrderBy = new UI.Shared.Controllers.ctrlOrderByCmb();
            this.cmbActive = new UI.Shared.Controllers.ctrlSortByCmb();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelContent.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelContent.Controls.Add(this.dgvUsers);
            this.panelContent.Controls.Add(this.cmbOrderBy);
            this.panelContent.Controls.Add(this.cmbActive);
            this.panelContent.Controls.Add(this.panelFilters);
            this.panelContent.Controls.Add(this.panelActions);
            this.panelContent.Controls.Add(this.panelFooter);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1180, 720);
            this.panelContent.TabIndex = 0;
            // 
            // dgvUsers
            // 
            this.dgvUsers.BackColor = System.Drawing.Color.White;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvUsers.Location = new System.Drawing.Point(0, 156);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(1180, 520);
            this.dgvUsers.TabIndex = 3;
            // 
            // cmbOrderBy
            // 
            this.cmbOrderBy.BackColor = System.Drawing.Color.White;
            this.cmbOrderBy.Location = new System.Drawing.Point(324, 87);
            this.cmbOrderBy.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbOrderBy.Name = "cmbOrderBy";
            this.cmbOrderBy.Size = new System.Drawing.Size(360, 68);
            this.cmbOrderBy.TabIndex = 12;
            // 
            // cmbActive
            // 
            this.cmbActive.BackColor = System.Drawing.Color.White;
            this.cmbActive.Location = new System.Drawing.Point(795, 83);
            this.cmbActive.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cmbActive.Name = "cmbActive";
            this.cmbActive.Size = new System.Drawing.Size(218, 79);
            this.cmbActive.TabIndex = 14;
            this.cmbActive.Title = "Active";
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.White;
            this.panelFilters.Controls.Add(this.lblSearch);
            this.panelFilters.Controls.Add(this.txtSearch);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 74);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1180, 82);
            this.panelFilters.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.Gray;
            this.lblSearch.Location = new System.Drawing.Point(18, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(180, 22);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search users";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(18, 34);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Controls.Add(this.btnRefresh);
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Controls.Add(this.btnEdit);
            this.panelActions.Controls.Add(this.btnView);
            this.panelActions.Controls.Add(this.btnResetPassword);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 0);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1180, 74);
            this.panelActions.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1038, 16);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 42);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(18, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(135, 42);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+ Add User";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(166, 16);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 42);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(298, 16);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(135, 42);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View Details";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Location = new System.Drawing.Point(446, 16);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(155, 42);
            this.btnResetPassword.TabIndex = 3;
            this.btnResetPassword.Text = "Reset Password";
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 676);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1180, 44);
            this.panelFooter.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(18, 11);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(500, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(616, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 42);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmShowUsers
            // 
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmShowUsers";
            this.Text = "Users";
            this.Load += new System.EventHandler(this.frmShowUsers_Load);
            this.panelContent.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Shared.Controllers.DgvCustom dgvUsers;
        private Shared.Controllers.ctrlOrderByCmb cmbOrderBy;
        private Shared.Controllers.ctrlSortByCmb cmbActive;
        private Button btnRefresh;
        private Button btnDelete;
    }
}

