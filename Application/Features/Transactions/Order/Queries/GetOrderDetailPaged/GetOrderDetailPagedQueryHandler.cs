using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Contract.Features.Transactions.Order.DTOs;
using Contract.Features.Transactions.Order.Mappers;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Order.Queries.GetOrderDetailPaged
{
    public sealed class GetOrderDetailPagedQueryHandler : IRequestHandler<GetOrderDetailPagedQuery, Result<List<OrderDetailForListDto>>>
    {
        private readonly ILogger<GetOrderDetailPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetOrderDetailPagedQueryHandler(IAppDbContext context,
            ILogger<GetOrderDetailPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<OrderDetailForListDto>>> Handle(GetOrderDetailPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetOrderDetailPagedQueryHandler));

            var query = await _context.OrderDetails
                .AsNoTracking()
                .Where(o => o.OrderId == request.OrderId)
                .Select(x => new OrderDetailForListDto() {
                    OrderId = x.OrderId,
                    UnitPrice = x.UnitPrice,
                    ActualQuantity = x.ActualQuantity,
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product!.ProductName,
                    Quantity = x.Quantity,
                    
                
                }).ToListAsync(cancellationToken);

            if (query.Count == 0)
            {
                _logger.LogInformation("GetOrderDetailPagedQueryHandler order details are not found.");
                return Error.NotFound("OrderDetails.NotFound" , $"Order details with order id {request.OrderId} were not found."); 
            }

            _logger.LogInformation("GetOrderDetailPagedQueryHandler completed successfully.");
            return query;
        }
    }
}

