using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Transactions.Orders.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;
using Contract.Common.Errors;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<OrderDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;  

        public UpdateOrderCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateOrderCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateOrderCommandHandler));

            var entity = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateOrderCommandHandler stopped: order {OrderId} not found.", request.Id);
                return ApplicationErrors.OrderNotFound; 
            }

            var result = 
                entity.Update(request.DiscountAmount, request.Notes, request.DueDate);

            if (result.IsError)

            {

                _logger.LogError("UpdateOrderCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            _logger.LogInformation("UpdateOrderCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateOrderCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Order), cancellationToken);

            _logger.LogInformation("Order updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

