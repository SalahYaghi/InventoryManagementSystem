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
using UI.Shared.Helpers.UI_Helpers;

namespace UI.Forms.Products
{ 
        public partial class frmProductDetails : Form
        {
            private readonly Guid _productId;
            private ProductDto _product;

            public frmProductDetails(Guid productId)
            {
                InitializeComponent();
                _productId = productId;
                SetupUI();
            }

            private async void frmProductDetails_Load(object sender, EventArgs e)
            {
                await LoadProduct();
            }

            private void SetupUI()
            {
                this.BackColor = Color.FromArgb(243, 246, 249);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnEdit, Color.FromArgb(74, 112, 139), Color.White);

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

            private async Task LoadProduct()
            {
                lblStatus.Text = "Loading product details...";

                var result = await ProductsServices.Get(_productId);

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to load product";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (result.Data == null)
                {
                    lblStatus.Text = "Failed to load product";
                    return;
                }

                _product = result.Data;
                BindProduct();

                lblStatus.Text = "Ready";
            }

            private void BindProduct()
            {
                if (_product == null)
                    return;

                lblProductName.Text = DisplayFormatter.Text(_product.ProductName, "Unnamed product");
                lblSku.Text = "SKU: " + DisplayFormatter.Text(_product.SKU);

                lblBarcodeValue.Text = DisplayFormatter.Text(_product.BarCode, DisplayFormatter.NotSetPlaceholder);
                lblCategoryValue.Text = _product.Category == null
                    ? DisplayFormatter.NotSetPlaceholder
                    : DisplayFormatter.Text(_product.Category.Name, DisplayFormatter.NotSetPlaceholder);
                lblUnitValue.Text = _product.Unit.ToString();
                lblPriceValue.Text = DisplayFormatter.Money(_product.SellingPrice);

                txtDescription.Text = string.IsNullOrWhiteSpace(_product.Description)
                    ? "No description provided."
                    : _product.Description.Trim();

                if (_product.IsActive)
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

            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }

            private void btnEdit_Click(object sender, EventArgs e)
            {
                using (var frm = new frmProductEditor(_productId))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        _ = LoadProduct();
                }
            }
        }
    }

