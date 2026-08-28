using Contract.Requests.Orders;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.Customers;
using UI.Forms.Products;
using UI.Forms.Suppliers;
using UI.Forms.Warehouses;
using UI.Services;
using UI.Shared.CurrentUser;
using UI.Shared.Helpers.UI_Helpers;

namespace UI.Forms.Orders
{
    public partial class frmTransactionEditor : Form
    {
        private const int MaxNotesLength = 500;

        private readonly bool _isUpdateMode;
        private readonly Guid _orderId;

        private readonly List<OrderDetailDto> _details = new List<OrderDetailDto>();

        private Guid? _supplierId;
        private Guid? _customerId;
        private Guid? _sourceWarehouseId;
        private Guid? _destinationWarehouseId;

        private OrderStatus _orderStatus = OrderStatus.Pending;
        private bool _isLoading;

        #region Construction

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

        public void SetOrderType(OrderType orderType)
        {
            if (cmbOrderType.Items.Count == 0)
                return;

            foreach (object item in cmbOrderType.Items)
            {
                if (item is OrderType && (OrderType)item == orderType)
                {
                    cmbOrderType.SelectedItem = item;
                    return;
                }
            }
        }

        #endregion

        #region Setup

        private void SetupUI()
        {
            _isLoading = true;

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

            txtNotes.MaxLength = MaxNotesLength;

            cmbOrderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrderType.DataSource = Enum.GetValues(typeof(OrderType));

            dtpDueDate.Format = DateTimePickerFormat.Custom;
            dtpDueDate.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpDueDate.MinDate = DateTime.Now;
            dtpDueDate.Value = DateTime.Now.AddDays(1);

            lblTitle.Text = _isUpdateMode ? "Update Transaction" : "Create Transaction";
            lblSubtitle.Text = _isUpdateMode
                ? "Update discount, due date and notes for this transaction."
                : "Create a purchase, sale, return or warehouse transfer transaction.";

            ApplyDefaultSourceWarehouse();
            ApplyModeRules();

            _isLoading = false;

            ApplyOrderTypeVisibility();
            BindDetailsGrid();
            RecalculateSummary();

            this.txtQuantity.Text = "0";
        }

        private void ApplyDefaultSourceWarehouse()
        {
            var employee = CurrentUser.User == null ? null : CurrentUser.User.Employee;

            if (employee == null)
                return;

            _sourceWarehouseId = employee.WarehouseId;
            txtSourceWarehouse.Text = employee.Warehouse == null ? string.Empty : employee.Warehouse.Name;
        }

