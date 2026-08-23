using Contract.Common.Interfaces;
using Contract.Features.Parties.SupplierProducts.DTOs;
using Contract.Features.Parties.SupplierProducts.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.SupplierProducts.Queries.GetSupplierProduct
{
    public sealed class GetSupplierProductQueryHandler : IRequestHandler<GetSupplierProductQuery, Result<SupplierProductDto>>
    {
        private readonly ILogger<GetSupplierProductQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetSupplierProductQueryHandler(IAppDbContext context,
            ILogger<GetSupplierProductQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<SupplierProductDto>> Handle(GetSupplierProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetSupplierProductQueryHandler));

            var entity = await _context.SupplierProducts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == request.ProductId &&
            x.SupplierId == request.SupplierId, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetSupplierProductQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"SupplierProduct.NotFound\", \"SupplierProduct was not found.\")");
                return Error.NotFound("SupplierProduct.NotFound", "SupplierProduct was not found.");

            }

            _logger.LogInformation("GetSupplierProductQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

