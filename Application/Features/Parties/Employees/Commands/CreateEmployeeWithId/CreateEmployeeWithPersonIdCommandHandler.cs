using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Domain.Identity.Employee;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Contract.Common.Constants;
using Contract.Features.Parties.Employees.Dtos;
using Contract.Features.Parties.Employees.Mappers;

namespace Contract.Features.Parties.Employees.Commands.CreateEmployeeWithId
{
    public class CreateEmployeeWithPersonIdCommandHandler(IAppDbContext context,
        ILogger<CreateEmployeeWithPersonIdCommandHandler> logger , ICachingService cachingService) :
        IRequestHandler<CreateEmployeeWithPersonIdCommand, Result<EmployeeDto>>
    {
        private readonly ILogger<CreateEmployeeWithPersonIdCommandHandler> _logger = logger;

       async  Task<Result<EmployeeDto>>  IRequestHandler<CreateEmployeeWithPersonIdCommand, Result<EmployeeDto>>.Handle(CreateEmployeeWithPersonIdCommand request, CancellationToken cancellationToken)
        {
            var warehouseFound = await context.Warehouses
                          .AnyAsync(r => request.warehouseId == r.Id, cancellationToken);  
                          
            if (!warehouseFound)
                          
            {
                          
                _logger.LogWarning("CreateEmployeeWithPersonIdCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.WarehouseNotFound");
                return ApplicationErrors.WarehouseNotFound;
                          
            }
 
            var personExists = await context.People
                .AnyAsync(p => p.Id == request.personId, cancellationToken);  
            if (!personExists)

            {

                _logger.LogWarning("CreateEmployeeWithPersonIdCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.PersonNotFound");
                return ApplicationErrors.PersonNotFound;

            }

            var empResult = Employee.Create(request.jobTitle,
                request.personId, request.hiringDate, request.warehouseId);

            if (empResult.IsError)

            {

                _logger.LogError("CreateEmployeeWithPersonIdCommandHandler stopped because an error result was returned: {ErrorResult}.", "empResult.Errors");
                return empResult.Errors;

            }

            await context.Employees.AddAsync(empResult.Value, cancellationToken);  
            await context.SaveChangesAsync(cancellationToken);
            

            await cachingService.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Employee), cancellationToken);

            _logger.LogInformation("CreateEmployeeWithPersonIdCommandHandler completed successfully.");
            return empResult.Value.ToDto();
        }
    }
}

