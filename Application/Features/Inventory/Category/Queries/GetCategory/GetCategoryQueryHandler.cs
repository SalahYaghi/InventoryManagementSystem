using Contract.Common.Interfaces;
using Contract.Features.Inventory.Categories.DTOs;
using Contract.Features.Inventory.Categories.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Categories.Queries.GetCategory
{
    public sealed class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Result<CategoryDto>>
    {
        private readonly ILogger<GetCategoryQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetCategoryQueryHandler(IAppDbContext context,
            ILogger<GetCategoryQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetCategoryQueryHandler));

            var entity = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetCategoryQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Category.NotFound\", \"Category was not found.\")");
                return Error.NotFound("Category.NotFound", "Category was not found.");

            }

            _logger.LogInformation("GetCategoryQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

