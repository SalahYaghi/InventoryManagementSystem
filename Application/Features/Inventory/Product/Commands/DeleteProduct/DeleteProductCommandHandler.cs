using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Contract.Common.Errors;

namespace Contract.Features.Inventory.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IFileStorage _fileStorage; 
        public DeleteProductCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteProductCommandHandler> logger , 
            IFileStorage fileStorage)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _fileStorage = fileStorage;
        }

        public async Task<Result<Deleted>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteProductCommandHandler));

            var entity = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteProductCommandHandler stopped: product {Id} not found.", request.Id);
                return ApplicationErrors.ProductNotFound;

            }

            var urls = entity.ProductImages.Select(x => x.ImageUrl).ToList(); 

            _logger.LogInformation("DeleteProductCommandHandler is marking entity data for persistence operation.");
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteProductCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Product), cancellationToken);
            _logger.LogInformation("DeleteProductCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Product deleted successfully with key {Key}", request.Id);

            foreach (var url in urls) {
                _fileStorage.DeleteFile(url);
            }

            _logger.LogInformation("DeleteProductCommandHandler completed successfully.");
            return Result.Deleted;
        }
    }
}

