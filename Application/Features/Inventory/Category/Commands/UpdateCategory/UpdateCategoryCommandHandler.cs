using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Categories.DTOs;
using Contract.Features.Inventory.Categories.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Inventory.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;

        public UpdateCategoryCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateCategoryCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateCategoryCommandHandler));

            var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateCategoryCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Category.NotFound\", \"Category was not found.\")");
                return Error.NotFound("Category.NotFound", "Category was not found.");

            }

            var result = entity.Update(request.Name);

            if (result.IsError)

            {

                _logger.LogError("UpdateCategoryCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            _logger.LogInformation("UpdateCategoryCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateCategoryCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Category), cancellationToken);

            _logger.LogInformation("Category updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

