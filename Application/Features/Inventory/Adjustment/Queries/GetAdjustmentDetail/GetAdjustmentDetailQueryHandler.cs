using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Adjustment.DTOs;
using Contract.Features.Inventory.Adjustment.Mappers;
using Microsoft.Extensions.Logging;
using Contract.Common.Errors;

namespace Contract.Features.Inventory.Adjustment.Queries.GetAdjustmentDetail
{
    public sealed class GetAdjustmentDetailQueryHandler : IRequestHandler<GetAdjustmentDetailQuery, Result<AdjustmentDetailDto>>
    {
        private readonly ILogger<GetAdjustmentDetailQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetAdjustmentDetailQueryHandler(IAppDbContext context,
            ILogger<GetAdjustmentDetailQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<AdjustmentDetailDto>> Handle(GetAdjustmentDetailQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetAdjustmentDetailQueryHandler));

            var entity = await _context.AdjustmentDetails
                .Include(x => x.Product)
                    .ThenInclude(p => p!.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetAdjustmentDetailQueryHandler stopped: adjustment detail {Id} not found.", request.Id);
                return ApplicationErrors.AdjustmentDetailNotFound;

            }

            _logger.LogInformation("GetAdjustmentDetailQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

