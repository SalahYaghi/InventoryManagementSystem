using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Inventory.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteProductImageCommandHandler> _logger;
        private readonly IFileStorage _fileStorage; 
        public DeleteProductImageCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteProductImageCommandHandler> logger , 
            IFileStorage fileStorage)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _fileStorage = fileStorage;
        }

        public async Task<Result<Deleted>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteProductImageCommandHandler));

            var entity = await _context.ProductImages
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteProductImageCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Product.NotFound\", \"Product was not found.\")");
                return Error.NotFound("Product.NotFound", "Product was not found.");

            }

            var url = entity.ImageUrl; 

            _logger.LogInformation("DeleteProductImageCommandHandler is marking entity data for persistence operation.");
            _context.ProductImages.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteProductImageCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Product), cancellationToken);
            _logger.LogInformation("DeleteProductImageCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Product deleted successfully with key {Key}", request.Id);

            _fileStorage.DeleteFile(url);
            
            return Result.Deleted;
        }
    }
}

