using Contract.Requests.Products;
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
using UI.Shared.CurrentUser;

namespace UI.Forms.Products
{
     
        public partial class frmProductEditor : Form
        {
            private readonly bool _isUpdateMode;
            private readonly Guid _productId;
            private  Guid? _warehouseId;

            public void DefineWarehouse(Guid warehouseId) {

            this._warehouseId = warehouseId;
        }
            public frmProductEditor()
            {
                InitializeComponent();
                _isUpdateMode = false;
                SetupUI();
            }

            public frmProductEditor(Guid productId)
            {
                InitializeComponent();
                _isUpdateMode = true;
                _productId = productId;
                SetupUI();
            }

            private async void frmProductEditor_Load(object sender, EventArgs e)
            {
                await LoadCategories();
                LoadUnits();

                if (_isUpdateMode)
                    await LoadProduct();
            }

            private void SetupUI()
            {
                this.BackColor = Color.FromArgb(243, 246, 249);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                lblTitle.Text = _isUpdateMode ? "Edit Product" : "Add Product";
                lblSubtitle.Text = _isUpdateMode
                    ? "Update product information, category, unit and pricing."
                    : "Create a new product record in your inventory system.";

                StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
                StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

                cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;

                chkIsActive.Checked = true;
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

            private async Task LoadCategories()
            {
                lblStatus.Text = "Loading categories...";

                var result = await CategoriesServices.GetAll();

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Failed to load categories";
                    return;
                }

                cmbCategory.DataSource = result.Data ?? new List<CategoryDto>();
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "Id";
                cmbCategory.SelectedIndex = cmbCategory.Items.Count > 0 ? 0 : -1;

                lblStatus.Text = "Ready";
            }

            private void LoadUnits()
            {
                cmbUnit.Items.Clear();
                cmbUnit.Items.Add(Unit.Piece);
                cmbUnit.Items.Add(Unit.Kg);
                cmbUnit.Items.Add(Unit.Liter);
                cmbUnit.Items.Add(Unit.Meter);
                cmbUnit.Items.Add(Unit.Box);
                cmbUnit.SelectedIndex = 0;
            }

            private async Task LoadProduct()
            {
                lblStatus.Text = "Loading product...";

                var result = await ProductsServices.Get(_productId);

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Failed to load product";
                    return;
                }

                ProductDto product = result.Data;

                txtProductName.Text = product.ProductName;
                txtSku.Text = product.SKU;
                txtBarcode.Text = product.BarCode;
                txtDescription.Text = product.Description;
                numSellingPrice.Value = product.SellingPrice;
                chkIsActive.Checked = product.IsActive;
                cmbUnit.SelectedItem = product.Unit;

                if (product.CategoryId != Guid.Empty)
                    cmbCategory.SelectedValue = product.CategoryId;

                lblStatus.Text = "Ready";
            }

            private bool ValidateForm()
            {
                errorProvider.Clear();

                bool isValid = true;

                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    errorProvider.SetError(txtProductName, "Product name is required.");
                    isValid = false;
                }
                else if (txtProductName.Text.Trim().Length > 30)
                {
                    errorProvider.SetError(txtProductName, "Product name must not exceed 30 characters.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(txtSku.Text))
                {
                    errorProvider.SetError(txtSku, "SKU is required.");
                    isValid = false;
                }
                else if (txtSku.Text.Trim().Length > 10)
                {
                    errorProvider.SetError(txtSku, "SKU must not exceed 10 characters.");
                    isValid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtBarcode.Text) && txtBarcode.Text.Trim().Length > 50)
                {
                    errorProvider.SetError(txtBarcode, "Barcode must not exceed 50 characters.");
                    isValid = false;
                }

                if (!string.IsNullOrWhiteSpace(txtDescription.Text) && txtDescription.Text.Trim().Length > 500)
                {
                    errorProvider.SetError(txtDescription, "Description must not exceed 500 characters.");
                    isValid = false;
                }

                if (cmbCategory.SelectedValue == null)
                {
                    errorProvider.SetError(cmbCategory, "Category is required.");
                    isValid = false;
                }

                if (cmbUnit.SelectedItem == null)
                {
                    errorProvider.SetError(cmbUnit, "Unit is required.");
                    isValid = false;
                }

                if (numSellingPrice.Value < 0)
                {
                    errorProvider.SetError(numSellingPrice, "Selling price must be greater than or equal to 0.");
                    isValid = false;
                }

                return isValid;
            }

            private CreateProductRequest BuildCreateRequest()
            {
                return new CreateProductRequest
                {
                    ProductName = txtProductName.Text.Trim(),
                    SKU = txtSku.Text.Trim(),
                    BarCode = string.IsNullOrWhiteSpace(txtBarcode.Text) ? null : txtBarcode.Text.Trim(),
                    Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                    SellingPrice = numSellingPrice.Value,
                    IsActive = chkIsActive.Checked,
                    Unit = (Unit)cmbUnit.SelectedItem,
                    CategoryId = (Guid)cmbCategory.SelectedValue
                };
            }

 
 
        private UpdateProductRequest BuildUpdateRequest()
            {
                return new UpdateProductRequest
                {
                    ProductName = txtProductName.Text.Trim(),
                    SKU = txtSku.Text.Trim(),
                    BarCode = string.IsNullOrWhiteSpace(txtBarcode.Text) ? null : txtBarcode.Text.Trim(),
                    Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                    SellingPrice = numSellingPrice.Value,
                    IsActive = chkIsActive.Checked,
                    Unit = (Unit)cmbUnit.SelectedItem,
                    CategoryId = (Guid)cmbCategory.SelectedValue
                };
            }

            private async void btnSave_Click(object sender, EventArgs e)
            {
                if (!ValidateForm())
                    return;

                btnSave.Enabled = false;
                lblStatus.Text = "Saving product...";

                if (_isUpdateMode)
                {
                    var result = await ProductsServices.Update(_productId, BuildUpdateRequest());

                    if (!result.IsSuccess)
                    {
                        btnSave.Enabled = true;
                        lblStatus.Text = "Failed to save product";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                var product = BuildCreateRequest();

                var result = await WarehouseStocksServices.AddProduct(new AddWarehouseProductRequest()
                {
                    Product = product ,
                    WarehouseId = _warehouseId == null || _warehouseId.Value == Guid.Empty ?  CurrentUser.User.Employee.WarehouseId.Value : _warehouseId.Value

                });

                    if (!result.IsSuccess)
                    {
                        btnSave.Enabled = true;
                        lblStatus.Text = "Failed to save product";
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

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelFooter_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

