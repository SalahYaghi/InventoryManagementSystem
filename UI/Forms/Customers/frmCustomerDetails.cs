using Contract.Responses;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Customers
{
    public partial class frmCustomerDetails : Form
    {
        private readonly Guid _customerId;
        private CustomerDto _customer;

        public frmCustomerDetails(Guid customerId)
        {
            InitializeComponent();
            _customerId = customerId;
            SetupUI();
        }

        private async void frmCustomerDetails_Load(object sender, EventArgs e)
        {
            await LoadCustomer();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            StyleButton(btnEdit, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            lblStatus.Text = "Loading...";
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

        private async Task LoadCustomer()
        {
            lblStatus.Text = "Loading customer details...";

            var result = await CustomersServices.Get(_customerId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load customer";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _customer = result.Data;
            BindCustomer();

            lblStatus.Text = "Ready";
        }

        private void BindCustomer()
        {
            if (_customer == null)
                return;

            lblCustomerName.Text = _customer.CustomerName;
            lblCustomerCode.Text = "Code: " + _customer.CustomerCode;

            txtNotes.Text = string.IsNullOrWhiteSpace(_customer.Notes)
                ? "No notes provided."
                : _customer.Notes;

            ctrlContactDetails1.LoadContact(_customer.Contact);
            ctrlAddressDetails1.LoadAddress(_customer.Address);

             
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCustomerEditor(_customerId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadCustomer();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

