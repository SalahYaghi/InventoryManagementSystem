using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Contract.Features.Inventory.WarehouseStocks.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;
using Contract.Common.Errors;

namespace Contract.Features.Inventory.WarehouseStocks.Commands.UpdateWarehouseStock
{
    public sealed class UpdateWarehouseStockMinimumLevelCommandHandler : IRequestHandler<UpdateWarehouseStockMinimumLevelCommand, Result<WarehouseStockDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateWarehouseStockMinimumLevelCommandHandler> _logger;

        public UpdateWarehouseStockMinimumLevelCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateWarehouseStockMinimumLevelCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<WarehouseStockDto>> Handle(UpdateWarehouseStockMinimumLevelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateWarehouseStockMinimumLevelCommandHandler));

            var entity = await _context.WarehouseStocks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateWarehouseStockMinimumLevelCommandHandler stopped: stock {Id} not found.", request.Id);
                return ApplicationErrors.WarehouseStockNotFound;

            }

            var result = entity.UpdateMinimumLevel(request.MinimumStockLevel);

            if (result.IsError)

            {

                _logger.LogError("UpdateWarehouseStockMinimumLevelCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            _logger.LogInformation("UpdateWarehouseStockMinimumLevelCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateWarehouseStockMinimumLevelCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.WarehouseStock), cancellationToken);

            _logger.LogInformation("WarehouseStock updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

