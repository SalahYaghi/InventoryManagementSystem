using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Forms.Invoices;
using UI.Services;
using UI.Shared.Helpers.UI_Helpers;

namespace UI.Forms.Orders
{
    public partial class frmTransactionDetails : Form
    {
        private const int DetailCardWidth = 830;
        private const int DetailCardHeight = 112;
        private const int DetailCardSpacing = 12;
        private const int EmptyCardHeight = 90;

        private readonly Guid _orderId;
        private OrderDto _order;

        private readonly int _flowDetailsTop;
        private readonly int _detailsPanelPadding;

        public frmTransactionDetails(Guid orderId)
        {
            InitializeComponent();

            _orderId = orderId;
            _flowDetailsTop = flowDetails.Top;
            _detailsPanelPadding = pnlDetails.Padding.Bottom;

            SetupUI();
        }

        #region Setup

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnEdit, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnIssueInvoice, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnShowInvoice, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

            lblStatus.Text = "Loading...";
            ClearDisplay();
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

        #endregion

        #region Loading

        private async void frmTransactionDetails_Load(object sender, EventArgs e)
        {
            await LoadTransaction();
        }

        private async Task LoadTransaction()
        {
            lblStatus.Text = "Loading transaction details...";
            btnRefresh.Enabled = false;

            var orderResult = await OrdersServices.Get(_orderId);

            btnRefresh.Enabled = true;

            if (!orderResult.IsSuccess || orderResult.Data == null)
            {
                lblStatus.Text = "Failed to load transaction";
                MessageBox.Show(orderResult.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _order = orderResult.Data;

            if (_order.OrderDetails == null)
                _order.OrderDetails = new List<OrderDetailDto>();

            ApplyOrderTypeVisibility();
            BindHeader();
            BindTransactionCards();
            BindDetailCards();
            BindSummary();
            BindPartyCaption();

            lblStatus.Text = "Ready";
        }

        #endregion

        #region Visibility rules

        private bool IsMonetaryTransaction
        {
            get { return _order != null && _order.OrderType != OrderType.Transfer; }
        }

        private void ApplyOrderTypeVisibility()
        {
            bool showMoney = IsMonetaryTransaction;

            lblSubTotalCaption.Visible = showMoney;
            lblSubTotalValue.Visible = showMoney;
            lblDiscountCaption.Visible = showMoney;
            lblDiscountValue.Visible = showMoney;
            lblNetCaption.Visible = showMoney;
            lblNetValue.Visible = showMoney;

            lblInvoiceIssued.Visible = showMoney;

            if (!showMoney)
            {
                btnIssueInvoice.Visible = false;
                btnShowInvoice.Visible = false;
                return;
            }

            bool hasInvoice = _order.InvoiceId.HasValue;

            btnShowInvoice.Visible = hasInvoice;
            btnIssueInvoice.Visible = !hasInvoice;
        }

        #endregion

        #region Binding

        private void BindHeader()
        {
            string type = GetOrderTypeName();
            string status = GetOrderStatusName();

            lblTitle.Text = type + " Transaction";
            lblTypeBadge.Text = type;
            lblStatusBadge.Text = status;

            ApplyTypeBadgeStyle(type);
            ApplyStatusBadgeStyle(status);

            if (!IsMonetaryTransaction)
                return;

            bool hasInvoice = _order.InvoiceId.HasValue;

            lblInvoiceIssued.Text = hasInvoice ? "Invoice Issued" : "No Invoice";
            ApplyIssuedBadgeStyle(hasInvoice);
        }

        private void BindPartyCaption()
        {
            string caption = "Party";
            string value = null;

            switch (_order.OrderType)
            {
                case OrderType.Purchase:
                case OrderType.ReturnOut:
                    caption = "Supplier";
                    value = _order.Supplier == null ? null : _order.Supplier.SupplierName;
                    break;

                case OrderType.Sale:
                case OrderType.ReturnIn:
                    caption = "Customer";
                    value = _order.Customer == null ? null : _order.Customer.CustomerName;
                    break;

                case OrderType.Transfer:
                    caption = "Destination Warehouse";
                    value = _order.DestinationWarehouseDto == null ? null : _order.DestinationWarehouseDto.Name;
                    break;
            }

            lblPartieCaption.Text = caption;
            lblPartieValue.Text = DisplayFormatter.Text(value, DisplayFormatter.NotSetPlaceholder);
        }

        private void BindTransactionCards()
        {
            lblDueDateValue.Text = DisplayFormatter.DateTimeValue(_order.DueDate);

            lblNotesValue.Text = string.IsNullOrWhiteSpace(_order.Notes)
                ? "No notes provided."
                : _order.Notes.Trim();

            lblSourceWarehouseValue.Text = DisplayFormatter.Text(
                _order.SourceWarehouseDto == null ? null : _order.SourceWarehouseDto.Name,
                DisplayFormatter.NotSetPlaceholder);
        }

        private void BindDetailCards()
        {
            flowDetails.SuspendLayout();

            foreach (Control control in flowDetails.Controls)
                control.Dispose();

            flowDetails.Controls.Clear();

            if (_order.OrderDetails.Count == 0)
            {
                flowDetails.Controls.Add(CreateEmptyDetailsCard());
                ResizeDetailsPanel(EmptyCardHeight + DetailCardSpacing);
                flowDetails.ResumeLayout();
                return;
            }

            int index = 1;

            foreach (var detail in _order.OrderDetails.OrderBy(d => d.ProductName))
            {
                flowDetails.Controls.Add(CreateDetailCard(detail, index));
                index++;
            }

            ResizeDetailsPanel(_order.OrderDetails.Count * (DetailCardHeight + DetailCardSpacing));
            flowDetails.ResumeLayout();
        }

        private void ResizeDetailsPanel(int contentHeight)
        {
            flowDetails.Height = contentHeight;
            pnlDetails.Height = _flowDetailsTop + contentHeight + _detailsPanelPadding;
        }

        private void BindSummary()
        {
            lblItemsCountValue.Text = DisplayFormatter.Count(_order.OrderDetails.Count);
            lblTotalQuantityValue.Text = DisplayFormatter.Quantity(_order.OrderDetails.Sum(d => d.Quantity));
            lblActualQuantityValue.Text = DisplayFormatter.Quantity(_order.OrderDetails.Sum(d => d.CurrentQuantity));

            if (!IsMonetaryTransaction)
                return;

            decimal subTotal = _order.SubTotalAmount;
            decimal discount = _order.DiscountAmount ?? 0m;

            if (subTotal == 0 && _order.OrderDetails.Count > 0)
                subTotal = _order.OrderDetails.Sum(d => d.TotalAmount);

            decimal net = _order.NetAmount;

            if (net == 0)
                net = subTotal - discount;

            if (net < 0)
                net = 0;

            lblSubTotalValue.Text = DisplayFormatter.Money(subTotal);
            lblDiscountValue.Text = DisplayFormatter.Money(discount);
            lblNetValue.Text = DisplayFormatter.Money(net);
        }

        #endregion

        #region Detail cards

        private Panel CreateDetailCard(OrderDetailDto detail, int index)
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Size = new Size(DetailCardWidth, DetailCardHeight);
            card.Margin = new Padding(0, 0, 0, DetailCardSpacing);

            Panel accent = new Panel();
            accent.BackColor = Color.FromArgb(74, 112, 139);
            accent.Location = new Point(0, 0);
            accent.Size = new Size(6, DetailCardHeight);
            card.Controls.Add(accent);

            Label lblIndex = new Label();
            lblIndex.Text = "#" + index;
            lblIndex.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblIndex.ForeColor = Color.FromArgb(74, 112, 139);
            lblIndex.Location = new Point(22, 18);
            lblIndex.Size = new Size(55, 25);
            card.Controls.Add(lblIndex);

            Label lblProduct = new Label();
            lblProduct.Text = DisplayFormatter.Text(detail.ProductName, "Unnamed product");
            lblProduct.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblProduct.ForeColor = Color.FromArgb(24, 33, 45);
            lblProduct.AutoEllipsis = true;
            lblProduct.Location = new Point(82, 15);
            lblProduct.Size = new Size(410, 32);
            card.Controls.Add(lblProduct);

            Label lblSKU = new Label();
            lblSKU.Text = "SKU: " + DisplayFormatter.Text(detail.Product == null ? null : detail.Product.SKU);
            lblSKU.Font = new Font("Segoe UI", 8.5F);
            lblSKU.ForeColor = Color.Gray;
            lblSKU.AutoEllipsis = true;
            lblSKU.Location = new Point(84, 48);
            lblSKU.Size = new Size(430, 22);
            card.Controls.Add(lblSKU);

            AddCaption(card, "Ordered Qty", DisplayFormatter.Quantity(detail.Quantity), 85, 76, 135);
            AddCaption(card, "Actual Qty", DisplayFormatter.Quantity(detail.CurrentQuantity), 240, 76, 135);

            if (!IsMonetaryTransaction)
                return card;

            AddCaption(card, "Unit Price", DisplayFormatter.Money(detail.UnitPrice), 395, 76, 135);

            Label lblTotalCaption = new Label();
            lblTotalCaption.Text = "Line Total";
            lblTotalCaption.Font = new Font("Segoe UI", 9F);
            lblTotalCaption.ForeColor = Color.Gray;
            lblTotalCaption.Location = new Point(640, 24);
            lblTotalCaption.Size = new Size(150, 22);
            lblTotalCaption.TextAlign = ContentAlignment.MiddleRight;
            card.Controls.Add(lblTotalCaption);

            Label lblTotal = new Label();
            lblTotal.Text = DisplayFormatter.Money(detail.TotalAmount);
            lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(74, 112, 139);
            lblTotal.Location = new Point(565, 48);
            lblTotal.Size = new Size(235, 40);
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            card.Controls.Add(lblTotal);

            return card;
        }

        private Panel CreateEmptyDetailsCard()
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Size = new Size(DetailCardWidth, EmptyCardHeight);
            card.Margin = new Padding(0, 0, 0, DetailCardSpacing);

            Label label = new Label();
            label.Text = "No transaction details found.";
            label.Font = new Font("Segoe UI", 11F);
            label.ForeColor = Color.Gray;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;

            card.Controls.Add(label);
            return card;
        }

