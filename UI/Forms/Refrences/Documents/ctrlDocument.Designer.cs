namespace UI.Forms.Refrences.Documents
{
        partial class ctrlDocumentDetails
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.GroupBox groupDocument;
            private System.Windows.Forms.Label lblDocumentType;

            private System.Windows.Forms.Label lblDocumentTypeValue;

            private System.Windows.Forms.PictureBox picPreview;

            protected override void Dispose(bool disposing)
            {
                if (disposing && components != null)
                    components.Dispose();

                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
            this.groupDocument = new System.Windows.Forms.GroupBox();
            this.lblUpdateDocument = new System.Windows.Forms.LinkLabel();
            this.lblDocumentType = new System.Windows.Forms.Label();
            this.lblDocumentTypeValue = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.groupDocument.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // groupDocument
            // 
            this.groupDocument.BackColor = System.Drawing.Color.White;
            this.groupDocument.Controls.Add(this.lblUpdateDocument);
            this.groupDocument.Controls.Add(this.lblDocumentType);
            this.groupDocument.Controls.Add(this.lblDocumentTypeValue);
            this.groupDocument.Controls.Add(this.picPreview);
            this.groupDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDocument.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupDocument.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupDocument.Location = new System.Drawing.Point(0, 0);
            this.groupDocument.Name = "groupDocument";
            this.groupDocument.Padding = new System.Windows.Forms.Padding(18);
            this.groupDocument.Size = new System.Drawing.Size(720, 164);
            this.groupDocument.TabIndex = 0;
            this.groupDocument.TabStop = false;
            this.groupDocument.Text = "Document Details";
            this.groupDocument.Enter += new System.EventHandler(this.groupDocument_Enter);
            // 
            // lblUpdateDocument
            // 
            this.lblUpdateDocument.ActiveLinkColor = System.Drawing.Color.DarkGray;
            this.lblUpdateDocument.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUpdateDocument.ForeColor = System.Drawing.Color.Gray;
            this.lblUpdateDocument.LinkColor = System.Drawing.Color.Gray;
            this.lblUpdateDocument.Location = new System.Drawing.Point(22, 124);
            this.lblUpdateDocument.Name = "lblUpdateDocument";
            this.lblUpdateDocument.Size = new System.Drawing.Size(200, 22);
            this.lblUpdateDocument.TabIndex = 7;
            this.lblUpdateDocument.TabStop = true;
            this.lblUpdateDocument.Text = "Click Here To Make Changes";
            this.lblUpdateDocument.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUpdateDocument_LinkClicked);
            // 
            // lblDocumentType
            // 
            this.lblDocumentType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDocumentType.ForeColor = System.Drawing.Color.Gray;
            this.lblDocumentType.Location = new System.Drawing.Point(22, 35);
            this.lblDocumentType.Name = "lblDocumentType";
            this.lblDocumentType.Size = new System.Drawing.Size(200, 22);
            this.lblDocumentType.TabIndex = 0;
            this.lblDocumentType.Text = "Document Type";
            // 
            // lblDocumentTypeValue
            // 
            this.lblDocumentTypeValue.Location = new System.Drawing.Point(22, 58);
            this.lblDocumentTypeValue.Name = "lblDocumentTypeValue";
            this.lblDocumentTypeValue.Size = new System.Drawing.Size(300, 30);
            this.lblDocumentTypeValue.TabIndex = 1;
            this.lblDocumentTypeValue.Text = "-";
            this.lblDocumentTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.picPreview.Location = new System.Drawing.Point(580, 35);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(104, 73);
            this.picPreview.TabIndex = 6;
            this.picPreview.TabStop = false;
            this.picPreview.Click += new System.EventHandler(this.picPreview_Click);
            // 
            // ctrlDocumentDetails
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupDocument);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ctrlDocumentDetails";
            this.Size = new System.Drawing.Size(720, 164);
            this.groupDocument.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

            }

        private System.Windows.Forms.LinkLabel lblUpdateDocument;
    }
    }