        private void ApplyModeRules()
        {
            btnUpdateQuantity.Visible = _isUpdateMode;
            btnUpdateQuantity.Enabled = _isUpdateMode;

            if (!_isUpdateMode)
                return;

            cmbOrderType.Enabled = false;
            btnSelectSupplier.Enabled = false;
            btnSelectCustomer.Enabled = false;
            btnSelectSourceWarehouse.Enabled = false;
            btnSelectDestinationWarehouse.Enabled = false;
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

        #endregion

        #region Loading

        private async void frmTransactionEditor_Load(object sender, EventArgs e)
        {
            if (_isUpdateMode)
                await LoadOrder();
        }

        private async Task LoadOrder()
        {
            _isLoading = true;
            lblStatus.Text = "Loading transaction...";

            var result = await OrdersServices.Get(_orderId);

            if (!result.IsSuccess || result.Data == null)
            {
                _isLoading = false;
                lblStatus.Text = "Failed to load transaction";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OrderDto order = result.Data;

            _orderStatus = order.OrderStatus;

            _supplierId = order.SupplierId;
            _customerId = order.CustomerId;
            _sourceWarehouseId = order.SourceWarehouseId;
            _destinationWarehouseId = order.DestinationWarehouseId;

            cmbOrderType.SelectedItem = order.OrderType;

            txtSupplier.Text = order.Supplier == null ? string.Empty : order.Supplier.SupplierName;
            txtCustomer.Text = order.Customer == null ? string.Empty : order.Customer.CustomerName;
            txtSourceWarehouse.Text = order.SourceWarehouseDto == null ? string.Empty : order.SourceWarehouseDto.Name;
            txtDestinationWarehouse.Text = order.DestinationWarehouseDto == null ? string.Empty : order.DestinationWarehouseDto.Name;

            txtNotes.Text = order.Notes ?? string.Empty;
            txtDiscount.Text = (order.DiscountAmount ?? 0m).ToString("0.00");

            ApplyDueDate(order.DueDate);

            _details.Clear();

            if (order.OrderDetails != null)
            {
                foreach (var item in order.OrderDetails)
                {
                    _details.Add(new OrderDetailDto
                    {
                        Id = item.Id,
                        OrderId = item.OrderId,
                        ProductId = item.ProductId,
                        Product = item.Product,
                        Quantity = item.Quantity,
                        ActualQuantity = item.ActualQuantity,
                        UnitPrice = item.UnitPrice,
                        RowVersion = item.RowVersion
                    });
                }
            }

            _isLoading = false;

            ApplyOrderTypeVisibility();
            ApplyStatusRules();
            BindDetailsGrid();
            RecalculateSummary();

            lblStatus.Text = "Ready";
            lblQuantity.Text = "0"; 
        }

        private void ApplyDueDate(DateTime dueDate)
        {
            DateTime lowerBound = dueDate < DateTime.Now ? dueDate : DateTime.Now;

            if (lowerBound < dtpDueDate.MinDate)
                lowerBound = dtpDueDate.MinDate;

            dtpDueDate.MinDate = lowerBound;

            if (dueDate < dtpDueDate.MinDate)
                dueDate = dtpDueDate.MinDate;

            if (dueDate > dtpDueDate.MaxDate)
                dueDate = dtpDueDate.MaxDate;

            dtpDueDate.Value = dueDate;
        }

        private void ApplyStatusRules()
        {
            bool isEditable = _orderStatus == OrderStatus.Pending;

            txtDiscount.Enabled = isEditable;
            txtNotes.Enabled = isEditable;
            dtpDueDate.Enabled = isEditable;
            txtQuantity.Enabled = isEditable;
            btnAddDetail.Enabled = isEditable;
            btnRemoveDetail.Enabled = isEditable;
            btnUpdateQuantity.Enabled = isEditable;
            btnUpdateQuantity.Visible = isEditable;
            btnSave.Enabled = isEditable;

            if (!isEditable)
                lblHint.Text = "This transaction is " + _orderStatus.ToString().ToLowerInvariant() + " and can no longer be edited.";
        }

        #endregion

        #region Order type rules

        private OrderType SelectedOrderType
        {
            get
            {
                if (cmbOrderType.SelectedItem == null)
                    return OrderType.Purchase;

                return (OrderType)cmbOrderType.SelectedItem;
            }
        }

        private bool RequiresSupplier
        {
            get { return SelectedOrderType == OrderType.Purchase || SelectedOrderType == OrderType.ReturnOut; }
        }

        private bool RequiresCustomer
        {
            get { return SelectedOrderType == OrderType.Sale || SelectedOrderType == OrderType.ReturnIn; }
        }

        private bool IsTransfer
        {
            get { return SelectedOrderType == OrderType.Transfer; }
        }

        private void ApplyOrderTypeVisibility()
        {
            if (cmbOrderType.SelectedItem == null)
                return;

            pnlSupplier.Visible = RequiresSupplier;
            pnlCustomer.Visible = RequiresCustomer;
            pnlSourceWarehouse.Visible = true;
            pnlDestinationWarehouse.Visible = IsTransfer;

            ShowDiscounts(!IsTransfer);

            lblHint.Text = BuildHintText();
        }

        private string BuildHintText()
        {
            switch (SelectedOrderType)
            {
                case OrderType.Purchase:
                    return "Purchase: supplier and source warehouse are required. Stock will be received into the source warehouse.";

                case OrderType.Sale:
                    return "Sale: customer and source warehouse are required. Stock will be issued from the source warehouse.";

                case OrderType.ReturnIn:
                    return "Return In: customer and source warehouse are required. Stock will be received into the source warehouse.";

                case OrderType.ReturnOut:
                    return "Return Out: supplier and source warehouse are required. Stock will be issued from the source warehouse.";

                case OrderType.Transfer:
                    return "Transfer: source and destination warehouses are required. Pricing and discounts do not apply.";

                default:
                    return string.Empty;
            }
        }

        private void ShowDiscounts(bool show)
        {
            lblDiscount.Visible = show;
            txtDiscount.Visible = show;
            lblDiscountValueTitle.Visible = show;
            lblDiscountValue.Visible = show;

            lblSubTotal.Visible = show;
            lblSubTotalValue.Visible = show;
            lblNet.Visible = show;
            lblNetValue.Visible = show;
        }

        private void ResetDetailsToDefault()
        {
            _details.Clear();

            txtDiscount.Text = "0.00";
            txtQuantity.Text = "0";

            lblSubTotalValue.Text = DisplayFormatter.Money(0m);
            lblDiscountValue.Text = DisplayFormatter.Money(0m);
            lblNetValue.Text = DisplayFormatter.Money(0m);

            BindDetailsGrid();
        }

        private void cmbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            ResetDetailsToDefault();
            ApplyOrderTypeVisibility();
            RecalculateSummary();
        }

        #endregion

        #region Grid

        private void BindDetailsGrid()
        {
            dgvDetails.SetData(_details.ToList());

            dgvDetails.HideColumns("Id", "OrderId", "ProductId", "Product", "RowVersion", "Quantity", "ActualQuantity");

            dgvDetails.SetColumnHeader("ProductName", "Product");
            dgvDetails.SetColumnHeader("CurrentQuantity", "Quantity");
            dgvDetails.SetColumnHeader("UnitPrice", "Unit Price");
            dgvDetails.SetColumnHeader("TotalAmount", "Total");

            dgvDetails.FormatColumnAsQuantity("CurrentQuantity");
            dgvDetails.FormatColumnsAsCurrency("UnitPrice", "TotalAmount");

            if (IsTransfer)
                dgvDetails.HideColumns("UnitPrice", "TotalAmount");
        }

        private OrderDetailDto GetSelectedDetail()
        {
            return dgvDetails.GetSelectedItem<OrderDetailDto>();
        }

        #endregion

        #region Summary

        private decimal SelectedQuantity
        {
            get
            {
                decimal result;
                return decimal.TryParse(txtQuantity.Text.Trim(), out result) ? result : 0m;
            }
        }

        private decimal SelectedDiscount
        {
            get
            {
                if (IsTransfer)
                    return 0m;

                decimal result;
                return decimal.TryParse(txtDiscount.Text.Trim(), out result) ? result : 0m;
            }
        }

        private void RecalculateSummary()
        {
            decimal subTotal = _details.Sum(d => d.CurrentQuantity * d.UnitPrice);

            decimal discount = SelectedDiscount;

            if (discount < 0)
                discount = 0;

            if (discount > subTotal)
                discount = subTotal;

            decimal net = subTotal - discount;

            if (net < 0)
                net = 0;

            lblSubTotalValue.Text = DisplayFormatter.Money(subTotal);
            lblDiscountValue.Text = DisplayFormatter.Money(discount);
            lblNetValue.Text = DisplayFormatter.Money(net);
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            RecalculateSummary();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading || _isUpdateMode)
                return;

            var selected = GetSelectedDetail();

            if (selected == null)
                return;

            int index = dgvDetails.GetSelectedItemPosition();

            selected.Quantity = SelectedQuantity;

            BindDetailsGrid();
            dgvDetails.SetAsSelected(index);
            RecalculateSummary();
        }