        private void AddCaption(Control parent, string caption, string value, int x, int y, int width)
        {
            Label lblCaption = new Label();
            lblCaption.Text = caption;
            lblCaption.Font = new Font("Segoe UI", 8.5F);
            lblCaption.ForeColor = Color.Gray;
            lblCaption.Location = new Point(x, y);
            lblCaption.Size = new Size(width, 18);
            parent.Controls.Add(lblCaption);

            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblValue.ForeColor = Color.FromArgb(24, 33, 45);
            lblValue.Location = new Point(x, y + 18);
            lblValue.Size = new Size(width, 22);
            parent.Controls.Add(lblValue);
        }

        #endregion

        #region Badge styling

        private void ApplyTypeBadgeStyle(string type)
        {
            lblTypeBadge.ForeColor = Color.White;

            if (type == OrderType.Purchase.ToString())
                lblTypeBadge.BackColor = Color.FromArgb(74, 112, 139);
            else if (type == OrderType.Sale.ToString())
                lblTypeBadge.BackColor = Color.FromArgb(39, 120, 97);
            else if (type == OrderType.Transfer.ToString())
                lblTypeBadge.BackColor = Color.FromArgb(106, 90, 205);
            else if (type == OrderType.ReturnIn.ToString() || type == OrderType.ReturnOut.ToString())
                lblTypeBadge.BackColor = Color.FromArgb(176, 106, 66);
            else
                lblTypeBadge.BackColor = Color.Gray;
        }

