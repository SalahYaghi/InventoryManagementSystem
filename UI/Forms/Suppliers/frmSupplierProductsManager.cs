using Contract.Requests.SupplierProducts;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.Products;
using UI.Services;

namespace UI.Forms.Suppliers
{
    public partial class frmSupplierProductsManager : Form
    {
        private readonly Guid _supplierId;
        private SupplierDto _supplier;
        private List<SupplierProductDtoForList> _allSupplierProducts = new List<SupplierProductDtoForList>();

        public frmSupplierProductsManager(Guid supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;
            SetupUI();
        }

        private async void frmSupplierProductsManager_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private void SetupUI()
        {
            this.dgvSupplierProducts.dgv.Click += dgvSupplierProducts_Click;

            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            StyleButton(btnDetails, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleButton(btnAdd, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnUpdate, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnRemove, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleTextBox(txtSelectedProduct);
            StyleSelectionButton(btnChooseProduct);
            txtSelectedProduct.Enabled = false;
            chkIsActive.Checked = true;

            numPurchasePrice.DecimalPlaces = 2;
            numPurchasePrice.Minimum = 0;
            numPurchasePrice.Maximum = 999999999;

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
        private void StyleSelectionButton(Button button)
        {
            button.Text = "...";
            button.Width = 36;
            button.Height = 30;

            button.BackColor = Color.FromArgb(248, 250, 252); // same as textbox
            button.ForeColor = Color.FromArgb(80, 95, 110);   // soft dark gray

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(220, 226, 232);
            button.FlatAppearance.BorderSize = 1;

            button.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    
        
        private async Task LoadData()
        {
            lblStatus.Text = "Loading supplier products...";

            await LoadSupplier();
             await LoadSupplierProducts();

            lblStatus.Text = "Ready";
        }
        private async Task LoadSupplier()
        {
            var result = await SuppliersServices.Get(_supplierId);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Failed to load supplier";
                return;
            }

            _supplier = result.Data;
            lblTitle.Text = "Supplier Products";
            lblSubtitle.Text = $"Supplier: {_supplier.SupplierName}  |  Code: {_supplier.SupplierCode}";
        }
         private async Task LoadSupplierProducts()
        {
            var result = await SuppliersServices.GetSupplierProducts(_supplierId);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Failed to load supplier products";
                return;
            }
            _allSupplierProducts = result.Data ?? new List<SupplierProductDtoForList>();

            dgvSupplierProducts.SetData(result.Data ?? new List<SupplierProductDtoForList>());

            dgvSupplierProducts.HideColumn("Id");
            dgvSupplierProducts.HideColumn("BarCode");
            dgvSupplierProducts.HideColumn("RowVersion");
            dgvSupplierProducts.HideColumn("ProductId");
            dgvSupplierProducts.HideColumn("SupplierId");

            dgvSupplierProducts.SetColumnHeader("SKU", "SKU");
            dgvSupplierProducts.SetColumnHeader("ProductName", "Product Name");
            dgvSupplierProducts.SetColumnHeader("SellingPrice", "Selling Price");
            dgvSupplierProducts.SetColumnHeader("IsActive", "Active");
            dgvSupplierProducts.SetColumnHeader("Unit", "Unit");
            dgvSupplierProducts.SetColumnHeader("Category", "Category");
        }

        private SupplierProductDtoForList SelectedGridProduct =>
            dgvSupplierProducts.GetSelectedItem<SupplierProductDtoForList>();

        private ProductDtoForList _selectedProduct;


        private bool ValidateForm(bool ForAdd = true)
        {
            errorProvider.Clear();

            bool isValid = true;

            if (_selectedProduct == null && ForAdd)
            {
                errorProvider.SetError(txtSelectedProduct, "Product is required.");
                isValid = false;
            }

            if (numPurchasePrice.Value < 0)
            {
                errorProvider.SetError(numPurchasePrice, "Purchase price must be greater than or equal to zero.");
                isValid = false;
            }

            return isValid;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            var product = _selectedProduct;

            if (product == null)
                return;

            btnAdd.Enabled = false;
            lblStatus.Text = "Adding product to supplier...";

            var request = new CreateSupplierProductRequest
            {
                ProductId = product.Id,
                PurchasePrice = numPurchasePrice.Value
            };

            var result = await SuppliersServices.CreateSupplierProduct(_supplierId, request);

            btnAdd.Enabled = true;

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to add product";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClearEditor();
            await LoadSupplierProducts();
            lblStatus.Text = "Product added successfully";
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var selected = SelectedGridProduct;

            if (selected == null)
            {
                MessageBox.Show("Please select a supplier product first.");
                return;
            }

            if (!ValidateForm(false))
                return;

            btnUpdate.Enabled = false;
            lblStatus.Text = "Updating supplier product...";

            var request = new UpdateSupplierProductRequest
            {
                PurchasePrice = numPurchasePrice.Value,
                IsActive = chkIsActive.Checked
            };

            var result = await SuppliersServices.UpdateSupplierProduct(_supplierId, selected.Id, request);

            btnUpdate.Enabled = true;

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to update supplier product";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClearEditor();
            await LoadSupplierProducts();
            lblStatus.Text = "Supplier product updated";
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            var selected = SelectedGridProduct;

            if (selected == null)
            {
                MessageBox.Show("Please select a supplier product first.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to remove {selected.ProductName} from the supplier?",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnRemove.Enabled = false;
            lblStatus.Text = "Removing product from supplier...";

            var result = await SuppliersServices.DeleteSupplierProduct(_supplierId, selected.Id);

            btnRemove.Enabled = true;

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to remove product";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClearEditor();
            await LoadSupplierProducts();
            lblStatus.Text = "Product removed";
        }

        private List<Guid> GetSupplierProducts() { 
        
            return _allSupplierProducts.Select(p => p.Id).ToList();        }
 
        private void dgvSupplierProducts_Click(object sender, EventArgs e)
        {
            var selected = SelectedGridProduct;

            if (selected == null)
                return;

            txtSelectedProduct.Text = selected.ProductName;
            chkIsActive.Checked = selected.IsActive;
            numPurchasePrice.Value = selected.PurchasePrice;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearEditor();
            await LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearEditor()
        {
            errorProvider.Clear();

            _selectedProduct = null;
            txtSelectedProduct.Text = "";
            numPurchasePrice.Value = 0;
            chkIsActive.Checked = true;
        }

        private void btnChooseProduct_Click(object sender, EventArgs e)
        {

            var supplierProductIds = GetSupplierProducts(); 

            using (var frm = new frmProductSelector(_supplierId))
            {
                //frm.ExcludeProducts(supplierProductIds);
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                _selectedProduct = frm.SelectedProduct;
                txtSelectedProduct.Text = $"{_selectedProduct.SKU} - {_selectedProduct.ProductName}";
            }
        
    }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            var selected = SelectedGridProduct;

            if(selected == null) return;

            using (var frm = new frmProductDetails(selected.ProductId)) {
                frm.ShowDialog();
            }

        }

        private void txtSelectedProduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSelectedProduct_Click(object sender, EventArgs e)
        {
            if(_selectedProduct == null) return;
            using (frmProductDetails frm = new frmProductDetails(_selectedProduct.Id)) { 
                
                frm.ShowDialog(this);
            }
            
        }
    }
}

