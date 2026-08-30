using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.Addresses.Commands.DeleteAddress
{
    public sealed class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteAddressCommandHandler> _logger;

        public DeleteAddressCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteAddressCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteAddressCommandHandler));

            var entity = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteAddressCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Address.NotFound\", \"Address was not found.\")");
                return Error.NotFound("Address.NotFound", "Address was not found.");

            }

            _logger.LogInformation("DeleteAddressCommandHandler is marking entity data for persistence operation.");
            _context.Addresses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteAddressCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Address), cancellationToken);
            _logger.LogInformation("DeleteAddressCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Address deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

