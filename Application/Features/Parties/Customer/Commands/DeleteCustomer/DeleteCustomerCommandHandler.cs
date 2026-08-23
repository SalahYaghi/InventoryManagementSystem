using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Contract.Common.Errors;

namespace Contract.Features.Parties.Customers.Commands.DeleteCustomer
{
    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;

        public DeleteCustomerCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteCustomerCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteCustomerCommandHandler));

            var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteCustomerCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Customer.NotFound\", \"Customer was not found.\")");
                return Error.NotFound("Customer.NotFound", "Customer was not found.");

            }

            var hasOrders = await _context.Orders.AnyAsync(o => o.CustomerId == request.Id, cancellationToken);

            if (hasOrders)
            {
                _logger.LogWarning("DeleteCustomerCommandHandler stopped: customer {Id} still has orders.", request.Id);
                return ApplicationErrors.CustomerHasOrders;
            }

            _logger.LogInformation("DeleteCustomerCommandHandler is marking entity data for persistence operation.");
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteCustomerCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Customer), cancellationToken);
            _logger.LogInformation("DeleteCustomerCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Customer deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

