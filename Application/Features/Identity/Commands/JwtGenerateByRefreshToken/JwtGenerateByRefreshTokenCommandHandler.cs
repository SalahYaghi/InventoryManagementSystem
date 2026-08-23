using Application.Common.Dtos.Loggs;
using Contract.Common.Interfaces;
using Contract.Features.Identity.Commands.JwtGenerate;
using Infrastructure.Identity;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Contract.Features.Identity.Commands.JwtGenerate
{
    public class JwtGenerateByRefreshTokenCommandHandler : IRequestHandler<JwtGenerateByRefreshTokenCommand, Result<JwtDto>>
    {
        private readonly ILogger<JwtGenerateByRefreshTokenCommandHandler> _logger;

       
       private IIdentityService _identityService;
        private IJwtProvider _jwtProvider;
        private IAuditLogService _audit;
        private IHttpContextAccessor _httpContext;

        public JwtGenerateByRefreshTokenCommandHandler(//IIdentityService identityService, 
            IJwtProvider jwtProvider,
            ILogger<JwtGenerateByRefreshTokenCommandHandler> logger,
             IAuditLogService audit,
        IHttpContextAccessor httpContext ,
        IIdentityService identityService) {
            _logger = logger;
      
            this._jwtProvider = jwtProvider;
            this._audit = audit;
            this._httpContext = httpContext;
            this._identityService = identityService;
        }

        public async Task<Result<JwtDto>> Handle(JwtGenerateByRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", 
                nameof(JwtGenerateByRefreshTokenCommandHandler));

            var token = await _jwtProvider.GenereateJwtTokenByRefreshToken(request.refresh);
            if (token.IsError)
                return token.Errors;
            var principal = _identityService.GetPrincipalFromToken(token.Value.AccessToken);

            if (principal is null) {

                _logger.LogError("Failed To Get Claim from new generate jwt token");

                return Error.Failure("JwtGenerate.Failed" , "failed to generate jwt token login throw another way.");
            }

            var value =
          principal.FindFirstValue(ClaimTypes.NameIdentifier)
          ??principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

           if (!Guid.TryParse(value, out var userId)){
                _logger.LogError("Failed to parse user id got from principle of jwt token");

                return Error.Failure("JwtGenerate.Failed", "failed to generate jwt token login throw another way.");

            }


            if (request.loginSource)
            await _audit.SaveUserLoggingAudits(new CreateUserLoggingCommands
            {
                Action = Domain.AuditLoggs.AuditActions.Login,
                Success = true,
                UserId = userId,
                IpAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Not-Defined",
                UserAgent = _httpContext.HttpContext?.Request?.Headers["User-Agent"].ToString() ?? "Not-Defined"
            }, cancellationToken);


            return token.Value;
        }
    }
}

