using Contract.Common.Constants;
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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
    public class UpdateUserCommandHandler(IAppDbContext context,
        ILogger<UpdateUserCommandHandler> logger,
        ICachingService cache) : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly ILogger<UpdateUserCommandHandler> _logger = logger;

        public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateUserCommandHandler));

            bool usernameFound = await context.Users.AnyAsync(u => u.Username == request.username&&
            u.Id != request.id, cancellationToken); // [FIX 6.11] +ct
            if (usernameFound)
            {
                _logger.LogWarning("UpdateUserCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.UserWithUsernameAlreadyExists");
                return ApplicationErrors.UserWithUsernameAlreadyExists;
            }
           
            bool emailFound = await context.Users.AnyAsync(u => u.Email == request.email && 
            u.Id != request.id, cancellationToken); // [FIX 6.11] +ct
            if (emailFound)
            {
                _logger.LogWarning("UpdateUserCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.UserWithEmailAlreadyExists");
                return ApplicationErrors.UserWithEmailAlreadyExists;
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken); // [FIX 6.11] +ct

            if (user == default)

            {

                _logger.LogWarning("UpdateUserCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.UserNotFound");
                return ApplicationErrors.UserNotFound;

            }

           var result =  user.Update(request.username , request.email , 
                request.isActive , request.role); 

            if (result.IsError)
            {

                _logger.LogError("UpdateUserCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            await context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("UpdateUserCommandHandler completed successfully.");
            await cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.User), cancellationToken); // [FIX 1.11]

            var dto=  user.ToDto();

             
            
            return dto;
        }
    }
}

