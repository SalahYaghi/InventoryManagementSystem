namespace UI.Forms.References.Documents
{
     
        partial class ctrlDocumentInfo
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.GroupBox groupDocument;
            private System.Windows.Forms.Label lblDocumentType;
            private System.Windows.Forms.Label lblDocumentImage;
            private System.Windows.Forms.Label lblStatus;

            private System.Windows.Forms.ComboBox cmbDocumentType;
            private System.Windows.Forms.TextBox txtDocumentPath;
            private System.Windows.Forms.Button btnBrowse;
            private System.Windows.Forms.Button btnClearFile;
            private System.Windows.Forms.PictureBox picPreview;

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
            this.groupDocument = new System.Windows.Forms.GroupBox();
            this.lblDocumentType = new System.Windows.Forms.Label();
            this.cmbDocumentType = new System.Windows.Forms.ComboBox();
            this.lblDocumentImage = new System.Windows.Forms.Label();
            this.txtDocumentPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnClearFile = new System.Windows.Forms.Button();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupDocument.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupDocument
            // 
            this.groupDocument.BackColor = System.Drawing.Color.White;
            this.groupDocument.Controls.Add(this.lblDocumentType);
            this.groupDocument.Controls.Add(this.cmbDocumentType);
            this.groupDocument.Controls.Add(this.lblDocumentImage);
            this.groupDocument.Controls.Add(this.txtDocumentPath);
            this.groupDocument.Controls.Add(this.btnBrowse);
            this.groupDocument.Controls.Add(this.btnClearFile);
            this.groupDocument.Controls.Add(this.picPreview);
            this.groupDocument.Controls.Add(this.lblStatus);
            this.groupDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDocument.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupDocument.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupDocument.Location = new System.Drawing.Point(0, 0);
            this.groupDocument.Name = "groupDocument";
            this.groupDocument.Padding = new System.Windows.Forms.Padding(18);
            this.groupDocument.Size = new System.Drawing.Size(720, 205);
            this.groupDocument.TabIndex = 0;
            this.groupDocument.TabStop = false;
            this.groupDocument.Text = "Document Information";
            // 
            // lblDocumentType
            // 
            this.lblDocumentType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDocumentType.ForeColor = System.Drawing.Color.Gray;
            this.lblDocumentType.Location = new System.Drawing.Point(22, 35);
            this.lblDocumentType.Name = "lblDocumentType";
            this.lblDocumentType.Size = new System.Drawing.Size(200, 22);
            this.lblDocumentType.TabIndex = 0;
            this.lblDocumentType.Text = "Document Type *";
            // 
            // cmbDocumentType
            // 
            this.cmbDocumentType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDocumentType.FormattingEnabled = true;
            this.cmbDocumentType.Location = new System.Drawing.Point(22, 60);
            this.cmbDocumentType.Name = "cmbDocumentType";
            this.cmbDocumentType.Size = new System.Drawing.Size(310, 31);
            this.cmbDocumentType.TabIndex = 1;
            // 
            // lblDocumentImage
            // 
            this.lblDocumentImage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDocumentImage.ForeColor = System.Drawing.Color.Gray;
            this.lblDocumentImage.Location = new System.Drawing.Point(22, 108);
            this.lblDocumentImage.Name = "lblDocumentImage";
            this.lblDocumentImage.Size = new System.Drawing.Size(200, 22);
            this.lblDocumentImage.TabIndex = 2;
            this.lblDocumentImage.Text = "Document Image *";
            // 
            // txtDocumentPath
            // 
            this.txtDocumentPath.Location = new System.Drawing.Point(22, 133);
            this.txtDocumentPath.Name = "txtDocumentPath";
            this.txtDocumentPath.Size = new System.Drawing.Size(365, 30);
            this.txtDocumentPath.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(398, 133);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(42, 30);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnClearFile
            // 
            this.btnClearFile.Location = new System.Drawing.Point(450, 133);
            this.btnClearFile.Name = "btnClearFile";
            this.btnClearFile.Size = new System.Drawing.Size(80, 30);
            this.btnClearFile.TabIndex = 5;
            this.btnClearFile.Text = "Clear";
            this.btnClearFile.UseVisualStyleBackColor = false;
            this.btnClearFile.Click += new System.EventHandler(this.btnClearFile_Click);
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.picPreview.Location = new System.Drawing.Point(551, 30);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(120, 90);
            this.picPreview.TabIndex = 6;
            this.picPreview.TabStop = false;
            this.picPreview.Click += new System.EventHandler(this.picPreview_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(470, 160);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(210, 25);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Document information";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ctrlDocumentInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupDocument);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ctrlDocumentInfo";
            this.Size = new System.Drawing.Size(720, 205);
            this.groupDocument.ResumeLayout(false);
            this.groupDocument.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

            }
        }
     
}

