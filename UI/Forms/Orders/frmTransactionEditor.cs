using Contract.Requests.Orders;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UI.Forms.Customers;
using UI.Forms.Products;
using UI.Forms.Suppliers;
using UI.Forms.Warehouses;
using UI.Services;
using UI.Shared.CurrentUser;

namespace UI.Forms.Orders
{
    public partial class frmTransactionEditor : Form
    {

        private readonly bool _isUpdateMode;
        private readonly Guid _orderId;

        private Guid? _supplierId = Guid.Empty;
        private Guid? _customerId = Guid.Empty;
        private Guid? _sourceWarehouseId = Guid.Empty;
        private Guid? _destinationWarehouseId = Guid.Empty;

        private readonly List<OrderDetailDto> _details = new List<OrderDetailDto>();

        public void SetOrderType(OrderType orderType) {

            if (!this.cmbOrderType.Items.Contains(orderType)) return;

            this.cmbOrderType.SelectedItem = orderType;

        }
        public frmTransactionEditor()
        {
            InitializeComponent();
            _isUpdateMode = false;
            SetupUI();
        }
        public frmTransactionEditor(Guid orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            _isUpdateMode = true;
            SetupUI();
        }

        private async void frmTransactionEditor_Load(object sender, EventArgs e)
        {
            if (_isUpdateMode)
                await LoadOrder();
        }

        private void RestDetailsToDefault() {

            _details.Clear();
 
            //_supplierId = Guid.Empty;
            //_customerId = Guid.Empty;
            //_destinationWarehouseId = Guid.Empty;
            //txtCustomer.Text = string.Empty;
            //txtSourceWarehouse.Text = string.Empty;
            //txtDestinationWarehouse.Text = string.Empty;
            //txtSupplier.Text = string.Empty;
           
            txtDiscount.Text = string.Empty;    
            lblSubTotalValue.Text = "0.0";
            lblDiscountValue.Text = "0.0";
            lblNetValue.Text = "0.0";
        }

