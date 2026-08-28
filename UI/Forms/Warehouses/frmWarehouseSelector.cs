using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Warehouses
{
    public partial class frmWarehouseSelector : Form
    {
        private List<WarehouseForListDto> _allWarehouses = new List<WarehouseForListDto>();

        public WarehouseForListDto SelectedWarehouse { get; private set; }

        public frmWarehouseSelector()
        {
            InitializeComponent();
            SetupUI();
        }

        private async void frmWarehouseSelector_Load(object sender, EventArgs e)
        {
            await LoadWarehouses();
        }

        private void SetupUI()
        {
            this.dgvWarehouses.dgv.DoubleClick += new System.EventHandler(this.dgvWarehouses_DoubleClick);

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

        private async Task LoadWarehouses()
        {
            var result = await WarehousesServices.GetAll();

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _allWarehouses = result.Data ?? new List<WarehouseForListDto>();
            ApplyCurrentView();
        }

        private List<WarehouseForListDto> ApplyLocalFilters()
        {
            IEnumerable<WarehouseForListDto> query = _allWarehouses;

            string search = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w =>
                    (w.Name ?? "").ToLower().Contains(search) ||
                    (w.Code ?? "").ToLower().Contains(search) ||
                    (w.BuildingNumber ?? "").ToLower().Contains(search) ||
                    (w.Street ?? "").ToLower().Contains(search));
            }

            return query.OrderBy(w => w.Name).ToList();
        }

        private void ApplyCurrentView()
        {
            var warehouses = ApplyLocalFilters();

            dgvWarehouses.SetData(warehouses);

            dgvWarehouses.HideColumn("Id");
            dgvWarehouses.HideColumn("AddressId");

            dgvWarehouses.SetColumnHeader("Name", "Warehouse Name");
            dgvWarehouses.SetColumnHeader("Code", "Code");
            dgvWarehouses.SetColumnHeader("BuildingNumber", "Building No.");
            dgvWarehouses.SetColumnHeader("Street", "Street");
            dgvWarehouses.SetColumnHeader("IsActived", "Active");
        }

        private WarehouseForListDto GetSelectedWarehouse()
        {
            return dgvWarehouses.GetSelectedItem<WarehouseForListDto>();
        }

        private void SelectCurrentWarehouse()
        {
            var selected = GetSelectedWarehouse();

            if (selected == null)
            {
                MessageBox.Show("Please select a warehouse first.");
                return;
            }

            SelectedWarehouse = selected;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void dgvWarehouses_DoubleClick(object sender, EventArgs e)
        {
            SelectCurrentWarehouse();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCurrentWarehouse();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadWarehouses();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmWarehouseEditor())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                    await LoadWarehouses();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

