namespace UI.Forms.Employees
{
    
        partial class frmShowWarehouseEmployees
        {
            private System.ComponentModel.IContainer components = null;

            protected override void Dispose(bool disposing)
            {
                if (disposing && components != null)
                    components.Dispose();

                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblAddEmployee = new System.Windows.Forms.Label();
            this.flowEmployees = new System.Windows.Forms.FlowLayoutPanel();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.lblSortDirection = new System.Windows.Forms.Label();
            this.cmbSortDirection = new System.Windows.Forms.ComboBox();
            this.lblOrderBy = new System.Windows.Forms.Label();
            this.cmbOrderBy = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTotalEmployees = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelContent.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelContent.Controls.Add(this.lblAddEmployee);
            this.panelContent.Controls.Add(this.flowEmployees);
            this.panelContent.Controls.Add(this.panelFilters);
            this.panelContent.Controls.Add(this.panelFooter);
            this.panelContent.Controls.Add(this.panelHeader);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1180, 720);
            this.panelContent.TabIndex = 0;
            // 
            // lblAddEmployee
            // 
            this.lblAddEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAddEmployee.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblAddEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblAddEmployee.Location = new System.Drawing.Point(390, 357);
            this.lblAddEmployee.Margin = new System.Windows.Forms.Padding(390, 120, 0, 0);
            this.lblAddEmployee.Name = "lblAddEmployee";
            this.lblAddEmployee.Size = new System.Drawing.Size(427, 63);
            this.lblAddEmployee.TabIndex = 0;
            this.lblAddEmployee.Text = "Click Here To Add New Employee";
            this.lblAddEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAddEmployee.Click += new System.EventHandler(this.lblAddEmployee_Click);
            // 
            // flowEmployees
            // 
            this.flowEmployees.AutoScroll = true;
            this.flowEmployees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.flowEmployees.Location = new System.Drawing.Point(0, 645);
            this.flowEmployees.Name = "flowEmployees";
            this.flowEmployees.Padding = new System.Windows.Forms.Padding(0, 8, 0, 200);
            this.flowEmployees.Size = new System.Drawing.Size(1180, 25);
            this.flowEmployees.TabIndex = 3;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.White;
            this.panelFilters.Controls.Add(this.lblSortDirection);
            this.panelFilters.Controls.Add(this.cmbSortDirection);
            this.panelFilters.Controls.Add(this.lblOrderBy);
            this.panelFilters.Controls.Add(this.cmbOrderBy);
            this.panelFilters.Controls.Add(this.lblSearch);
            this.panelFilters.Controls.Add(this.txtSearch);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 80);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(18, 8, 18, 10);
            this.panelFilters.Size = new System.Drawing.Size(1180, 78);
            this.panelFilters.TabIndex = 1;
            // 
            // lblSortDirection
            // 
            this.lblSortDirection.AutoSize = true;
            this.lblSortDirection.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSortDirection.ForeColor = System.Drawing.Color.Gray;
            this.lblSortDirection.Location = new System.Drawing.Point(734, 10);
            this.lblSortDirection.Name = "lblSortDirection";
            this.lblSortDirection.Size = new System.Drawing.Size(104, 20);
            this.lblSortDirection.TabIndex = 5;
            this.lblSortDirection.Text = "Sort Direction";
            // 
            // cmbSortDirection
            // 
            this.cmbSortDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortDirection.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSortDirection.FormattingEnabled = true;
            this.cmbSortDirection.Location = new System.Drawing.Point(738, 33);
            this.cmbSortDirection.Name = "cmbSortDirection";
            this.cmbSortDirection.Size = new System.Drawing.Size(160, 31);
            this.cmbSortDirection.TabIndex = 4;
            this.cmbSortDirection.SelectedIndexChanged += new System.EventHandler(this.cmbSortDirection_SelectedIndexChanged);
            // 
            // lblOrderBy
            // 
            this.lblOrderBy.AutoSize = true;
            this.lblOrderBy.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrderBy.ForeColor = System.Drawing.Color.Gray;
            this.lblOrderBy.Location = new System.Drawing.Point(486, 10);
            this.lblOrderBy.Name = "lblOrderBy";
            this.lblOrderBy.Size = new System.Drawing.Size(70, 20);
            this.lblOrderBy.TabIndex = 3;
            this.lblOrderBy.Text = "Order By";
            // 
            // cmbOrderBy
            // 
            this.cmbOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbOrderBy.FormattingEnabled = true;
            this.cmbOrderBy.Location = new System.Drawing.Point(490, 33);
            this.cmbOrderBy.Name = "cmbOrderBy";
            this.cmbOrderBy.Size = new System.Drawing.Size(220, 31);
            this.cmbOrderBy.TabIndex = 2;
            this.cmbOrderBy.SelectedIndexChanged += new System.EventHandler(this.cmbOrderBy_SelectedIndexChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.Gray;
            this.lblSearch.Location = new System.Drawing.Point(16, 10);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(210, 20);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search warehouse employees";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.txtSearch.Location = new System.Drawing.Point(18, 34);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(440, 30);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 670);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(18, 10, 18, 10);
            this.panelFooter.Size = new System.Drawing.Size(1180, 50);
            this.panelFooter.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(18, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(500, 30);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTotalEmployees);
            this.panelHeader.Controls.Add(this.lblTotalTitle);
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.panelHeader.Size = new System.Drawing.Size(1180, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTotalEmployees
            // 
            this.lblTotalEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalEmployees.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalEmployees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(112)))), ((int)(((byte)(139)))));
            this.lblTotalEmployees.Location = new System.Drawing.Point(897, 30);
            this.lblTotalEmployees.Name = "lblTotalEmployees";
            this.lblTotalEmployees.Size = new System.Drawing.Size(100, 36);
            this.lblTotalEmployees.TabIndex = 4;
            this.lblTotalEmployees.Text = "0";
            this.lblTotalEmployees.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalTitle.Location = new System.Drawing.Point(842, 12);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(155, 20);
            this.lblTotalTitle.TabIndex = 3;
            this.lblTotalTitle.Text = "Total Employees";
            this.lblTotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1018, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 42);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(21, 48);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(600, 24);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "View employees assigned to this warehouse in dynamic cards.";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(18, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(520, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Warehouse Employees";
            // 
            // frmShowWarehouseEmployees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmShowWarehouseEmployees";
            this.Text = "Warehouse Employees";
            this.Load += new System.EventHandler(this.frmShowWarehouseEmployees_Load);
            this.panelContent.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

            }
        private System.Windows.Forms.Panel panelContent;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblSubtitle;
            private System.Windows.Forms.Button btnRefresh;
            private System.Windows.Forms.Label lblTotalTitle;
            private System.Windows.Forms.Label lblTotalEmployees;
            private System.Windows.Forms.Panel panelFilters;
            private System.Windows.Forms.TextBox txtSearch;
            private System.Windows.Forms.Label lblSearch;
            private System.Windows.Forms.ComboBox cmbOrderBy;
            private System.Windows.Forms.Label lblOrderBy;
            private System.Windows.Forms.ComboBox cmbSortDirection;
            private System.Windows.Forms.Label lblSortDirection;
            private System.Windows.Forms.FlowLayoutPanel flowEmployees;
            private System.Windows.Forms.Panel panelFooter;
            private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblAddEmployee;
    }
  
}
