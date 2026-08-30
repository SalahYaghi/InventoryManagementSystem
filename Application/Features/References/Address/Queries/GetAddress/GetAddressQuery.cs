using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Addresses.DTOs;

namespace Contract.Features.References.Addresses.Queries.GetAddress
{
    public sealed record GetAddressQuery(Guid Id) : ICachedQuery<Result<AddressDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.References, CacheEntities.Address, nameof(GetAddressQuery), Id);
        public string[] Tags => [CacheEntities.Address];
    }
}

