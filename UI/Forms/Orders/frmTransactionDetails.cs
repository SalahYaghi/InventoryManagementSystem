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
using System.Windows.Forms.VisualStyles;
using UI.Forms.Invoices;
using UI.Services;

namespace UI.Forms.Orders
{
    public partial class frmTransactionDetails : Form
    {
        private readonly Guid _orderId;
            private OrderDto _order;

            public frmTransactionDetails(Guid orderId)
            {
                InitializeComponent();
                _orderId = orderId;
                SetupUI();
            }

            private async void frmTransactionDetails_Load(object sender, EventArgs e)
            {
                await LoadTransaction();
            }


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

            private async Task LoadTransaction()
        {
            lblStatus.Text = "Loading transaction details...";

            var orderResult = await OrdersServices.Get(_orderId);

            if (!orderResult.IsSuccess)
            {
                lblStatus.Text = "Failed to load transaction";
                MessageBox.Show(orderResult.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _order = orderResult.Data;

            if (_order.InvoiceId.HasValue)
            {

                btnShowInvoice.Visible = true;
                btnIssueInvoice.Visible = false;

            }
            else {
                btnShowInvoice.Visible = false;
                btnIssueInvoice.Visible = true;

            }
            if (_order.OrderType == OrderType.Transfer) {
                btnShowInvoice.Visible = false;
                btnIssueInvoice.Visible = false;
                lblInvoiceIssued.Visible = false;
            }

                BindHeader();
                BindTransactionCards();
                BindDetailCards();
                BindSummary();
                DeterminePartiyCaptionData();
                lblStatus.Text = "Ready";
            }


            private void BindHeader()
            {
                string type = GetOrderTypeName();
                string status = GetOrderStatusName();

                lblTitle.Text = type + " Transaction";

                lblTypeBadge.Text = type;
                lblStatusBadge.Text = status;

            lblInvoiceIssued.Text = _order.InvoiceId.HasValue ? "Issued" : "Not Issued";

            ApplyIssuedBadgeStyle(_order.InvoiceId.HasValue);

            ApplyTypeBadgeStyle(type);
                ApplyStatusBadgeStyle(status);
            }

            private void DeterminePartiyCaptionData() {
 
            string caption = "Partie";
            string value = "-";
            switch (this._order.OrderType) {

                case OrderType.Purchase :
                    caption = "Supplier";
                    value = _order.Supplier.SupplierName;
                    break;
                case OrderType.Sale:
                    caption = "Customer";
                    value = _order.Customer.CustomerName;
                    break;
                case OrderType.Transfer:
                    caption = "Destination Warehouse";
                    value = _order.DestinationWarehouseDto.Name;
                    break;
                case OrderType.ReturnIn:
                    caption = "Customer";
                    value = _order.Customer.CustomerName;
                    break;
                case OrderType.ReturnOut:
                    caption = "Supplier";
                    value = _order.Supplier.SupplierName;
                    break;


            }
            this.lblPartieCaption.Text = caption;
            this.lblPartieValue.Text = value;

        }
            private void BindTransactionCards()
            {
              
            lblDueDateValue.Text = _order.DueDate.ToString("dd MMM yyyy HH:mm");
                lblNotesValue.Text = string.IsNullOrWhiteSpace(_order.Notes) ? "No notes provided." : _order.Notes;

                string type = GetOrderTypeName();

                 lblSourceWarehouseValue.Text = HandleIfNull(_order == null ? "" : 
                     _order.SourceWarehouseDto.Name);
              }

            private void BindDetailCards()
            {
                flowDetails.Controls.Clear();

                if (_order.OrderDetails.Count == 0)
                {
                    Panel emptyCard = CreateEmptyDetailsCard();
                    flowDetails.Controls.Add(emptyCard);
                    return;
                }

                int index = 1;
                foreach (var detail in _order.OrderDetails.OrderBy(d => d.ProductName))
                {
                    flowDetails.Controls.Add(CreateDetailCard(detail, index));
                pnlDetails.Height = pnlDetails.Height + 80;
                    index++;
                }
            }

            private void BindSummary()
            {
                decimal subTotal = _order.SubTotalAmount;
                decimal discount = (_order.DiscountAmount??0);
                decimal net = _order.NetAmount;

                if (subTotal == 0 && _order.OrderDetails.Count > 0)
                    subTotal = _order.OrderDetails.Sum(d => d.TotalAmount);

                if (net == 0)
                    net = subTotal - discount;
                if (net < 0)
                    net = 0;

                lblSubTotalValue.Text = FormatMoney(subTotal);
                lblDiscountValue.Text = FormatMoney(discount);
                lblNetValue.Text = FormatMoney(net);

                lblItemsCountValue.Text = _order.OrderDetails.Count.ToString();
                lblTotalQuantityValue.Text = _order.OrderDetails.Sum(d => d.Quantity).ToString("0.##");
                lblActualQuantityValue.Text = _order.OrderDetails.Sum(d => d.ActualQuantity == 0 ? d.Quantity : d.ActualQuantity??0).ToString("0.##");
            }

            private Panel CreateDetailCard(OrderDetailDto detail, int index)
            {
                Panel card = new Panel();
                card.BackColor = Color.White;
                card.Size = new Size(830, 112);
                card.Margin = new Padding(0, 0, 0, 12);

                Panel accent = new Panel();
                accent.BackColor = Color.FromArgb(74, 112, 139);
                accent.Location = new Point(0, 0);
                accent.Size = new Size(6, 112);
                card.Controls.Add(accent);

                Label lblIndex = new Label();
                lblIndex.Text = "#" + index;
                lblIndex.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                lblIndex.ForeColor = Color.FromArgb(74, 112, 139);
                lblIndex.Location = new Point(22, 18);
                lblIndex.Size = new Size(55, 25);
                card.Controls.Add(lblIndex);

                Label lblProduct = new Label();
                lblProduct.Text = HandleIfNull(detail.ProductName);
                lblProduct.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
                lblProduct.ForeColor = Color.FromArgb(24, 33, 45);
                lblProduct.Location = new Point(82, 15);
                lblProduct.Size = new Size(410, 32);
                card.Controls.Add(lblProduct);


            Label lblSKU = new Label();
            lblSKU.Text = "SKU: " + HandleIfNull(detail.Product.SKU);
            lblSKU.Font = new Font("Segoe UI", 8.5F);
            lblSKU.ForeColor = Color.Gray;
            lblSKU.Location = new Point(84, 48);
            lblSKU.Size = new Size(430, 22);
            card.Controls.Add(lblSKU);

            AddCaption(card, "Ordered Qty", detail.Quantity.ToString("0.##"), 85, 76, 135);
                AddCaption(card, "Actual Qty", (detail.ActualQuantity == 0 ? detail.Quantity : detail.ActualQuantity??0).ToString("0.##"), 240, 76, 135);
                AddCaption(card, "Unit Price", FormatMoney(detail.UnitPrice), 395, 76, 135);

                Label lblTotalCaption = new Label();
                lblTotalCaption.Text = "Line Total";
                lblTotalCaption.Font = new Font("Segoe UI", 9F);
                lblTotalCaption.ForeColor = Color.Gray;
                lblTotalCaption.Location = new Point(640, 24);
                lblTotalCaption.Size = new Size(150, 22);
                lblTotalCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                card.Controls.Add(lblTotalCaption);

                Label lblTotal = new Label();
                lblTotal.Text = FormatMoney(detail.TotalAmount);
                lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                lblTotal.ForeColor = Color.FromArgb(74, 112, 139);
                lblTotal.Location = new Point(565, 48);
                lblTotal.Size = new Size(235, 40);
                lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                card.Controls.Add(lblTotal);

                return card;
            }

            private Panel CreateEmptyDetailsCard()
            {
                Panel card = new Panel();
                card.BackColor = Color.White;
                card.Size = new Size(830, 90);
                card.Margin = new Padding(0, 0, 0, 12);

                Label label = new Label();
                label.Text = "No transaction details found.";
                label.Font = new Font("Segoe UI", 11F);
                label.ForeColor = Color.Gray;
                label.Dock = DockStyle.Fill;
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

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

            private void ApplyTypeBadgeStyle(string type)
            {
                lblTypeBadge.ForeColor = Color.White;

                if (type == "Purchase")
                    lblTypeBadge.BackColor = Color.FromArgb(74, 112, 139);
                else if (type == "Sale")
                    lblTypeBadge.BackColor = Color.FromArgb(39, 120, 97);
                else if (type == "Transfer")
                    lblTypeBadge.BackColor = Color.FromArgb(106, 90, 205);
                else
                    lblTypeBadge.BackColor = Color.Gray;
            }

            private void ApplyStatusBadgeStyle(string status)
            {
                if (status == "Completed")
                {
                    lblStatusBadge.BackColor = Color.FromArgb(219, 242, 230);
                    lblStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);
                }
                else if (status == "Cancelled")
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
                lblInvoiceIssued.BackColor = Color.FromArgb(254, 226, 226);
                lblInvoiceIssued.ForeColor = Color.FromArgb(153, 27, 27);
            }
        }

        private string GetOrderTypeName()
            {
           
                return _order.OrderType.ToString();
            }

            private string GetOrderStatusName()
            {
                return _order.OrderStatus.ToString();
            }


            private string HandleIfNull(string value)
            {
                return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
            }

            private string FormatMoney(decimal value)
            {
                return value.ToString("0.00");
            }

            private void ClearDisplay()
            {
                lblTitle.Text = "Transaction Details";
                lblTypeBadge.Text = "-";
                lblStatusBadge.Text = "-";

                lblPartieValue.Text = "-";
                lblDueDateValue.Text = "-";
                lblPartieValue.Text = "-";
                 lblSourceWarehouseValue.Text = "-";
                lblNotesValue.Text = "-";

            lblPartieCaption.Text = "Partie Caption";
            lblPartieValue.Text = "-";
                lblSubTotalValue.Text = "0.00";
                lblDiscountValue.Text = "0.00";
                lblNetValue.Text = "0.00";
                lblItemsCountValue.Text = "0";
                lblTotalQuantityValue.Text = "0";
                lblActualQuantityValue.Text = "0";
            }

            private void btnEdit_Click(object sender, EventArgs e)
            {
                using (var frm = new frmTransactionEditor(_orderId))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        _ = LoadTransaction();
                }
            }

          

