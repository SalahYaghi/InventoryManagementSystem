using Contract.Requests.Customers;
using Contract.Responses;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Customers
{
    public partial class frmCustomerEditor : Form
    {
        private readonly bool _isUpdateMode;
        private readonly Guid _customerId;
        private CustomerDto _customer;

        public frmCustomerEditor()
        {
            InitializeComponent();
            _isUpdateMode = false;
            SetupUI();
        }

        public frmCustomerEditor(Guid customerId)
        {
            InitializeComponent();
            _customerId = customerId;
            _isUpdateMode = true;
            SetupUI();
        }

        private async void frmCustomerEditor_Load(object sender, EventArgs e)
        {
            await ctrlAddressInfo1.LoadData();

            if (_isUpdateMode)
                await LoadCustomer();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            lblTitle.Text = _isUpdateMode ? "Update Customer" : "Add Customer";
            lblSubtitle.Text = _isUpdateMode
                ? "Update customer profile, contact information and address."
                : "Create a new customer with contact information and address.";

            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleTextBox(txtCustomerName);
            StyleTextBox(txtCustomerCode);
            StyleTextBox(txtNotes);

            chkStatus.Checked = true;
            lblStatus.Text = "Ready";
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

        private async Task LoadCustomer()
        {
            lblStatus.Text = "Loading customer...";

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

        private async void BindCustomer()
        {
            if (_customer == null)
                return;

            txtCustomerName.Text = _customer.CustomerName;
            txtCustomerCode.Text = _customer.CustomerCode;
             txtNotes.Text = _customer.Notes ?? "";

            ctrlContactInfo1.LoadContact(_customer.Contact);

            if (_customer.Address != null)
                await ctrlAddressInfo1.LoadAddress(_customer.Address);
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                errorProvider.SetError(txtCustomerName, "Customer name is required.");
                isValid = false;
            }

            if (txtCustomerName.Text.Trim().Length > 50)
            {
                errorProvider.SetError(txtCustomerName, "Customer name must be 50 characters or less.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtCustomerCode.Text))
            {
                errorProvider.SetError(txtCustomerCode, "Customer code is required.");
                isValid = false;
            }

            if (txtCustomerCode.Text.Trim().Length > 50)
            {
                errorProvider.SetError(txtCustomerCode, "Customer code must be 50 characters or less.");
                isValid = false;
            }

            if (txtNotes.Text.Trim().Length > 500)
            {
                errorProvider.SetError(txtNotes, "Notes must be 500 characters or less.");
                isValid = false;
            }

            if (!ctrlContactInfo1.ValidateControl())
                isValid = false;

            if (!ctrlAddressInfo1.ValidateControl())
                isValid = false;

            return isValid;
        }

        private CreateCustomerRequest BuildCreateRequest()
        {
            return new CreateCustomerRequest
            {
                CustomerName = txtCustomerName.Text.Trim(),
                CustomerCode = txtCustomerCode.Text.Trim(),
                 Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
                Contact = ctrlContactInfo1.GetCreateRequest(),
                Address = ctrlAddressInfo1.GetCreateRequest()
            };
        }

        private UpdateCustomerRequest BuildUpdateRequest()
        {
            return new UpdateCustomerRequest
            {
                CustomerName = txtCustomerName.Text.Trim(),
                CustomerCode = txtCustomerCode.Text.Trim(),
                 Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
                Contact = ctrlContactInfo1.GetUpdateRequest(),
                Address = ctrlAddressInfo1.GetUpdateRequest()
            };
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            btnSave.Enabled = false;
            lblStatus.Text = "Saving customer...";

            if (_isUpdateMode)
            {
                var result = await CustomersServices.Update(_customerId, BuildUpdateRequest());

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                    lblStatus.Text = "Failed to save customer";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                var result = await CustomersServices.Create(BuildCreateRequest());

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                    lblStatus.Text = "Failed to save customer";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            lblStatus.Text = "Saved successfully";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

