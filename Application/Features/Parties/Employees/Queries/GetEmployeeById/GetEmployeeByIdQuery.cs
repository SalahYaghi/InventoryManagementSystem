using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.Employees.Dtos;
using Contract.Features.Parties.Employees.Queries.GetEmployees;
using Domain.Warehouses;
using Inventory.Domain.Common.Results;
using Inventory.Domain.Common.Results.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(Guid Id) : ICachedQuery<Result<EmployeeDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Parties, CacheEntities.Employee, nameof(GetEmployeesQuery), Id);
        public string[] Tags => [CacheEntities.Employee];
    }
}

