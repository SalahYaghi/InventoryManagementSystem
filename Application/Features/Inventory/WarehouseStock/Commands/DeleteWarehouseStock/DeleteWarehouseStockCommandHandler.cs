using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Inventory.WarehouseStocks.Commands.DeleteWarehouseStock
{
    public sealed class DeleteWarehouseStockCommandHandler : IRequestHandler<DeleteWarehouseStockCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteWarehouseStockCommandHandler> _logger;

        public DeleteWarehouseStockCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteWarehouseStockCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteWarehouseStockCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteWarehouseStockCommandHandler));

            var entity = await _context.WarehouseStocks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteWarehouseStockCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"WarehouseStock.NotFound\", \"WarehouseStock was not found.\")");
                return Error.NotFound("WarehouseStock.NotFound", "WarehouseStock was not found.");

            }

            _logger.LogInformation("DeleteWarehouseStockCommandHandler is marking entity data for persistence operation.");
            _context.WarehouseStocks.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteWarehouseStockCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.WarehouseStock), cancellationToken);
            _logger.LogInformation("DeleteWarehouseStockCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("WarehouseStock deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

