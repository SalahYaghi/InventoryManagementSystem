using Application.Common.Dtos.Loggs;
using Contract.Common.Interfaces;
using Contract.Features.Identity.Commands.JwtGenerate;
using Domain.Identity.Users;
using Domain.Products.Enums;
using Infrastructure.Identity;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Identity.Commands.JwtGenerate
{
    public class JwtGenerateCommandHandler : IRequestHandler<JwtGeneratCommand, Result<JwtDto>>
    {
        private readonly ILogger<JwtGenerateCommandHandler> _logger;

       
        private IIdentityService _identityService;
        private IJwtProvider _jwtProvider;
        private IAuditLogService _audit;
        private IHttpContextAccessor _httpContext;
        private readonly IAppDbContext _context;  

        public JwtGenerateCommandHandler(
            IIdentityService identityService,
            IJwtProvider jwtProvider,
            ILogger<JwtGenerateCommandHandler> logger,
            IAuditLogService audit,
            IHttpContextAccessor httpContext,
            IAppDbContext context)
        {
            _logger = logger;
            _httpContext = httpContext;
            _audit = audit;
            _identityService = identityService;
            _jwtProvider = jwtProvider;
            _context = context;
        }

        public async Task<Result<JwtDto>> Handle(JwtGeneratCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(JwtGenerateCommandHandler));

            var userResult = await _identityService.AuthenticateAsync(request.email, request.password);

            if (userResult.IsError)
            {
                await TryAuditFailedLoginAsync(request.email, userResult.Errors.FirstOrDefault().Description, cancellationToken);

                _logger.LogWarning("JwtGenerateCommandHandler stopped: authentication failed for {Email}.", request.email);
                return userResult.Errors;
            }

            var jwtToken = await _jwtProvider.GenereateJwtToken(userResult.Value);

            if (jwtToken.IsError)

            {

                _logger.LogError("JwtGenerateCommandHandler stopped because an error result was returned: {ErrorResult}.", "jwtToken.Errors");
                return jwtToken.Errors;

            }


            await _audit.SaveUserLoggingAudits(new CreateUserLoggingCommands
            {
                Action = Domain.AuditLoggs.AuditActions.Login,
                Success = true,
                UserId = userResult.Value.Id,
                IpAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Not-Defined",
                UserAgent = _httpContext.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "Not-Defined"
            }, cancellationToken);


            var lastLoginResult = await _identityService.UpdateLastLoginAt(userResult.Value.Id, cancellationToken);

            if (lastLoginResult.IsError)
            {
                _logger.LogError(
                    "Failed to update last login date with errors: {Errors}",
                    lastLoginResult.Errors);
            }



            _logger.LogInformation("JwtGenerateCommandHandler completed successfully.");
            return jwtToken.Value;
        }

        private async Task TryAuditFailedLoginAsync(string email, string? reason, CancellationToken ct)
        {
            var userId = await _context.Users
                .Where(u => u.Email == email)
                .Select(u => (Guid?)u.Id)
                .FirstOrDefaultAsync(ct);

            if (userId is null || userId == Guid.Empty)
            {
                _logger.LogWarning("Failed login for an unknown email address; no user to attribute the audit entry to.");
                return;
            }

            await _audit.SaveUserLoggingAudits(new CreateUserLoggingCommands
            {
                Action = Domain.AuditLoggs.AuditActions.Login,
                Success = false,
                UserId = userId.Value,
                ErrorMessages = reason,
                IpAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Not-Defined",
                UserAgent = _httpContext.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "Not-Defined"
            }, ct);
        }
    }
}