        private void ApplyStatusBadgeStyle(string status)
        {
            if (status == OrderStatus.Completed.ToString())
            {
                lblStatusBadge.BackColor = Color.FromArgb(219, 242, 230);
                lblStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);
            }
            else if (status == OrderStatus.Cancelled.ToString())
            {
                lblStatusBadge.BackColor = Color.FromArgb(254, 226, 226);
                lblStatusBadge.ForeColor = Color.FromArgb(153, 27, 27);
            }
            else
            {
                lblStatusBadge.BackColor = Color.FromArgb(255, 247, 214);
                lblStatusBadge.ForeColor = Color.FromArgb(146, 64, 14);
            }
        }

        private void ApplyIssuedBadgeStyle(bool issued)
        {
            if (issued)
            {
                lblInvoiceIssued.BackColor = Color.FromArgb(219, 242, 230);
                lblInvoiceIssued.ForeColor = Color.FromArgb(22, 101, 52);
            }
            else
            {
                lblInvoiceIssued.BackColor = Color.FromArgb(255, 247, 214);
                lblInvoiceIssued.ForeColor = Color.FromArgb(146, 64, 14);
            }
        }

        #endregion

        #region Display helpers

        private string GetOrderTypeName()
        {
            return _order == null ? DisplayFormatter.EmptyPlaceholder : _order.OrderType.ToString();
        }

