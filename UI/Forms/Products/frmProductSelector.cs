using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.HttpClient;
using UI.Services;
using UI.Shared.Helpers.UI_Helpers;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities;

namespace UI.Forms.Products
{
    public partial class frmProductSelector : Form
    {
        private List<ProductDtoForList> _allProducts = new List<ProductDtoForList>();
        public ProductDtoForList SelectedProduct { get; private set; }
        public frmProductSelector(Guid? supplierId)
        {
            InitializeComponent();
            SetupUI();
            ExceptSupplier(supplierId);
        }

        public frmProductSelector()
        {
            InitializeComponent();
            SetupUI();
        }

        private Guid?  _excludeSupplierId = null;
        private Guid?  _warehouseId = null;
        private List<Guid> _excludeProducts = new List<Guid>();
        private Guid? _fromSupplierId = null;
        public void ExceptSupplier(Guid? supplierId) {

            if (supplierId == null) return;

            this._excludeSupplierId = supplierId;
            this._warehouseId = null;
            this._fromSupplierId = null;
           
        }
        public void FromWarehouse(Guid? warehouseId)
        {

            if (warehouseId == null) return;

            this._warehouseId = warehouseId;

            this._excludeSupplierId = null;
            this._fromSupplierId = null; 
        }
        public void FromSupplier(Guid? supplierId)
        {

            if (supplierId == null) return;

            this._fromSupplierId = supplierId;

            this._excludeSupplierId = null;
            this._warehouseId = null; 
        }
        public void ExcludeProducts(List<Guid> products)
        {

            if (products == null) return;

            this._excludeProducts = products;

           }
      
        private async void frmProductSelector_Load(object sender, EventArgs e)
        {
            dgvProducts.SubscribeToLoadData(LoadProducts);
            await dgvProducts.LoadDataGridViewData();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            this.dgvProducts.DgvCustom.dgv.DoubleClick += dgvProducts_DoubleClick;

            StyleButton(btnSelect, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnEdit, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnDelete, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnDetails, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            txtSearch.BackColor = Color.FromArgb(248, 250, 252);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F);

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

        private async Task<ApiResult<PaginatedList>> LoadProducts(int pageNumber,
                                        int pageSize)
        {
            lblStatus.Text = "Loading products...";

            var result = await ProductsServices.GetAll(pageNumber, pageSize, _excludeSupplierId , 
                _excludeProducts , _warehouseId , _fromSupplierId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load products";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Failed to load data";
                 
            }

        
            if (result.Data == null)
            {
                lblStatus.Text = "Failed to load products";
                return "Failed to load data";
            }

            _allProducts = result.Data.Items ?? new List<ProductDtoForList>();


            ApplyCurrentView();

            lblStatus.Text = _allProducts.Count == 1
                ? "1 product loaded"
                : DisplayFormatter.Count(_allProducts.Count) + " products loaded";

            return result.Data;
        }

        private void ApplyCurrentView()
        {
            string search = txtSearch.Text.Trim().ToLower();

            var query = _allProducts.AsEnumerable();


            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    (p.ProductName ?? "").ToLower().Contains(search) ||
                    (p.SKU ?? "").ToLower().Contains(search) ||
                    (p.BarCode ?? "").ToLower().Contains(search) ||
                    (p.Category ?? "").ToLower().Contains(search) ||
                    (p.Unit ?? "").ToLower().Contains(search));
            }

            dgvProducts.SetData(query.ToList());

            dgvProducts.DgvCustom.HideColumn("Id");
            dgvProducts.DgvCustom.HideColumn("RowVersion");
            dgvProducts.DgvCustom.HideColumn("ProductId");

            if (_fromSupplierId.HasValue || _excludeSupplierId.HasValue
)
            {
                dgvProducts.DgvCustom.HideColumn("Quantity");
                dgvProducts.DgvCustom.HideColumn("ReservedQuantity");
                dgvProducts.DgvCustom.HideColumn("TotalQuantity");
             
                if(_excludeSupplierId.HasValue)
                dgvProducts.DgvCustom.HideColumn("PurchasePrice");

            }
            else  {
                dgvProducts.DgvCustom.HideColumn("PurchasePrice");

            }
            dgvProducts.DgvCustom.HideColumn("IsActive");

             
            dgvProducts.DgvCustom.HideColumn("BarCode");
            dgvProducts.DgvCustom.SetColumnHeader("MinimumStockLevel", "MinLevel");
            dgvProducts.DgvCustom.SetColumnHeader("SKU", "SKU");
            dgvProducts.DgvCustom.SetColumnHeader("BarCode", "Barcode");
            dgvProducts.DgvCustom.SetColumnHeader("ProductName", "Product Name");
            dgvProducts.DgvCustom.SetColumnHeader("SellingPrice", "Selling Price");
             dgvProducts.DgvCustom.SetColumnHeader("Unit", "Unit");
            dgvProducts.DgvCustom.SetColumnHeader("Category", "Category");
            dgvProducts.DgvCustom.SetColumnHeader("PurchasePrice", "Purchase Price");
            dgvProducts.DgvCustom.SetColumnHeader("ReservedQuantity", "Reserved");
            dgvProducts.DgvCustom.SetColumnHeader("TotalQuantity", "Total");
            dgvProducts.DgvCustom.FormatColumnsAsCurrency("SellingPrice", "PurchasePrice");
            dgvProducts.DgvCustom.FormatColumnsAsQuantity("Quantity", "ReservedQuantity", "TotalQuantity");
        }

        private ProductDtoForList GetSelectedProduct()
        {
            return dgvProducts.DgvCustom.GetSelectedItem<ProductDtoForList>();
        }

        private void SelectCurrentProduct()
        {
            var selected = GetSelectedProduct();

            if (selected == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            SelectedProduct = selected;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyCurrentView();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCurrentProduct();
        }

        private void dgvProducts_DoubleClick(object sender, EventArgs e)
        {
            SelectCurrentProduct();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmProductEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    await dgvProducts.LoadDataGridViewData();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProduct();

            if (selected == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            using (var frm = new frmProductEditor(selected.Id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    await dgvProducts.LoadDataGridViewData();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProduct();

            if (selected == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            var result = await ProductsServices.Delete(selected.Id);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await dgvProducts.LoadDataGridViewData();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProduct();

            if (selected == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            using (var frm = new frmProductDetails(selected.Id))
            {
                frm.ShowDialog();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await dgvProducts.LoadDataGridViewData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

