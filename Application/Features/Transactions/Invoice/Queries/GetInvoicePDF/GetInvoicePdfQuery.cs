using Contract.Common.Files;
using Inventory.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Transactions.Invoice.Queries.GetInvoicePDF
{
    public sealed record GetInvoicePdfQuery(Guid InvoiceId) : IRequest<Result<FileDto>>
    {
    }
}

