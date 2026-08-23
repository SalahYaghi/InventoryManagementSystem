using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Parties.Customers.DTOs;

namespace Contract.Features.Parties.Customers.Queries.GetCustomer
{
    public sealed record GetCustomerQuery(Guid Id) : ICachedQuery<Result<CustomerDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Parties, CacheEntities.Customer, nameof(GetCustomerQuery), Id);
        public string[] Tags => [CacheEntities.Customer];
    }
}

