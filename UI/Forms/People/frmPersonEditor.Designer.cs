using UI.Forms.People.Controls;

namespace UI.Forms.People
{
        partial class frmPersonEditor
        {
            private System.ComponentModel.IContainer components = null;

            private System.Windows.Forms.Panel panelRoot;
            private System.Windows.Forms.Panel panelHeader;
            private System.Windows.Forms.Panel panelBody;
            private System.Windows.Forms.Panel panelFooter;

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblSubtitle;
            private System.Windows.Forms.Label lblStatus;

            private ctrlPersonEditor ctrlPersonEditor1;

            private System.Windows.Forms.Button btnSave;
            private System.Windows.Forms.Button btnCancel;

            protected override void Dispose(bool disposing)
            {
                if (disposing && components != null)
                    components.Dispose();

                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
            this.panelRoot = new System.Windows.Forms.Panel();
            this.panelBody = new System.Windows.Forms.Panel();
            this.ctrlPersonEditor1 = new UI.Forms.People.Controls.ctrlPersonEditor();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnChangeImage = new System.Windows.Forms.Button();
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelRoot.Controls.Add(this.panelBody);
            this.panelRoot.Controls.Add(this.panelFooter);
            this.panelRoot.Controls.Add(this.panelHeader);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Size = new System.Drawing.Size(820, 820);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.ctrlPersonEditor1);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 100);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.panelBody.Size = new System.Drawing.Size(820, 640);
            this.panelBody.TabIndex = 0;
            // 
            // ctrlPersonEditor1
            // 
            this.ctrlPersonEditor1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonEditor1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlPersonEditor1.Location = new System.Drawing.Point(24, 20);
            this.ctrlPersonEditor1.Name = "ctrlPersonEditor1";
            this.ctrlPersonEditor1.Size = new System.Drawing.Size(760, 822);
            this.ctrlPersonEditor1.TabIndex = 0;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnChangeImage);
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 740);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.panelFooter.Size = new System.Drawing.Size(820, 80);
            this.panelFooter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(24, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(420, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Ready";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(580, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(695, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
            this.panelHeader.Size = new System.Drawing.Size(820, 100);
            this.panelHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Person Editor";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(28, 62);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(740, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Create or update person profile information.";
            // 
            // btnChangeImage
            // 
            this.btnChangeImage.Location = new System.Drawing.Point(443, 19);
            this.btnChangeImage.Name = "btnChangeImage";
            this.btnChangeImage.Size = new System.Drawing.Size(131, 40);
            this.btnChangeImage.TabIndex = 3;
            this.btnChangeImage.Text = "Change Image";
            this.btnChangeImage.UseVisualStyleBackColor = false;
            this.btnChangeImage.Click += new System.EventHandler(this.btnChangeImage_Click);
            // 
            // frmPersonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 820);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmPersonEditor";
            this.Text = "Person Editor";
            this.Load += new System.EventHandler(this.frmPersonEditor_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        private System.Windows.Forms.Button btnChangeImage;
    }
    
}
