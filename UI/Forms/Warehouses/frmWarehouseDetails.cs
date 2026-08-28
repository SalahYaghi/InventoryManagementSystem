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
using UI.Forms.References.Contacts;
using UI.Services;

namespace UI.Forms.Warehouses
{
    public partial class frmWarehouseDetails : Form
    {
   
            private readonly Guid _warehouseId;
            private WarehouseDto _warehouse;

        public bool IsActive => _warehouse == null ? false : _warehouse.WarehouseStatus == WarehouseStatus.Active; public frmWarehouseDetails(Guid warehouseId)
            {
                InitializeComponent();
                _warehouseId = warehouseId;
                SetupUI();
            }

            private async void frmWarehouseDetails_Load(object sender, EventArgs e)
            {
                await LoadWarehouse();
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

            private async Task LoadWarehouse()
            {
                lblStatus.Text = "Loading warehouse details...";

                var result = await WarehousesServices.Get(_warehouseId);

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to load warehouse";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _warehouse = result.Data;
                BindWarehouse();

                lblStatus.Text = "Ready";
            }

            private void BindWarehouse()
            {
                if (_warehouse == null)
                    return;

                lblWarehouseName.Text = _warehouse.Name;
                lblWarehouseCode.Text = "Code: " + _warehouse.Code;

                ctrlAddressDetails1.LoadAddress(_warehouse.Address);

                if (IsActive)
                {
                    lblStatusBadge.Text = "Active";
                    lblStatusBadge.BackColor = Color.FromArgb(219, 242, 230);
                    lblStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);
                }
                else
                {
                    lblStatusBadge.Text = "Inactive";
                    lblStatusBadge.BackColor = Color.FromArgb(243, 244, 246);
                    lblStatusBadge.ForeColor = Color.FromArgb(107, 114, 128);
                }
            }

            private void btnEdit_Click(object sender, EventArgs e)
            {
                using (var frm = new frmWarehouseEditor(_warehouseId))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        _ = LoadWarehouse();
                }
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }
        }
    }
