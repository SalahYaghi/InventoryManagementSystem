using Contract.Common.Interfaces;
using Contract.Features.Inventory.Adjustments.DTOs;
using Contract.Features.Inventory.Adjustments.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;
using Contract.Common.Errors;

namespace Contract.Features.Inventory.Adjustments.Queries.GetAdjustment
{
    public sealed class GetAdjustmentQueryHandler : IRequestHandler<GetAdjustmentQuery, Result<AdjustmentDto>>
    {
        private readonly ILogger<GetAdjustmentQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetAdjustmentQueryHandler(IAppDbContext context,
            ILogger<GetAdjustmentQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<AdjustmentDto>> Handle(GetAdjustmentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetAdjustmentQueryHandler));

            var entity = await _context.Adjustments
                .Include(a => a.AdjustmentDetails)
                .ThenInclude(a => a.Product)
                .Include(a => a.Warehouse)
                
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetAdjustmentQueryHandler stopped: adjustment {Id} not found.", request.Id);
                return ApplicationErrors.AdjustmentNotFound;

            }

            _logger.LogInformation("GetAdjustmentQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

