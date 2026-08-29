using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Transactions.Orders.Mappers;
using Domain.Orders;
using Domain.Warehouses;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly IProductPolicies _productPolicies;
        private readonly IOrderPolicies _orderPolicies;

        public CreateOrderCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateOrderCommandHandler> logger,
            IProductPolicies productPolicies,
            IOrderPolicies orderPolicies)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _productPolicies = productPolicies;
            _orderPolicies = orderPolicies;
        }

        private sealed record ProductVersionInfo(Guid ProductId, decimal UnitPrice, byte[] RowVersion);

        public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateOrderCommandHandler));

            var orderDetails = new List<OrderDetail>();
            var productIds = request.OrderDetails.Select(p => p.ProductId).ToList();


            List<ProductVersionInfo> versions;

            if (request.OrderType == OrderType.Purchase || request.OrderType == OrderType.ReturnOut)
            {
                versions = await _context.SupplierProducts
                    .Where(p => productIds.Contains(p.ProductId) && p.SupplierId == request.SupplierId)
                    .Select(p => new ProductVersionInfo(p.ProductId, p.PurchasePrice, p.RowVersion))
                    .ToListAsync(cancellationToken);
            }
            else
            {
               
                var dbStock  = await _context.WarehouseStocks
                    .Include(r => r.Product)
                    .Where(p => productIds.Contains(p.ProductId) && p.WarehouseId == request.SourceWarehouseId)
                    .ToListAsync(cancellationToken);

                versions = new List<ProductVersionInfo>(); 
 
                foreach (var stock in dbStock) {
                    versions.Add(new ProductVersionInfo(stock.ProductId, stock.Product!.SellingPrice, stock.RowVersion));
                    _context.Entry(stock).Property(r => r.LastModifiedBy).IsModified = true; 
                }


            }

            foreach (var detail in request.OrderDetails)
            {
                var record = versions.FirstOrDefault(v => v.ProductId == detail.ProductId);

                if (record is null)
                {
                    _logger.LogWarning( 
                        "CreateOrderCommandHandler stopped: product {ProductId} is not available in the selected source.",
                        detail.ProductId);
                    return ApplicationErrors.ProductNotFound;
                }

                if (!record.RowVersion.SequenceEqual(detail.RowVersion))
                {
                    _logger.LogWarning(
                        "CreateOrderCommandHandler stopped: stale row version for product {ProductId}.",
                        detail.ProductId);
                    return ApplicationErrors.UpdateOccursOnProducts;
                }

                var detailObject = OrderDetail.Create(Guid.NewGuid(), detail.ProductId, detail.Quantity, record.UnitPrice);

                if (detailObject.IsError)
                {
                    _logger.LogError("CreateOrderCommandHandler stopped: {Errors}", detailObject.Errors);
                    return detailObject.Errors;
                }

                orderDetails.Add(detailObject.Value);
            }

            
            var entityResult = Domain.Orders.Order.Create(
                Guid.NewGuid(),
                request.OrderType,
                request.SupplierId,
                request.CustomerId,
                request.SourceWarehouseId,
                request.DestinationWarehouseId,
                request.Notes,
                request.Discount,
                orderDetails,
                request.DueDate);

            if (entityResult.IsError)
            {
                _logger.LogError("CreateOrderCommandHandler stopped: {Errors}", entityResult.Errors);
                return entityResult.Errors;
            }

            var order = entityResult.Value;

            var sourceWarehouseActive = await _context.Warehouses
                .Where(s => s.Id == order.SourceWarehouseId)
                .Select(s => (bool?)(s.WarehouseStatus == WarehouseStatus.Active))
                .FirstOrDefaultAsync(cancellationToken);

            if (sourceWarehouseActive is null)
            {
                _logger.LogWarning("CreateOrderCommandHandler stopped: source warehouse not found.");
                return ApplicationErrors.WarehouseNotFound;
            }

            if (!sourceWarehouseActive.Value)
            {
                _logger.LogWarning("CreateOrderCommandHandler stopped: source warehouse is inactive.");
                return ApplicationErrors.WarehouseInActive;
            }

            switch (order.OrderType)
            {
                case OrderType.Purchase:
                {
                    var supplierCheck = await ValidateSupplierAsync(order, cancellationToken);
                    if (supplierCheck.IsError) return supplierCheck.Errors;

                    break;
                }

                case OrderType.ReturnOut:
                {
                    var supplierCheck = await ValidateSupplierAsync(order, cancellationToken);
                    if (supplierCheck.IsError) return supplierCheck.Errors;

                    var stockCheck = await ValidateAvailabilityAsync(order, cancellationToken);
                    if (stockCheck.IsError) return stockCheck.Errors;
                    break;
                }

                case OrderType.Sale:
                {
                    var customerCheck = await ValidateCustomerAsync(order, cancellationToken);
                    if (customerCheck.IsError) return customerCheck.Errors;

                    var stockCheck = await ValidateAvailabilityAsync(order, cancellationToken);
                    if (stockCheck.IsError) return stockCheck.Errors;
                    break;
                }

                case OrderType.ReturnIn:
                {
                    var customerCheck = await ValidateCustomerAsync(order, cancellationToken);
                    if (customerCheck.IsError) return customerCheck.Errors;
                    break;
                }

                case OrderType.Transfer:
                {
                    var destinationActive = await _context.Warehouses
                        .Where(s => s.Id == order.DestinationWarehouseId)
                        .Select(s => (bool?)(s.WarehouseStatus == WarehouseStatus.Active))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (destinationActive is null)
                    {
                        _logger.LogWarning("CreateOrderCommandHandler stopped: destination warehouse not found.");
                        return ApplicationErrors.WarehouseNotFound;
                    }

                    if (!destinationActive.Value)
                    {
                        _logger.LogWarning("CreateOrderCommandHandler stopped: destination warehouse is inactive.");
                        return ApplicationErrors.WarehouseInActive;
                    }

                    var stockCheck = await ValidateAvailabilityAsync(order, cancellationToken);
                    if (stockCheck.IsError) return stockCheck.Errors;
                    break;
                }

                default:
                    _logger.LogError("CreateOrderCommandHandler stopped: unsupported order type {OrderType}.", order.OrderType);
                    return ApplicationErrors.UnsupportedOrderType;
            }


            _logger.LogInformation("CreateOrderCommandHandler is adding new entity data to the context.");
            await _context.OrderDetails.AddRangeAsync(orderDetails, cancellationToken);
            await _context.Orders.AddAsync(order, cancellationToken);


            await _context.SaveChangesAsync(cancellationToken);


            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Order), cancellationToken);

            _logger.LogInformation("Order created successfully with key {Key}", order.Id);

            return order.ToDto();
        }

        private async Task<Result<Success>> ValidateSupplierAsync(Domain.Orders.Order order, CancellationToken ct)
        {
            var supplierStatus = await _context.Suppliers
                .Where(s => s.Id == order.SupplierId)
                .Select(s => (bool?)s.Status)
                .FirstOrDefaultAsync(ct);

            if (supplierStatus is null)
            {
                _logger.LogWarning("CreateOrderCommandHandler stopped: supplier not found.");
                return ApplicationErrors.SupplierNotFound;
            }

            if (!supplierStatus.Value)
            {
                _logger.LogWarning("CreateOrderCommandHandler stopped: supplier is inactive.");
                return ApplicationErrors.SupplierInActive;
            }

            var sellsResult = await _productPolicies.CheckSupplierSellsProducts(
                order.SupplierId!.Value,
                order.OrderDetails.Select(p => p.ProductId).ToArray(),
                ct);

            if (sellsResult.IsError)
            {
                _logger.LogError("CreateOrderCommandHandler stopped: {Errors}", sellsResult.Errors);
                return sellsResult.Errors;
            }

            return Result.Success;
        }

        private async Task<Result<Success>> ValidateCustomerAsync(Domain.Orders.Order order, CancellationToken ct)
        {
            var customerExists = await _context.Customers
                .AnyAsync(s => s.Id == order.CustomerId, ct);

            if (!customerExists)
            {
                _logger.LogWarning("CreateOrderCommandHandler stopped: customer not found.");
                return ApplicationErrors.CustomerNotFound;
            }

            return Result.Success;
        }

        private async Task<Result<Success>> ValidateAvailabilityAsync(Domain.Orders.Order order, CancellationToken ct)
        {
            foreach (var detail in order.OrderDetails)
            {
                var result = await _orderPolicies.CheckPrductAvailableQuantity(
                    order.SourceWarehouseId, detail.ProductId, detail.Quantity, ct);

                if (result.IsError)
                {
                    _logger.LogError("CreateOrderCommandHandler stopped: {Errors}", result.Errors);
                    return result.Errors;
                }
            }

            return Result.Success;
        }
    }
}
