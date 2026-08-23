 using Contract.Responses;
using Domain.Common.Helpers;
using global::UI.Services; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Shared.Helpers.IO_Helper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities;
namespace UI.Forms.Invoices
{
     
        public partial class frmShowInvoice : Form
        {
            private readonly Guid _invoiceId;
            private InvoiceDto _invoice;

            public frmShowInvoice(Guid invoiceId)
            {
                InitializeComponent();
                _invoiceId = invoiceId;
                SetupUI();
            }

            private async void frmShowInvoice_Load(object sender, EventArgs e)
            {
                await LoadInvoice();
            }

            private void SetupUI()
            {
                BackColor = Color.FromArgb(243, 246, 249);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;

                StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
                StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnDownloadAsPdf, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

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

            private async Task LoadInvoice()
            {
                lblStatus.Text = "Loading invoice...";

                var result = await InvoicesServices.Get(_invoiceId);

                if (!result.IsSuccess)
                {
                    lblStatus.Text = "Failed to load invoice";
                    MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _invoice = result.Data;

                BindInvoice();
            BindOverviewCard();

                lblStatus.Text = "Ready";
            }
        private void DeterminePartiyCaptionData()
        {

            string caption = "Partie";
            string value = "-";
            switch (this._invoice.Order.OrderType)
            {

                case OrderType.Purchase:
                    caption = "Supplier";
                    value = _invoice.Order.Supplier.SupplierName;
                    break;
                case OrderType.Sale:
                    caption = "Customer";
                    value = _invoice.Order.Customer.CustomerName;
                    break;
                case OrderType.Transfer:
                    caption = "Destination Warehouse";
                    value = _invoice.Order.DestinationWarehouseDto.Name;
                    break;

            }
            this.lblPartieCaption.Text = caption;
            this.lblPartieValue.Text = value;

        }
        private void BindOverviewCard()
        {

            lblDueDateValue.Text = _invoice.Order.DueDate.ToString("dd MMM yyyy HH:mm");

            string type = GetOrderTypeName();

            lblSourceWarehouseValue.Text = HandleIfNull(_invoice.Order == null ? "" :
                _invoice.Order.SourceWarehouseDto.Name);
            DeterminePartiyCaptionData();
        }
        private string HandleIfNull(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
        }
        private string GetOrderTypeName()
        {

            return _invoice.Order.OrderType.ToString();
        }

        private void BindInvoice()
            {
                if (_invoice == null)
                    return;

                lblInvoiceTitle.Text = "INVOICE";
             
                lblInvoiceTypeBadge.Text = _invoice.InvoiceType;
                lblInvoiceStatusBadge.Text = _invoice.Status;

                ApplyStatusBadgeStyle(_invoice.Status);
                ApplyTypeBadgeStyle(_invoice.InvoiceType);

                BindLineItems();
                BindTotals();
            }

            private void BindLineItems()
            {
                flowLineItems.Controls.Clear();

                if (_invoice.InvoiceLineItems == null || _invoice.InvoiceLineItems.Count == 0)
                {
                    flowLineItems.Controls.Add(CreateEmptyLineItem());
                    return;
                }

                foreach (var item in _invoice.InvoiceLineItems.OrderBy(i => i.LineNo))
                    flowLineItems.Controls.Add(CreateLineItemCard(item));
            }

            private Panel CreateLineItemCard(InvoiceLineItemDto item)
            {
                Panel card = new Panel();
                card.BackColor = Color.White;
                card.Size = new Size(830, 72);
                card.Margin = new Padding(0, 0, 0, 10);

                Panel accent = new Panel();
                accent.BackColor = Color.FromArgb(74, 112, 139);
                accent.Location = new Point(0, 0);
                accent.Size = new Size(5, 72);
                card.Controls.Add(accent);

                Label lblLineNo = CreateLabel("#" + item.LineNo, 20, 22, 55, 26, 10F, FontStyle.Bold, Color.FromArgb(74, 112, 139));
                card.Controls.Add(lblLineNo);

                Label lblDescription = CreateLabel(item.Description, 85, 14, 360, 26, 11.5F, FontStyle.Bold, Color.FromArgb(24, 33, 45));
                card.Controls.Add(lblDescription);

                Label lblQtyCaption = CreateCaption("Qty", 450, 12, 70);
                card.Controls.Add(lblQtyCaption);

                Label lblQty = CreateValue(item.Quantity.ToString("0.##"), 450, 36, 70);
                card.Controls.Add(lblQty);

                Label lblUnitCaption = CreateCaption("Unit Price", 530, 12, 100);
                card.Controls.Add(lblUnitCaption);

                Label lblUnit = CreateValue(FormatMoney(item.UnitPrice), 530, 36, 100);
                card.Controls.Add(lblUnit);


            Label lblTaxCaption = CreateCaption("Tax", 630, 12, 100);
            card.Controls.Add(lblTaxCaption);

            Label lblTax = CreateValue(FormatMoney(item.Tax), 630, 36, 100);
            card.Controls.Add(lblTax);



            Label lblTotalCaption = CreateCaption("Total", 705, 12, 100);
                lblTotalCaption.TextAlign = ContentAlignment.MiddleRight;
                card.Controls.Add(lblTotalCaption);

                Label lblTotal = CreateLabel(FormatMoney(item.TotalAmount), 675, 35, 130, 28, 12F, FontStyle.Bold, Color.FromArgb(74, 112, 139));
                lblTotal.TextAlign = ContentAlignment.MiddleRight;
                card.Controls.Add(lblTotal);

                return card;
            }

            private Panel CreateEmptyLineItem()
            {
                Panel card = new Panel();
                card.BackColor = Color.White;
                card.Size = new Size(830, 80);
                card.Margin = new Padding(0, 0, 0, 10);

                Label label = new Label();
                label.Text = "No invoice line items found.";
                label.Dock = DockStyle.Fill;
                label.Font = new Font("Segoe UI", 11F);
                label.ForeColor = Color.Gray;
                label.TextAlign = ContentAlignment.MiddleCenter;

                card.Controls.Add(label);
                return card;
            }

            private void BindTotals()
            {
                lblSubTotalValue.Text = FormatMoney(_invoice.SubTotalAmount);
                lblTaxValue.Text = FormatMoney(_invoice.TaxAmount);
                lblDiscountValue.Text = FormatMoney(_invoice.DiscountAmount);
                lblNetValue.Text = FormatMoney(_invoice.NetAmount);

                lblItemsCountValue.Text = _invoice.InvoiceLineItems == null
                    ? "0"
                    : _invoice.InvoiceLineItems.Count.ToString();

                lblTotalQuantityValue.Text = _invoice.InvoiceLineItems == null
                    ? "0"
                    : _invoice.InvoiceLineItems.Sum(i => i.Quantity).ToString("0.##");
            }

            private Label CreateCaption(string text, int x, int y, int width)
            {
                return CreateLabel(text, x, y, width, 20, 8.5F, FontStyle.Regular, Color.Gray);
            }

            private Label CreateValue(string text, int x, int y, int width)
            {
                return CreateLabel(text, x, y, width, 24, 9.5F, FontStyle.Bold, Color.FromArgb(24, 33, 45));
            }

            private Label CreateLabel(string text, int x, int y, int width, int height, float fontSize, FontStyle style, Color color)
            {
                Label label = new Label();
                label.Text = string.IsNullOrWhiteSpace(text) ? "-" : text;
                label.Location = new Point(x, y);
                label.Size = new Size(width, height);
                label.Font = new Font("Segoe UI", fontSize, style);
                label.ForeColor = color;
                label.TextAlign = ContentAlignment.MiddleLeft;
                return label;
            }

            private void ApplyStatusBadgeStyle(string status)
            {
                if (status == "Paid" || status == "Completed" || status == "Approved")
                {
                    lblInvoiceStatusBadge.BackColor = Color.FromArgb(219, 242, 230);
                    lblInvoiceStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);
                }
                else if (status == "Cancelled" || status == "Rejected")
                {
                    lblInvoiceStatusBadge.BackColor = Color.FromArgb(254, 226, 226);
                    lblInvoiceStatusBadge.ForeColor = Color.FromArgb(153, 27, 27);
                }
                else
                {
                    lblInvoiceStatusBadge.BackColor = Color.FromArgb(255, 247, 214);
                    lblInvoiceStatusBadge.ForeColor = Color.FromArgb(146, 64, 14);
                }
            }

            private void ApplyTypeBadgeStyle(string type)
            {
                lblInvoiceTypeBadge.BackColor = Color.FromArgb(74, 112, 139);
                lblInvoiceTypeBadge.ForeColor = Color.White;
            }

            private string FormatMoney(decimal value)
            {
                return value.ToString("0.00");
            }

            private void ClearDisplay()
            {
                lblInvoiceTitle.Text = "INVOICE";
                lblInvoiceTypeBadge.Text = "-";
                lblInvoiceStatusBadge.Text = "-";

                lblSubTotalValue.Text = "0.00";
                lblTaxValue.Text = "0.00";
                lblDiscountValue.Text = "0.00";
                lblNetValue.Text = "0.00";

                lblItemsCountValue.Text = "0";
                lblTotalQuantityValue.Text = "0";
            }

            private async void btnRefresh_Click(object sender, EventArgs e)
            {
                await LoadInvoice();
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }

        private async void btnDownloadAsPdf_Click(object sender, EventArgs e)
        {
            string path = FileHelper.ChooseFolderPathDialog();

            if (string.IsNullOrWhiteSpace(path))
                return;

            if (!ValidationHelper.ValidateLocalUrl(path))
            {
                MessageBox.Show(
                    "The selected path is invalid.",
                    "Invalid Path",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var pdfGenerated = await InvoicesServices.GetPdfbyId(_invoiceId);

            if (!pdfGenerated.IsSuccess ||
                pdfGenerated.Data == null ||
                pdfGenerated.Data.FileBytes.Length == 0)
            {
                MessageBox.Show(
                    pdfGenerated.Title_Full,
                    "Download Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            string filePath = Path.Combine(path, pdfGenerated.Data.FileName);

            File.WriteAllBytes(filePath, pdfGenerated.Data.FileBytes);

            MessageBox.Show(
                $"The file was downloaded successfully.\nLocation:\n{filePath}",
                "Download Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    } 
}

