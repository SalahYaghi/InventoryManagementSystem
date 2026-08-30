using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.ContactInfos.DTOs;

namespace Contract.Features.References.ContactInfos.Queries.GetContactInfo
{
    public sealed record GetContactInfoQuery(Guid Id) : ICachedQuery<Result<ContactInfoDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.References, CacheEntities.ContactInfo, nameof(GetContactInfoQuery), Id);
        public string[] Tags => [CacheEntities.ContactInfo];
    }
}

