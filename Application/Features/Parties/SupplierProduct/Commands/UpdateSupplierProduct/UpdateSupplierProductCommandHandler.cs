using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.SupplierProducts.DTOs;
using Contract.Features.Parties.SupplierProducts.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Parties.SupplierProducts.Commands.UpdateSupplierProduct
{
    public sealed class UpdateSupplierProductCommandHandler : IRequestHandler<UpdateSupplierProductCommand, Result<SupplierProductDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateSupplierProductCommandHandler> _logger;

        public UpdateSupplierProductCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateSupplierProductCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<SupplierProductDto>> Handle(UpdateSupplierProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateSupplierProductCommandHandler));

            var entity = await _context.SupplierProducts.FirstOrDefaultAsync(x => x.ProductId == request.ProductId &&
            x.SupplierId == request.SupplierId, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateSupplierProductCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"SupplierProduct.NotFound\", \"SupplierProduct was not found.\")");
                return Error.NotFound("SupplierProduct.NotFound", "SupplierProduct was not found.");

            }

            var result = entity.Update(request.PurchasePrice, request.IsActive);

            if (result.IsError)
            {
                _logger.LogWarning("Failed to update SupplierProduct with key supplier {Key}. Errors: {Errors}", request.SupplierId, result.Errors);
                return result.Errors;
            }
            _logger.LogInformation("UpdateSupplierProductCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateSupplierProductCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(
                CacheFanout.Expand(CacheEntities.SupplierProduct, CacheEntities.Product), cancellationToken);

            _logger.LogInformation("SupplierProduct updated successfully with key supplier {Key}", request.SupplierId);

            return entity.ToDto();
        }
    }
}

