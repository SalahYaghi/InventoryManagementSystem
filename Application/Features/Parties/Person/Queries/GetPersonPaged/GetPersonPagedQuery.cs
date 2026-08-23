using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Parties.People.DTOs;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Parties.Person.DTOs;

namespace Contract.Features.Parties.People.Queries.GetPersonPaged
{
    public sealed record GetPersonPagedQuery : ICachedQuery<Result<PaginatedList<PersonForListDto>>>
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;

        public string CacheKey => CacheKeys.ForEntityPaged(CacheGroups.Parties, CacheEntities.Person, nameof(GetPersonPagedQuery), PageNumber, PageSize);
        public string[] Tags => [CacheEntities.Person];
    }
}

