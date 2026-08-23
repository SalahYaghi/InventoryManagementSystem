using Contract.Features.Parties.Employees.Dtos;
using Contract.Features.Parties.People.Commands.CreatePerson;
using Domain.Warehouses;
using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.CreateEmployeeWithPerson
{
    public record CreateEmployeeWithPersonCommand(string jobTitle, CreatePersonCommand person,
            DateOnly hiringDate, Guid  warehouseId) : IRequest<Result<EmployeeDto>>; 
    
}

