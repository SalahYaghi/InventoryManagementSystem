namespace UI.Shared.Controllers
{
    partial class ctrlOrderByCmb
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmbData = new System.Windows.Forms.ComboBox();
            this.lblAscDesc = new System.Windows.Forms.Label();
            this.cmbSortDirection = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(90, 17);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Order By";
            // 
            // cmbData
            // 
            this.cmbData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbData.Location = new System.Drawing.Point(0, 18);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(157, 31);
            this.cmbData.TabIndex = 11;
            this.cmbData.SelectedIndexChanged += new System.EventHandler(this.cmbSortChaned_SelectedIndexChanged);
            // 
            // lblAscDesc
            // 
            this.lblAscDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAscDesc.ForeColor = System.Drawing.Color.Gray;
            this.lblAscDesc.Location = new System.Drawing.Point(163, 0);
            this.lblAscDesc.Name = "lblAscDesc";
            this.lblAscDesc.Size = new System.Drawing.Size(106, 17);
            this.lblAscDesc.TabIndex = 12;
            this.lblAscDesc.Text = "Sort Direction";
            // 
            // cmbSortDirection
            // 
            this.cmbSortDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortDirection.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSortDirection.Location = new System.Drawing.Point(163, 18);
            this.cmbSortDirection.Name = "cmbSortDirection";
            this.cmbSortDirection.Size = new System.Drawing.Size(106, 31);
            this.cmbSortDirection.TabIndex = 13;
            this.cmbSortDirection.SelectedIndexChanged += new System.EventHandler(this.cmbSortChaned_SelectedIndexChanged);
            // 
            // ctrlOrderByCmb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAscDesc);
            this.Controls.Add(this.cmbSortDirection);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cmbData);
            this.Name = "ctrlOrderByCmb";
            this.Size = new System.Drawing.Size(278, 54);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cmbData;
        private System.Windows.Forms.Label lblAscDesc;
        private System.Windows.Forms.ComboBox cmbSortDirection;
    }
}

