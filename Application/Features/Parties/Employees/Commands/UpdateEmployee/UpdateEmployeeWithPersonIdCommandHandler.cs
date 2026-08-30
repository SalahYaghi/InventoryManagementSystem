using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Parties.Employees.Dtos;
using Contract.Features.Parties.Employees.Mappers;
using Domain.Identity.Employee;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler(IAppDbContext context,
        ILogger<UpdateEmployeeCommandHandler> logger, ICachingService cachingService) :
        IRequestHandler<UpdateEmployeeCommand, Result<EmployeeDto>>
    {
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger = logger;

       async  Task<Result<EmployeeDto>>  IRequestHandler<UpdateEmployeeCommand, Result<EmployeeDto>>.Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var emp = await context.Employees.FirstOrDefaultAsync(e => e.Id == request.employeeId, cancellationToken); 

            if (emp == null)

            {

                _logger.LogWarning("UpdateEmployeeCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.EmployeeNotFound");
                return ApplicationErrors.EmployeeNotFound;

            }

            var warehouseFound = await context.Warehouses
                          .AnyAsync(r => request.warehouseId == r.Id, cancellationToken);  
                          
            if (!warehouseFound)
                          
            {
                          
                _logger.LogWarning("UpdateEmployeeCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.WarehouseNotFound");
                return ApplicationErrors.WarehouseNotFound;
                          
            }
 
           
            var empResult = emp.Update(request.jobTitle
          , request.hiringDate, request.warehouseId);

            if (empResult.IsError)

            {

                _logger.LogError("UpdateEmployeeCommandHandler stopped because an error result was returned: {ErrorResult}.", "empResult.Errors");
                return empResult.Errors;

            }        

            await context.SaveChangesAsync(cancellationToken);

            await cachingService.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Employee), cancellationToken);

            _logger.LogInformation("UpdateEmployeeCommandHandler completed successfully.");
            return emp.ToDto();
        }
    }
}

