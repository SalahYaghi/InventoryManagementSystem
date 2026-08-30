using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Adjustments.DTOs;
using Contract.Features.Inventory.Adjustments.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.Adjustment.DTOs;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Adjustments.Queries.GetAdjustmentPaged
{
    public sealed class GetAdjustmentPagedQueryHandler : IRequestHandler<GetAdjustmentPagedQuery, Result<PaginatedList<AdjustmentForListDto>>>
    {
        private readonly ILogger<GetAdjustmentPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetAdjustmentPagedQueryHandler(IAppDbContext context,
            ILogger<GetAdjustmentPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PaginatedList<AdjustmentForListDto>>> Handle(GetAdjustmentPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetAdjustmentPagedQueryHandler));

            var query = _context.Adjustments
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Select(x => new AdjustmentForListDto() {                 
                        AdjustmentStatus = x.AdjustmentStatus.ToString(),
                        AprovedAt = x.AprovedAt,
                        AsjustmentType = x.AdjustmentType.ToString(),
                        AdjustmentReason = x.AdjustmentReason.ToString(),
                        Id = x.Id,
                        CreatedAt = x.CreatedAtUtc,
                        WarehouseId  = x.WarehouseId,
                        WarehouseName = x.Warehouse.Name
                });

            var result = await query.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation("GetAdjustmentPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

