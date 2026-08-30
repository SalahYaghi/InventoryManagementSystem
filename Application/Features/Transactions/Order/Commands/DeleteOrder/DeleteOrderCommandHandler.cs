using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;
using Domain.Orders;
using Contract.Common.Errors;

namespace Contract.Features.Transactions.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteOrderCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteOrderCommandHandler));

            var entity = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteOrderCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.OrderNotFound");
                return ApplicationErrors.OrderNotFound;

            }

            if (entity.IsLocked)

            {

                _logger.LogError("DeleteOrderCommandHandler stopped because an error result was returned: {ErrorResult}.", "OrderErrors.OrderIsLocked");
                return OrderErrors.OrderIsLocked;

            }

            _logger.LogInformation("DeleteOrderCommandHandler is marking entity data for persistence operation.");
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteOrderCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Order), cancellationToken);

            _logger.LogInformation("Order deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

