using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Parties.Supplier.DTOs;

namespace Contract.Features.Parties.Supplier.Queries.GetSupplierPaged
{
    public sealed record GetSupplierPagedQuery : ICachedQuery<Result<List<SupplierForListDto>>>
    {
   
        public string CacheKey => CacheKeys.ForEntityList(CacheGroups.Parties, CacheEntities.Supplier, nameof(GetSupplierPagedQuery));
        public string[] Tags => [CacheEntities.Supplier];
    }
}

