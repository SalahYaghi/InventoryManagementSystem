using UI.Shared.Controllers;

namespace UI.Forms.Orders
{ 
        partial class frmShowOrders
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelContent;
            private DgvCustomPaginated dgvOrders;

            private System.Windows.Forms.Panel panelFilters;
            private System.Windows.Forms.TextBox txtSearch;
            private System.Windows.Forms.Label lblSearch;

            private System.Windows.Forms.Panel panelActions;
            private System.Windows.Forms.Button btnAdd;
            private System.Windows.Forms.Button btnEdit;
            private System.Windows.Forms.Button btnView;
            private System.Windows.Forms.Button btnCancel;
            private System.Windows.Forms.Button btnDelete;
            private System.Windows.Forms.Button btnRefresh;

            protected override void Dispose(bool disposing)
            {
                if (disposing && components != null)
                    components.Dispose();

                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvOrders = new UI.Shared.Controllers.DgvCustomPaginated();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.cmbOrderBy = new UI.Shared.Controllers.ctrlOrderByCmb();
            this.cmbOrderStatus = new UI.Shared.Controllers.ctrlSortByCmb();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCompeleted = new System.Windows.Forms.Button();
            this.panelContent.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelContent.Controls.Add(this.dgvOrders);
            this.panelContent.Controls.Add(this.panelFilters);
            this.panelContent.Controls.Add(this.panelActions);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1180, 720);
            this.panelContent.TabIndex = 0;
            // 
            // dgvOrders
            // 
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(0, 146);
            this.dgvOrders.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(1180, 574);
            this.dgvOrders.TabIndex = 0;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.White;
            this.panelFilters.Controls.Add(this.cmbOrderBy);
            this.panelFilters.Controls.Add(this.cmbOrderStatus);
            this.panelFilters.Controls.Add(this.lblSearch);
            this.panelFilters.Controls.Add(this.txtSearch);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 74);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1180, 72);
            this.panelFilters.TabIndex = 1;
            // 
            // cmbOrderBy
            // 
            this.cmbOrderBy.BackColor = System.Drawing.Color.White;
            this.cmbOrderBy.Location = new System.Drawing.Point(412, 15);
            this.cmbOrderBy.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbOrderBy.Name = "cmbOrderBy";
            this.cmbOrderBy.Size = new System.Drawing.Size(318, 68);
            this.cmbOrderBy.TabIndex = 8;
            // 
            // cmbOrderStatus
            // 
            this.cmbOrderStatus.BackColor = System.Drawing.Color.White;
            this.cmbOrderStatus.Location = new System.Drawing.Point(736, 11);
            this.cmbOrderStatus.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbOrderStatus.Name = "cmbOrderStatus";
            this.cmbOrderStatus.Size = new System.Drawing.Size(208, 79);
            this.cmbOrderStatus.TabIndex = 9;
            this.cmbOrderStatus.Title = "Order Status";
            // 
            // lblSearch
            // 
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.Gray;
            this.lblSearch.Location = new System.Drawing.Point(18, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(160, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search Orders";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(18, 33);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(290, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.btnCompeleted);
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Controls.Add(this.btnEdit);
            this.panelActions.Controls.Add(this.btnView);
            this.panelActions.Controls.Add(this.btnCancel);
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Controls.Add(this.btnRefresh);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 0);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1180, 74);
            this.panelActions.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(18, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 42);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+ Add Order";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(186, 16);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(115, 42);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(319, 16);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(130, 42);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View Details";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(467, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 42);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(750, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(115, 42);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1032, 16);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 42);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCompeleted
            // 
            this.btnCompeleted.Location = new System.Drawing.Point(608, 16);
            this.btnCompeleted.Name = "btnCompeleted";
            this.btnCompeleted.Size = new System.Drawing.Size(130, 42);
            this.btnCompeleted.TabIndex = 6;
            this.btnCompeleted.Text = "Compelete";
            this.btnCompeleted.UseVisualStyleBackColor = false;
            this.btnCompeleted.Click += new System.EventHandler(this.btnCompeleted_Click);
            // 
            // frmShowOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmShowOrders";
            this.Text = "Orders";
            this.Load += new System.EventHandler(this.frmShowOrders_Load);
            this.panelContent.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        private ctrlOrderByCmb cmbOrderBy;
        private ctrlSortByCmb cmbOrderStatus;
        private System.Windows.Forms.Button btnCompeleted;
    }
    } 
