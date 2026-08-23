using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Domain.Suppliers.SupplierProducts;
using Contract.Features.Parties.SupplierProducts.DTOs;
using Contract.Features.Parties.SupplierProducts.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Domain.Suppliers;
using Contract.Features.References.Document;
using Contract.Common.Errors;
using Microsoft.EntityFrameworkCore;

namespace Contract.Features.Parties.SupplierProducts.Commands.CreateSupplierProduct
{
    public sealed class CreateSupplierProductCommandHandler : IRequestHandler<CreateSupplierProductCommand, Result<SupplierProductDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateSupplierProductCommandHandler> _logger;

        public CreateSupplierProductCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateSupplierProductCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<SupplierProductDto>> Handle(CreateSupplierProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateSupplierProductCommandHandler));

            var supplier = await _context.Suppliers.FindAsync(new object[] 
            { request.SupplierId }, cancellationToken);

            if (supplier is null) {
                _logger.LogWarning("CreateSupplierProductCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.SupplierNotFound");
                return ApplicationErrors.SupplierNotFound;
            }
            if (!supplier.Status)
            {
                _logger.LogWarning("CreateSupplierProductCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.SupplierInActive");
                return ApplicationErrors.SupplierInActive;
            }
            var existingEntity = await _context.SupplierProducts.AnyAsync( sp => 
            sp.SupplierId ==  request.SupplierId && sp.ProductId ==  request.ProductId , cancellationToken);  

            if (existingEntity)

            {

                _logger.LogWarning("CreateSupplierProductCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.SupplierProductAlreadyExists");
                return ApplicationErrors.SupplierProductAlreadyExists;

            }
               

            var entityResult = Domain.Suppliers.SupplierProducts.SupplierProduct.Create(Guid.NewGuid(), request.SupplierId, request.ProductId, request.PurchasePrice);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateSupplierProductCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }

            _context.SupplierProducts.Add(entityResult.Value);
            _logger.LogInformation("CreateSupplierProductCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateSupplierProductCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(
                CacheFanout.Expand(CacheEntities.SupplierProduct, CacheEntities.Product), cancellationToken);

            _logger.LogInformation("SupplierProduct created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

