using Contract.Requests.Warehouses;
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

namespace UI.Forms.Warehouses
{
    public partial class frmWarehouseEditor : Form
        {
            private readonly bool _isUpdateMode;
            private readonly Guid _warehouseId;

            private WarehouseDto _warehouse;

        public bool IsActive => _warehouse == null ? false : _warehouse.WarehouseStatus == WarehouseStatus.Active;
            public frmWarehouseEditor()
            {
                InitializeComponent();
                _isUpdateMode = false;
                SetupUI();
            }

            public frmWarehouseEditor(Guid warehouseId)
            {
                InitializeComponent();
                _isUpdateMode = true;
                _warehouseId = warehouseId;
                SetupUI();
            }

            private async void frmWarehouseEditor_Load(object sender, EventArgs e)
            {
                await ctrlAddressInfo1.LoadData();

                if (_isUpdateMode)
                    await LoadWarehouse();
            }

            private void SetupUI()
            {
                BackColor = Color.FromArgb(243, 246, 249);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;

                lblTitle.Text = _isUpdateMode ? "Edit Warehouse" : "Add Warehouse";
                lblSubtitle.Text = _isUpdateMode
                    ? "Update warehouse information and address."
                    : "Create a new warehouse record in your inventory system.";

                chkStatus.Checked = true;
                lblStatus.Text = "Ready";

                StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

                StyleTextBox(txtWarehouseName);
                StyleTextBox(txtWarehouseCode);
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

            private async Task LoadWarehouse()
            {
                lblStatus.Text = "Loading warehouse.";

                var result = await WarehousesServices.Get(_warehouseId);

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to load warehouse";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _warehouse = result.Data;

                await BindWarehouse();

                lblStatus.Text = "Ready";
            }

            private async Task BindWarehouse()
            {
                txtWarehouseName.Text = _warehouse.Name;
                txtWarehouseCode.Text = _warehouse.Code;
                chkStatus.Checked = IsActive;

                await ctrlAddressInfo1.LoadAddress(_warehouse.Address);
            }

            private bool ValidateForm()
            {
                errorProvider.Clear();

                bool isValid = true;

                if (string.IsNullOrWhiteSpace(txtWarehouseName.Text))
                {
                    errorProvider.SetError(txtWarehouseName, "Warehouse name is required.");
                    isValid = false;
                }

                if (txtWarehouseName.Text.Trim().Length > 50)
                {
                    errorProvider.SetError(txtWarehouseName, "Warehouse name must be 50 characters or less.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(txtWarehouseCode.Text))
                {
                    errorProvider.SetError(txtWarehouseCode, "Warehouse code is required.");
                    isValid = false;
                }

                if (txtWarehouseCode.Text.Trim().Length > 50)
                {
                    errorProvider.SetError(txtWarehouseCode, "Warehouse code must be 50 characters or less.");
                    isValid = false;
                }

                if (!ctrlAddressInfo1.ValidateControl())
                    isValid = false;

                return isValid;
            }

            private CreateWarehouseRequest BuildCreateRequest()
            {
                return new CreateWarehouseRequest
                {
                    Name = txtWarehouseName.Text.Trim(),
                    Code = txtWarehouseCode.Text.Trim(),
                    Address = ctrlAddressInfo1.GetCreateRequest()
                };
            }

            private UpdateWarehouseRequest BuildUpdateRequest()
            {
                return new UpdateWarehouseRequest
                {
                    Name = txtWarehouseName.Text.Trim(),
                    Code = txtWarehouseCode.Text.Trim(),
                    WarehouseStatus = chkStatus.Checked ? (int)WarehouseStatus.Active : (int)WarehouseStatus.Inactive,
                    Address = ctrlAddressInfo1.GetUpdateRequest()
                };
            }

            private async void btnSave_Click(object sender, EventArgs e)
            {
                if (!ValidateForm())
                    return;

                btnSave.Enabled = false;
                lblStatus.Text = "Saving warehouse.";

                if (_isUpdateMode)
                {
                    var result = await WarehousesServices.Update(_warehouseId, BuildUpdateRequest());

                    if (!result.IsSuccess)
                    {
                        btnSave.Enabled = true;
                        lblStatus.Text = "Failed to save warehouse";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    var result = await WarehousesServices.Create(BuildCreateRequest());

                    if (!result.IsSuccess)
                    {
                        btnSave.Enabled = true;
                        lblStatus.Text = "Failed to save warehouse";
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
