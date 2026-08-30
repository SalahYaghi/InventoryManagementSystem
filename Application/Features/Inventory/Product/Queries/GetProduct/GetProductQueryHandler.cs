using Contract.Common.Interfaces;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Inventory.Product.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Product.Queries.GetProduct
{
    public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductDto>>
    {
        private readonly ILogger<GetProductQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetProductQueryHandler(IAppDbContext context,
            ILogger<GetProductQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetProductQueryHandler));

            var entity = await _context.Products
                .Include(c => c.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetProductQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Product.NotFound\", \"Product was not found.\")");
                return Error.NotFound("Product.NotFound", "Product was not found.");

            }

            _logger.LogInformation("GetProductQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

