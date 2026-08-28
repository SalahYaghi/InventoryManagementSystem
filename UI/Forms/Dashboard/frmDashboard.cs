using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Shared.Helpers.UI_Helpers;
using UI.Shared.Services;

namespace UI.Forms.Dashboard
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
            BuildDashboard();
        }

        private Image GetCardIcon(string title)
        {
            switch (title.ToLower())
            {
                case "customers":
                    return Properties.Resources.icons8_customer_48;

                case "suppliers":
                    return Properties.Resources.icons8_supplier_50;

                case "low stock":
                    return Properties.Resources.icons8_low_64;

                case "out of stock":
                    return Properties.Resources.icons8_out_of_stock_50;

                case "total products":
                case "reserved stock":
                    return Properties.Resources.icons8_product_50;

                case "pending orders":
                case "today sales orders":
                case "today purchase orders":
                    return Properties.Resources.icons8_order_50;

                case "draft adjustments":
                case "stock movements today":
                    return Properties.Resources.icons8_adjustment_50;

                case "warehouses":
                    return Properties.Resources.icons8_warehouse_50;

                case "sales today revenue":
                case "purchases today revenue":
                case "sales revenue":
                case "total expenses":
                    return Properties.Resources.icons8_stocks_growth_96;

                default:
                    return Properties.Resources.icons8_warehouse_50;
            }
        }

        private void BuildDashboard()
        {
            FormBorderStyle = FormBorderStyle.None;
            TopLevel = false;
            Dock = DockStyle.Fill;

            Text = "Dashboard";
            BackColor = Color.FromArgb(243, 246, 249);
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            flpCards.Dock = DockStyle.Fill;
            flpCards.AutoScroll = true;
            flpCards.WrapContents = true;
            flpCards.BackColor = Color.FromArgb(243, 246, 249);
            flpCards.Padding = new Padding(18);

            Controls.Add(flpCards);
        }

        public async Task RefreshData()
        {
            await _LoadCards();
        }

        private void AddCard(string title, decimal value, Color accentColor, bool isMoney = false)
        {
            Panel shadow = new Panel
            {
                Width = 265,
                Height = 140,
                BackColor = Color.FromArgb(225, 230, 235),
                Margin = new Padding(14)
            };

            Panel card = new Panel
            {
                Width = 260,
                Height = 135,
                BackColor = Color.White,
                Location = new Point(0, 0),
                Cursor = Cursors.Hand
            };

            Panel accent = new Panel
            {
                Width = 5,
                Dock = DockStyle.Left,
                BackColor = accentColor
            };

            Label titleLabel = new Label
            {
                Text = title.ToUpper(),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(120, 128, 138),
                Location = new Point(22, 22),
                Size = new Size(180, 24)
            };

            Label valueLabel = new Label
            {
                Text = isMoney ? DisplayFormatter.Money(value) : DisplayFormatter.Quantity(value),
                Font = new Font("Segoe UI", isMoney ? 19F : 27F, FontStyle.Bold),
                AutoEllipsis = true,
                ForeColor = Color.FromArgb(24, 33, 45),
                Location = new Point(20, 48),
                Size = new Size(175, 58)
            };

            Label subtitleLabel = new Label
            {
                Text = isMoney ? "Financial overview" : "Inventory overview",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(145, 150, 158),
                Location = new Point(23, 105),
                Size = new Size(160, 22)
            };

            Panel iconPanel = new Panel
            {
                Size = new Size(54, 54),
                Location = new Point(205, 38),
                BackColor = Color.White
            };

            PictureBox iconBox = new PictureBox
            {
                Size = new Size(32, 32),
                Location = new Point(11, 11),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = GetCardIcon(title)
            };

            iconPanel.Controls.Add(iconBox);

            card.Controls.Add(accent);
            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(subtitleLabel);
            card.Controls.Add(iconPanel);

            shadow.Controls.Add(card);
            flpCards.Controls.Add(shadow);

            void HoverOn()
            {
                card.BackColor = Color.FromArgb(250, 252, 254);
                shadow.BackColor = Color.FromArgb(205, 213, 222);
            }

            void HoverOff()
            {
                card.BackColor = Color.White;
                shadow.BackColor = Color.FromArgb(225, 230, 235);
            }

            card.MouseEnter += (s, e) => HoverOn();
            card.MouseLeave += (s, e) => HoverOff();
            iconBox.MouseEnter += (s, e) => HoverOn();
            iconBox.MouseLeave += (s, e) => HoverOff();
            titleLabel.MouseEnter += (s, e) => HoverOn();
            titleLabel.MouseLeave += (s, e) => HoverOff();
            valueLabel.MouseEnter += (s, e) => HoverOn();
            valueLabel.MouseLeave += (s, e) => HoverOff();
            subtitleLabel.MouseEnter += (s, e) => HoverOn();
            subtitleLabel.MouseLeave += (s, e) => HoverOff();
        }

        private async Task _LoadCards()
        {
            flpCards.Controls.Clear();
            
            var cards = await DashboardServices.Get();

            if (!cards.IsSuccess || cards.Data == null)
                return;

            var data = cards.Data;

            AddCard("Customers", data.Customers, Color.FromArgb(74, 112, 139));
            AddCard("Suppliers", data.Suppliers, Color.FromArgb(70, 130, 100));
            AddCard("Low Stock", data.LowStockProducts, Color.FromArgb(160, 120, 55));
            AddCard("Out of Stock", data.OutOfStockProducts, Color.FromArgb(150, 60, 60));
            AddCard("Total Products", data.TotalProducts, Color.FromArgb(74, 112, 139));
            AddCard("Pending Orders", data.PendingOrders, Color.FromArgb(160, 120, 55));
            AddCard("Draft Adjustments", data.DraftAdjustments, Color.FromArgb(100, 90, 140));
            AddCard("Warehouses", data.Warehouses, Color.FromArgb(74, 112, 139));

            AddCard("Today Sales Orders", data.TodaySaleOrders, Color.FromArgb(70, 130, 100));
            AddCard("Today Purchase Orders", data.TodayPurchaseOrders, Color.FromArgb(74, 112, 139));
            AddCard("Reserved Stock", data.ReservedStock, Color.FromArgb(160, 120, 55));
            AddCard("Stock Movements Today", data.StockMovementsToday, Color.FromArgb(100, 90, 140));

            AddCard("Sales Today Revenue", data.SalesTodayRevenue, Color.FromArgb(70, 130, 100), true);
            AddCard("Purchases Today Revenue", data.PurchasesTodayRevenue, Color.FromArgb(150, 90, 60), true);
            AddCard("Sales Revenue", data.SalesRevenue, Color.FromArgb(74, 112, 139), true);
            AddCard("Total Expenses", data.TotalExpenses, Color.FromArgb(150, 60, 60), true);
        }

        private async void frmDashboard_Load(object sender, EventArgs e)
        {
         //   await _LoadCards();
        }

    }
}