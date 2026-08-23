using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Domain.Orders;

namespace Contract.Features.Transactions.Order.Commands.DeleteOrderDetail
{
    public sealed class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteOrderDetailCommandHandler> _logger;

        public DeleteOrderDetailCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteOrderDetailCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteOrderDetailCommandHandler));

            var entity = await _context.OrderDetails
                .Include(o => o.Order)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteOrderDetailCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"OrderDetail.NotFound\", \"OrderDetail was not found.\")");
                return Error.NotFound("OrderDetail.NotFound", "OrderDetail was not found.");

            }

            if (entity.Order!.IsLocked)

            {

                _logger.LogError("DeleteOrderDetailCommandHandler stopped because an error result was returned: {ErrorResult}.", "OrderErrors.OrderIsLocked");
                return OrderErrors.OrderIsLocked;

            }

            _logger.LogInformation("DeleteOrderDetailCommandHandler is marking entity data for persistence operation.");
            _context.OrderDetails.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteOrderDetailCommandHandler invalidated related cache entries successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.OrderDetail), cancellationToken);
 
            _logger.LogInformation("OrderDetail deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

