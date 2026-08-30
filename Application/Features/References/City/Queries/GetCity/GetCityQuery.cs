using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Cities.DTOs;

namespace Contract.Features.References.Cities.Queries.GetCity
{
    public sealed record GetCityQuery(Guid Id) : ICachedQuery<Result<CityDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.References, CacheEntities.City, nameof(GetCityQuery), Id);
        public string[] Tags => [CacheEntities.City];
    }
}

