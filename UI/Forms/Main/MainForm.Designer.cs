namespace InventorySystemUI.Main
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnAdjustments = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnEmployees = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnSalesOrders = new System.Windows.Forms.Button();
            this.btnPurchaseOrders = new System.Windows.Forms.Button();
            this.btnTransferOrders = new System.Windows.Forms.Button();
            this.btnWarehouses = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnPeople = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.lblMenuTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblAddress = new System.Windows.Forms.Label();
            this.ctrlClock1 = new UI.Shared.Controllers.ctrlClock();
            this.panelUserInfo = new System.Windows.Forms.Panel();
            this.picUserImage = new System.Windows.Forms.PictureBox();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblBranchName = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelStats4 = new System.Windows.Forms.Panel();
            this.lblStatValue4 = new System.Windows.Forms.Label();
            this.lblStatTitle4 = new System.Windows.Forms.Label();
            this.panelStats3 = new System.Windows.Forms.Panel();
            this.lblStatValue3 = new System.Windows.Forms.Label();
            this.lblStatTitle3 = new System.Windows.Forms.Label();
            this.panelStats2 = new System.Windows.Forms.Panel();
            this.lblStatValue2 = new System.Windows.Forms.Label();
            this.lblStatTitle2 = new System.Windows.Forms.Label();
            this.panelStats1 = new System.Windows.Forms.Panel();
            this.lblStatValue1 = new System.Windows.Forms.Label();
            this.lblStatTitle1 = new System.Windows.Forms.Label();
            this.btnReturnIn = new System.Windows.Forms.Button();
            this.btnReturnOut = new System.Windows.Forms.Button();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserImage)).BeginInit();
            this.panelMain.SuspendLayout();
            this.panelStats4.SuspendLayout();
            this.panelStats3.SuspendLayout();
            this.panelStats2.SuspendLayout();
            this.panelStats1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.panelSidebar.Controls.Add(this.btnReturnOut);
            this.panelSidebar.Controls.Add(this.btnReturnIn);
            this.panelSidebar.Controls.Add(this.btnAdjustments);
            this.panelSidebar.Controls.Add(this.btnUsers);
            this.panelSidebar.Controls.Add(this.btnEmployees);
            this.panelSidebar.Controls.Add(this.btnExit);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnSalesOrders);
            this.panelSidebar.Controls.Add(this.btnPurchaseOrders);
            this.panelSidebar.Controls.Add(this.btnTransferOrders);
            this.panelSidebar.Controls.Add(this.btnWarehouses);
            this.panelSidebar.Controls.Add(this.btnSuppliers);
            this.panelSidebar.Controls.Add(this.btnCustomers);
            this.panelSidebar.Controls.Add(this.btnPeople);
            this.panelSidebar.Controls.Add(this.btnProducts);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.lblMenuTitle);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(245, 820);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnAdjustments
            // 
            this.btnAdjustments.Location = new System.Drawing.Point(12, 684);
            this.btnAdjustments.Name = "btnAdjustments";
            this.btnAdjustments.Size = new System.Drawing.Size(221, 44);
            this.btnAdjustments.TabIndex = 17;
            this.btnAdjustments.Text = "Adjustments";
            this.btnAdjustments.UseVisualStyleBackColor = true;
            this.btnAdjustments.Click += new System.EventHandler(this.btnAdjustments_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.Location = new System.Drawing.Point(12, 362);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(221, 44);
            this.btnUsers.TabIndex = 7;
            this.btnUsers.Text = "Users";
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnEmployees
            // 
            this.btnEmployees.Location = new System.Drawing.Point(12, 315);
            this.btnEmployees.Name = "btnEmployees";
            this.btnEmployees.Size = new System.Drawing.Size(221, 44);
            this.btnEmployees.TabIndex = 16;
            this.btnEmployees.Text = "Employees";
            this.btnEmployees.UseVisualStyleBackColor = true;
            this.btnEmployees.Click += new System.EventHandler(this.btnEmployees_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 777);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(221, 44);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(12, 730);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(221, 44);
            this.btnLogout.TabIndex = 14;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnSalesOrders
            // 
            this.btnSalesOrders.Location = new System.Drawing.Point(12, 547);
            this.btnSalesOrders.Name = "btnSalesOrders";
            this.btnSalesOrders.Size = new System.Drawing.Size(221, 44);
            this.btnSalesOrders.TabIndex = 9;
            this.btnSalesOrders.Text = "Sales Orders";
            this.btnSalesOrders.UseVisualStyleBackColor = true;
            this.btnSalesOrders.Click += new System.EventHandler(this.btnSalesOrders_Click);
            // 
            // btnPurchaseOrders
            // 
            this.btnPurchaseOrders.Location = new System.Drawing.Point(12, 501);
            this.btnPurchaseOrders.Name = "btnPurchaseOrders";
            this.btnPurchaseOrders.Size = new System.Drawing.Size(221, 44);
            this.btnPurchaseOrders.TabIndex = 8;
            this.btnPurchaseOrders.Text = "Purchase Orders";
            this.btnPurchaseOrders.UseVisualStyleBackColor = true;
            this.btnPurchaseOrders.Click += new System.EventHandler(this.btnPurchaseOrders_Click);
            // 
            // btnTransferOrders
            // 
            this.btnTransferOrders.Location = new System.Drawing.Point(12, 455);
            this.btnTransferOrders.Name = "btnTransferOrders";
            this.btnTransferOrders.Size = new System.Drawing.Size(221, 44);
            this.btnTransferOrders.TabIndex = 7;
            this.btnTransferOrders.Text = "Transfer Orders";
            this.btnTransferOrders.UseVisualStyleBackColor = true;
            this.btnTransferOrders.Click += new System.EventHandler(this.btnTransferOrders_Click);
            // 
            // btnWarehouses
            // 
            this.btnWarehouses.Location = new System.Drawing.Point(12, 409);
            this.btnWarehouses.Name = "btnWarehouses";
            this.btnWarehouses.Size = new System.Drawing.Size(221, 44);
            this.btnWarehouses.TabIndex = 6;
            this.btnWarehouses.Text = "Warehouses";
            this.btnWarehouses.UseVisualStyleBackColor = true;
            this.btnWarehouses.Click += new System.EventHandler(this.btnWarehouses_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.Location = new System.Drawing.Point(12, 268);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(221, 44);
            this.btnSuppliers.TabIndex = 5;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.UseVisualStyleBackColor = true;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.Location = new System.Drawing.Point(12, 222);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(221, 44);
            this.btnCustomers.TabIndex = 4;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.UseVisualStyleBackColor = true;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnPeople
            // 
            this.btnPeople.Location = new System.Drawing.Point(12, 176);
            this.btnPeople.Name = "btnPeople";
            this.btnPeople.Size = new System.Drawing.Size(221, 44);
            this.btnPeople.TabIndex = 3;
            this.btnPeople.Text = "People";
            this.btnPeople.UseVisualStyleBackColor = true;
            this.btnPeople.Click += new System.EventHandler(this.btnPeople_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.Location = new System.Drawing.Point(12, 130);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(221, 44);
            this.btnProducts.TabIndex = 2;
            this.btnProducts.Text = "Products";
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(12, 82);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(221, 44);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.AutoSize = true;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMenuTitle.ForeColor = System.Drawing.Color.White;
            this.lblMenuTitle.Location = new System.Drawing.Point(18, 24);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new System.Drawing.Size(194, 37);
            this.lblMenuTitle.TabIndex = 0;
            this.lblMenuTitle.Text = "INVENTORY";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblAddress);
            this.panelHeader.Controls.Add(this.ctrlClock1);
            this.panelHeader.Controls.Add(this.panelUserInfo);
            this.panelHeader.Controls.Add(this.lblBranchName);
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(245, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1255, 126);
            this.panelHeader.TabIndex = 1;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAddress.ForeColor = System.Drawing.Color.Gray;
            this.lblAddress.Location = new System.Drawing.Point(31, 82);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(194, 23);
            this.lblAddress.TabIndex = 5;
            this.lblAddress.Text = "Palestine - Gaza - North";
            // 
            // ctrlClock1
            // 
            this.ctrlClock1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlClock1.Location = new System.Drawing.Point(324, 3);
            this.ctrlClock1.Name = "ctrlClock1";
            this.ctrlClock1.Size = new System.Drawing.Size(480, 53);
            this.ctrlClock1.TabIndex = 4;
            // 
            // panelUserInfo
            // 
            this.panelUserInfo.Controls.Add(this.picUserImage);
            this.panelUserInfo.Controls.Add(this.lblUserRole);
            this.panelUserInfo.Controls.Add(this.lblUserName);
            this.panelUserInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelUserInfo.Location = new System.Drawing.Point(810, 0);
            this.panelUserInfo.Name = "panelUserInfo";
            this.panelUserInfo.Padding = new System.Windows.Forms.Padding(5);
            this.panelUserInfo.Size = new System.Drawing.Size(445, 126);
            this.panelUserInfo.TabIndex = 3;
            // 
            // picUserImage
            // 
            this.picUserImage.BackColor = System.Drawing.Color.Gainsboro;
            this.picUserImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.picUserImage.Location = new System.Drawing.Point(307, 5);
            this.picUserImage.Name = "picUserImage";
            this.picUserImage.Size = new System.Drawing.Size(133, 116);
            this.picUserImage.TabIndex = 2;
            this.picUserImage.TabStop = false;
            this.picUserImage.Click += new System.EventHandler(this.picUserImage_Click);
            // 
            // lblUserRole
            // 
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUserRole.ForeColor = System.Drawing.Color.Gray;
            this.lblUserRole.Location = new System.Drawing.Point(27, 68);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(268, 20);
            this.lblUserRole.TabIndex = 1;
            this.lblUserRole.Text = "Role";
            this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblUserName.Location = new System.Drawing.Point(8, 17);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(287, 41);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Username";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBranchName
            // 
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBranchName.ForeColor = System.Drawing.Color.Gray;
            this.lblBranchName.Location = new System.Drawing.Point(30, 58);
            this.lblBranchName.Name = "lblBranchName";
            this.lblBranchName.Size = new System.Drawing.Size(208, 23);
            this.lblBranchName.TabIndex = 2;
            this.lblBranchName.Text = "Main Warehouse / Branch";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblPageTitle.Location = new System.Drawing.Point(28, 17);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(171, 41);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Dashboard";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelMain.Controls.Add(this.panelStats4);
            this.panelMain.Controls.Add(this.panelStats3);
            this.panelMain.Controls.Add(this.panelStats2);
            this.panelMain.Controls.Add(this.panelStats1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(245, 126);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(24);
            this.panelMain.Size = new System.Drawing.Size(1255, 694);
            this.panelMain.TabIndex = 2;
            // 
            // panelStats4
            // 
            this.panelStats4.BackColor = System.Drawing.Color.White;
            this.panelStats4.Controls.Add(this.lblStatValue4);
            this.panelStats4.Controls.Add(this.lblStatTitle4);
            this.panelStats4.Location = new System.Drawing.Point(874, 32);
            this.panelStats4.Name = "panelStats4";
            this.panelStats4.Size = new System.Drawing.Size(260, 120);
            this.panelStats4.TabIndex = 3;
            // 
            // lblStatValue4
            // 
            this.lblStatValue4.AutoSize = true;
            this.lblStatValue4.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblStatValue4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblStatValue4.Location = new System.Drawing.Point(20, 47);
            this.lblStatValue4.Name = "lblStatValue4";
            this.lblStatValue4.Size = new System.Drawing.Size(60, 47);
            this.lblStatValue4.TabIndex = 1;
            this.lblStatValue4.Text = "32";
            // 
            // lblStatTitle4
            // 
            this.lblStatTitle4.AutoSize = true;
            this.lblStatTitle4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatTitle4.ForeColor = System.Drawing.Color.Gray;
            this.lblStatTitle4.Location = new System.Drawing.Point(20, 18);
            this.lblStatTitle4.Name = "lblStatTitle4";
            this.lblStatTitle4.Size = new System.Drawing.Size(118, 23);
            this.lblStatTitle4.TabIndex = 0;
            this.lblStatTitle4.Text = "Open Invoices";
            // 
            // panelStats3
            // 
            this.panelStats3.BackColor = System.Drawing.Color.White;
            this.panelStats3.Controls.Add(this.lblStatValue3);
            this.panelStats3.Controls.Add(this.lblStatTitle3);
            this.panelStats3.Location = new System.Drawing.Point(594, 32);
            this.panelStats3.Name = "panelStats3";
            this.panelStats3.Size = new System.Drawing.Size(260, 120);
            this.panelStats3.TabIndex = 2;
            // 
            // lblStatValue3
            // 
            this.lblStatValue3.AutoSize = true;
            this.lblStatValue3.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblStatValue3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblStatValue3.Location = new System.Drawing.Point(20, 47);
            this.lblStatValue3.Name = "lblStatValue3";
            this.lblStatValue3.Size = new System.Drawing.Size(60, 47);
            this.lblStatValue3.TabIndex = 1;
            this.lblStatValue3.Text = "14";
            // 
            // lblStatTitle3
            // 
            this.lblStatTitle3.AutoSize = true;
            this.lblStatTitle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatTitle3.ForeColor = System.Drawing.Color.Gray;
            this.lblStatTitle3.Location = new System.Drawing.Point(20, 18);
            this.lblStatTitle3.Name = "lblStatTitle3";
            this.lblStatTitle3.Size = new System.Drawing.Size(132, 23);
            this.lblStatTitle3.TabIndex = 0;
            this.lblStatTitle3.Text = "Low Stock Items";
            // 
            // panelStats2
            // 
            this.panelStats2.BackColor = System.Drawing.Color.White;
            this.panelStats2.Controls.Add(this.lblStatValue2);
            this.panelStats2.Controls.Add(this.lblStatTitle2);
            this.panelStats2.Location = new System.Drawing.Point(314, 32);
            this.panelStats2.Name = "panelStats2";
            this.panelStats2.Size = new System.Drawing.Size(260, 120);
            this.panelStats2.TabIndex = 1;
            // 
            // lblStatValue2
            // 
            this.lblStatValue2.AutoSize = true;
            this.lblStatValue2.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblStatValue2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblStatValue2.Location = new System.Drawing.Point(20, 47);
            this.lblStatValue2.Name = "lblStatValue2";
            this.lblStatValue2.Size = new System.Drawing.Size(80, 47);
            this.lblStatValue2.TabIndex = 1;
            this.lblStatValue2.Text = "480";
            // 
            // lblStatTitle2
            // 
            this.lblStatTitle2.AutoSize = true;
            this.lblStatTitle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatTitle2.ForeColor = System.Drawing.Color.Gray;
            this.lblStatTitle2.Location = new System.Drawing.Point(20, 18);
            this.lblStatTitle2.Name = "lblStatTitle2";
            this.lblStatTitle2.Size = new System.Drawing.Size(77, 23);
            this.lblStatTitle2.TabIndex = 0;
            this.lblStatTitle2.Text = "Products";
            // 
            // panelStats1
            // 
            this.panelStats1.BackColor = System.Drawing.Color.White;
            this.panelStats1.Controls.Add(this.lblStatValue1);
            this.panelStats1.Controls.Add(this.lblStatTitle1);
            this.panelStats1.Location = new System.Drawing.Point(34, 32);
            this.panelStats1.Name = "panelStats1";
            this.panelStats1.Size = new System.Drawing.Size(260, 120);
            this.panelStats1.TabIndex = 0;
            // 
            // lblStatValue1
            // 
            this.lblStatValue1.AutoSize = true;
            this.lblStatValue1.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblStatValue1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblStatValue1.Location = new System.Drawing.Point(20, 47);
            this.lblStatValue1.Name = "lblStatValue1";
            this.lblStatValue1.Size = new System.Drawing.Size(60, 47);
            this.lblStatValue1.TabIndex = 1;
            this.lblStatValue1.Text = "25";
            // 
            // lblStatTitle1
            // 
            this.lblStatTitle1.AutoSize = true;
            this.lblStatTitle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatTitle1.ForeColor = System.Drawing.Color.Gray;
            this.lblStatTitle1.Location = new System.Drawing.Point(20, 18);
            this.lblStatTitle1.Name = "lblStatTitle1";
            this.lblStatTitle1.Size = new System.Drawing.Size(102, 23);
            this.lblStatTitle1.TabIndex = 0;
            this.lblStatTitle1.Text = "Warehouses";
            // 
            // btnReturnIn
            // 
            this.btnReturnIn.Location = new System.Drawing.Point(12, 592);
            this.btnReturnIn.Name = "btnReturnIn";
            this.btnReturnIn.Size = new System.Drawing.Size(221, 44);
            this.btnReturnIn.TabIndex = 18;
            this.btnReturnIn.Text = "Return In Orders";
            this.btnReturnIn.UseVisualStyleBackColor = true;
            this.btnReturnIn.Click += new System.EventHandler(this.btnReturnIn_Click);
            // 
            // btnReturnOut
            // 
            this.btnReturnOut.Location = new System.Drawing.Point(12, 638);
            this.btnReturnOut.Name = "btnReturnOut";
            this.btnReturnOut.Size = new System.Drawing.Size(221, 44);
            this.btnReturnOut.TabIndex = 19;
            this.btnReturnOut.Text = "Return Out Orders";
            this.btnReturnOut.UseVisualStyleBackColor = true;
            this.btnReturnOut.Click += new System.EventHandler(this.btnReturnOut_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1500, 820);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(1366, 768);
            this.Name = "MainForm";
            this.Text = "Inventory Management System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelUserInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picUserImage)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelStats4.ResumeLayout(false);
            this.panelStats4.PerformLayout();
            this.panelStats3.ResumeLayout(false);
            this.panelStats3.PerformLayout();
            this.panelStats2.ResumeLayout(false);
            this.panelStats2.PerformLayout();
            this.panelStats1.ResumeLayout(false);
            this.panelStats1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblMenuTitle;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnPeople;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnWarehouses;
        private System.Windows.Forms.Button btnTransferOrders;
        private System.Windows.Forms.Button btnPurchaseOrders;
        private System.Windows.Forms.Button btnSalesOrders;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelUserInfo;
        private System.Windows.Forms.PictureBox picUserImage;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblBranchName;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelStats1;
        private System.Windows.Forms.Label lblStatTitle1;
        private System.Windows.Forms.Label lblStatValue1;
        private System.Windows.Forms.Panel panelStats2;
        private System.Windows.Forms.Label lblStatValue2;
        private System.Windows.Forms.Label lblStatTitle2;
        private System.Windows.Forms.Panel panelStats3;
        private System.Windows.Forms.Label lblStatValue3;
        private System.Windows.Forms.Label lblStatTitle3;
        private System.Windows.Forms.Panel panelStats4;
        private System.Windows.Forms.Label lblStatValue4;
        private System.Windows.Forms.Label lblStatTitle4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogout;
        private UI.Shared.Controllers.ctrlClock ctrlClock1;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Button btnEmployees;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnAdjustments;
        private System.Windows.Forms.Button btnReturnIn;
        private System.Windows.Forms.Button btnReturnOut;
    }
}

