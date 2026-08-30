using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.ContactInfos.Commands.DeleteContactInfo
{
    public sealed class DeleteContactInfoCommandHandler : IRequestHandler<DeleteContactInfoCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteContactInfoCommandHandler> _logger;

        public DeleteContactInfoCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteContactInfoCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteContactInfoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteContactInfoCommandHandler));

            var entity = await _context.ContactInfos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteContactInfoCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"ContactInfo.NotFound\", \"ContactInfo was not found.\")");
                return Error.NotFound("ContactInfo.NotFound", "ContactInfo was not found.");

            }

            _logger.LogInformation("DeleteContactInfoCommandHandler is marking entity data for persistence operation.");
            _context.ContactInfos.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteContactInfoCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.ContactInfo), cancellationToken);
            _logger.LogInformation("DeleteContactInfoCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("ContactInfo deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

