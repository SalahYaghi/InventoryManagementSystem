using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Transactions.Order.DTOs;
using Contract.Features.Transactions.Order.Mappers;
using Domain.Orders;
using Contract.Common.Errors;

namespace Contract.Features.Transactions.Order.Commands.UpdateOrderDetail
{
    public sealed class UpdateOrderDetailQuantityCommandHandler : IRequestHandler<UpdateOrderDetailCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateOrderDetailQuantityCommandHandler> _logger;
        private readonly IOrderPolicies _orderPolicies;

        public UpdateOrderDetailQuantityCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateOrderDetailQuantityCommandHandler> logger,
            IOrderPolicies orderPolicies)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _orderPolicies = orderPolicies;

        }

        public async Task<Result<Updated>> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateOrderDetailQuantityCommandHandler));

            var entity = await _context.OrderDetails
                .Include(o => o.Order)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("UpdateOrderDetailQuantityCommandHandler stopped: order detail {Id} not found.", request.Id);
                return ApplicationErrors.OrderDetailNotFound;
            }

            if (entity.RowVersion is null || !entity.RowVersion.SequenceEqual(request.RowVersion))
            {
                _logger.LogWarning("UpdateOrderDetailQuantityCommandHandler stopped: stale row version for order detail {Id}.", request.Id);
                return ApplicationErrors.UpdateOccursOnProducts;
            }
            
            if (entity.Order!.IsLocked)
            
            {
            
                _logger.LogError("UpdateOrderDetailQuantityCommandHandler stopped because an error result was returned: {ErrorResult}.", "OrderErrors.OrderIsLocked");
                return OrderErrors.OrderIsLocked;
            
            }

            var netQuantity = request.Quantity - entity.Quantity;

            if (netQuantity == 0)
            {
                _logger.LogInformation("UpdateOrderDetailQuantityCommandHandler: quantity unchanged, nothing to do.");
                return Result.Updated;
            }

            if (netQuantity > 0 &&
                (entity.Order!.OrderType == OrderType.Sale ||
                 entity.Order.OrderType == OrderType.Transfer ||
                 entity.Order.OrderType == OrderType.ReturnOut))
            {
                var checkResult = await _orderPolicies.CheckPrductAvailableQuantity(
                    entity.Order.SourceWarehouseId, entity.ProductId, netQuantity, cancellationToken);

                if (checkResult.IsError)
                {
                    _logger.LogError("UpdateOrderDetailQuantityCommandHandler stopped: {Errors}", checkResult.Errors);
                    return checkResult.Errors;
                }
            }

            var result = entity.UpdateQuantity(request.Quantity);

            if (result.IsError)
            {
                _logger.LogError("UpdateOrderDetailQuantityCommandHandler stopped: {Errors}", result.Errors);
                return result.Errors;
            }


            _logger.LogInformation("UpdateOrderDetailQuantityCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateOrderDetailQuantityCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.OrderDetail), cancellationToken);

            _logger.LogInformation("OrderDetail updated successfully with key {Key}", request.Id);

            return Result.Updated;
        }
    }
}

