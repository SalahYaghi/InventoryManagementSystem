namespace UI.Forms.Warehouses
{
    public partial class frmShowWarehouses
    { 
            private System.ComponentModel.IContainer components = null;

            private UI.Shared.Controllers.DgvCustom dgvWarehouses;
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEmployees = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvWarehouses = new UI.Shared.Controllers.DgvCustom();
            this.btnShowStock = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.btnShowStock);
            this.panelTop.Controls.Add(this.btnDelete);
            this.panelTop.Controls.Add(this.btnEmployees);
            this.panelTop.Controls.Add(this.btnAdd);
            this.panelTop.Controls.Add(this.btnEdit);
            this.panelTop.Controls.Add(this.btnView);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelTop.Size = new System.Drawing.Size(1052, 67);
            this.panelTop.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(700, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(132, 37);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEmployees
            // 
            this.btnEmployees.Location = new System.Drawing.Point(424, 13);
            this.btnEmployees.Name = "btnEmployees";
            this.btnEmployees.Size = new System.Drawing.Size(132, 37);
            this.btnEmployees.TabIndex = 4;
            this.btnEmployees.Text = "Employees";
            this.btnEmployees.Click += new System.EventHandler(this.btnWarehouseEmployees_Click);
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
            this.btnEdit.Location = new System.Drawing.Point(148, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(132, 37);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(286, 13);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(132, 37);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(908, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(132, 37);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvWarehouses
            // 
            this.dgvWarehouses.BackColor = System.Drawing.Color.White;
            this.dgvWarehouses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWarehouses.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvWarehouses.Location = new System.Drawing.Point(0, 67);
            this.dgvWarehouses.Name = "dgvWarehouses";
            this.dgvWarehouses.Size = new System.Drawing.Size(1052, 392);
            this.dgvWarehouses.TabIndex = 0;
            // 
            // btnShowStock
            // 
            this.btnShowStock.Location = new System.Drawing.Point(562, 13);
            this.btnShowStock.Name = "btnShowStock";
            this.btnShowStock.Size = new System.Drawing.Size(132, 37);
            this.btnShowStock.TabIndex = 6;
            this.btnShowStock.Text = "Show Stock";
            this.btnShowStock.Click += new System.EventHandler(this.btnShowStock_Click);
            // 
            // frmShowWarehouses
            // 
            this.ClientSize = new System.Drawing.Size(1052, 459);
            this.Controls.Add(this.dgvWarehouses);
            this.Controls.Add(this.panelTop);
            this.Name = "frmShowWarehouses";
            this.Text = "Suppliers";
            this.Load += new System.EventHandler(this.frmShowWarehouses_Load);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

            }

            private System.Windows.Forms.Button btnEmployees;
            private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnShowStock;
    }
    } 
