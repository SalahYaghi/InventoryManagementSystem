using Contract.Common.Errors;
using Contract.Common.Interfaces;
 using Contract.Features.User.Dtos;
using Contract.Features.Users.Mappers;
using Domain.Identity.Users;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Users.Queries.GetUserById
{
    public class GetUsersByUsernameQueryHandler(IAppDbContext context,
        ILogger<GetUsersByUsernameQueryHandler> logger) : IRequestHandler<GetUserByEmailQuery, Result<UserDto>>
    {
        private readonly ILogger<GetUsersByUsernameQueryHandler> _logger = logger;

        public async Task<Result<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken ct)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetUsersByUsernameQueryHandler));


            var user = await context.Users
               .Include(u => u.Employee)
                     .ThenInclude(e => e!.Person)
                 .Include(u => u.Employee)
                     .ThenInclude(e => e!.Warehouse)
                         .ThenInclude(p => p!.Address)
                            .ThenInclude(p => p!.City)
                 .Include(u => u.Employee)
                     .ThenInclude(e => e!.Warehouse)
                         .ThenInclude(p => p!.Address)
                            .ThenInclude(p => p!.Country)
                .Include(u => u.Employee)
                     .ThenInclude(e => e!.Person)
                         .ThenInclude(p => p!.Contact)
                 .FirstOrDefaultAsync(u => u.Email == request.email, ct);

            if (user == null)

            {

                _logger.LogWarning("GetUsersByUsernameQueryHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.UserNotFound");
                return ApplicationErrors.UserNotFound;

            }


            var dto =  user.ToDto();

            _logger.LogInformation("GetUsersByUsernameQueryHandler completed successfully.");
            return dto;
        }
    }
}

