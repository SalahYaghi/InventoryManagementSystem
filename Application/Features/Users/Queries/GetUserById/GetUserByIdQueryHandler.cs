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
    public class GetUsersQueryHandler(IAppDbContext context,
        ILogger<GetUsersQueryHandler> logger) : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly ILogger<GetUsersQueryHandler> _logger = logger;

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetUsersQueryHandler));

           

            var user = await context.Users
                 .Include(u => u.Employee)
                     .ThenInclude(e => e!.Person)
                         .ThenInclude(p => p!.Address)
                            .ThenInclude(p => p!.City)
                 .Include(u => u.Employee)
                     .ThenInclude(e => e!.Person)
                         .ThenInclude(p => p!.Address)
                            .ThenInclude(p => p!.Country)
                .Include(u => u.Employee)
                     .ThenInclude(e => e!.Person)
                         .ThenInclude(p => p!.Contact)
                 .Include(u =>u.Employee)
                    .ThenInclude(w => w!.Warehouse)
                 .FirstOrDefaultAsync(u => u.Id == request.Id, ct);

            if (user == null)

            {

                _logger.LogWarning("GetUsersQueryHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.UserNotFound");
                return ApplicationErrors.UserNotFound;

            }


            _logger.LogInformation("GetUsersQueryHandler completed successfully.");
            return user.ToDto();
        }
    }
}

