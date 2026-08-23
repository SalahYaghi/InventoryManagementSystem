using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Parties.Supplier.DTOs;

namespace Contract.Features.Parties.Supplier.Queries.GetSupplier
{
    public sealed record GetSupplierQuery(Guid Id) : ICachedQuery<Result<SupplierDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Parties, CacheEntities.Supplier, nameof(GetSupplierQuery), Id);
        public string[] Tags => [CacheEntities.Supplier];
    }
}

