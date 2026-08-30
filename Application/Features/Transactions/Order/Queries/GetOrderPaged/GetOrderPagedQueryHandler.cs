using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Transactions.Orders.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Contract.Features.Transactions.Order.DTOs;
using Microsoft.Extensions.Logging;
using Contract.Common.Constants;

namespace Contract.Features.Transactions.Orders.Queries.GetOrderPaged
{
    public sealed class GetOrderPagedQueryHandler : IRequestHandler<GetOrderPagedQuery, Result<PaginatedList<OrderForListDto>>>
    {
        private readonly ILogger<GetOrderPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetOrderPagedQueryHandler(IAppDbContext context,
            ILogger<GetOrderPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PaginatedList<OrderForListDto>>> Handle(GetOrderPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetOrderPagedQueryHandler));

            var query = _context.Orders
                .OrderBy(x => x.Id)
                .Where(x => request.OrderType == null || 
                x.OrderType == request.OrderType)
                .AsNoTracking()
                .Select(x => new OrderForListDto()
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    InvoiceId = x.InvoiceId,
                    NetAmount = x.OrderDetails.Sum(d => d.UnitPrice * (d.ActualQuantity ?? d.Quantity)) - (x.DiscountAmount ?? 0),
                    CustomerName = x.Customer == null ? null : x.Customer.CustomerName,
                    
                    DestinationWarehouseId = x.DestinationWarehouseId,
                    DestinationWarehouseName = x.DestinationWarehouse == null ? null : x.DestinationWarehouse.Name,
                    
                    SourceWarehouseId = x.SourceWarehouseId,
                    SourceWarehouseName = x.SourceWarehouse == null ? null : x.SourceWarehouse.Name,
                    DiscountAmount = x.DiscountAmount ?? 0,
                    DueDate = x.DueDate,
                    OrderStatus = x.OrderStatus.ToString(),
                    
                    OrderType = x.OrderType.ToString(),
                 
                    SubTotalAmount = x.OrderDetails.Sum(d => d.UnitPrice * (d.ActualQuantity ?? d.Quantity)),
                   
                    SupplierId = x.SupplierId,
                    SupplierName = x.Supplier == null ? null : x.Supplier.SupplierName , 

                    CreatedAt = x.CreatedAtUtc ,
                    UpdatedAt = x.LastModifiedUtc
                });
            

            var result = await query.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation("GetOrderPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

