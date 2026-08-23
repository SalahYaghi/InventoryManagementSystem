using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Inventory.Product.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Contract.Common.Errors;

namespace Contract.Features.Inventory.Product.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateProductCommandHandler));

            var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateProductCommandHandler stopped: product {Id} not found.", request.Id);
                return ApplicationErrors.ProductNotFound;

            }

            var skuTaken = await _context.Products
                .AnyAsync(p => p.SKU == request.SKU && p.Id != request.Id, cancellationToken);

            if (skuTaken)
            {
                _logger.LogWarning("UpdateProductCommandHandler stopped: SKU {SKU} already exists.", request.SKU);
                return ApplicationErrors.SKUAlreadyExits;
            }

            var updateResult = entity.Update(
                request.SKU, request.BarCode, request.ProductName, request.Description,
                request.CategoryId, request.SellingPrice, request.IsActive, request.Unit);

            if (updateResult.IsError)
            {
                _logger.LogError("UpdateProductCommandHandler stopped: {Errors}", updateResult.Errors);
                return updateResult.Errors;
            }

            _logger.LogInformation("UpdateProductCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateProductCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Product), cancellationToken);

            _logger.LogInformation("Product updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

