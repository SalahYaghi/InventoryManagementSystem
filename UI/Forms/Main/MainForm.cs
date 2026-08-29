using HotelSystemUI.Login;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using UI.Shared.Helpers.UI_Helpers;
using UI.Shared.Storage;

namespace InventorySystemUI.Main
{
    public partial class MainForm : BaseForm
    {
        private readonly Color DefaultButtonColor = Color.FromArgb(24, 33, 45);
        private readonly Color HighlightButtonColor = Color.FromArgb(74, 112, 139);
        private readonly Color HoverButtonColor = Color.FromArgb(34, 45, 60);

        private readonly frmLogin _login;
        private readonly Dictionary<string, Form> _sections = new Dictionary<string, Form>();

        private Form _activeSection;

        public MainForm(frmLogin login)
        {
            InitializeComponent();

            _login = login;

            if (_login != null)
                _login.Visible = false;

            _ = SetupUI();
 
        }

        #region Setup

        private async Task SetupUI()
        {
            ctrlClock1.StartClock();

            Text = "Inventory Management System";
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(243, 246, 249);

            StyleSidebar();
            BindUserHeader();

            await LoadUserImage();
        }

        private void StyleSidebar()
        {
            foreach (Button button in SidebarButtons())
                StyleSidebarButton(button);
        }

