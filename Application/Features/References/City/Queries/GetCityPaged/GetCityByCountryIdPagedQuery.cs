using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Cities.DTOs;

namespace Contract.Features.References.Cities.Queries.GetCityPaged
{
    public sealed record GetCityByCountryIdPagedQuery(Guid CountryId) : ICachedQuery<Result<List<CityDto>>>
    {

        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.References, CacheEntities.City, nameof(GetCityByCountryIdPagedQuery) , CountryId);
        public string[] Tags => [CacheEntities.City];
    }
}