            private async void btnRefresh_Click(object sender, EventArgs e)
            {
                await LoadTransaction();
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }

        private void lblTypeBadge_Click(object sender, EventArgs e)
        {

        }

        private async void btnIssueInvoice_Click(object sender, EventArgs e)
        {
            if (_order.InvoiceId.HasValue)
            {
                MessageBox.Show("order is already has an invoice", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show(
                  $"Are you sure you want to issue invoice?",
                  "Confirm Delete",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;
            btnIssueInvoice.Enabled = false;

            var dto = await InvoicesServices.Create(new Contract.Requests.Invoices.CreateInvoiceRequest()
            {
                OrderId = _orderId,
            });


            if (!dto.IsSuccess)
            {

                MessageBox.Show(dto.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueInvoice.Enabled = true;
                return;
            }
            else { 
            
                btnShowInvoice.Visible = true;
                btnIssueInvoice.Visible = false;
            
            }


            _order.InvoiceId = dto.Data.InvoiceId;
        }

        private void btnShowInvoice_Click(object sender, EventArgs e)
        {
            if (!_order.InvoiceId.HasValue) return;

            using (frmShowInvoice frm = new frmShowInvoice(_order.InvoiceId.Value))
            {
                frm.ShowDialog();
            }
        }
    }
    }