        private Button[] SidebarButtons()
        {
            return new[]
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
                btnReturnIn,
                btnReturnOut,
                btnAdjustments,
                btnEmployees,
                btnUsers,
                btnLogout,
                btnExit
            };
        }

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
                if (button.BackColor != HighlightButtonColor)
                    button.BackColor = HoverButtonColor;
            };

            button.MouseLeave += (s, e) =>
            {
                if (button.BackColor != HighlightButtonColor)
                    button.BackColor = DefaultButtonColor;
            };
        }

        private void HighlightActiveButton(Button activeButton)
        {
            foreach (Button button in SidebarButtons())
                button.BackColor = DefaultButtonColor;

            if (activeButton != null)
                activeButton.BackColor = HighlightButtonColor;
        }

        #endregion

        #region User header

        private void BindUserHeader()
        {
            var user = CurrentUser.User;

            lblUserName.Text = user == null
                ? DisplayFormatter.NotAvailablePlaceholder
                : DisplayFormatter.Text(user.Username, DisplayFormatter.NotAvailablePlaceholder);

            lblUserRole.Text = user == null
                ? DisplayFormatter.EmptyPlaceholder
                : user.Role.ToString();

            var warehouse = user == null || user.Employee == null ? null : user.Employee.Warehouse;

            lblBranchName.Text = warehouse == null
                ? "No warehouse assigned"
                : DisplayFormatter.Text(warehouse.Name, "No warehouse assigned");

            lblAddress.Text = BuildAddressText(warehouse);

            picUserImage.SizeMode = PictureBoxSizeMode.Zoom;
            picUserImage.BackColor = Color.White;

            ImageHelper.LoadDefaultImage(picUserImage);
        }

        private string BuildAddressText(Contract.Responses.WarehouseDto warehouse)
        {
            if (warehouse == null || warehouse.Address == null)
                return DisplayFormatter.NotSetPlaceholder;

            var address = warehouse.Address;

            string text = TextFormattingHelper.JoinString(new[]
            {
                address.Country == null ? null : address.Country.Name,
                address.City == null ? null : address.City.Name,
                address.Street,
                address.BuildingNumber
            }, " - ");

            return DisplayFormatter.Text(text, DisplayFormatter.NotSetPlaceholder);
        }

        private async Task LoadUserImage()
        {
            var user = CurrentUser.User;

            if (user == null || user.Employee == null)
                return;

            try
            {
                byte[] imageBytes = await PeopleServices.GetPersonImage(user.Employee.PersonId);

                if (imageBytes == null || imageBytes.Length == 0)
                    return;

                Image image = FileHelper.BytesToImage(imageBytes);

                if (image != null)
                    ImageHelper.SetImage(picUserImage, image);
            }
            catch (Exception)
            {
                ImageHelper.LoadDefaultImage(picUserImage);
            }
        }

        #endregion

        #region Section navigation

        private void ShowSection(string key, Func<Form> factory, Button sourceButton, string title)
        {
            Form section;

            if (!_sections.TryGetValue(key, out section) || section.IsDisposed)
            {
                section = factory();
                _sections[key] = section;
            }

            if (!ReferenceEquals(_activeSection, section))
            {
                panelMain.Controls.Clear();
                panelMain.Controls.Add(section);
                _activeSection = section;
            }

            section.Show();
            section.BringToFront();

            HighlightActiveButton(sourceButton);
            lblPageTitle.Text = title;
        }

        private void ShowOrders(OrderType orderType, Button sourceButton, string title)
        {
            ShowSection("Orders:" + orderType, () => new frmShowOrders(orderType), sourceButton, title);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnDashboard_Click(sender, e);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
           ShowSection("Dashboard", () => new frmDashboard(), btnDashboard, "Dashboard");

            var dashboard = _sections["Dashboard"] as frmDashboard;

            if (dashboard != null)
                _ = dashboard.RefreshData();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            Guid warehouseId = CurrentUser.User == null || CurrentUser.User.Employee == null
                ? Guid.Empty
                : CurrentUser.User.Employee.WarehouseId ?? Guid.Empty;

            ShowSection("Products", () => new frmShowProducts(warehouseId), btnProducts, "Products");
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            ShowSection("People", () => new frmShowPeople(), btnPeople, "People");
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ShowSection("Customers", () => new frmShowCustomers(), btnCustomers, "Customers");
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            ShowSection("Suppliers", () => new frmShowSuppliers(), btnSuppliers, "Suppliers");
        }

        private void btnWarehouses_Click(object sender, EventArgs e)
        {
            ShowSection("Warehouses", () => new frmShowWarehouses(), btnWarehouses, "Warehouses");
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            ShowSection("Employees", () => new frmShowEmployees(), btnEmployees, "Employees");
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            ShowSection("Users", () => new frmShowUsers(), btnUsers, "Users");
        }

        private void btnAdjustments_Click(object sender, EventArgs e)
        {
            ShowSection("Adjustments", () => new frmShowAdjustments(), btnAdjustments, "Adjustments");
        }

        private void btnTransferOrders_Click(object sender, EventArgs e)
        {
            ShowOrders(OrderType.Transfer, btnTransferOrders, "Warehouse Transfers");
        }

        private void btnPurchaseOrders_Click(object sender, EventArgs e)
        {
            ShowOrders(OrderType.Purchase, btnPurchaseOrders, "Purchase Orders");
        }

        private void btnSalesOrders_Click(object sender, EventArgs e)
        {
            ShowOrders(OrderType.Sale, btnSalesOrders, "Sales Orders");
        }

        private void btnReturnIn_Click(object sender, EventArgs e)
        {
            ShowOrders(OrderType.ReturnIn, btnReturnIn, "Returns In");
        }

        private void btnReturnOut_Click(object sender, EventArgs e)
        {
            ShowOrders(OrderType.ReturnOut, btnReturnOut, "Returns Out");
        }

        #endregion

        #region Session

        private void picUserImage_Click(object sender, EventArgs e)
        {
            if (picUserImage.Image == null)
                return;

            using (var preview = new frmImagePreviewer(picUserImage.Image))
            {
                preview.ShowDialog(this);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to sign out?",
                "Confirm Sign Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            SecurityStorage.Clear();
            RegistryStorage.DeleteEmail();

            CurrentUser.User = null;
            CurrentUser.Jwt = null;

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ctrlClock1.StopClock();

            panelMain.Controls.Clear();
            _activeSection = null;

            foreach (var section in _sections.Values)
            {
                if (section != null && !section.IsDisposed)
                    section.Dispose();
            }

            _sections.Clear();

            base.OnFormClosed(e);
        }

        #endregion
    }
}
