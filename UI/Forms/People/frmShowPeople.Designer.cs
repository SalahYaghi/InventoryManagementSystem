using UI.Shared.Controllers;

namespace UI.Forms.People
{
    
        partial class frmShowPeople
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelContent;
            private DgvCustomPaginated dgvPeople;

            private System.Windows.Forms.Panel panelFilters;
            private System.Windows.Forms.TextBox txtSearch;
            private System.Windows.Forms.Label lblSearch;

            private System.Windows.Forms.Panel panelActions;
            private System.Windows.Forms.Button btnAdd;
            private System.Windows.Forms.Button btnEdit;
            private System.Windows.Forms.Button btnView;
            private System.Windows.Forms.Button btnImage;
            private System.Windows.Forms.Button btnDocument;
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
            this.dgvPeople = new UI.Shared.Controllers.DgvCustomPaginated();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.cmbGender = new UI.Shared.Controllers.ctrlSortByCmb();
            this.cmbOrderBy = new UI.Shared.Controllers.ctrlOrderByCmb();
            this.cmbCity = new UI.Shared.Controllers.ctrlSortByCmb();
            this.cmbCountry = new UI.Shared.Controllers.ctrlSortByCmb();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnImage = new System.Windows.Forms.Button();
            this.btnDocument = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelContent.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelContent.Controls.Add(this.dgvPeople);
            this.panelContent.Controls.Add(this.panelFilters);
            this.panelContent.Controls.Add(this.panelActions);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1180, 720);
            this.panelContent.TabIndex = 0;
            // 
            // dgvPeople
            // 
            this.dgvPeople.BackColor = System.Drawing.Color.White;
            this.dgvPeople.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPeople.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvPeople.Location = new System.Drawing.Point(0, 156);
            this.dgvPeople.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.Size = new System.Drawing.Size(1180, 564);
            this.dgvPeople.TabIndex = 2;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.White;
            this.panelFilters.Controls.Add(this.cmbGender);
            this.panelFilters.Controls.Add(this.cmbOrderBy);
            this.panelFilters.Controls.Add(this.cmbCity);
            this.panelFilters.Controls.Add(this.cmbCountry);
            this.panelFilters.Controls.Add(this.lblSearch);
            this.panelFilters.Controls.Add(this.txtSearch);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 74);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(18, 8, 18, 10);
            this.panelFilters.Size = new System.Drawing.Size(1180, 82);
            this.panelFilters.TabIndex = 1;
            // 
            // cmbGender
            // 
            this.cmbGender.BackColor = System.Drawing.Color.White;
            this.cmbGender.Location = new System.Drawing.Point(988, 14);
            this.cmbGender.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(218, 79);
            this.cmbGender.TabIndex = 11;
            this.cmbGender.Title = "Gender";
            this.cmbGender.Load += new System.EventHandler(this.ctrlSortByCmb1_Load);
            // 
            // cmbOrderBy
            // 
            this.cmbOrderBy.BackColor = System.Drawing.Color.White;
            this.cmbOrderBy.Location = new System.Drawing.Point(340, 18);
            this.cmbOrderBy.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbOrderBy.Name = "cmbOrderBy";
            this.cmbOrderBy.Size = new System.Drawing.Size(360, 68);
            this.cmbOrderBy.TabIndex = 8;
            // 
            // cmbCity
            // 
            this.cmbCity.BackColor = System.Drawing.Color.White;
            this.cmbCity.Location = new System.Drawing.Point(811, 14);
            this.cmbCity.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(218, 79);
            this.cmbCity.TabIndex = 10;
            this.cmbCity.Title = "City";
            // 
            // cmbCountry
            // 
            this.cmbCountry.BackColor = System.Drawing.Color.White;
            this.cmbCountry.Location = new System.Drawing.Point(631, 14);
            this.cmbCountry.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(218, 79);
            this.cmbCountry.TabIndex = 9;
            this.cmbCountry.Title = "Country";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.Gray;
            this.lblSearch.Location = new System.Drawing.Point(18, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(186, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search across people data";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(18, 34);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(290, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Controls.Add(this.btnEdit);
            this.panelActions.Controls.Add(this.btnView);
            this.panelActions.Controls.Add(this.btnImage);
            this.panelActions.Controls.Add(this.btnDocument);
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Controls.Add(this.btnRefresh);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 0);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.panelActions.Size = new System.Drawing.Size(1180, 74);
            this.panelActions.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(18, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 42);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+ Add Person";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(170, 16);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 42);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(302, 16);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(130, 42);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View Details";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnImage
            // 
            this.btnImage.Location = new System.Drawing.Point(444, 16);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(130, 42);
            this.btnImage.TabIndex = 3;
            this.btnImage.Text = "Image";
            this.btnImage.UseVisualStyleBackColor = false;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // btnDocument
            // 
            this.btnDocument.Location = new System.Drawing.Point(586, 16);
            this.btnDocument.Name = "btnDocument";
            this.btnDocument.Size = new System.Drawing.Size(130, 42);
            this.btnDocument.TabIndex = 4;
            this.btnDocument.Text = "Document";
            this.btnDocument.UseVisualStyleBackColor = false;
            this.btnDocument.Click += new System.EventHandler(this.btnDocument_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(728, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 42);
            this.btnDelete.TabIndex = 5;
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
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmShowPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmShowPeople";
            this.Text = "People";
            this.Load += new System.EventHandler(this.frmShowPeople_Load);
            this.panelContent.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        private ctrlOrderByCmb cmbOrderBy;
        private ctrlSortByCmb cmbCity;
        private ctrlSortByCmb cmbCountry;
        private ctrlSortByCmb cmbGender;
    }
    
}
