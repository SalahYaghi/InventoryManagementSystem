using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Transactions.Order.DTOs;
using Contract.Features.Transactions.Order.Mappers;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Order.Queries.GetOrderDetail
{
    public sealed class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQuery, Result<OrderDetailDto>>
    {
        private readonly ILogger<GetOrderDetailQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetOrderDetailQueryHandler(IAppDbContext context,
            ILogger<GetOrderDetailQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<OrderDetailDto>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetOrderDetailQueryHandler));

            var entity = await _context.OrderDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetOrderDetailQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"OrderDetail.NotFound\", \"OrderDetail was not found.\")");
                return Error.NotFound("OrderDetail.NotFound", "OrderDetail was not found.");

            }

            _logger.LogInformation("GetOrderDetailQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

