using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.Employees.Dtos;
using MechanicShop.Domain.Common.Results;
using MechanicShop.Domain.Common.Results.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Queries.GetEmployees
{
    public record GetEmployeesQuery(Guid? warehouseId = null) : ICachedQuery<Result<List<EmployeeDtoForList>>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Parties, CacheEntities.Employee, nameof(GetEmployeesQuery), warehouseId.HasValue ? warehouseId.Value
            : Guid.Empty); 
        public string[] Tags => [CacheEntities.Employee];

    }
}

