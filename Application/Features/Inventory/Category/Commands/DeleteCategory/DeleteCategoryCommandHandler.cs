using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;
using Contract.Common.Errors;

namespace Contract.Features.Inventory.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteCategoryCommandHandler> _logger;

        public DeleteCategoryCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteCategoryCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteCategoryCommandHandler));

            var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteCategoryCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Category.NotFound\", \"Category was not found.\")");
                return Error.NotFound("Category.NotFound", "Category was not found.");

            }

            var hasProducts = await _context.Products.AnyAsync(p => p.CategoryId == request.Id, cancellationToken);

            if (hasProducts)
            {
                _logger.LogWarning("DeleteCategoryCommandHandler stopped: category {Id} still has products.", request.Id);
                return ApplicationErrors.CategoryHasProducts;
            }

            _logger.LogInformation("DeleteCategoryCommandHandler is marking entity data for persistence operation.");
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteCategoryCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Category), cancellationToken);
            _logger.LogInformation("DeleteCategoryCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Category deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

