using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Contract.Common.Errors;

namespace Contract.Features.Inventory.Warehouses.Commands.DeleteWarehouse
{
    public sealed class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteWarehouseCommandHandler> _logger;

        public DeleteWarehouseCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteWarehouseCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteWarehouseCommandHandler));

            var entity = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteWarehouseCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Warehouse.NotFound\", \"Warehouse was not found.\")");
                return Error.NotFound("Warehouse.NotFound", "Warehouse was not found.");

            }

            var hasStock = await _context.WarehouseStocks.AnyAsync(w => w.WarehouseId == request.Id, cancellationToken);

            if (hasStock)
            {
                _logger.LogWarning("DeleteWarehouseCommandHandler stopped: warehouse {Id} still holds stock.", request.Id);
                return ApplicationErrors.WarehouseHasStock;
            }

            var hasEmployees = await _context.Employees.AnyAsync(e => e.WarehouseId == request.Id, cancellationToken);

            if (hasEmployees)
            {
                _logger.LogWarning("DeleteWarehouseCommandHandler stopped: warehouse {Id} still has employees.", request.Id);
                return ApplicationErrors.WarehouseHasEmployees;
            }

            _logger.LogInformation("DeleteWarehouseCommandHandler is marking entity data for persistence operation.");
            _context.Warehouses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteWarehouseCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Warehouse), cancellationToken);
            _logger.LogInformation("DeleteWarehouseCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Warehouse deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

