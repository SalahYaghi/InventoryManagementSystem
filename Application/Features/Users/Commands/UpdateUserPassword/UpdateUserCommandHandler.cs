using Application.Common.Dtos.Loggs;
using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.User.Dtos;
using Contract.Features.Users.Mappers;
using Domain.Common.Helpers;
using Domain.Identity.Employee;
using Domain.Identity.Users;
using Domain.Products.Enums;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
    public class UpdateUserPasswordCommandHandler(IAppDbContext context ,
        IHashingHelper hasher,
        ILogger<UpdateUserPasswordCommandHandler> logger,
        ICachingService cache,
        IAuditLogService audit,
        IHttpContextAccessor httpContext) : IRequestHandler<UpdateUserPasswordCommand, Result<Updated>>
    {
        private readonly ILogger<UpdateUserPasswordCommandHandler> _logger = logger;

        public async Task<Result<Updated>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateUserPasswordCommandHandler));

         
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken); 

            if (user == default)

            {

                _logger.LogWarning("UpdateUserPasswordCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.UserNotFound");
                return ApplicationErrors.UserNotFound;

            }

            var verifiedPassword = hasher.VerifyHashed<Domain.Identity.Users.User>(user.HashedPassword ,request.oldpassword);
            if (!verifiedPassword)
            {
                _logger.LogError("UpdateUserPasswordCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.PasswordIsWrong");
                return ApplicationErrors.PasswordIsWrong;
            }

            if (hasher.VerifyHashed<Domain.Identity.Users.User>(user.HashedPassword, request.newpassword))
            {
                _logger.LogWarning("UpdateUserPasswordCommandHandler stopped: new password matches the current one.");
                return ApplicationErrors.NewPasswordMustDiffer;
            }

            var hashedNewPassowd = hasher.Hash<Domain.Identity.Users.User>(request.newpassword);

            var result = user.UpdatePassword(hashedNewPassowd);

            if (result.IsError)

            {

                _logger.LogError("UpdateUserPasswordCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            var activeTokens = await context.RefreshTokens
                .Where(t => t.UserId == user.Id && t.RevokedAt == null)
                .ToListAsync(cancellationToken);

            foreach (var token in activeTokens)
            {
                var revokeResult = token.Revoke();

                if (revokeResult.IsError)
                {
                    _logger.LogWarning("Could not revoke refresh token {TokenId}: {Errors}", token.Id, revokeResult.Errors);
                }
            }

            _logger.LogInformation(
                "Revoking {Count} refresh token(s) for user {UserId} after a password change.",
                activeTokens.Count, user.Id);

            await context.SaveChangesAsync(cancellationToken);
            await cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.User), cancellationToken); 
            await audit.SaveUserLoggingAudits(new CreateUserLoggingCommands
            {
                Action = Domain.AuditLoggs.AuditActions.ResetPassword,
                Success = true,
                UserId = request.id,

                IpAddress = httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Not-Defined",
                UserAgent = httpContext.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "Not-Defined"
            }, cancellationToken);
            _logger.LogInformation("UpdateUserPasswordCommandHandler completed successfully.");
            return Result.Updated;
        }
    }
}

