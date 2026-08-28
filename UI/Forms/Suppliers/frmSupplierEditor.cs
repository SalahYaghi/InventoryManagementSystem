using Contract.Requests.Addresses;
using Contract.Requests.ContactInfos;
using Contract.Requests.Suppliers;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Suppliers
{
     
        public partial class frmSupplierEditor : Form
        {
            private readonly bool _isUpdateMode;
            private readonly Guid _supplierId;

            private SupplierDto _supplier;
            private ContactInfoDto _contact;
            private AddressDto _address;

            public frmSupplierEditor()
            {
                InitializeComponent();
                _isUpdateMode = false;
                SetupUI();
            }

            public frmSupplierEditor(Guid supplierId)
            {
                InitializeComponent();
                _isUpdateMode = true;
                _supplierId = supplierId;
                SetupUI();
            }

            private async void frmSupplierEditor_Load(object sender, EventArgs e)
            {

            await ctrlAddressInfo1.LoadData();
            if (_isUpdateMode)
                    await LoadSupplier();

            }

            private void SetupUI()
            {
                BackColor = Color.FromArgb(243, 246, 249);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;

                lblTitle.Text = _isUpdateMode ? "Edit Supplier" : "Add Supplier";
                lblSubtitle.Text = _isUpdateMode
                    ? "Update supplier information, contact and address."
                    : "Create a new supplier record in your inventory system.";

                chkStatus.Checked = true;
                lblStatus.Text = "Ready";

                StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleTextBox(txtNotes); StyleTextBox(txtSupplierCode);
            StyleTextBox(txtSupplierName);


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


        private async Task LoadSupplier()
            {
                lblStatus.Text = "Loading supplier.";

                var supplierResult = await SuppliersServices.Get(_supplierId);

                if (!supplierResult.IsSuccess)
                {
                    lblStatus.Text = "Failed to load supplier";
                    MessageBox.Show(supplierResult.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _supplier = supplierResult.Data;
             

                 
                await BindSupplier();
                lblStatus.Text = "Ready";
            }

            private async Task BindSupplier()
            {
                txtSupplierName.Text = _supplier.SupplierName;
                txtSupplierCode.Text = _supplier.SupplierCode;
                chkStatus.Checked = _supplier.Status;
                txtNotes.Text = _supplier.Notes ?? string.Empty;

            await ctrlAddressInfo1.LoadAddress(_supplier.Address);
             ctrlContactInfo1.LoadContact(_supplier.Contact);
                }

            private bool ValidateForm()
            {
                errorProvider.Clear();

                bool isValid = true;

                if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
                {
                    errorProvider.SetError(txtSupplierName, "Supplier name is required.");
                    isValid = false;
                }

                if (txtSupplierName.Text.Trim().Length > 50)
                {
                    errorProvider.SetError(txtSupplierName, "Supplier name must be 50 characters or less.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(txtSupplierCode.Text))
                {
                    errorProvider.SetError(txtSupplierCode, "Supplier code is required.");
                    isValid = false;
                }

                if (txtSupplierCode.Text.Trim().Length > 50)
                {
                    errorProvider.SetError(txtSupplierCode, "Supplier code must be 50 characters or less.");
                    isValid = false;
                }

                if (txtNotes.Text.Trim().Length > 500)
                {
                    errorProvider.SetError(txtNotes, "Notes must be 500 characters or less.");
                    isValid = false;
                }

                return isValid;
            }

            private CreateSupplierRequest BuildCreateRequest()
            {
            return new CreateSupplierRequest
            {
                SupplierName = txtSupplierName.Text.Trim(),
                SupplierCode = txtSupplierCode.Text.Trim(),
                Status = chkStatus.Checked,
                Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
                Contact = ctrlContactInfo1.GetCreateRequest(),
                Address = ctrlAddressInfo1.GetCreateRequest()
            };
            }

            private UpdateSupplierRequest BuildUpdateRequest()
            {
                return new UpdateSupplierRequest
                {
                    SupplierName = txtSupplierName.Text.Trim(),
                    SupplierCode = txtSupplierCode.Text.Trim(),
                    Status = chkStatus.Checked,
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
                lblStatus.Text = "Saving supplier.";

                if (_isUpdateMode)
                {
                    var result = await SuppliersServices.Update(_supplierId, BuildUpdateRequest());

                    if (!result.IsSuccess)
                    {
                        btnSave.Enabled = true;
                        lblStatus.Text = "Failed to save supplier";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    var result = await SuppliersServices.Create(BuildCreateRequest());

                    if (!result.IsSuccess)
                    {
                        btnSave.Enabled = true;
                        lblStatus.Text = "Failed to save supplier";
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

        private void ctrlContactInfo1_Load(object sender, EventArgs e)
        {

        }

        private void txtSupplierCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }
 
