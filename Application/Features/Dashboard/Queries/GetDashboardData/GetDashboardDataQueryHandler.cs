using Contract.Common.Interfaces;
using Contract.Features.Dashboard.Dtos;
using Domain.Orders;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contract.Features.Dashboard.Queries.GetDashboardData
{
    public class GetDashboardDataQueryHandler(IAppDbContext context)
        : IRequestHandler<GetDashboardDataQuery, Result<DashboardDto>>
    {
        public async Task<Result<DashboardDto>> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            var dashboardDto = new DashboardDto();

            dashboardDto.Customers = await context.Customers.CountAsync(cancellationToken);
            dashboardDto.Suppliers = await context.Suppliers.CountAsync(cancellationToken);
            dashboardDto.Warehouses = await context.Warehouses.CountAsync(cancellationToken);
            dashboardDto.TotalProducts = await context.Products.CountAsync(cancellationToken);

            dashboardDto.DraftAdjustments = await context.Adjustments
                .CountAsync(a => a.AdjustmentStatus == Domain.Adjustments.AdjustmentStatus.Draft, cancellationToken);

            dashboardDto.PendingOrders = await context.Orders
                .CountAsync(a => a.OrderStatus == OrderStatus.Pending, cancellationToken);

            dashboardDto.LowStockProducts = await context.WarehouseStocks
                .CountAsync(a => a.Quantity > 0 && a.Quantity <= a.MinimumStockLevel, cancellationToken);

            dashboardDto.OutOfStockProducts = await context.WarehouseStocks
                .CountAsync(a => a.Quantity == 0, cancellationToken);

            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            dashboardDto.TodayPurchaseOrders = await context.Orders.CountAsync(
                o => o.OrderType == OrderType.Purchase && o.DueDate >= today && o.DueDate < tomorrow,
                cancellationToken);

            dashboardDto.TodaySaleOrders = await context.Orders.CountAsync(
                o => o.OrderType == OrderType.Sale && o.DueDate >= today && o.DueDate < tomorrow,
                cancellationToken);

            dashboardDto.StockMovementsToday = await context.OrderDetails
                .Where(o => o.Order!.OrderType == OrderType.Transfer &&
                            o.Order.DueDate >= today &&
                            o.Order.DueDate < tomorrow)
                .SumAsync(o => o.Quantity, cancellationToken);

            dashboardDto.ReservedStock = await context.OrderDetails
                .Where(w => w.Order!.OrderStatus == OrderStatus.Pending &&
                            (w.Order!.OrderType == OrderType.Sale ||
                             w.Order!.OrderType == OrderType.Transfer ||
                             w.Order!.OrderType == OrderType.ReturnOut))
                .SumAsync(v => v.Quantity, cancellationToken);

            dashboardDto.ReservedStock += await context.AdjustmentDetails
                .Where(w => w.Adjustment!.AdjustmentStatus == Domain.Adjustments.AdjustmentStatus.Draft &&
                            w.Adjustment.AdjustmentType == Domain.Adjustments.AdjustmentType.Decrease)
                .SumAsync(v => v.Quantity, cancellationToken);

            dashboardDto.PurchasesTodayRevenue = await SumCompletedNetAsync(OrderType.Purchase, today, tomorrow, cancellationToken);
            dashboardDto.SalesTodayRevenue = await SumCompletedNetAsync(OrderType.Sale, today, tomorrow, cancellationToken);

            dashboardDto.SalesRevenue = await SumCompletedNetAsync(OrderType.Sale, null, null, cancellationToken);
            dashboardDto.TotalExpenses = await SumCompletedNetAsync(OrderType.Purchase, null, null, cancellationToken);


            return dashboardDto;
        }

        private async Task<decimal> SumCompletedNetAsync(
            OrderType orderType, DateTime? from, DateTime? to, CancellationToken ct)
        {
            var query = context.Orders
                .Where(o => o.OrderStatus == OrderStatus.Completed && o.OrderType == orderType);

            if (from.HasValue && to.HasValue)
                query = query.Where(o => o.LastModifiedUtc >= from.Value && o.LastModifiedUtc < to.Value);

            return await query
                .Select(o => o.OrderDetails.Sum(d => d.Quantity * d.UnitPrice) - (o.DiscountAmount ?? 0))
                .SumAsync(ct);
        }
    }
}
