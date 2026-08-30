using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Categories.DTOs;
using Contract.Features.Inventory.Categories.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Categories.Queries.GetCategoryPaged
{
    public sealed class GetCategoryPagedQueryHandler : IRequestHandler<GetCategoryPagedQuery, Result<List<CategoryDto>>>
    {
        private readonly ILogger<GetCategoryPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetCategoryPagedQueryHandler(IAppDbContext context,
            ILogger<GetCategoryPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetCategoryPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetCategoryPagedQueryHandler));

            var query = _context.Categories
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => x.ToDto());

            var result = await query.ToListAsync(
                cancellationToken);

            _logger.LogInformation("GetCategoryPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

