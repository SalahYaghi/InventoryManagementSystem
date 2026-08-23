using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Transactions.Invoice.DTOs;

namespace Contract.Features.Transactions.Invoice.Queries.GetInvoice
{
    public sealed record GetInvoiceQuery(Guid Id) : ICachedQuery<Result<InvoiceDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Transactions, CacheEntities.Invoice, nameof(GetInvoiceQuery), Id);
        public string[] Tags => [CacheEntities.Invoice];
    }
}

