using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.User.Commands.CreateUser;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IAppDbContext context,
        ILogger<DeleteUserCommandHandler> logger,
        ICachingService cache) : IRequestHandler<DeleteUserCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Started handling {RequestName}.", nameof(DeleteUserCommandHandler));  


            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userId , cancellationToken);

            if (user == default)

            {

                logger.LogWarning("DeleteUserCommand stopped because an error result was returned: {@ErrorResult}.", $"{ApplicationErrors.UserNotFound}");
                return ApplicationErrors.UserNotFound;

            }

            var activeTokens = await context.RefreshTokens
                .Where(t => t.UserId == user.Id && t.RevokedAt == null)
                .ToListAsync(cancellationToken);

            foreach (var token in activeTokens)
            {
                var revokeResult = token.Revoke();

                if (revokeResult.IsError)
                {
                    logger.LogWarning("Could not revoke refresh token {TokenId}: {Errors}", token.Id, revokeResult.Errors);
                }
            }

            logger.LogInformation("DeleteUserCommandHandler is marking entity data for persistence operation.");
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("DeleteUserCommandHandler is invalidating related cache entries.");
            await cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.User), cancellationToken); 

            logger.LogInformation("DeleteUserCommandHandler invalidated related cache entries successfully.");

            logger.LogInformation("User deleted successfully with key {Key}", request.userId);


            return Result.Deleted;
        }
    }
}
