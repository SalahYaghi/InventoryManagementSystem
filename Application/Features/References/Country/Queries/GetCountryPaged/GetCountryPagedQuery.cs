using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Countries.DTOs;

namespace Contract.Features.References.Countries.Queries.GetCountryPaged
{
    public sealed record GetCountryPagedQuery : ICachedQuery<Result<List<CountryDto>>>
    {

        public string CacheKey => CacheKeys.ForEntityList(CacheGroups.References, CacheEntities.Country, nameof(GetCountryPagedQuery));
        public string[] Tags => [CacheEntities.Country];
    }
}

