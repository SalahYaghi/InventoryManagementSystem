using UI.Shared.Controllers;

namespace UI.Forms.Adjustments
{
    partial class frmShowAdjustments
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelContent;
        private DgvCustomPaginated dgvAdjustments;

        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;

        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;

        private ctrlOrderByCmb cmbOrderBy;
        private ctrlSortByCmb cmbAdjustmentStatus;
        private ctrlSortByCmb cmbAdjustmentReason;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvAdjustments = new UI.Shared.Controllers.DgvCustomPaginated();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.cmbOrderBy = new UI.Shared.Controllers.ctrlOrderByCmb();
            this.cmbAdjustmentStatus = new UI.Shared.Controllers.ctrlSortByCmb();
            this.cmbAdjustmentReason = new UI.Shared.Controllers.ctrlSortByCmb();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.panelContent.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();

            // panelContent
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(243, 246, 249);
            this.panelContent.Controls.Add(this.dgvAdjustments);
            this.panelContent.Controls.Add(this.panelFilters);
            this.panelContent.Controls.Add(this.panelActions);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1180, 720);

            // dgvAdjustments
            this.dgvAdjustments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdjustments.Location = new System.Drawing.Point(0, 146);
            this.dgvAdjustments.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvAdjustments.Name = "dgvAdjustments";
            this.dgvAdjustments.Size = new System.Drawing.Size(1180, 574);

            // panelFilters
            this.panelFilters.BackColor = System.Drawing.Color.White;
            this.panelFilters.Controls.Add(this.cmbOrderBy);
            this.panelFilters.Controls.Add(this.cmbAdjustmentStatus);
            this.panelFilters.Controls.Add(this.cmbAdjustmentReason);
            this.panelFilters.Controls.Add(this.lblSearch);
            this.panelFilters.Controls.Add(this.txtSearch);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 74);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1180, 72);

            // lblSearch
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.Gray;
            this.lblSearch.Location = new System.Drawing.Point(18, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(180, 20);
            this.lblSearch.Text = "Search Adjustments";

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(18, 33);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(290, 27);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // cmbOrderBy
            this.cmbOrderBy.BackColor = System.Drawing.Color.White;
            this.cmbOrderBy.Location = new System.Drawing.Point(412, 15);
            this.cmbOrderBy.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbOrderBy.Name = "cmbOrderBy";
            this.cmbOrderBy.Size = new System.Drawing.Size(318, 68);

            // cmbAdjustmentStatus
            this.cmbAdjustmentStatus.BackColor = System.Drawing.Color.White;
            this.cmbAdjustmentStatus.Location = new System.Drawing.Point(736, 11);
            this.cmbAdjustmentStatus.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbAdjustmentStatus.Name = "cmbAdjustmentStatus";
            this.cmbAdjustmentStatus.Size = new System.Drawing.Size(208, 79);
            this.cmbAdjustmentStatus.Title = "Status";

            // cmbAdjustmentReason
            this.cmbAdjustmentReason.BackColor = System.Drawing.Color.White;
            this.cmbAdjustmentReason.Location = new System.Drawing.Point(950, 11);
            this.cmbAdjustmentReason.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbAdjustmentReason.Name = "cmbAdjustmentReason";
            this.cmbAdjustmentReason.Size = new System.Drawing.Size(208, 79);
            this.cmbAdjustmentReason.Title = "Reason";

            // panelActions
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Controls.Add(this.btnEdit);
            this.panelActions.Controls.Add(this.btnView);
            this.panelActions.Controls.Add(this.btnCancel);
            this.panelActions.Controls.Add(this.btnApprove);
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Controls.Add(this.btnRefresh);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 0);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1180, 74);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(18, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(170, 42);
            this.btnAdd.Text = "+ Add Adjustment";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(206, 16);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(115, 42);
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnView
            this.btnView.Location = new System.Drawing.Point(339, 16);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(130, 42);
            this.btnView.Text = "View Details";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(487, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 42);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // btnApprove
            this.btnApprove.Location = new System.Drawing.Point(628, 16);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(130, 42);
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(770, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(115, 42);
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnRefresh
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1032, 16);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 42);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // frmShowAdjustments
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 246, 249);
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmShowAdjustments";
            this.Text = "Adjustments";
            this.Load += new System.EventHandler(this.frmShowAdjustments_Load);

            this.panelContent.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

