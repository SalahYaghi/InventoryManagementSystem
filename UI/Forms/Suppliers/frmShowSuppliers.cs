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
     
        public partial class frmShowSuppliers : Form
        {
            private List<SupplierForListDto> _suppliers = new List<SupplierForListDto>();

            public frmShowSuppliers()
            {
                InitializeComponent();
                SetupUI();
            }

            private async void frmShowSuppliers_Load(object sender, EventArgs e)
            {
                await LoadSuppliers();
            }

            private void SetupUI()
            {
                FormBorderStyle = FormBorderStyle.None;
                TopLevel = false;
                Dock = DockStyle.Fill;

 
            StyleButton(btnManageProducts, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnEdit, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnView, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);

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

            private async Task LoadSuppliers()
            {
                var result = await SuppliersServices.GetAll();

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Title_Full);
                    return;
                }

                _suppliers = result.Data ?? new List<SupplierForListDto>();

                dgvSuppliers.SetData(_suppliers);

                dgvSuppliers.HideColumn("Id");
                dgvSuppliers.HideColumn("ContactId");
                dgvSuppliers.HideColumn("AddressId");

                dgvSuppliers.SetColumnHeader("SupplierName", "Supplier Name");
                dgvSuppliers.SetColumnHeader("SupplierCode", "Code");
                dgvSuppliers.SetColumnHeader("Email", "Email");
                dgvSuppliers.SetColumnHeader("PhoneNumber", "Phone");
                dgvSuppliers.SetColumnHeader("BuildingNumber", "Building No.");
                dgvSuppliers.SetColumnHeader("Street", "Street");
                dgvSuppliers.SetColumnHeader("Status", "Active");

                dgvSuppliers.SetDefaultValueForNulls("BuildingNumber", "NotDefined");
                dgvSuppliers.SetDefaultValueForNulls("Street", "NotDefined");

                btnAdd.Focus();
            }

            private async void btnRefresh_Click(object sender, EventArgs e)
            {
                await LoadSuppliers();
            }

            private void btnAdd_Click(object sender, EventArgs e)
            {
            using (var frm = new frmSupplierEditor())
            {

                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadSuppliers();
            }
        }

            private void btnEdit_Click(object sender, EventArgs e)
            {
                var selected = dgvSuppliers.GetSelectedItem<SupplierForListDto>();

                if (selected == null)
                {
                    MessageBox.Show("Please select a supplier first.");
                    return;
                }

            using (var frm = new frmSupplierEditor(selected.Id))
            {

                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadSuppliers();
            }
        }

            private void btnView_Click(object sender, EventArgs e)
            {
                var selected = dgvSuppliers.GetSelectedItem<SupplierForListDto>();

                if (selected == null)
                {
                    MessageBox.Show("Please select a supplier first.");
                    return;
                }

            using (var frm = new frmSupplierDetails(selected.Id))
            {
                frm.ShowDialog();
            }
            }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            var selected = dgvSuppliers.GetSelectedItem<SupplierForListDto>();

            if (selected == null)
            {
                MessageBox.Show("Please select a supplier first.");
                return;
            }

            using (var frm = new frmSupplierProductsManager(selected.Id))
            {
                frm.ShowDialog();
            }

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = dgvSuppliers.GetSelectedItem<SupplierForListDto>();
            if (selected == null)
            {
                MessageBox.Show("Please select a supplier first.");
                return;
            }

             
            var confirm = MessageBox.Show(
                $"Are you sure you want to delete {selected.SupplierName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            var result = await SuppliersServices.Delete(selected.Id);

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
               
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await LoadSuppliers();


        }
    }
    }

