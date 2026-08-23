using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Adjustment.DTOs;
using Contract.Features.Inventory.Adjustment.Mappers;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Adjustment.Queries.GetAdjustmentDetailPaged
{
    public sealed class GetAdjustmentDetailPagedQueryHandler : IRequestHandler<GetAdjustmentDetailPagedQuery, Result<List<AdjustmentDetailForListDto>>>
    {
        private readonly ILogger<GetAdjustmentDetailPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetAdjustmentDetailPagedQueryHandler(IAppDbContext context,
            ILogger<GetAdjustmentDetailPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<AdjustmentDetailForListDto>>> Handle(GetAdjustmentDetailPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetAdjustmentDetailPagedQueryHandler));

            var query = _context.AdjustmentDetails
                .Where(a => a.AdjustmentId == request.AdjustmentId)

                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Select(x => new AdjustmentDetailForListDto()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    ProductName = x.Product.ProductName,
                    RowVersion = x.RowVersion,
                    
                });

            var result = await query.ToListAsync(
                cancellationToken);

            _logger.LogInformation("GetAdjustmentDetailPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

