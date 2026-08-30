using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.ContactInfos.DTOs;

namespace Contract.Features.References.ContactInfos.Queries.GetContactInfoPaged
{
    public sealed record GetContactInfoPagedQuery : ICachedQuery<Result<PaginatedList<ContactInfoDto>>>
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;

        public string CacheKey => CacheKeys.ForEntityPaged(CacheGroups.References, CacheEntities.ContactInfo, nameof(GetContactInfoPagedQuery), PageNumber, PageSize);
        public string[] Tags => [CacheEntities.ContactInfo];
    }
}

