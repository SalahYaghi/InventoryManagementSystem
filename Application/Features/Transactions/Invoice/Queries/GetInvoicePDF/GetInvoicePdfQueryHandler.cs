using Contract.Common.Files;
using Contract.Common.Interfaces;
using Contract.Common.Errors;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Invoice.Queries.GetInvoicePDF
{
    public class GetInvoicePdfQueryHandler(IAppDbContext context ,
        IInvoicePdfGenerator generator,
        ILogger<GetInvoicePdfQueryHandler> logger) : IRequestHandler<GetInvoicePdfQuery, Result<FileDto>>
    {
        private readonly ILogger<GetInvoicePdfQueryHandler> _logger = logger;

        public async Task<Result<FileDto>> Handle(GetInvoicePdfQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetInvoicePdfQueryHandler));

            var entity = await context.Invoices
              .Include(i => i.LineItems)
              .Include(i => i.Order)
                .ThenInclude(o => o.Customer)
                    .ThenInclude(c => c!.Address)
                        .ThenInclude(a => a!.Country)
              .Include(i => i.Order)
                .ThenInclude(o => o.Customer)
                    .ThenInclude(c => c!.Address)
                        .ThenInclude(a => a!.City)
              .Include(i => i.Order)
                .ThenInclude(o => o.Customer)
                    .ThenInclude(o => o!.Contact)
              .Include(i => i.Order)
                .ThenInclude(o => o.Supplier)
                    .ThenInclude(s => s!.Address)
                        .ThenInclude(s => s!.Country)
              .Include(i => i.Order)
                .ThenInclude(o => o.Supplier)
                    .ThenInclude(s => s!.Address)
                       .ThenInclude(s => s!.City)
              .Include(i => i.Order)
                .ThenInclude(o => o.Supplier)
                    .ThenInclude(o => o!.Contact)
            
             .Include(i => i.Order)
                .ThenInclude(o => o.DestinationWarehouse)
                    .ThenInclude(o => o!.Address)
                        .ThenInclude(o => o!.City)

             .Include(i => i.Order)
                .ThenInclude(o => o.DestinationWarehouse)
                    .ThenInclude(o => o!.Address)
                        .ThenInclude(o => o!.Country)

             .Include(i => i.Order)
                .ThenInclude(o => o.SourceWarehouse)
                    .ThenInclude(o => o!.Address)
                        .ThenInclude(o => o!.City)

             .Include(i => i.Order)
                .ThenInclude(o => o.SourceWarehouse)
                    .ThenInclude(o => o!.Address)
                        .ThenInclude(o => o!.Country)

                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.InvoiceId, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetInvoicePdfQueryHandler stopped: invoice {Id} not found.", request.InvoiceId);
                return ApplicationErrors.InvoiceNotFound;

            }

            var bytes = generator.Generate(entity);

            return new FileDto() {
                ContentType = "application/pdf",
                Data = bytes,
                FileName = $"order_{DateTimeOffset.UtcNow.ToString("yyyy-MM-dd_hh-mm-ss")}_invoice.pdf"
            }; 
        }
    }
}

