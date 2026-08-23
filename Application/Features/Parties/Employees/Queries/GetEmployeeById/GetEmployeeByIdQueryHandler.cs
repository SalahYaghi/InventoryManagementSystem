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
using Contract.Features.Parties.Employees.Mappers;

namespace Contract.Features.Parties.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler(IAppDbContext context,
        ILogger<GetEmployeeByIdQueryHandler> logger) : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>
    {
        private readonly ILogger<GetEmployeeByIdQueryHandler> _logger = logger;

        public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetEmployeeByIdQueryHandler));


            var emp = await context.Employees
                .Include(e => e.Warehouse)
                .Include(e => e.Person)
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (emp == null)

            {

                _logger.LogWarning("GetEmployeeByIdQueryHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.EmployeeNotFound");
                return ApplicationErrors.EmployeeNotFound;

            }


            _logger.LogInformation("GetEmployeeByIdQueryHandler completed successfully.");
            return emp.ToDto();
        }
    }
}

