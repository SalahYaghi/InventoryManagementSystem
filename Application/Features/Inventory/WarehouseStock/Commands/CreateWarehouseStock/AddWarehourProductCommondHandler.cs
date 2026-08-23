using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.Inventory.Warehouses.Mappers;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Contract.Features.Inventory.WarehouseStocks.Mappers;
using Domain.Warehouses;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.WarehouseStock.Commands.AddWarehouseProducts
{
    public class AddWarehourProductCommondHandler
        : IRequestHandler<AddWarehourProductCommand, Result<WarehouseStockDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<AddWarehourProductCommondHandler> _logger;
        private readonly ICachingService _cache;

        public AddWarehourProductCommondHandler(
            IAppDbContext context,
            ILogger<AddWarehourProductCommondHandler> logger,
            ICachingService cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Result<WarehouseStockDto>> Handle(
            AddWarehourProductCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(AddWarehourProductCommondHandler));

            _logger.LogInformation(
                "Adding product {} to warehouse {WarehouseId}",
                request.Product.ProductName,
                request.WarehousesId);

            var warehouseFound = await _context.Warehouses
                .AnyAsync(x => x.Id == request.WarehousesId, cancellationToken);

            if (!warehouseFound)
            {
                _logger.LogWarning(
                    "Warehouse not found. WarehouseId: {WarehouseId}",
                    request.WarehousesId);

                return Error.NotFound("Warehouse.NotFound", "Warehouse was not found.");
            }


             var skuExist = await _context.Products.AnyAsync(p => p.SKU == request.Product.SKU, cancellationToken);

            if (skuExist)
            {
                _logger.LogWarning("AddWarehourProductCommondHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.SKUAlreadyExits");
                return ApplicationErrors.SKUAlreadyExits;
            }

            var entityResult = Domain.Products.Product.Create(Guid.NewGuid(), request.Product.SKU, request.Product.BarCode, request.Product.ProductName, request.Product.Description, request.Product.CategoryId, request.Product.SellingPrice, request.Product.IsActive, request.Product.Unit);

            if (entityResult.IsError)

            {

                _logger.LogError("AddWarehourProductCommondHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }


         
            var stockResult = Domain.Warehouses.WarehouseStock.Create(
                Guid.NewGuid(),
                request.WarehousesId,
                entityResult.Value.Id , 0.0m);

            if (stockResult.IsError)
            {
                _logger.LogWarning(
                    "Failed to create WarehouseStock. Errors: {Errors}",
                    stockResult.Errors);

                return stockResult.Errors;
            }

            _logger.LogInformation("AddWarehourProductCommondHandler is adding new entity data to the context.");
            await _context.Products.AddAsync(entityResult.Value ,cancellationToken);
            await _context.WarehouseStocks.AddAsync(stockResult.Value , cancellationToken);
            _logger.LogInformation("AddWarehourProductCommondHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("AddWarehourProductCommondHandler saved changes to the database successfully.");

            _logger.LogInformation(
                "Successfully added product {ProductId} to warehouse {WarehouseId}",
                entityResult.Value.Id,
                request.WarehousesId);

            _logger.LogInformation("AddWarehourProductCommondHandler is invalidating related cache entries.");
            
            await _cache.RemoveByTagAsync(
                CacheFanout.Expand(CacheEntities.WarehouseStock, CacheEntities.Product), cancellationToken);
            _logger.LogInformation("AddWarehourProductCommondHandler invalidated related cache entries successfully.");

            _logger.LogInformation("AddWarehourProductCommondHandler completed successfully.");
            return stockResult.Value.ToDto();
        }
    }
}
