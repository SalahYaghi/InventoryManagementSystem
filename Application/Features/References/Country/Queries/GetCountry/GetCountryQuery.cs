using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Countries.DTOs;

namespace Contract.Features.References.Countries.Queries.GetCountry
{
    public sealed record GetCountryQuery(Guid Id) : ICachedQuery<Result<CountryDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.References, CacheEntities.Country, nameof(GetCountryQuery), Id);
        public string[] Tags => [CacheEntities.Country];
    }
}

