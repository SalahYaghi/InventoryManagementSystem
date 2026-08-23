using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.References.Addresses.DTOs;

namespace Contract.Features.References.Addresses.Queries.GetAddressPaged
{
    public sealed record GetAddressPagedQuery : ICachedQuery<Result<PaginatedList<AddressDto>>>
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;

        public string CacheKey => CacheKeys.ForEntityPaged(CacheGroups.References, CacheEntities.Address, nameof(GetAddressPagedQuery), PageNumber, PageSize);
        public string[] Tags => [CacheEntities.Address];
    }
}

