using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Parties.SupplierProducts.Commands.DeleteSupplierProduct
{
    public sealed class DeleteSupplierProductCommandHandler : IRequestHandler<DeleteSupplierProductCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteSupplierProductCommandHandler> _logger;

        public DeleteSupplierProductCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteSupplierProductCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteSupplierProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteSupplierProductCommandHandler));

            var entity = await _context.SupplierProducts.FirstOrDefaultAsync(
                x => x.ProductId == request.ProductId && 
            x.SupplierId == request.SupplierId, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteSupplierProductCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"SupplierProduct.NotFound\", \"SupplierProduct was not found.\")");
                return Error.NotFound("SupplierProduct.NotFound", "SupplierProduct was not found.");

            }

            _logger.LogInformation("DeleteSupplierProductCommandHandler is marking entity data for persistence operation.");
            _context.SupplierProducts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteSupplierProductCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.SupplierProduct), cancellationToken);
            _logger.LogInformation("DeleteSupplierProductCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("SupplierProduct deleted successfully with supplier {Key}", request.SupplierId);

            return Result.Deleted;
        }
    }
}

