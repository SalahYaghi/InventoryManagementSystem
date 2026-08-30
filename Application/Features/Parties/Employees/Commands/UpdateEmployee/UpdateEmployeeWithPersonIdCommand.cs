using Contract.Features.Parties.Employees.Dtos;
using Domain.Warehouses;
using Inventory.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(Guid employeeId,string jobTitle,
            DateOnly hiringDate, Guid  warehouseId) : IRequest<Result<EmployeeDto>>; 
    
}

