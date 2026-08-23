namespace Contract.Features.Dashboard.Dtos
{
    public class DashboardDto
    {
        public int Customers { get; set; }
        public int Suppliers { get; set; }
        public int LowStockProducts { get; set; }
        public int OutOfStockProducts { get; set; }
        public int TotalProducts { get; set; }
        public int PendingOrders { get; set; }
        public int DraftAdjustments { get; set; }
        public int Warehouses { get; set; }

        public int TodaySaleOrders { get; set; }
        public int TodayPurchaseOrders { get; set; }
         public decimal ReservedStock { get; set; }
        public decimal StockMovementsToday { get; set; }

        public decimal SalesTodayRevenue { get; set; }
        public decimal PurchasesTodayRevenue { get; set; }

        public decimal SalesRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
    }
}
