using Contract.Common.Interfaces;
using Contract.Common.Errors;
using Contract.Features.Transactions.Invoice.DTOs;
using Contract.Features.Transactions.Invoice.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Invoice.Queries.GetInvoice
{
    public sealed class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Result<InvoiceDto>>
    {
        private readonly ILogger<GetInvoiceQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetInvoiceQueryHandler(IAppDbContext context,
            ILogger<GetInvoiceQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<InvoiceDto>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetInvoiceQueryHandler));

            var entity = await _context.Invoices
                .Include(i => i.LineItems)
                .Include(i => i.Order)
                .ThenInclude(o => o.Customer)
                .Include(i => i.Order)
                .ThenInclude(o => o.Supplier)
                .Include(i => i.Order)
                .ThenInclude(o => o.DestinationWarehouse)
                .Include(i => i.Order)
                .ThenInclude(o => o.SourceWarehouse)
                .AsSplitQuery() // [FIX 4.5] avoid the cartesian explosion from the Include chain
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetInvoiceQueryHandler stopped: invoice {Id} not found.", request.Id);
                return ApplicationErrors.InvoiceNotFound;

            }

            var e =  entity.ToDto();

            _logger.LogInformation("GetInvoiceQueryHandler completed successfully.");
            return e;
        }
    }
}

