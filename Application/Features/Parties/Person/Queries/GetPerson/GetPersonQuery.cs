using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.People.DTOs;
using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.Parties.People.Queries.GetPerson
{
    public sealed record GetPersonQuery(Guid Id) : ICachedQuery<Result<PersonDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Parties, CacheEntities.Person, nameof(GetPersonQuery), Id);
        public string[] Tags => [CacheEntities.Person];
    }
}

