using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Domain.Adjustments;
using Domain.Orders;
using Inventory.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class OrderServices(IAppDbContext context) : IOrderPolicies
    {
        public async Task<Result<bool>> 
            CheckPrductAvailableQuantity(
            Guid warehouseId, Guid productId, decimal quantity , CancellationToken ct)
        {   
            
            var stock = await context.WarehouseStocks
                .Where(s => s.WarehouseId == warehouseId &&
                s.ProductId == productId)
                .Select(s => (decimal?)s.Quantity)
                .FirstOrDefaultAsync(ct);


            if (stock is null)
                return ApplicationErrors.WarehouseStockNotFound;

            if (stock < quantity)
                return ApplicationErrors.QuantityInvalid;
            

            var ordersReservedQuantity = await context.OrderDetails
                 .Where(o => o.Order!.OrderStatus == OrderStatus.Pending &&
                   o.Order!.SourceWarehouseId == warehouseId &&
                   o.ProductId == productId && (
                   o.Order!.OrderType == OrderType.Sale ||
                   o.Order!.OrderType == OrderType.Transfer ||
                   o.Order!.OrderType == OrderType.ReturnOut )
                   
                   )
                 .Select(o => o.Quantity).SumAsync(ct);


            var adjustmentsReservedQuantity = await context.AdjustmentDetails
               .Where(o => 
                 o.Adjustment!.WarehouseId == warehouseId &&
                 o.Adjustment!.AdjustmentType == AdjustmentType.Decrease &&
                 o.Adjustment!.AdjustmentStatus == AdjustmentStatus.Draft &&
                 o.ProductId == productId)
               .Select(o => o.Quantity).SumAsync(ct);


            if (stock.Value - (ordersReservedQuantity + adjustmentsReservedQuantity) < quantity)
                return ApplicationErrors.QuantityInvaidReservedQuanity;


            return true;
        }
    }
}

