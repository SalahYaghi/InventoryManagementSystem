using Contract.Features.Parties.Employees.Dtos;
using Domain.Warehouses;

using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.CreateEmployeeWithId
{
    public record CreateEmployeeWithPersonIdCommand(string jobTitle, Guid personId,
            DateOnly hiringDate, Guid  warehouseId) : IRequest<Result<EmployeeDto>>; 
    
}

