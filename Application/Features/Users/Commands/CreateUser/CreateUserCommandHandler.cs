using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.User.Dtos;
using Contract.Features.Users.Mappers;
using Domain.Common.Helpers;
using Domain.Identity.Employee;
using Domain.Identity.Users;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Extensions.Logging;
using Contract.Common.Constants;
using Application.Common.Dtos.Loggs;
using Microsoft.AspNetCore.Http;

namespace Contract.Features.User.Commands.CreateUser
{
    public class CreateUserCommandHandler(IAppDbContext context ,
        IHashingHelper hashingHelper,
        ILogger<CreateUserCommandHandler> logger,
        ICachingService cache,
        IAuditLogService logService,
        IUser currentUser,                       // [FIX 6.4] acting user for the audit trail
        IHttpContextAccessor httpContext) : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger = logger;

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateUserCommandHandler));

            bool usernameFound = await context.Users.AnyAsync(u => u.Username == request.username, cancellationToken); // [FIX 6.11] +ct
            if (usernameFound)
            {
                _logger.LogWarning("CreateUserCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.UserWithUsernameAlreadyExists");
                return ApplicationErrors.UserWithUsernameAlreadyExists;
            }
           
            bool emailFound = await context.Users.AnyAsync(u => u.Email == request.email, cancellationToken); // [FIX 6.11] +ct
            if (emailFound)
            {
                _logger.LogWarning("CreateUserCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.UserWithEmailAlreadyExists");
                return ApplicationErrors.UserWithEmailAlreadyExists;
            }

             

            bool employeeFound = await context.Employees.AnyAsync(e => e.Id == request.employeeId, cancellationToken);
            if (!employeeFound)
            {
                _logger.LogWarning("CreateUserCommandHandler stopped: employee {EmployeeId} not found.", request.employeeId);
                return ApplicationErrors.EmployeeNotFound;
            }

            bool employeeAlreadyHasUser = await context.Users
                .AnyAsync(u => u.EmployeeId == request.employeeId, cancellationToken);

            if (employeeAlreadyHasUser)
            {
                _logger.LogWarning("CreateUserCommandHandler stopped: employee {EmployeeId} already has a user.", request.employeeId);
                return ApplicationErrors.EmployeeAlreadyHasUser;
            }

            if (!ValidationHelper.ValidatePassword(request.password)) {

                _logger.LogError("CreateUserCommandHandler stopped because an error result was returned: {ErrorResult}.", "UserErrors.InvalidPasswordSent");
                return UserErrors.InvalidPasswordSent;
            }

            var passwordHashed = hashingHelper.Hash<Domain.Identity.Users.User>(request.password);


            var result = Domain.Identity.Users.User.Create(request.username, passwordHashed, request.email,
             request.role,  true,  request.employeeId);

            if (result.IsError)

            {

                _logger.LogError("CreateUserCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            await context.Users.AddAsync(result.Value, cancellationToken); // [FIX 6.11] +ct
            await context.SaveChangesAsync(cancellationToken);
            if (currentUser.UserId.HasValue && currentUser.UserId.Value != Guid.Empty)
            {
                await logService.SaveUserOperationsAudits(new CreateUserOperationsCommands
                {
                    UserId = currentUser.UserId.Value,
                    Request = nameof(CreateUserCommand),
                    Success = true,
                    IpAddress = httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    UserAgent = httpContext.HttpContext?.Request?.Headers["User-Agent"].ToString()
                }, cancellationToken);
            }
            else
            {
                _logger.LogWarning(
                    "CreateUserCommandHandler could not write an operations audit: no acting user in the current context.");
            }
            _logger.LogInformation("CreateUserCommandHandler completed successfully.");
            await cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.User), cancellationToken);

            return result.Value.ToDto();

        }
    }
}

