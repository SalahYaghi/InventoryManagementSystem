using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Contract.Common.Errors;

namespace Contract.Features.Parties.Supplier.Commands.DeleteSupplier
{
    public sealed class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteSupplierCommandHandler> _logger;

        public DeleteSupplierCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteSupplierCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteSupplierCommandHandler));

            var entity = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteSupplierCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Supplier.NotFound\", \"Supplier was not found.\")");
                return Error.NotFound("Supplier.NotFound", "Supplier was not found.");

            }

            var hasOrders = await _context.Orders.AnyAsync(o => o.SupplierId == request.Id, cancellationToken);

            if (hasOrders)
            {
                _logger.LogWarning("DeleteSupplierCommandHandler stopped: supplier {Id} still has orders.", request.Id);
                return ApplicationErrors.SupplierHasOrders;
            }

            _logger.LogInformation("DeleteSupplierCommandHandler is marking entity data for persistence operation.");
            _context.Suppliers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteSupplierCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Supplier), cancellationToken);
            _logger.LogInformation("DeleteSupplierCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Supplier deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

