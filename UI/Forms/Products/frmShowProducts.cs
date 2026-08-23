using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.Products.Categories;
using UI.HttpClient;
using UI.Services;
using UI.Shared.CurrentUser;

namespace UI.Forms.Products
{
        public partial class frmShowProducts : Form
        {

            private List<WarehouseStockDtoForList> _allProducts = new List<WarehouseStockDtoForList>();
        
        private Guid _warehouseId = Guid.Empty;

            public frmShowProducts(Guid warehouseId , bool topLevel = false)
            {
                InitializeComponent();
            this._warehouseId = warehouseId;
                SetupUI(topLevel);
                }

        private void MakeForTopLevel() {

            this.TopLevel = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        
        }

            private async void frmShowProducts_Load(object sender, EventArgs e)
            {
                dgvProducts.SubscribeToLoadData(LoadData);
                await dgvProducts.LoadDataGridViewData();
           
        }
            private void SetupUI(bool toplevel)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

             if(toplevel)
                MakeForTopLevel();

            this.cmbOrderBy.Size = new System.Drawing.Size(270, 55);


        }
        private List<WarehouseStockDtoForList> ApplyLocalFilters()
        {
            IEnumerable<WarehouseStockDtoForList> query = _allProducts;

            string search = txtSearch.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    (p.SKU ?? "").ToLower().Contains(search) ||
                    (p.BarCode ?? "").ToLower().Contains(search) ||
                    (p.ProductName ?? "").ToLower().Contains(search) ||
                    (p.Category ?? "").ToLower().Contains(search) ||
                    (p.Unit ?? "").ToLower().Contains(search)
                );
            }


            
            switch (cmbOrderBy.GetSelectedItemName())
            {
                case "Quantity":
                    query = cmbOrderBy.SortData<WarehouseStockDtoForList>(query, d => d.Quantity);

                    break;

                case "ProductName":
                    query = cmbOrderBy.SortData<WarehouseStockDtoForList>(query, d => d.ProductName);

                    break;

                case "SKU":
                    query = cmbOrderBy.SortData<WarehouseStockDtoForList>(query , d => d.SKU);

                    break;

                case "SellingPrice":

                    query = cmbOrderBy.SortData<WarehouseStockDtoForList>(query, d => d.SellingPrice);
                    break;

                case "Category":

                    query = cmbOrderBy.SortData<WarehouseStockDtoForList>(query, d => d.Category); break;

                case "Unit":

                    query = cmbOrderBy.SortData<WarehouseStockDtoForList>(query, d => d.Unit); break;
               
                case "MinimumStockLevel":

                    query = cmbOrderBy.SortData<WarehouseStockDtoForList>(query, d => d.MinimumStockLevel); break;

                default:

                    query = cmbOrderBy.SortData<WarehouseStockDtoForList>(query, d => d.SKU);
                    break;
            }

           query =   cmbCategory.FilterData<WarehouseStockDtoForList>(query,
                p => p.Category == cmbCategory.GetSelectedItemName());

            query = cmbUnit.FilterData<WarehouseStockDtoForList>(query,
                 p => p.Unit == cmbUnit.GetSelectedItemName());

            return query.ToList();
        }
            private void ApplyCurrentView()
            {
            var products = ApplyLocalFilters();

            dgvProducts.SetData(products);
            dgvProducts.DgvCustom.HideColumn("Id");
            dgvProducts.DgvCustom.HideColumn("RowVersion");
            dgvProducts.DgvCustom.HideColumn("ProductId");

            dgvProducts.DgvCustom.HideColumn("BarCode");
            dgvProducts.DgvCustom.SetColumnHeader("MinimumStockLevel", "MinLevel");
            dgvProducts.DgvCustom.SetColumnHeader("SKU", "SKU");
                dgvProducts.DgvCustom.SetColumnHeader("BarCode", "Barcode");
                dgvProducts.DgvCustom.SetColumnHeader("ProductName", "Product Name");
                dgvProducts.DgvCustom.SetColumnHeader("SellingPrice", "Selling Price");
                dgvProducts.DgvCustom.SetColumnHeader("IsActive", "Active");
                dgvProducts.DgvCustom.SetColumnHeader("Unit", "Unit");
                dgvProducts.DgvCustom.SetColumnHeader("Category", "Category");

            }



        public async Task<ApiResult<PaginatedList>> LoadData(int pageNumber,
                                        int pageSize) {

            var result = await WarehouseStocksServices.GetByWarehouse(_warehouseId, pageNumber, pageSize);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full);
                return "Failed to load data";
            }

            _allProducts = result.Data.Items ?? new List<WarehouseStockDtoForList>();

            dgvProducts.SetData(_allProducts);

            cmbCategory.LoadData<WarehouseStockDtoForList>(_allProducts,
           p => p.Category);
            cmbCategory.IndexChanged += ApplyCurrentView;

            cmbUnit.LoadData<WarehouseStockDtoForList>(_allProducts,
        p => p.Unit);
            cmbUnit.IndexChanged += ApplyCurrentView;

            cmbOrderBy.LoadData(this.dgvProducts.DgvCustom.GetColumnNamesExcept(new HashSet<string>() { 
                "IsActive" , "Id" , "RowVersion" , "ProductId" , "BarCode"
            }));
            cmbOrderBy.IndexChanged += ApplyCurrentView;


            ApplyCurrentView();
            btnAdd.Focus();
            return result.Data;
        }
            private void txtSearch_TextChanged(object sender, EventArgs e)
            {
                ApplyCurrentView();
            }
            private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await dgvProducts.LoadDataGridViewData();
        }
            private async void btnAdd_Click(object sender, EventArgs e)
            {
            using (var frm = new frmProductEditor())
            {
                frm.DefineWarehouse(_warehouseId);
                if (frm.ShowDialog() == DialogResult.OK)
                    await dgvProducts.LoadDataGridViewData();
            }
        }
            private async void btnEdit_Click(object sender, EventArgs e)
            {
            var selected = dgvProducts.DgvCustom.GetSelectedItem<WarehouseStockDtoForList>();

            if (selected == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            using (var frm = new frmProductEditor(selected.ProductId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    await dgvProducts.LoadDataGridViewData();
            }
        }
            private void btnView_Click(object sender, EventArgs e)
            {
            var selected = dgvProducts.DgvCustom.GetSelectedItem<WarehouseStockDtoForList>();

            if (selected == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            using (var frm = new frmProductDetails(selected.ProductId))
            {
                frm.ShowDialog();
            }
        }
            private void btnViewImages_Click(object sender, EventArgs e)
        {
            var selected = dgvProducts.DgvCustom.GetSelectedItem<WarehouseStockDtoForList>();

            if (selected == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            using (var frm = new frmProductImagesGallery(selected.ProductId))
            {
                frm.ShowDialog();
            }
        }
            private void btnManageCategories_Click(object sender, EventArgs e)
        {
            using (var frm = new frmManageCategories())
            {
                frm.ShowDialog();
            }
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = dgvProducts.DgvCustom.GetSelectedItem<WarehouseStockDtoForList>();

            if (selected == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

           

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete {selected.ProductName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            dgvProducts.Status = "Deleting image...";
            btnDelete.Enabled = false;

            var result = await WarehouseStocksServices.Delete(selected.Id);

            btnDelete.Enabled = true;

            if (!result.IsSuccess)
            {
                lblSearch.Text = "Failed to delete product";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await dgvProducts.LoadDataGridViewData();


        }
    }
    } 
