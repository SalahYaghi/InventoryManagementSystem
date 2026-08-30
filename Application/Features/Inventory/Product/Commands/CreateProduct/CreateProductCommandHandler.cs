using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Domain.Products;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Inventory.Product.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using Contract.Common.Errors;
namespace Contract.Features.Inventory.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateProductCommandHandler> _logger;
 
        public CreateProductCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateProductCommandHandler> logger  
         
            )
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateProductCommandHandler));

            var skuExist = await _context.Products.AnyAsync(p => p.SKU == request.SKU, cancellationToken);

            if (skuExist) { 
                _logger.LogWarning("CreateProductCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.SKUAlreadyExits");
                return ApplicationErrors.SKUAlreadyExits;
            }

            var entityResult = Domain.Products.Product.Create(Guid.NewGuid(), request.SKU, request.BarCode, request.ProductName, request.Description, request.CategoryId, request.SellingPrice, request.IsActive, request.Unit);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateProductCommandHandler stopped because an error result was returned: {ErrorResult}.", entityResult.Errors);
                return entityResult.Errors;

            }

            _context.Products.Add(entityResult.Value);
            _logger.LogInformation("CreateProductCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateProductCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Product), cancellationToken);
            _logger.LogInformation("Product created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

