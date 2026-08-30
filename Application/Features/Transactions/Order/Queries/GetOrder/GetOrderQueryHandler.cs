using Contract.Common.Interfaces;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Transactions.Orders.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Orders.Queries.GetOrder
{
    public sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result<OrderDto>>
    {
        private readonly ILogger<GetOrderQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetOrderQueryHandler(IAppDbContext context,
            ILogger<GetOrderQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetOrderQueryHandler));

            var entity = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Product)
                .Include(o => o.DestinationWarehouse)
                .Include(o => o.SourceWarehouse)
                .Include(o => o.Customer)
                .Include(o => o.Supplier)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetOrderQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Order.NotFound\", \"Order was not found.\")");
                return Error.NotFound("Order.NotFound", "Order was not found.");

            }

            _logger.LogInformation("GetOrderQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

