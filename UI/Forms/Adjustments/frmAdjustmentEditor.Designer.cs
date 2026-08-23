namespace UI.Forms.Adjustments
{
    partial class frmAdjustmentEditor
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelFooter;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.FlowLayoutPanel flowBody;

        private System.Windows.Forms.GroupBox groupHeader;
        private System.Windows.Forms.GroupBox groupDetails;
        private System.Windows.Forms.GroupBox groupSummary;

        private System.Windows.Forms.Panel panelHeaderFields;
        private System.Windows.Forms.FlowLayoutPanel flowSelections;

        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.ComboBox cmbReason;

        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;

        private System.Windows.Forms.Label lblHint;

        private System.Windows.Forms.Panel pnlWarehouse;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.TextBox txtWarehouse;
        private System.Windows.Forms.Button btnSelectWarehouse;

        private System.Windows.Forms.Panel panelDetailInputs;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAddDetail;
        private System.Windows.Forms.Button btnRemoveDetail;
        private System.Windows.Forms.Button btnUpdateQuantity;

        private UI.Shared.Controllers.DgvCustom dgvDetails;

        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

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
            this.panelBody = new System.Windows.Forms.Panel();
            this.flowBody = new System.Windows.Forms.FlowLayoutPanel();
            this.groupHeader = new System.Windows.Forms.GroupBox();
            this.flowSelections = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlWarehouse = new System.Windows.Forms.Panel();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.txtWarehouse = new System.Windows.Forms.TextBox();
            this.btnSelectWarehouse = new System.Windows.Forms.Button();
            this.panelHeaderFields = new System.Windows.Forms.Panel();
            this.lblReason = new System.Windows.Forms.Label();
            this.cmbReason = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.groupDetails = new System.Windows.Forms.GroupBox();
            this.panelDetailInputs = new System.Windows.Forms.Panel();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAddDetail = new System.Windows.Forms.Button();
            this.btnUpdateQuantity = new System.Windows.Forms.Button();
            this.btnRemoveDetail = new System.Windows.Forms.Button();
            this.groupSummary = new System.Windows.Forms.GroupBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvDetails = new UI.Shared.Controllers.DgvCustom();
            this.panelRoot.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.flowBody.SuspendLayout();
            this.groupHeader.SuspendLayout();
            this.flowSelections.SuspendLayout();
            this.pnlWarehouse.SuspendLayout();
            this.panelHeaderFields.SuspendLayout();
            this.groupDetails.SuspendLayout();
            this.panelDetailInputs.SuspendLayout();
            this.groupSummary.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.panelRoot.Size = new System.Drawing.Size(980, 760);
            this.panelRoot.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelBody.Controls.Add(this.flowBody);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 100);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(980, 580);
            this.panelBody.TabIndex = 0;
            // 
            // flowBody
            // 
            this.flowBody.AutoScroll = true;
            this.flowBody.AutoSize = true;
            this.flowBody.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowBody.Controls.Add(this.groupHeader);
            this.flowBody.Controls.Add(this.groupDetails);
            this.flowBody.Controls.Add(this.groupSummary);
            this.flowBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBody.Location = new System.Drawing.Point(0, 0);
            this.flowBody.Name = "flowBody";
            this.flowBody.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.flowBody.Size = new System.Drawing.Size(980, 580);
            this.flowBody.TabIndex = 0;
            this.flowBody.WrapContents = false;
            // 
            // groupHeader
            // 
            this.groupHeader.BackColor = System.Drawing.Color.White;
            this.groupHeader.Controls.Add(this.flowSelections);
            this.groupHeader.Controls.Add(this.panelHeaderFields);
            this.groupHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupHeader.Location = new System.Drawing.Point(24, 20);
            this.groupHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.groupHeader.Name = "groupHeader";
            this.groupHeader.Size = new System.Drawing.Size(910, 220);
            this.groupHeader.TabIndex = 0;
            this.groupHeader.TabStop = false;
            this.groupHeader.Text = "Adjustment Header";
            // 
            // flowSelections
            // 
            this.flowSelections.Controls.Add(this.pnlWarehouse);
            this.flowSelections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSelections.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowSelections.Location = new System.Drawing.Point(3, 121);
            this.flowSelections.Name = "flowSelections";
            this.flowSelections.Padding = new System.Windows.Forms.Padding(20, 5, 20, 10);
            this.flowSelections.Size = new System.Drawing.Size(904, 96);
            this.flowSelections.TabIndex = 0;
            this.flowSelections.WrapContents = false;
            // 
            // pnlWarehouse
            // 
            this.pnlWarehouse.Controls.Add(this.lblWarehouse);
            this.pnlWarehouse.Controls.Add(this.txtWarehouse);
            this.pnlWarehouse.Controls.Add(this.btnSelectWarehouse);
            this.pnlWarehouse.Location = new System.Drawing.Point(20, 5);
            this.pnlWarehouse.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlWarehouse.Name = "pnlWarehouse";
            this.pnlWarehouse.Size = new System.Drawing.Size(850, 50);
            this.pnlWarehouse.TabIndex = 0;
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarehouse.ForeColor = System.Drawing.Color.Gray;
            this.lblWarehouse.Location = new System.Drawing.Point(0, 0);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(180, 22);
            this.lblWarehouse.TabIndex = 0;
            this.lblWarehouse.Text = "Warehouse *";
            // 
            // txtWarehouse
            // 
            this.txtWarehouse.Location = new System.Drawing.Point(0, 22);
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.Size = new System.Drawing.Size(760, 30);
            this.txtWarehouse.TabIndex = 1;
            // 
            // btnSelectWarehouse
            // 
            this.btnSelectWarehouse.Location = new System.Drawing.Point(770, 22);
            this.btnSelectWarehouse.Name = "btnSelectWarehouse";
            this.btnSelectWarehouse.Size = new System.Drawing.Size(45, 27);
            this.btnSelectWarehouse.TabIndex = 2;
            this.btnSelectWarehouse.Text = "...";
            this.btnSelectWarehouse.UseVisualStyleBackColor = false;
            this.btnSelectWarehouse.Click += new System.EventHandler(this.btnSelectWarehouse_Click);
            // 
            // panelHeaderFields
            // 
            this.panelHeaderFields.Controls.Add(this.lblReason);
            this.panelHeaderFields.Controls.Add(this.cmbReason);
            this.panelHeaderFields.Controls.Add(this.lblType);
            this.panelHeaderFields.Controls.Add(this.cmbType);
            this.panelHeaderFields.Controls.Add(this.lblHint);
            this.panelHeaderFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeaderFields.Location = new System.Drawing.Point(3, 26);
            this.panelHeaderFields.Name = "panelHeaderFields";
            this.panelHeaderFields.Size = new System.Drawing.Size(904, 95);
            this.panelHeaderFields.TabIndex = 1;
            // 
            // lblReason
            // 
            this.lblReason.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReason.ForeColor = System.Drawing.Color.Gray;
            this.lblReason.Location = new System.Drawing.Point(24, 28);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(180, 22);
            this.lblReason.TabIndex = 0;
            this.lblReason.Text = "Adjustment Reason *";
            // 
            // cmbReason
            // 
            this.cmbReason.Location = new System.Drawing.Point(24, 52);
            this.cmbReason.Name = "cmbReason";
            this.cmbReason.Size = new System.Drawing.Size(260, 31);
            this.cmbReason.TabIndex = 1;
            this.cmbReason.SelectedIndexChanged += new System.EventHandler(this.cmbReason_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblType.ForeColor = System.Drawing.Color.Gray;
            this.lblType.Location = new System.Drawing.Point(320, 28);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(180, 22);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Adjustment Type *";
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(320, 52);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(220, 31);
            this.cmbType.TabIndex = 3;
            // 
            // lblHint
            // 
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(575, 45);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(300, 42);
            this.lblHint.TabIndex = 4;
            this.lblHint.Text = "Adjustment hint";
            // 
            // groupDetails
            // 
            this.groupDetails.BackColor = System.Drawing.Color.White;
            this.groupDetails.Controls.Add(this.dgvDetails);
            this.groupDetails.Controls.Add(this.panelDetailInputs);
            this.groupDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupDetails.Location = new System.Drawing.Point(24, 254);
            this.groupDetails.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.groupDetails.Name = "groupDetails";
            this.groupDetails.Size = new System.Drawing.Size(910, 405);
            this.groupDetails.TabIndex = 1;
            this.groupDetails.TabStop = false;
            this.groupDetails.Text = "Adjustment Details";
            // 
            // panelDetailInputs
            // 
            this.panelDetailInputs.Controls.Add(this.lblQuantity);
            this.panelDetailInputs.Controls.Add(this.txtQuantity);
            this.panelDetailInputs.Controls.Add(this.btnAddDetail);
            this.panelDetailInputs.Controls.Add(this.btnUpdateQuantity);
            this.panelDetailInputs.Controls.Add(this.btnRemoveDetail);
            this.panelDetailInputs.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetailInputs.Location = new System.Drawing.Point(3, 26);
            this.panelDetailInputs.Name = "panelDetailInputs";
            this.panelDetailInputs.Size = new System.Drawing.Size(904, 76);
            this.panelDetailInputs.TabIndex = 1;
            // 
            // lblQuantity
            // 
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuantity.ForeColor = System.Drawing.Color.Gray;
            this.lblQuantity.Location = new System.Drawing.Point(24, 15);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(120, 22);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Quantity";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(24, 38);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(160, 30);
            this.txtQuantity.TabIndex = 1;
            this.txtQuantity.Text = "1";
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.Location = new System.Drawing.Point(220, 30);
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Size = new System.Drawing.Size(145, 36);
            this.btnAddDetail.TabIndex = 2;
            this.btnAddDetail.Text = "Add Product";
            this.btnAddDetail.UseVisualStyleBackColor = false;
            this.btnAddDetail.Click += new System.EventHandler(this.btnAddDetail_Click);
            // 
            // btnUpdateQuantity
            // 
            this.btnUpdateQuantity.Location = new System.Drawing.Point(380, 30);
            this.btnUpdateQuantity.Name = "btnUpdateQuantity";
            this.btnUpdateQuantity.Size = new System.Drawing.Size(145, 36);
            this.btnUpdateQuantity.TabIndex = 3;
            this.btnUpdateQuantity.Text = "Update Qty";
            this.btnUpdateQuantity.UseVisualStyleBackColor = false;
            this.btnUpdateQuantity.Click += new System.EventHandler(this.btnUpdateQuantity_Click);
            // 
            // btnRemoveDetail
            // 
            this.btnRemoveDetail.Location = new System.Drawing.Point(540, 30);
            this.btnRemoveDetail.Name = "btnRemoveDetail";
            this.btnRemoveDetail.Size = new System.Drawing.Size(145, 36);
            this.btnRemoveDetail.TabIndex = 4;
            this.btnRemoveDetail.Text = "Remove";
            this.btnRemoveDetail.UseVisualStyleBackColor = false;
            this.btnRemoveDetail.Click += new System.EventHandler(this.btnRemoveDetail_Click);
            // 
            // groupSummary
            // 
            this.groupSummary.BackColor = System.Drawing.Color.White;
            this.groupSummary.Controls.Add(this.lblNotes);
            this.groupSummary.Controls.Add(this.txtNotes);
            this.groupSummary.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.groupSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.groupSummary.Location = new System.Drawing.Point(24, 673);
            this.groupSummary.Margin = new System.Windows.Forms.Padding(0, 0, 0, 30);
            this.groupSummary.Name = "groupSummary";
            this.groupSummary.Size = new System.Drawing.Size(910, 145);
            this.groupSummary.TabIndex = 2;
            this.groupSummary.TabStop = false;
            this.groupSummary.Text = "Notes";
            // 
            // lblNotes
            // 
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNotes.ForeColor = System.Drawing.Color.Gray;
            this.lblNotes.Location = new System.Drawing.Point(24, 32);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(120, 22);
            this.lblNotes.TabIndex = 0;
            this.lblNotes.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(24, 58);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(850, 65);
            this.txtNotes.TabIndex = 1;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 680);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(980, 80);
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
            this.btnSave.Location = new System.Drawing.Point(735, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(850, 20);
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
            this.panelHeader.Size = new System.Drawing.Size(980, 100);
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
            this.lblTitle.Text = "Adjustment Editor";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(28, 62);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(760, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Create or update inventory adjustment.";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // dgvDetails
            // 
            this.dgvDetails.BackColor = System.Drawing.Color.White;
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvDetails.Location = new System.Drawing.Point(3, 102);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.Size = new System.Drawing.Size(904, 300);
            this.dgvDetails.TabIndex = 0;
            // 
            // frmAdjustmentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 760);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmAdjustmentEditor";
            this.Text = "Adjustment Editor";
            this.Load += new System.EventHandler(this.frmAdjustmentEditor_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowBody.ResumeLayout(false);
            this.groupHeader.ResumeLayout(false);
            this.flowSelections.ResumeLayout(false);
            this.pnlWarehouse.ResumeLayout(false);
            this.pnlWarehouse.PerformLayout();
            this.panelHeaderFields.ResumeLayout(false);
            this.groupDetails.ResumeLayout(false);
            this.panelDetailInputs.ResumeLayout(false);
            this.panelDetailInputs.PerformLayout();
            this.groupSummary.ResumeLayout(false);
            this.groupSummary.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

