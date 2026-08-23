using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Domain.Products.Domain.Products;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Product.Commands.CreateProductImage
{
    public class CreateProductCommandHandler(
        IAppDbContext context,
        ILogger<CreateProductCommandHandler> _logger,
        IFileStorage fileStorage,
        ICachingService cache) : IRequestHandler<CreateProductImageCommand, Result<Created>>
    {
        public async Task<Result<Created>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateProductCommandHandler));

            var entity = await context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("CreateProductCommandHandler stopped: product {Id} not found.", request.ProductId);
                return ApplicationErrors.ProductNotFound;
            }

            var result = await fileStorage.SaveFile(request.Image!, DefaultDirectory.DefaultProductDirectory, cancellationToken);

            if (result.IsError)
            {
                _logger.LogError("CreateProductCommandHandler stopped: {Errors}", result.Errors);
                return result.Errors;
            }

            var savedPath = result.Value;

            var image = ProductImage.Create(Guid.NewGuid(), request.ProductId, savedPath);

            if (image.IsError)
            {
                _logger.LogError("CreateProductCommandHandler stopped: {Errors}. Removing orphaned file {Path}.", image.Errors, savedPath);
                fileStorage.DeleteFile(savedPath);
                return image.Errors;
            }

            var addResult = entity.AddProductImage(image.Value);

            if (addResult.IsError)
            {
                _logger.LogError("CreateProductCommandHandler stopped: {Errors}. Removing orphaned file {Path}.", addResult.Errors, savedPath);
                fileStorage.DeleteFile(savedPath);
                return addResult.Errors;
            }

            await context.ProductImages.AddAsync(image.Value, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Product), cancellationToken);

            _logger.LogInformation("CreateProductCommandHandler completed successfully.");
            return Result.Created;
        }
    }
}
