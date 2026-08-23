namespace UI.Forms.Products.Categories
{
    
        partial class frmManageCategories
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Panel panelBody;
            private System.Windows.Forms.Panel panelFooter;
            private System.Windows.Forms.GroupBox groupEditor;
            private System.Windows.Forms.GroupBox groupCategories;

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblSubtitle;
            private System.Windows.Forms.Label lblCategoryName;
            private System.Windows.Forms.Label lblStatus;

            private System.Windows.Forms.TextBox txtCategoryName;
            private System.Windows.Forms.ListBox lstCategories;

            private System.Windows.Forms.Button btnAdd;
            private System.Windows.Forms.Button btnUpdate;
            private System.Windows.Forms.Button btnDelete;
            private System.Windows.Forms.Button btnClear;
            private System.Windows.Forms.Button btnRefresh;
            private System.Windows.Forms.Button btnClose;

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
                this.panelHeader = new System.Windows.Forms.Panel();
                this.panelBody = new System.Windows.Forms.Panel();
                this.panelFooter = new System.Windows.Forms.Panel();
                this.groupEditor = new System.Windows.Forms.GroupBox();
                this.groupCategories = new System.Windows.Forms.GroupBox();

                this.lblTitle = new System.Windows.Forms.Label();
                this.lblSubtitle = new System.Windows.Forms.Label();
                this.lblCategoryName = new System.Windows.Forms.Label();
                this.lblStatus = new System.Windows.Forms.Label();

                this.txtCategoryName = new System.Windows.Forms.TextBox();
                this.lstCategories = new System.Windows.Forms.ListBox();

                this.btnAdd = new System.Windows.Forms.Button();
                this.btnUpdate = new System.Windows.Forms.Button();
                this.btnDelete = new System.Windows.Forms.Button();
                this.btnClear = new System.Windows.Forms.Button();
                this.btnRefresh = new System.Windows.Forms.Button();
                this.btnClose = new System.Windows.Forms.Button();

                this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);

                ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
                this.panelRoot.SuspendLayout();
                this.panelHeader.SuspendLayout();
                this.panelBody.SuspendLayout();
                this.panelFooter.SuspendLayout();
                this.groupEditor.SuspendLayout();
                this.groupCategories.SuspendLayout();
                this.SuspendLayout();

                // frmManageCategories
                this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(760, 560);
                this.Controls.Add(this.panelRoot);
                this.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.Name = "frmManageCategories";
                this.Text = "Manage Categories";
                this.Load += new System.EventHandler(this.frmManageCategories_Load);

                // panelRoot
                this.panelRoot.BackColor = System.Drawing.Color.FromArgb(243, 246, 249);
                this.panelRoot.Controls.Add(this.panelBody);
                this.panelRoot.Controls.Add(this.panelFooter);
                this.panelRoot.Controls.Add(this.panelHeader);
                this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;

                // panelHeader
                this.panelHeader.BackColor = System.Drawing.Color.White;
                this.panelHeader.Controls.Add(this.lblSubtitle);
                this.panelHeader.Controls.Add(this.lblTitle);
                this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
                this.panelHeader.Location = new System.Drawing.Point(0, 0);
                this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
                this.panelHeader.Size = new System.Drawing.Size(760, 100);

                // lblTitle
                this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
                this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.lblTitle.Location = new System.Drawing.Point(24, 14);
                this.lblTitle.Size = new System.Drawing.Size(400, 42);
                this.lblTitle.Text = "Manage Categories";

                // lblSubtitle
                this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
                this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
                this.lblSubtitle.Location = new System.Drawing.Point(27, 58);
                this.lblSubtitle.Size = new System.Drawing.Size(650, 24);
                this.lblSubtitle.Text = "Create, update and organize product categories.";

                // panelBody
                this.panelBody.BackColor = System.Drawing.Color.FromArgb(243, 246, 249);
                this.panelBody.Controls.Add(this.groupCategories);
                this.panelBody.Controls.Add(this.groupEditor);
                this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panelBody.Location = new System.Drawing.Point(0, 100);
                this.panelBody.Padding = new System.Windows.Forms.Padding(24);
                this.panelBody.Size = new System.Drawing.Size(760, 380);

                // groupEditor
                this.groupEditor.BackColor = System.Drawing.Color.White;
                this.groupEditor.Controls.Add(this.btnClear);
                this.groupEditor.Controls.Add(this.btnDelete);
                this.groupEditor.Controls.Add(this.btnUpdate);
                this.groupEditor.Controls.Add(this.btnAdd);
                this.groupEditor.Controls.Add(this.txtCategoryName);
                this.groupEditor.Controls.Add(this.lblCategoryName);
                this.groupEditor.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
                this.groupEditor.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.groupEditor.Location = new System.Drawing.Point(24, 24);
                this.groupEditor.Padding = new System.Windows.Forms.Padding(18);
                this.groupEditor.Size = new System.Drawing.Size(712, 145);
                this.groupEditor.TabStop = false;
                this.groupEditor.Text = "Category Information";

                // lblCategoryName
                this.lblCategoryName.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.lblCategoryName.ForeColor = System.Drawing.Color.Gray;
                this.lblCategoryName.Location = new System.Drawing.Point(22, 35);
                this.lblCategoryName.Size = new System.Drawing.Size(200, 22);
                this.lblCategoryName.Text = "Category Name";

                // txtCategoryName
                this.txtCategoryName.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
                this.txtCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 10F);
                this.txtCategoryName.Location = new System.Drawing.Point(22, 60);
                this.txtCategoryName.Size = new System.Drawing.Size(665, 30);

                // btnAdd
                this.btnAdd.Location = new System.Drawing.Point(22, 100);
                this.btnAdd.Size = new System.Drawing.Size(130, 34);
                this.btnAdd.Text = "+ Add";
                this.btnAdd.UseVisualStyleBackColor = false;
                this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

                // btnUpdate
                this.btnUpdate.Location = new System.Drawing.Point(165, 100);
                this.btnUpdate.Size = new System.Drawing.Size(130, 34);
                this.btnUpdate.Text = "Update";
                this.btnUpdate.UseVisualStyleBackColor = false;
                this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

                // btnDelete
                this.btnDelete.Location = new System.Drawing.Point(308, 100);
                this.btnDelete.Size = new System.Drawing.Size(130, 34);
                this.btnDelete.Text = "Delete";
                this.btnDelete.UseVisualStyleBackColor = false;
                this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

                // btnClear
                this.btnClear.Location = new System.Drawing.Point(451, 100);
                this.btnClear.Size = new System.Drawing.Size(130, 34);
                this.btnClear.Text = "Clear";
                this.btnClear.UseVisualStyleBackColor = false;
                this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

                // groupCategories
                this.groupCategories.BackColor = System.Drawing.Color.White;
                this.groupCategories.Controls.Add(this.lstCategories);
                this.groupCategories.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
                this.groupCategories.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.groupCategories.Location = new System.Drawing.Point(24, 190);
                this.groupCategories.Padding = new System.Windows.Forms.Padding(18);
                this.groupCategories.Size = new System.Drawing.Size(712, 165);
                this.groupCategories.TabStop = false;
                this.groupCategories.Text = "Categories";

                // lstCategories
                this.lstCategories.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
                this.lstCategories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.lstCategories.Font = new System.Drawing.Font("Segoe UI", 10F);
                this.lstCategories.ForeColor = System.Drawing.Color.FromArgb(24, 33, 45);
                this.lstCategories.ItemHeight = 23;
                this.lstCategories.Location = new System.Drawing.Point(22, 35);
                this.lstCategories.Size = new System.Drawing.Size(665, 117);
                this.lstCategories.SelectedIndexChanged += new System.EventHandler(this.lstCategories_SelectedIndexChanged);

                // panelFooter
                this.panelFooter.BackColor = System.Drawing.Color.White;
                this.panelFooter.Controls.Add(this.lblStatus);
                this.panelFooter.Controls.Add(this.btnClose);
                this.panelFooter.Controls.Add(this.btnRefresh);
                this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.panelFooter.Location = new System.Drawing.Point(0, 480);
                this.panelFooter.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
                this.panelFooter.Size = new System.Drawing.Size(760, 80);

                // lblStatus
                this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
                this.lblStatus.ForeColor = System.Drawing.Color.Gray;
                this.lblStatus.Location = new System.Drawing.Point(24, 29);
                this.lblStatus.Size = new System.Drawing.Size(360, 23);
                this.lblStatus.Text = "Ready";

                // btnRefresh
                this.btnRefresh.Location = new System.Drawing.Point(500, 20);
                this.btnRefresh.Size = new System.Drawing.Size(105, 40);
                this.btnRefresh.Text = "Refresh";
                this.btnRefresh.UseVisualStyleBackColor = false;
                this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

                // btnClose
                this.btnClose.Location = new System.Drawing.Point(615, 20);
                this.btnClose.Size = new System.Drawing.Size(120, 40);
                this.btnClose.Text = "Close";
                this.btnClose.UseVisualStyleBackColor = false;
                this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

                // errorProvider
                this.errorProvider.ContainerControl = this;

                this.groupCategories.ResumeLayout(false);
                this.groupEditor.ResumeLayout(false);
                this.groupEditor.PerformLayout();
                this.panelFooter.ResumeLayout(false);
                this.panelBody.ResumeLayout(false);
                this.panelHeader.ResumeLayout(false);
                this.panelRoot.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
                this.ResumeLayout(false);
            }
        }
    } 
