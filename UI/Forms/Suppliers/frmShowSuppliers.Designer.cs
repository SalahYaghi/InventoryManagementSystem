namespace UI.Forms.Suppliers
{
    partial class frmShowSuppliers
    {
            private System.ComponentModel.IContainer components = null;

            private UI.Shared.Controllers.DgvCustom dgvSuppliers;
            private System.Windows.Forms.Panel panelTop;
            private System.Windows.Forms.Button btnAdd;
            private System.Windows.Forms.Button btnEdit;
            private System.Windows.Forms.Button btnView;
            private System.Windows.Forms.Button btnRefresh;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                    components.Dispose();

                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnManageProducts = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvSuppliers = new UI.Shared.Controllers.DgvCustom();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.btnDelete);
            this.panelTop.Controls.Add(this.btnManageProducts);
            this.panelTop.Controls.Add(this.btnAdd);
            this.panelTop.Controls.Add(this.btnEdit);
            this.panelTop.Controls.Add(this.btnView);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelTop.Size = new System.Drawing.Size(932, 67);
            this.panelTop.TabIndex = 1;
            // 
            // btnManageProducts
            // 
            this.btnManageProducts.Location = new System.Drawing.Point(590, 12);
            this.btnManageProducts.Name = "btnManageProducts";
            this.btnManageProducts.Size = new System.Drawing.Size(132, 37);
            this.btnManageProducts.TabIndex = 4;
            this.btnManageProducts.Text = "Manage Products";
            this.btnManageProducts.Click += new System.EventHandler(this.btnManageProducts_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(10, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(132, 37);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(162, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(132, 37);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(314, 12);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(132, 37);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(787, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(132, 37);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvSuppliers
            // 
            this.dgvSuppliers.BackColor = System.Drawing.Color.White;
            this.dgvSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSuppliers.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvSuppliers.Location = new System.Drawing.Point(0, 67);
            this.dgvSuppliers.Name = "dgvSuppliers";
            this.dgvSuppliers.Size = new System.Drawing.Size(932, 300);
            this.dgvSuppliers.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(452, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(132, 37);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmShowSuppliers
            // 
            this.ClientSize = new System.Drawing.Size(932, 367);
            this.Controls.Add(this.dgvSuppliers);
            this.Controls.Add(this.panelTop);
            this.Name = "frmShowSuppliers";
            this.Text = "Suppliers";
            this.Load += new System.EventHandler(this.frmShowSuppliers_Load);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        private System.Windows.Forms.Button btnManageProducts;
        private System.Windows.Forms.Button btnDelete;
    }
    }
