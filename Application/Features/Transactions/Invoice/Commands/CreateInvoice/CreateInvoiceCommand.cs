using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Transactions.Invoice.DTOs;

namespace Contract.Features.Transactions.Invoice.Commands.CreateInvoice
{
    public sealed record CreateInvoiceCommand : IRequest<Result<InvoiceDto>>
    {
        public Guid OrderId { get; init; } 
    }
}

