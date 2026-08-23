using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Domain.Products.Category;
using Contract.Features.Inventory.Categories.DTOs;
using Contract.Features.Inventory.Categories.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Inventory.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateCategoryCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateCategoryCommandHandler));

            var entityResult = Category.Create(Guid.NewGuid(), request.Name);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateCategoryCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }

            _context.Categories.Add(entityResult.Value);
            _logger.LogInformation("CreateCategoryCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateCategoryCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Category), cancellationToken);

            _logger.LogInformation("Category created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

