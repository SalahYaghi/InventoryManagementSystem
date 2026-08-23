using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Services;

namespace UI.Forms.Adjustments
{
    public partial class frmAdjustmentDetails : Form
    {
        private readonly Guid _adjustmentId;
        private AdjustmentDto _adjustment;
        private List<AdjustmentDetailDto> _details = new List<AdjustmentDetailDto>();

        public frmAdjustmentDetails(Guid adjustmentId)
        {
            InitializeComponent();
            _adjustmentId = adjustmentId;
            SetupUI();
        }

        private async void frmAdjustmentDetails_Load(object sender, EventArgs e)
        {
            await LoadAdjustment();
        }

        private void SetupUI()
        {
            BackColor = Color.FromArgb(243, 246, 249);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            StyleButton(btnEdit, Color.FromArgb(74, 112, 139), Color.White);
            StyleButton(btnClose, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));
            StyleButton(btnRefresh, Color.FromArgb(243, 246, 249), Color.FromArgb(24, 33, 45));

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

        private async Task LoadAdjustment()
        {
            lblStatus.Text = "Loading adjustment details...";

            var result = await AdjustmentsServices.Get(_adjustmentId);

            if (!result.IsSuccess)
            {
                lblStatus.Text = "Failed to load adjustment";
                MessageBox.Show(result.Title_Full, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _adjustment = result.Data;
            _details = _adjustment.AdjustmentDetailDtos ?? new List<AdjustmentDetailDto>();

            BindHeader();
            BindOverview();
            BindDetailsCards();

            lblStatus.Text = "Ready";
        }

        private void BindHeader()
        {
            lblTitle.Text = _adjustment.AdjustmentType + " Adjustment";
            lblSubtitle.Text = "Adjustment ID: " + _adjustment.Id;

            lblTypeBadge.Text = _adjustment.AdjustmentType.ToString();
            lblStatusBadge.Text = _adjustment.AdjustmentStatus.ToString();

            ApplyTypeBadgeStyle(_adjustment.AdjustmentType.ToString());
            ApplyStatusBadgeStyle(_adjustment.AdjustmentStatus.ToString());
        }

        private void BindOverview()
        {
            lblWarehouseValue.Text = _adjustment.Warehouse == null ? _adjustment.WarehouseId.ToString() : _adjustment.Warehouse.Name;
            lblReasonValue.Text = _adjustment.AdjustmentReason.ToString();
            lblNotesValue.Text = string.IsNullOrWhiteSpace(_adjustment.Notes) ? "No notes provided." : _adjustment.Notes;

            lblItemsCountValue.Text = _details.Count.ToString();
            lblTotalQuantityValue.Text = _details.Sum(d => d.Quantity).ToString("0.##");
        }

        private void BindDetailsCards()
        {
            flowDetails.Controls.Clear();
            pnlDetails.Height = 0; flowDetails.Height = 0;

            if (_details.Count == 0)
            {
                flowDetails.Controls.Add(CreateEmptyDetailsCard());
                return;
            }

            int index = 1;

            foreach (var detail in _details.OrderBy(d => d.ProductName))
            {
                flowDetails.Controls.Add(CreateDetailCard(detail, index));
                index++;
                pnlDetails.Height = pnlDetails.Height + 142;
                flowDetails.Height = flowDetails.Height + 142;

            }
        }

        private Panel CreateDetailCard(AdjustmentDetailDto detail, int index)
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Size = new Size(830, 125);
            card.Margin = new Padding(0, 0, 0, 12);

            Panel accent = new Panel();
            accent.BackColor = _adjustment.AdjustmentType.ToString() == "Increase"
                ? Color.FromArgb(39, 120, 97)
                : Color.FromArgb(220, 53, 69);
            accent.Location = new Point(0, 0);
            accent.Size = new Size(6, 115);
            card.Controls.Add(accent);

            Label lblIndex = new Label();
            lblIndex.Text = "#" + index;
            lblIndex.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblIndex.ForeColor = Color.FromArgb(74, 112, 139);
            lblIndex.Location = new Point(22, 17);
            lblIndex.Size = new Size(55, 25);
            card.Controls.Add(lblIndex);

            Label lblProduct = new Label();
            lblProduct.Text = HandlerIfNull(detail.ProductName);
            lblProduct.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblProduct.ForeColor = Color.FromArgb(24, 33, 45);
            lblProduct.Location = new Point(82, 15);
            lblProduct.Size = new Size(470, 32);
            card.Controls.Add(lblProduct);
 

            Label lblSKU = new Label();
            lblSKU.Text = "SKU: " + HandlerIfNull(detail.Product.SKU);
            lblSKU.Font = new Font("Segoe UI", 8.5F);
            lblSKU.ForeColor = Color.Gray;
            lblSKU.Location = new Point(84, 48);
            lblSKU.Size = new Size(430, 22);
            card.Controls.Add(lblSKU);

            AddCaption(card, "Ordered Qty", detail.Quantity.ToString("0.##"), 85, 76, 135);


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

        private Panel CreateEmptyDetailsCard()
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Size = new Size(830, 90);
            card.Margin = new Padding(0, 0, 0, 12);

            Label label = new Label();
            label.Text = "No adjustment details found.";
            label.Font = new Font("Segoe UI", 11F);
            label.ForeColor = Color.Gray;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;

            card.Controls.Add(label);
            return card;
        }

        private void ApplyTypeBadgeStyle(string type)
        {
            lblTypeBadge.ForeColor = Color.White;

            if (type == "Increase")
                lblTypeBadge.BackColor = Color.FromArgb(39, 120, 97);
            else if (type == "Decrease")
                lblTypeBadge.BackColor = Color.FromArgb(220, 53, 69);
            else
                lblTypeBadge.BackColor = Color.Gray;
        }

        private void ApplyStatusBadgeStyle(string status)
        {
            if (status == "Approved")
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

        private void ClearDisplay()
        {
            lblTitle.Text = "Adjustment Details";
            lblSubtitle.Text = "Loading adjustment...";
            lblTypeBadge.Text = "-";
            lblStatusBadge.Text = "-";
            lblWarehouseValue.Text = "-";
            lblReasonValue.Text = "-";
            lblItemsCountValue.Text = "0";
            lblTotalQuantityValue.Text = "0";
            lblNotesValue.Text = "-";
        }

        private string HandlerIfNull(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAdjustmentEditor(_adjustmentId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadAdjustment();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAdjustment();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

