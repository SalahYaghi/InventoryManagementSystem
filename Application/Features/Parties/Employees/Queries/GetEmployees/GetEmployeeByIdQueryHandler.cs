using Contract.Common.Errors;
using Contract.Common.Interfaces;
 using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Contract.Features.Parties.Employees.Dtos;

namespace Contract.Features.Parties.Employees.Queries.GetEmployees
{
    public class GetEmployeeQueryHandler(IAppDbContext context,
        ILogger<GetEmployeeQueryHandler> logger) : IRequestHandler<GetEmployeesQuery, Result<List<EmployeeDtoForList>>>
    {
        private readonly ILogger<GetEmployeeQueryHandler> _logger = logger;

        public async Task<Result<List<EmployeeDtoForList>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetEmployeeQueryHandler));


            var emp = await context.Employees
                .Where(e => !request.warehouseId.HasValue || e.WarehouseId == request.warehouseId)
                .AsNoTracking()
                .Select(e => new EmployeeDtoForList() {
                    EmployeeId = e.Id,
                    City = e.Person!.Address!.City!.Name,
                    Country = e.Person!.Address!.Country!.Name,
                    FullName = e.Person!.FirstName + " " + e.Person.SecondName + " " +
                               (e.Person.ThirdName == null || e.Person.ThirdName == "" ? "" : e.Person.ThirdName + " ") +
                               e.Person.LastName,
                    HiringDate = e.HiringDate,
                    JobTitle = e.JobTitle,
                    PersonId = e.PersonId,
                    WarehouseId = e.WarehouseId,
                    WarehouseName = e.Warehouse!.Name,
                    Email = e.Person.Contact!.Email ,
                    PhoneNumber = e.Person.Contact!.PhoneNumber,
                    NationalNo = e.Person.NationalNo ,
                })
                .ToListAsync(cancellationToken);


            _logger.LogInformation("GetEmployeeQueryHandler completed successfully.");
            return emp;
        }
    }
}

