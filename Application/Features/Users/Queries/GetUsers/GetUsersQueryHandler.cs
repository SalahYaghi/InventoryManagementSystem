using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.User.Dtos;
using Contract.Features.Users.Mappers;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IAppDbContext context,
        ILogger<GetUserByIdQueryHandler> logger) : IRequestHandler<GetUsersQuery, Result<List<UserForListDto>>>
    {
        private readonly ILogger<GetUserByIdQueryHandler> _logger = logger;

        public async Task<Result<List<UserForListDto>>> Handle(GetUsersQuery request, CancellationToken ct)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetUserByIdQueryHandler));


            return await context.Users.AsNoTracking().Select(u => new UserForListDto() { 
            
                        Email = u.Email,
                        Username = u.Username ,
                        EmployeeId = u.EmployeeId,
                        Id = u.Id,
                        IsActive = u.IsActive,
                        JobTitle = u.Employee!.JobTitle,
                        LastLoginAt = u.LastLoginAt,
                        PersonId = u.Employee.PersonId,
                        PersonName = u.Employee.Person!.FirstName + " " + u.Employee.Person.SecondName + " " +
                                     (u.Employee.Person.ThirdName == null || u.Employee.Person.ThirdName == "" ? "" : u.Employee.Person.ThirdName + " ") +
                                     u.Employee.Person.LastName,
                        Role = u.Role.ToString(),
                        WarehouseName = u.Employee.Warehouse!.Name
            
            }).ToListAsync(ct);
           
        }
    }
}