        private string GetOrderStatusName()
        {
            return _order == null ? DisplayFormatter.EmptyPlaceholder : _order.OrderStatus.ToString();
        }

        private void ClearDisplay()
        {
            lblTitle.Text = "Transaction Details";
            lblTypeBadge.Text = DisplayFormatter.EmptyPlaceholder;
            lblStatusBadge.Text = DisplayFormatter.EmptyPlaceholder;

            lblPartieCaption.Text = "Party";
            lblPartieValue.Text = DisplayFormatter.EmptyPlaceholder;
            lblDueDateValue.Text = DisplayFormatter.EmptyPlaceholder;
            lblSourceWarehouseValue.Text = DisplayFormatter.EmptyPlaceholder;
            lblNotesValue.Text = DisplayFormatter.EmptyPlaceholder;

            lblSubTotalValue.Text = DisplayFormatter.Money(0m);
            lblDiscountValue.Text = DisplayFormatter.Money(0m);
            lblNetValue.Text = DisplayFormatter.Money(0m);

            lblItemsCountValue.Text = "0";
            lblTotalQuantityValue.Text = "0";
            lblActualQuantityValue.Text = "0";
        }

        #endregion

        #region Actions

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var frm = new frmTransactionEditor(_orderId))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                    _ = LoadTransaction();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadTransaction();
        }

        private async void btnIssueInvoice_Click(object sender, EventArgs e)
        {
            if (_order == null)
                return;

            if (!IsMonetaryTransaction)
            {
                MessageBox.Show("Transfer transactions cannot be invoiced.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_order.InvoiceId.HasValue)
            {
                MessageBox.Show("This transaction already has an invoice.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to issue an invoice for this transaction?",
                "Confirm Invoice",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            btnIssueInvoice.Enabled = false;
            lblStatus.Text = "Issuing invoice...";

            var result = await InvoicesServices.Create(new Contract.Requests.Invoices.CreateInvoiceRequest
            {
                OrderId = _orderId
            });

            btnIssueInvoice.Enabled = true;

            if (!result.IsSuccess || result.Data == null)
            {
                lblStatus.Text = "Failed to issue invoice";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _order.InvoiceId = result.Data.InvoiceId;

            ApplyOrderTypeVisibility();
            BindHeader();

            lblStatus.Text = "Invoice issued successfully";
        }

        private void btnShowInvoice_Click(object sender, EventArgs e)
        {
            if (_order == null || !_order.InvoiceId.HasValue)
                return;

            using (var frm = new frmShowInvoice(_order.InvoiceId.Value))
            {
                frm.ShowDialog(this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
