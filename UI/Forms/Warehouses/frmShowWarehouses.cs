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
using UI.Forms.Employees;
using UI.Forms.Products;
using UI.Forms.Suppliers;
using UI.Services;

namespace UI.Forms.Warehouses
{
    public partial class frmShowWarehouses : Form
    {

 
            private List<WarehouseForListDto> _warehouses = new List<WarehouseForListDto>();

            public frmShowWarehouses()
            {
                InitializeComponent();
                SetupUI();
            }

            private async void frmShowWarehouses_Load(object sender, EventArgs e)
            {
                await LoadWarehouses();
            }

            private void SetupUI()
            {
                FormBorderStyle = FormBorderStyle.None;
                TopLevel = false;
                Dock = DockStyle.Fill;


                StyleButton(btnEmployees, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnEdit, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnView, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnShowStock, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

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

            private async Task LoadWarehouses()
            {
                var result = await WarehousesServices.GetAll();

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Title_Full);
                    return;
                } 
    
            _warehouses = result.Data ?? new List<WarehouseForListDto>();

                dgvWarehouses.SetData(_warehouses);

                dgvWarehouses.HideColumn("Id"); 

             
                btnAdd.Focus();
            }

            private async void btnRefresh_Click(object sender, EventArgs e)
            {
                await LoadWarehouses();
            }

            private void btnAdd_Click(object sender, EventArgs e)
            {
                using (var frm = new frmWarehouseEditor())
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                        _ = LoadWarehouses();
                }
            }

            private void btnEdit_Click(object sender, EventArgs e)
            {
                var selected = dgvWarehouses.GetSelectedItem<WarehouseForListDto>();

                if (selected == null)
                {
                    MessageBox.Show("Please select a warehouse first.");
                    return;
                }

                using (var frm = new frmWarehouseEditor(selected.Id))
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                        _ = LoadWarehouses();
                }
            }

            private void btnView_Click(object sender, EventArgs e)
            {
                var selected = dgvWarehouses.GetSelectedItem<WarehouseForListDto>();

                if (selected == null)
                {
                    MessageBox.Show("Please select a warehouse first.");
                    return;
                }

                using (var frm = new frmWarehouseDetails(selected.Id))
                {
                    frm.ShowDialog();
                }
            }

            private void btnWarehouseEmployees_Click(object sender, EventArgs e)
            {
                var selected = dgvWarehouses.GetSelectedItem<WarehouseForListDto>();

                if (selected == null)
                {
                    MessageBox.Show("Please select a warehouse first.");
                    return;
                }

                using (var frm = new frmShowWarehouseEmployees(selected.Id , selected.Name))
                {
                    frm.ShowDialog();
                }

            }

            private async void btnDelete_Click(object sender, EventArgs e)
            {
                var selected = dgvWarehouses.GetSelectedItem<WarehouseForListDto>();
                if (selected == null)
                {
                    MessageBox.Show("Please select a warehouse first.");
                    return;
                }


                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete {selected.Name}?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                btnDelete.Enabled = false;

                var result = await WarehousesServices.Delete(selected.Id);

                btnDelete.Enabled = true;

                if (!result.IsSuccess)
                {

                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await LoadWarehouses();


            }

        private void btnShowStock_Click(object sender, EventArgs e)
        {
            var selected = dgvWarehouses.GetSelectedItem<WarehouseForListDto>();
            if (selected == null)
            {
                MessageBox.Show("Please select a warehouse first.");
                return;
            }

            using (frmShowProducts frm = new frmShowProducts(selected.Id,  true)) {
                frm.ShowDialog();
            }
        }
    }
    }