        #endregion

        #region Party selection

        private void btnSelectSupplier_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSupplierSelector())
            {
                if (frm.ShowDialog(this) != DialogResult.OK || frm.SelectedSupplier == null)
                    return;

                _supplierId = frm.SelectedSupplier.Id;
                txtSupplier.Text = frm.SelectedSupplier.SupplierName;
                errorProvider.SetError(txtSupplier, string.Empty);
            }
        }

        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCustomerSelector())
            {
                if (frm.ShowDialog(this) != DialogResult.OK || frm.SelectedCustomer == null)
                    return;

                _customerId = frm.SelectedCustomer.Id;
                txtCustomer.Text = frm.SelectedCustomer.CustomerName;
                errorProvider.SetError(txtCustomer, string.Empty);
            }
        }

        private void btnSelectSourceWarehouse_Click(object sender, EventArgs e)
        {
            using (var frm = new frmWarehouseSelector())
            {
                if (frm.ShowDialog(this) != DialogResult.OK || frm.SelectedWarehouse == null)
                    return;

                _sourceWarehouseId = frm.SelectedWarehouse.Id;
                txtSourceWarehouse.Text = frm.SelectedWarehouse.Name;
                errorProvider.SetError(txtSourceWarehouse, string.Empty);
            }
        }

        private void btnSelectDestinationWarehouse_Click(object sender, EventArgs e)
        {
            using (var frm = new frmWarehouseSelector())
            {
                if (frm.ShowDialog(this) != DialogResult.OK || frm.SelectedWarehouse == null)
                    return;

                _destinationWarehouseId = frm.SelectedWarehouse.Id;
                txtDestinationWarehouse.Text = frm.SelectedWarehouse.Name;
                errorProvider.SetError(txtDestinationWarehouse, string.Empty);
            }
        }

        #endregion

        #region Details management

        private List<Guid> SelectedProducts()
        {
            return _details.Select(x => x.ProductId).ToList();
        }

        private frmProductSelector MakeProductSelectorForm()
        {
            if (RequiresSupplier && !HasValue(_supplierId))
            {
                errorProvider.SetError(txtSupplier, "Select a supplier before adding products.");
                return null;
            }

            if (!RequiresSupplier && !HasValue(_sourceWarehouseId))
            {
                errorProvider.SetError(txtSourceWarehouse, "Select a source warehouse before adding products.");
                return null;
            }

            frmProductSelector frm = new frmProductSelector();

            try
            {
                frm.ExcludeProducts(SelectedProducts());

                if (RequiresSupplier)
                    frm.FromSupplier(_supplierId);
                else
                    frm.FromWarehouse(_sourceWarehouseId);

                return frm;
            }
            catch
            {
                frm.Dispose();
                throw;
            }
        }

        private async void btnAddDetail_Click(object sender, EventArgs e)
        {
            decimal quantity = SelectedQuantity;

            //if (quantity <= 0)
            //{
            //    errorProvider.SetError(txtQuantity, "Enter a quantity greater than zero before adding a product.");
            //    txtQuantity.Focus();
            //    return;
            //}

           // errorProvider.SetError(txtQuantity, string.Empty);

            var form = MakeProductSelectorForm();

            if (form == null)
                return;

            using (var frm = form)
            {
                if (frm.ShowDialog(this) != DialogResult.OK)
                    return;

                var product = frm.SelectedProduct;

                if (product == null)
                    return;

                if (_details.Any(d => d.ProductId == product.Id))
                {
                    MessageBox.Show("This product is already part of the transaction.", "Duplicate Product",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var orderDetail = BuildOrderDetail(product, quantity);

                if (_isUpdateMode && !await PersistNewDetail(orderDetail, product.Id, quantity, product.RowVersion))
                    return;

                _details.Add(orderDetail);

                BindDetailsGrid();
                RecalculateSummary();

                lblStatus.Text = "Detail added";
            }
        }

        private OrderDetailDto BuildOrderDetail(ProductDtoForList product, decimal quantity)
        {
            decimal unitPrice = RequiresSupplier
                ? (product.PurchasePrice ?? product.SellingPrice)
                : product.SellingPrice;

            return new OrderDetailDto
            {
                Product = new ProductDto
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    BarCode = product.BarCode,
                    IsActive = product.IsActive,
                    SKU = product.SKU,
                    SellingPrice = product.SellingPrice
                },
                ProductId = product.Id,
                Quantity = quantity,
                UnitPrice = unitPrice,
                RowVersion = product.RowVersion
            };
        }

        private async Task<bool> PersistNewDetail(OrderDetailDto orderDetail, Guid productId, decimal quantity, byte[] rowVersion)
        {
            btnAddDetail.Enabled = false;

            var result = await OrdersServices.CreateOrderDetail(new CreateOrderDetailRequest
            {
                OrderId = _orderId,
                ProductId = productId,
                Quantity = quantity,
                RowVersion = rowVersion
            });

            btnAddDetail.Enabled = true;

            if (!result.IsSuccess || result.Data == null)
            {
                lblStatus.Text = "Failed to add detail";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            orderDetail.Id = result.Data.Id;
            orderDetail.OrderId = result.Data.OrderId;
            orderDetail.RowVersion = result.Data.RowVersion;
            orderDetail.UnitPrice = result.Data.UnitPrice;

            return true;
        }

        private async void btnRemoveDetail_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedDetail();

            if (selected == null)
            {
                MessageBox.Show("Please select a detail first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_isUpdateMode)
            {
                var confirm = MessageBox.Show(
                    "Are you sure you want to remove " + DisplayFormatter.Text(selected.ProductName, "this product") + "?",
                    "Confirm Remove",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                btnRemoveDetail.Enabled = false;

                var result = await OrdersServices.DeleteOrderDetail(selected.Id);

                btnRemoveDetail.Enabled = true;

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to remove detail";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _details.Remove(selected);

            BindDetailsGrid();
            RecalculateSummary();

            lblStatus.Text = "Detail removed";
        }

        private async void btnUpdateQuantity_Click(object sender, EventArgs e)
        {
            if (!_isUpdateMode)
                return;

            var selected = GetSelectedDetail();

            if (selected == null)
            {
                MessageBox.Show("Please select a detail first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal quantity = SelectedQuantity;

            if (quantity <= 0)
            {
                errorProvider.SetError(txtQuantity, "Enter a quantity greater than zero.");
                txtQuantity.Focus();
                return;
            }

            errorProvider.SetError(txtQuantity, string.Empty);

            btnUpdateQuantity.Enabled = false;

            var result = await OrdersServices.UpdateOrderDetailQuantity(selected.Id, new UpdateOrderDetailQuantityRequest
            {
                Quantity = quantity,
                RowVersion = selected.RowVersion
            });

            btnUpdateQuantity.Enabled = true;

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to update quantity";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            selected.Quantity = quantity;
            selected.ActualQuantity = null;

            await RefreshDetailConcurrencyToken(selected);

            int index = dgvDetails.GetSelectedItemPosition();

            BindDetailsGrid();
            dgvDetails.SetAsSelected(index);
            RecalculateSummary();

            lblStatus.Text = "Quantity updated";
        }

        private async Task RefreshDetailConcurrencyToken(OrderDetailDto detail)
        {
            var refreshed = await OrdersServices.GetOrderDetail(detail.Id);

            if (!refreshed.IsSuccess || refreshed.Data == null)
                return;

            detail.RowVersion = refreshed.Data.RowVersion;
            detail.Quantity = refreshed.Data.Quantity;
            detail.ActualQuantity = refreshed.Data.ActualQuantity;
            detail.UnitPrice = refreshed.Data.UnitPrice;
        }

        #endregion

        #region Validation

        private static bool HasValue(Guid? id)
        {
            return id.HasValue && id.Value != Guid.Empty;
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();

            bool valid = true;

            if (RequiresSupplier && !HasValue(_supplierId))
            {
                errorProvider.SetError(txtSupplier, "Supplier is required for " + SelectedOrderType + " transactions.");
                valid = false;
            }

            if (RequiresCustomer && !HasValue(_customerId))
            {
                errorProvider.SetError(txtCustomer, "Customer is required for " + SelectedOrderType + " transactions.");
                valid = false;
            }

            if (!HasValue(_sourceWarehouseId))
            {
                errorProvider.SetError(txtSourceWarehouse, "Source warehouse is required.");
                valid = false;
            }

            if (IsTransfer)
            {
                if (!HasValue(_destinationWarehouseId))
                {
                    errorProvider.SetError(txtDestinationWarehouse, "Destination warehouse is required for a transfer.");
                    valid = false;
                }
                else if (_sourceWarehouseId == _destinationWarehouseId)
                {
                    errorProvider.SetError(txtDestinationWarehouse, "Destination warehouse cannot be the same as the source warehouse.");
                    valid = false;
                }
            }

            if (!_isUpdateMode && _details.Count == 0)
            {
                MessageBox.Show("Please add at least one transaction detail.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valid = false;
            }

            if (!IsTransfer && !ValidateDiscount())
                valid = false;

            if (txtNotes.Text.Trim().Length > MaxNotesLength)
            {
                errorProvider.SetError(txtNotes, "Notes must not exceed " + MaxNotesLength + " characters.");
                valid = false;
            }

            return valid;
        }

        private bool ValidateDiscount()
        {
            string text = txtDiscount.Text.Trim();

            if (string.IsNullOrEmpty(text))
                return true;

            decimal discount;

            if (!decimal.TryParse(text, out discount))
            {
                errorProvider.SetError(txtDiscount, "Discount must be a valid number.");
                return false;
            }

            if (discount < 0)
            {
                errorProvider.SetError(txtDiscount, "Discount must be zero or greater.");
                return false;
            }

            decimal subTotal = _details.Sum(d => d.CurrentQuantity * d.UnitPrice);

            if (_details.Count > 0 && discount > subTotal)
            {
                errorProvider.SetError(txtDiscount, "Discount cannot be greater than the sub total.");
                return false;
            }

            return true;
        }

        #endregion

        #region Requests

        private CreateOrderRequest BuildCreateRequest()
        {
            return new CreateOrderRequest
            {
                OrderType = SelectedOrderType,
                SupplierId = RequiresSupplier ? _supplierId : null,
                CustomerId = RequiresCustomer ? _customerId : null,
                SourceWarehouseId = _sourceWarehouseId.Value,
                DestinationWarehouseId = IsTransfer ? _destinationWarehouseId : null,
                DueDate = dtpDueDate.Value,
                Discount = SelectedDiscount,
                Notes = txtNotes.Text.Trim(),
                OrderDetails = _details.Select(d => new CreateOrderDetailRequestInner
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    RowVersion = d.RowVersion
                }).ToList()
            };
        }

        private UpdateOrderRequest BuildUpdateRequest()
        {
            return new UpdateOrderRequest
            {
                Id = _orderId,
                DiscountAmount = SelectedDiscount,
                Notes = txtNotes.Text.Trim(),
                DueDate = dtpDueDate.Value
            };
        }

        #endregion

        #region Save

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            btnSave.Enabled = false;
            lblStatus.Text = "Saving transaction...";

            bool saved = _isUpdateMode ? await SaveUpdate() : await SaveCreate();

            btnSave.Enabled = true;

            if (!saved)
                return;

            lblStatus.Text = "Saved successfully";
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task<bool> SaveUpdate()
        {
            var result = await OrdersServices.Update(_orderId, BuildUpdateRequest());

            if (result.IsSuccess)
                return true;

            lblStatus.Text = "Failed to update transaction";
            MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private async Task<bool> SaveCreate()
        {
            var result = await OrdersServices.Create(BuildCreateRequest());

            if (result.IsSuccess)
                return true;

            lblStatus.Text = "Failed to create transaction";
            MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}