        private void ShowDiscounts(bool show) { 
        
            this.lblDiscount.Visible = show;
            this.txtDiscount.Visible = show;
            this.lblDiscountValueTitle.Visible = show;
            this.lblDiscountValue.Visible = show;

        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            StyleButton(btnSelectSupplier, Color.FromArgb(248, 250, 252), Color.FromArgb(74, 112, 139));
            StyleButton(btnSelectCustomer, Color.FromArgb(248, 250, 252), Color.FromArgb(74, 112, 139));
            StyleButton(btnSelectSourceWarehouse, Color.FromArgb(248, 250, 252), Color.FromArgb(74, 112, 139));
            StyleButton(btnSelectDestinationWarehouse, Color.FromArgb(248, 250, 252), Color.FromArgb(74, 112, 139));


            StyleButton(btnUpdateQuantity, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnAddDetail, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnRemoveDetail, Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnSave, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnCancel, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            StyleTextBox(txtSupplier);
            StyleTextBox(txtCustomer);
            StyleTextBox(txtSourceWarehouse);
            StyleTextBox(txtDestinationWarehouse);
            StyleTextBox(txtNotes);
            StyleTextBox(txtQuantity);
            StyleTextBox(txtDiscount);

            txtSupplier.ReadOnly = true;
            txtCustomer.ReadOnly = true;
            txtSourceWarehouse.ReadOnly = true;
            txtDestinationWarehouse.ReadOnly = true;

            cmbOrderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrderType.DataSource = Enum.GetValues(typeof(OrderType));

            dtpDueDate.Format = DateTimePickerFormat.Custom;
            dtpDueDate.CustomFormat = ("yyyy MM dd HH:mm");
            dtpDueDate.MinDate = DateTime.UtcNow;

            lblTitle.Text = _isUpdateMode ? "Update Transaction" : "Create Transaction";
            lblSubtitle.Text = _isUpdateMode
                ? "Update discount and notes for this transaction."
                : "Create purchase, sale, or warehouse transfer transaction.";

            _sourceWarehouseId = CurrentUser.User.Employee.WarehouseId.Value;
            txtSourceWarehouse.Text = CurrentUser.User.Employee.Warehouse == null ? "" : CurrentUser.User.Employee.Warehouse.Name;
            btnUpdateQuantity.Visible = false;
            btnUpdateQuantity.Enabled = false;
            if (_isUpdateMode)
            {
                cmbOrderType.Enabled = false;
                btnSelectSupplier.Enabled = false;
                btnSelectCustomer.Enabled = false;
                btnSelectSourceWarehouse.Enabled = false;
                btnSelectDestinationWarehouse.Enabled = false;
                btnAddDetail.Enabled = false;
                btnRemoveDetail.Enabled = false;
                txtQuantity.Enabled = false;
                dtpDueDate.Enabled = false;
            }

         

            if (_isUpdateMode) {
                btnUpdateQuantity.Visible = true;
                btnUpdateQuantity.Enabled = true;
            }

            ApplyOrderTypeVisibility();
            BindDetailsGrid();
            RecalculateSummary();
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

        private async System.Threading.Tasks.Task LoadOrder()
        {
            lblStatus.Text = "Loading transaction...";

            var result = await OrdersServices.Get(_orderId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load transaction";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var order = result.Data;

            
            cmbOrderType.SelectedItem = order.OrderType;
            txtDiscount.Text = (order.DiscountAmount ?? 0).ToString("0.##");
            txtNotes.Text = order.Notes ?? "";

            this.dtpDueDate.MinDate = (order.DueDate > DateTimeOffset.UtcNow ?   DateTimeOffset.UtcNow: order.DueDate).UtcDateTime ;
            dtpDueDate.Value = (order.DueDate);

            _supplierId = order.SupplierId;
            _customerId = order.CustomerId;
            _sourceWarehouseId = order.SourceWarehouseId;
            _destinationWarehouseId = order.DestinationWarehouseId;

            txtSupplier.Text = order.SupplierId == null ? "" : order.Supplier.SupplierName;
            txtCustomer.Text = order.Customer == null ? "" : order.Customer.CustomerName;
            txtSourceWarehouse.Text = order.SourceWarehouseDto == null ? "" : order.SourceWarehouseDto.Name;
            txtDestinationWarehouse.Text = order.DestinationWarehouseDto == null ? "" : order.DestinationWarehouseDto.Name;

             txtDiscount.Enabled = order.OrderStatus == OrderStatus.Pending;
            txtNotes.Enabled = order.OrderStatus == OrderStatus.Pending;
            dtpDueDate.Enabled = order.OrderStatus == OrderStatus.Pending;
            txtQuantity.Enabled = order.OrderStatus == OrderStatus.Pending;
            btnUpdateQuantity.Enabled = order.OrderStatus == OrderStatus.Pending;
            btnUpdateQuantity.Visible = order.OrderStatus == OrderStatus.Pending;
            btnRemoveDetail.Enabled = order.OrderStatus == OrderStatus.Pending;
            btnAddDetail.Enabled = order.OrderStatus == OrderStatus.Pending;

            ApplyOrderTypeVisibility();
            txtDiscount.Text = (order.DiscountAmount ?? 0).ToString("0.00");
            lblDiscountValue.Text = txtDiscount.Text;


            foreach (var item in order.OrderDetails)
                {
                    _details.Add(new OrderDetailDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        ActualQuantity = item.ActualQuantity,
                        OrderId = item.OrderId,
                        Id = item.Id,
                        Product = item.Product,
                        RowVersion = item.RowVersion,
                    
                    });
                }

                BindDetailsGrid();
                RecalculateSummary();
            

            lblStatus.Text = "Ready";
        }

        private OrderType SelectedOrderType
        {
            get { return (OrderType)cmbOrderType.SelectedItem; }
        }

        private void ApplyOrderTypeVisibility()
        {
            if (cmbOrderType.SelectedItem == null)
                return;
            RestDetailsToDefault();
            var type = SelectedOrderType;

            pnlSupplier.Visible = type == OrderType.Purchase || type == OrderType.ReturnOut;
            pnlCustomer.Visible = type == OrderType.Sale || type == OrderType.ReturnIn;
            pnlSourceWarehouse.Visible = true;
            pnlDestinationWarehouse.Visible = type == OrderType.Transfer;

            if (type == OrderType.Purchase)
            {
                lblHint.Text = "Purchase: supplier and source warehouse are required. Stock will be received into source warehouse.";
            }
            else if (type == OrderType.Sale)
            {
                lblHint.Text = "Sale: customer and source warehouse are required. Stock will be issued from source warehouse.";
            }
            else if (type == OrderType.ReturnIn)
            {
                lblHint.Text = "Return In: customer and source warehouse are required. Stock will be issued from source warehouse.";
            }
            else if (type == OrderType.ReturnOut)
            {
                lblHint.Text = "Return Out : supplier and source warehouse are required. Stock will be received into source warehouse.";
            }
            else if(type == OrderType.Transfer)
            {
                ShowDiscounts(false);
                lblHint.Text = "Transfer: source and destination warehouses are required. Customer and supplier are not used.";
            }
        }
        private bool ValidateForm()
        {
            errorProvider.Clear();

            bool valid = true;

            if (SelectedOrderType == OrderType.Purchase && _supplierId == Guid.Empty)
            {
                errorProvider.SetError(txtSupplier, "Supplier is required for purchase.");
                valid = false;
            }

            if (SelectedOrderType == OrderType.Sale && _customerId == Guid.Empty)
            {
                errorProvider.SetError(txtCustomer, "Customer is required for sale.");
                valid = false;
            }

            if (_sourceWarehouseId == Guid.Empty)
            {
                errorProvider.SetError(txtSourceWarehouse, "Source warehouse is required.");
                valid = false;
            }

            if (SelectedOrderType == OrderType.Transfer)
            {
                if (_destinationWarehouseId == Guid.Empty)
                {
                    errorProvider.SetError(txtDestinationWarehouse, "Destination warehouse is required for transfer.");
                    valid = false;
                }

                if (_sourceWarehouseId != Guid.Empty && _sourceWarehouseId == _destinationWarehouseId)
                {
                    errorProvider.SetError(txtDestinationWarehouse, "Destination warehouse cannot be same as source warehouse.");
                    valid = false;
                }
            }

            if (!_isUpdateMode && _details.Count == 0)
            {
                MessageBox.Show("Please add at least one transaction detail.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valid = false;
            }

            decimal discount;

            if (!decimal.TryParse(txtDiscount.Text.Trim(), out discount) || discount < 0)
            {
                errorProvider.SetError(txtDiscount, "Discount must be zero or greater.");
                valid = false;
            }

            if (txtNotes.Text.Trim().Length > 500)
            {
                errorProvider.SetError(txtNotes, "Notes must not exceed 500 characters.");
                valid = false;
            }

            return valid;
        }

        private CreateOrderRequest BuildCreateRequest()
        {
            return new CreateOrderRequest
            {
                OrderType = SelectedOrderType,

                SupplierId = SelectedOrderType == OrderType.Purchase || SelectedOrderType == OrderType.ReturnOut ? _supplierId.Value : Guid.Empty,
                CustomerId = SelectedOrderType == OrderType.Sale || SelectedOrderType == OrderType.ReturnIn ? _customerId.Value : Guid.Empty,
                SourceWarehouseId = _sourceWarehouseId.Value,
                DestinationWarehouseId = SelectedOrderType == OrderType.Transfer ? _destinationWarehouseId.Value : Guid.Empty,
                DueDate = dtpDueDate.Value,
                Discount = SelectedDiscount,
                Notes = txtNotes.Text.Trim(),

                OrderDetails = _details.Select(d => new CreateOrderDetailRequestInner
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                     RowVersion = d.RowVersion ,
            
                }).ToList(),
            };
        }
        private UpdateOrderRequest BuildUpdateRequest()
        {
            return new UpdateOrderRequest
            {
                Id = _orderId,
                DiscountAmount = decimal.Parse(txtDiscount.Text.Trim()),
                Notes = txtNotes.Text.Trim() ,
                DueDate = dtpDueDate.Value
            };
        }




        private void BindDetailsGrid()
        {
            dgvDetails.SetData(_details.ToList());

            dgvDetails.HideColumn("Quantity");

            dgvDetails.HideColumn("ActualQuantity");

            dgvDetails.HideColumn("ProductId");

            dgvDetails.HideColumn("OrderId");

            dgvDetails.HideColumn("Id");

            dgvDetails.HideColumn("Product");

            dgvDetails.HideColumn("RowVersion");

            dgvDetails.SetColumnHeader("ProductName", "Product");
            dgvDetails.SetColumnHeader("CurrentQuantity", "Quantity");
            dgvDetails.SetColumnHeader("UnitPrice", "Unit Price");
            dgvDetails.SetColumnHeader("TotalAmount", "Total");
        }

        private OrderDetailDto GetSelectedDetail()
        {
            return dgvDetails.GetSelectedItem<OrderDetailDto>();
        }

        private void RecalculateSummary()
        {
            
            decimal subTotal = _details.Sum(d => d.Quantity * d.UnitPrice);

            decimal discount = 0;
            decimal.TryParse(txtDiscount.Text.Trim(), out discount);

            if (discount < 0)
                discount = 0;

            decimal net = subTotal - discount;

            if (net < 0)
                net = 0;

            lblSubTotalValue.Text = subTotal.ToString("0.00");
            lblDiscountValue.Text = discount.ToString("0.00");
            lblNetValue.Text = net.ToString("0.00");
        }

        private void cmbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyOrderTypeVisibility();
        }

        private void btnSelectSupplier_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSupplierSelector())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                _supplierId = frm.SelectedSupplier.Id;
                txtSupplier.Text = frm.SelectedSupplier.SupplierName;
            }
        }

        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCustomerSelector())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                _customerId = frm.SelectedCustomer.Id;
                txtCustomer.Text = frm.SelectedCustomer.CustomerName;
            }
        }

        private void btnSelectSourceWarehouse_Click(object sender, EventArgs e)
        {
            using (var frm = new frmWarehouseSelector())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                _sourceWarehouseId = frm.SelectedWarehouse.Id;
                txtSourceWarehouse.Text = frm.SelectedWarehouse.Name;
            }
        }

            private void btnSelectDestinationWarehouse_Click(object sender, EventArgs e)
        {
            using (var frm = new frmWarehouseSelector())
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                _destinationWarehouseId = frm.SelectedWarehouse.Id;
                txtDestinationWarehouse.Text = frm.SelectedWarehouse.Name;
            }
        }

            private List<Guid> SelectedProducts() {

            return this._details.Select(x => x.ProductId).ToList();

        }
            private decimal SelectedQuantity {
            
            get {
                if (decimal.TryParse(txtQuantity.Text, out decimal result))
                    return result;
                return 0;
            }
            
            }
        private decimal SelectedDiscount
        {

            get
            {
                if (decimal.TryParse(txtDiscount.Text, out decimal result))
                    return result;
                return 0;
            }

        }

        private frmProductSelector MakeProductSelectorForm() {

            frmProductSelector frm = new frmProductSelector();

            var selectedProduct = SelectedProducts();

            frm.ExcludeProducts(selectedProduct);


            if (SelectedOrderType == OrderType.Purchase || SelectedOrderType == OrderType.ReturnOut)
            {
                if (_supplierId == null || _supplierId == Guid.Empty)
                {
                    errorProvider.SetError(txtSupplier, "Supplier is required for purchase.");
                    return null;
                }
                frm.FromSupplier(_supplierId);
            }
            else
            {
                if (_sourceWarehouseId == null || _sourceWarehouseId == Guid.Empty)
                {
                    errorProvider.SetError(txtSourceWarehouse, "Source Warehouse is required for purchase.");
                    return null;
                }
                frm.FromWarehouse(_sourceWarehouseId);
            }

            return frm;
        }
            private async void btnAddDetail_Click(object sender, EventArgs e)
            {


            var form = MakeProductSelectorForm();
            if (form == null) return;
                using (var frm = form)
                {
                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    var product = frm.SelectedProduct;

                if (product == null) return;

                var orderDetail = new OrderDetailDto()
                {
                    Product = new ProductDto()
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        BarCode = product.BarCode,
                        IsActive = product.IsActive,
                        SKU = product.SKU,
                        SellingPrice = product.SellingPrice,


                    },
                    ProductId = product.Id,
                    Quantity = SelectedQuantity,
                    UnitPrice = SelectedOrderType == OrderType.Purchase ? product.PurchasePrice.Value : product.SellingPrice,
                    RowVersion = product.RowVersion

                };

                if (_isUpdateMode)
                {
                    decimal qt = SelectedQuantity;
                    if (qt <= 0) {
                        MessageBox.Show("Invalid quantity defined."); 
                        return;
                    }
                    var result = await OrdersServices.CreateOrderDetail(new CreateOrderDetailRequest()
                    {
                        OrderId = _orderId,
                        ProductId = product.Id,
                        Quantity = qt,
                        RowVersion = product.RowVersion ,
                     });

                    if (!result.IsSuccess) {
                        lblStatus.Text = "Failed to add detail";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    orderDetail.RowVersion = result.Data.RowVersion;
                    orderDetail.Id = result.Data.Id; ; 
                    orderDetail.OrderId = result.Data.OrderId;
                   
                    _details.Add(orderDetail);

                }
                else
                {
                    _details.Add(orderDetail);
                }
                
                    BindDetailsGrid();
                    RecalculateSummary();
                }
            }
            private async void btnRemoveDetail_Click(object sender, EventArgs e)
            {
                var selected = GetSelectedDetail();

                if (selected == null)
                {
                    MessageBox.Show("Please select a detail first.");
                    return;
                }

            if (_isUpdateMode) {

                 var confirm = MessageBox.Show(
                  $"Are you sure you want to delete {selected.ProductName}?",
                  "Confirm Delete",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                btnRemoveDetail.Enabled = false;

                var result = await OrdersServices.DeleteOrderDetail(selected.Id);

                btnRemoveDetail.Enabled = true;

                if (!result.IsSuccess)
                {

                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }


                _details.Remove(selected);
                BindDetailsGrid();
                RecalculateSummary();
            }
            private void txtDiscount_TextChanged(object sender, EventArgs e)
            {
            if (string.IsNullOrEmpty(txtDiscount.Text))
            {
                txtDiscount.Text = "0";
                return;
            }
                RecalculateSummary();
            }
            private async void btnSave_Click(object sender, EventArgs e)
            {
                if (!ValidateForm())
                    return;

                btnSave.Enabled = false;
                lblStatus.Text = "Saving transaction...";

                if (_isUpdateMode)
                {
                    var result = await OrdersServices.Update(_orderId, BuildUpdateRequest());

                    btnSave.Enabled = true;

                    if (!result.IsSuccess)
                    {
                        lblStatus.Text = "Failed to update transaction";
                        MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    var result = await OrdersServices.Create(BuildCreateRequest());

                    btnSave.Enabled = true;

                    if (!result.IsSuccess)
                    {
                        lblStatus.Text = "Failed to create transaction";
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
            private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdateMode) return;
            var selected = GetSelectedDetail();
            var index = dgvDetails.GetselectedItemPosition();
            if (selected == null)
            {
                return;
            }

            selected.Quantity = SelectedQuantity;
            BindDetailsGrid();
            dgvDetails.SetAsSelected(index);
            RecalculateSummary(); 

        }

        private async void btnAddQuantity_Click(object sender, EventArgs e)
        {
            if (!_isUpdateMode) return;

 
            var selected = GetSelectedDetail();
            if (selected == null)
            {
                MessageBox.Show("Please select a detail first.");
                return;
            }
            var quantity = SelectedQuantity;
            if (quantity <= 0)
            {
                MessageBox.Show("Invalid quantity selected.");
                return;
            }

            var result = await OrdersServices.UpdateOrderDetailQuantity(selected.Id, new UpdateOrderDetailQuantityRequest()
            {
                Quantity = quantity,
                RowVersion = selected.RowVersion
            });

            if (!(result.IsSuccess)) {
                lblStatus.Text = "Failed to update quantity";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            selected.Quantity = quantity;

            BindDetailsGrid();
            RecalculateSummary();
        }
    }
    }
