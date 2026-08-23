using HotelSystemUI.Login;
 using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI;
using UI.Forms.Adjustments;
using UI.Forms.Customers;
using UI.Forms.Dashboard;
using UI.Forms.Employees;
using UI.Forms.Orders;
using UI.Forms.People;
using UI.Forms.Products;
using UI.Forms.Suppliers;
using UI.Forms.Users;
using UI.Forms.Warehouses;
using UI.Helpers.UI_Helpers;
using UI.Services;
using UI.Shared;
using UI.Shared.CurrentUser;
using UI.Shared.Helpers.IO_Helper;
using UI.Shared.Storage;

namespace InventorySystemUI.Main
{
    public partial class MainForm : BaseForm
    {
        frmLogin _login;
         public MainForm(frmLogin login)
        {
            InitializeComponent();
            login.Visible = false;
            this._login = login;
            _ = SetupUI(); 
        }


        private async Task SetupUI()
        {
            ctrlClock1.StartClock();
            this.Text = "Inventory Management System";
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(243, 246, 249);

            StyleSidebarButton(btnUsers);

            StyleSidebarButton(btnDashboard);
            StyleSidebarButton(btnProducts);
            StyleSidebarButton(btnPeople);
            StyleSidebarButton(btnCustomers);
            StyleSidebarButton(btnSuppliers);
            StyleSidebarButton(btnWarehouses);
            StyleSidebarButton(btnTransferOrders);
            StyleSidebarButton(btnPurchaseOrders);
            StyleSidebarButton(btnSalesOrders);
            StyleSidebarButton(btnReturnIn);
            StyleSidebarButton(btnReturnOut);
            StyleSidebarButton(btnLogout);
            StyleSidebarButton(btnExit);
            StyleSidebarButton(btnEmployees);
            StyleSidebarButton(btnAdjustments);

            HighlightActiveButton(btnDashboard);

            lblUserName.Text = CurrentUser.User == null ? "Ahmed Hassan" : CurrentUser.User.Username;
            lblUserRole.Text = CurrentUser.User == null ? "System Administrator" : CurrentUser.User.Role.ToString();
         
            lblBranchName.Text =CurrentUser.User == null ? "Main Warehouse / Head Office" : 
                CurrentUser.User.Employee.Warehouse == null ? "Main Warehouse / Head Office" : 
                CurrentUser.User.Employee.Warehouse.Name;

            var address  = CurrentUser.User.Employee.Warehouse.Address;
            var country = address.Country;
            lblAddress.Text = CurrentUser.User == null ? "Main Warehouse / Head Office" :
                CurrentUser.User.Employee.Warehouse == null ? "Main Warehouse / Head Office" :
              country.Name + " - " + address.City.Name + " - " + address.Street + " - "  + address.BuildingNumber ;


            picUserImage.SizeMode = PictureBoxSizeMode.Zoom;
            picUserImage.BackColor = Color.White;
        
          
            ImageHelper.LoadDefaultImage(picUserImage);

            var userimage = await PeopleServices.GetPersonImage(CurrentUser.User.Employee.PersonId);

            if (userimage != null && userimage.Length > 0)
            {

                Image img = FileHelper.BytesToImage(userimage);

                picUserImage.Image = img;

            }

            await Task.CompletedTask;
        }

      
        private Color DefaultButtonColor = Color.FromArgb(24, 33, 45);
        private Color HightLightButtonColor = Color.FromArgb(74, 112, 139);
        private Color HoverButtonColor = Color.FromArgb(34, 45, 60);

      
        
        private void StyleSidebarButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = DefaultButtonColor;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            button.Height = 44;
            button.Cursor = Cursors.Hand;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(18, 0, 0, 0);

            button.MouseEnter += (s, e) =>
            {
                if (button.BackColor != HightLightButtonColor)
                    button.BackColor = (HoverButtonColor);
            };

            button.MouseLeave += (s, e) =>
            {
                if (button.BackColor != HightLightButtonColor)
                    button.BackColor = DefaultButtonColor;
            };
        }
        private void HighlightActiveButton(Button activeButton)
        {
            Button[] buttons =
            {
                btnDashboard,
                btnProducts,
                btnPeople,
                btnCustomers,
                btnSuppliers,
                btnWarehouses,
                btnTransferOrders,
                btnPurchaseOrders,
                btnSalesOrders,
                btnAdjustments,
                btnReturnOut,
                btnReturnIn,
                btnLogout ,
                btnExit,btnUsers,
                btnEmployees
            };

            foreach (var btn in buttons)
            {
                btn.BackColor = DefaultButtonColor;
            }

            activeButton.BackColor = HightLightButtonColor;
        }
        private void OpenSection(string sectionName)
        {
            lblPageTitle.Text = sectionName;
        }

