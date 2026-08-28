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
    public partial class frmShowCustomers : Form
    {
        private List<CustomerForListDto> _customers = new List<CustomerForListDto>();

        public frmShowCustomers()
        {
            InitializeComponent();
            SetupUI();
        }

        private async void frmShowCustomers_Load(object sender, EventArgs e)
        {
            await LoadCustomers();
        }

        private void SetupUI()
        {
            this.TopLevel = false;
            BackColor = Color.FromArgb(243, 246, 249);

            StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnEdit, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnView, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
             StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);

            this.Dock = DockStyle.Fill;
            this.FormBorderStyle = FormBorderStyle.None;
            txtSearch.BackColor = Color.FromArgb(248, 250, 252);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F);

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

        private async Task LoadCustomers()
        {
 
            var result = await CustomersServices.GetAll();

            if (!result.IsSuccess)
            {
                 MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _customers = result.Data ?? new List<CustomerForListDto>();
            ApplyCurrentView();

         }

        private void ApplyCurrentView()
        {
            string search = txtSearch.Text.Trim().ToLower();

            var query = _customers.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    (c.CustomerCode ?? "").ToLower().Contains(search) ||
                    (c.CustomerName ?? "").ToLower().Contains(search) ||
                    (c.BuildingNumber ?? "").ToLower().Contains(search) ||
                    (c.Email ?? "").ToLower().Contains(search) ||
                    (c.Street ?? "").ToLower().Contains(search) ||
                    (c.PhoneNumber ?? "").ToLower().Contains(search));
            }

            dgvCustomers.SetData(query.ToList());

            dgvCustomers.HideColumn("Id");
            dgvCustomers.HideColumn("ContactId");
            dgvCustomers.HideColumn("AddressId");

            dgvCustomers.SetColumnHeaders(new Dictionary<string, string>
            {
                { "CustomerName", "Customer Name" },
                { "CustomerCode", "Code" },
                { "Email", "Email" },
                { "PhoneNumber", "Phone" },
                { "BuildingNumber", "Building No." },
                { "Street", "Street" }
            });
        }

        private CustomerForListDto SelectedCustomer => dgvCustomers.GetSelectedItem<CustomerForListDto>();

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCustomerEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadCustomers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = SelectedCustomer;

            if (selected == null)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            using (var frm = new frmCustomerEditor(selected.Id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadCustomers();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = SelectedCustomer;

            if (selected == null)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            using (var frm = new frmCustomerDetails(selected.Id))
            {
                frm.ShowDialog();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = SelectedCustomer;

            if (selected == null)
            {
                MessageBox.Show("Please select a customer first.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete {selected.CustomerName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;
 
            var result = await CustomersServices.Delete(selected.Id);

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                 MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await LoadCustomers();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCustomers();
        }
    }
}

