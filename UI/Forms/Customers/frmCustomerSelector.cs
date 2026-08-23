using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Customers
{
    public partial class frmCustomerSelector : Form
    {
        private List<CustomerForListDto> _allCustomers = new List<CustomerForListDto>();

        public CustomerForListDto SelectedCustomer { get; private set; }

        public frmCustomerSelector()
        {
            InitializeComponent();
            SetupUI();
        }

        private async void frmCustomerSelector_Load(object sender, EventArgs e)
        {
            await LoadCustomers();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnSelect, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleTextBox(txtSearch);
        }

        private void StyleButton(Button button, Color backColor, Color foreColor)
        {
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(248, 250, 252);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F);
            textBox.ForeColor = Color.FromArgb(24, 33, 45);
        }

        private async Task LoadCustomers()
        {
            var result = await CustomersServices.GetAll();

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _allCustomers = result.Data ?? new List<CustomerForListDto>();
            ApplyCurrentView();
        }

        private List<CustomerForListDto> ApplyLocalFilters()
        {
            IEnumerable<CustomerForListDto> query = _allCustomers;

            string search = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    (c.CustomerName ?? "").ToLower().Contains(search) ||
                    (c.CustomerCode ?? "").ToLower().Contains(search) ||
                    (c.Email ?? "").ToLower().Contains(search) ||
                    (c.PhoneNumber ?? "").ToLower().Contains(search) ||
                    (c.BuildingNumber ?? "").ToLower().Contains(search) ||
                    (c.Street ?? "").ToLower().Contains(search));
            }

            return query.OrderBy(c => c.CustomerName).ToList();
        }

        private void ApplyCurrentView()
        {
            var customers = ApplyLocalFilters();

            dgvCustomers.SetData(customers);

            dgvCustomers.HideColumn("Id");
            dgvCustomers.HideColumn("ContactId");
            dgvCustomers.HideColumn("AddressId");

            dgvCustomers.SetColumnHeader("CustomerName", "Customer Name");
            dgvCustomers.SetColumnHeader("CustomerCode", "Code");
            dgvCustomers.SetColumnHeader("Email", "Email");
            dgvCustomers.SetColumnHeader("PhoneNumber", "Phone");
            dgvCustomers.SetColumnHeader("BuildingNumber", "Building No.");
            dgvCustomers.SetColumnHeader("Street", "Street");
            dgvCustomers.SetColumnHeader("Status", "Active");
        }

        private CustomerForListDto GetSelectedCustomer()
        {
            return dgvCustomers.GetSelectedItem<CustomerForListDto>();
        }

        private void SelectCurrentCustomer()
        {
            var selected = GetSelectedCustomer();

            if (selected == null)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            SelectedCustomer = selected;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void dgvCustomers_DoubleClick(object sender, EventArgs e)
        {
            SelectCurrentCustomer();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCurrentCustomer();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCustomers();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCustomerEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadCustomers();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