        frmDashboard frmDash = new frmDashboard();
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Show(frmDash);
            HighlightActiveButton(btnDashboard);
            OpenSection("Dashboard");
        }
        frmShowProducts frm = new frmShowProducts(CurrentUser.User.Employee.WarehouseId.Value);
        private void Show(Form frm) {
           
            this.panelMain.Controls.Clear();
            this.panelMain.Controls.Add(frm);
            frm.Show();
        }
        private void btnProducts_Click(object sender, EventArgs e)
        {
            Show(frm);
            HighlightActiveButton(btnProducts);
            OpenSection("Products");
        }
       
        frmShowPeople pf = new frmShowPeople();
        private void btnPeople_Click(object sender, EventArgs e)
        {
            Show(pf);
            HighlightActiveButton(btnPeople);
            OpenSection("People");
        }

        frmShowCustomers frmC = new frmShowCustomers();
        private void btnCustomers_Click(object sender, EventArgs e)
        {
            Show(frmC);
            HighlightActiveButton(btnCustomers);
            OpenSection("Customers");
        }

        frmShowSuppliers frS = new frmShowSuppliers();
        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            Show(frS);
            HighlightActiveButton(btnSuppliers);
            OpenSection("Suppliers");
        }
        frmShowWarehouses frmWF = new frmShowWarehouses();
        private void btnWarehouses_Click(object sender, EventArgs e)
        {
            Show(frmWF);
            HighlightActiveButton(btnWarehouses);
            OpenSection("Warehouses");
        }

        frmShowOrders frmEDsss = new frmShowOrders();
        private void btnTrasnfer_Click(object sender, EventArgs e)
        {
            frmEDsss = new frmShowOrders(OrderType.Transfer); 
            Show(frmEDsss);
             HighlightActiveButton(btnTransferOrders);
            OpenSection("Transactions");
        }

        private void btnPurchaseOrders_Click(object sender, EventArgs e)
        {
            frmEDsss = new frmShowOrders(OrderType.Purchase);
            Show(frmEDsss);

            HighlightActiveButton(btnPurchaseOrders);
            OpenSection("Purchase Orders");
        }

        private void btnSalesOrders_Click(object sender, EventArgs e)
        {
            frmEDsss = new frmShowOrders(OrderType.Sale);
            Show(frmEDsss);

            HighlightActiveButton(btnSalesOrders);
            OpenSection("Sales Orders");
        }

        //private void btnMovements_Click(object sender, EventArgs e)
        //{
        //    HighlightActiveButton(btnMovements);
        //    OpenSection("Stock Movements");
        //}

        //private void btnInvoices_Click(object sender, EventArgs e)
        //{
        //    HighlightActiveButton(btnInvoices);
        //    OpenSection("Invoices");
        //}

        //private void btnReports_Click(object sender, EventArgs e)
        //{
        //    HighlightActiveButton(btnReports);
        //    OpenSection("Reports");
        //}

        //private void btnSettings_Click(object sender, EventArgs e)
        //{
        //    HighlightActiveButton(btnSettings);
        //    OpenSection("Settings");
        //}

        private void picUserImage_Click(object sender, EventArgs e)
        {
            if (picUserImage.Image == null)
                return;

            frmImagePreviewer preview = new frmImagePreviewer(picUserImage.Image);
          
            preview.ShowDialog();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
           //  ImageHelper.MakePictureBoxCircular(picUserImage);
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            SecurityStorage.Clear();
            RegisteryStorage.DeleteEmail();
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {

        }
        frmShowEmployees frmE = new frmShowEmployees();
        private void btnEmployees_Click(object sender, EventArgs e)
        {
            Show(frmE);
            HighlightActiveButton(btnEmployees);
            OpenSection("Employees");

        }
        frmShowUsers frmUsers = new frmShowUsers();

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Show(frmUsers);
            HighlightActiveButton(btnUsers);
            OpenSection("Users");
        }

        frmShowAdjustments frmAdg = new frmShowAdjustments();

        private void btnAdjustments_Click(object sender, EventArgs e)
        {
            Show(frmAdg);
            HighlightActiveButton(btnAdjustments);
            OpenSection("Adjustments");

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Show(frmDash);
            HighlightActiveButton(btnDashboard);
            OpenSection("Dashboard");

        }

        private void btnReturnIn_Click(object sender, EventArgs e)
        {
            frmEDsss = new frmShowOrders(OrderType.ReturnIn);
            Show(frmEDsss);

            HighlightActiveButton(btnReturnIn);
            OpenSection("Returns In Orders");

        }

        private void btnReturnOut_Click(object sender, EventArgs e)
        {
            frmEDsss = new frmShowOrders(OrderType.ReturnOut);
            Show(frmEDsss);

            HighlightActiveButton(btnReturnOut);
            OpenSection("Return Out Orders");

        }
    }
}

