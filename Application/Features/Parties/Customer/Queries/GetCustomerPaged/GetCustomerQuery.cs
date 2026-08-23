using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Parties.Customers.DTOs;

namespace Contract.Features.Parties.Customers.Queries.GetCustomerPaged
{
    public sealed record GetCustomerQuery( ) : ICachedQuery<Result<List<CustomerForListDto>>>
    {
        
        public string CacheKey => CacheKeys.ForEntityList(CacheGroups.Parties, CacheEntities.Customer, nameof(GetCustomerQuery));
        public string[] Tags => [CacheEntities.Customer];
    }
}

