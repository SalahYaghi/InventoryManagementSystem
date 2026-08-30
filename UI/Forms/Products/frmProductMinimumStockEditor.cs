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
using UI.Shared.Helpers.UI_Helpers;

namespace UI.Forms.Products
{

    public partial class frmProductMinimumStockEditor : Form
    {

        private readonly Guid _warehouseStockId;
        private  WarehouseStockDto _warehouseStockDto;
        private ProductDto _product; 
       
        public frmProductMinimumStockEditor( Guid warehouseStockId)
        {
            InitializeComponent();
            _warehouseStockId = warehouseStockId;
            SetupUI();
        }

        private async void frmProductEditor_Load(object sender, EventArgs e)
        {
                await LoadWarehouseStock();
        }

        private void SetupUI()
        {
            this.BackColor = Color.FromArgb(243, 246, 249);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblTitle.Text = "Edit Product Minimum Stock Level";
            lblSubtitle.Text = "Update product minimum stock level.";

            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
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

      
        private async Task LoadWarehouseStock()
        {
          var result =   await  WarehouseStocksServices.GetByWarehouseStockById(_warehouseStockId);
        
            var productsResult = await ProductsServices.Get(result.Data.ProductId);

            if (!result.IsSuccess || !productsResult.IsSuccess)
            {
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _warehouseStockDto = result.Data;
            _product = productsResult.Data;

            lblTitle.Text = _product.ProductName;
            NumericInputHelper.SetValue(numMinimumStockLevel, _warehouseStockDto.MinimumStockLevel);
          
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();

            bool isValid = true;
            if (numMinimumStockLevel.Value < 0)
            {
                errorProvider.SetError(numMinimumStockLevel, "Minimum stock level must be greater than or equal to 0.");
                isValid = false;
            }

            return isValid;
        }

        private UpdateWarehouseStockMinimumLevelRequest BuildUpdateRequest()
        {
            return new UpdateWarehouseStockMinimumLevelRequest
            {
                MinimumStockLevel = numMinimumStockLevel.Value, 
              
            };
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            btnSave.Enabled = false;

                var result = await WarehouseStocksServices.UpdateMinimumLevel(_warehouseStockId, BuildUpdateRequest());

                if (!result.IsSuccess)
                {
                    btnSave.Enabled = true;
                     MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            
       
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
