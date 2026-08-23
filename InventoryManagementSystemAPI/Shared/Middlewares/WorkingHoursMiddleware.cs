using Application.Common.Dtos.Loggs;
using Contract.Common.Interfaces;
using Infrastructure.Common.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InventoryManagementSystemAPI.Shared.Middlewares
{
    public class WorkingHoursMiddleware
    {
        private readonly RequestDelegate _next;
        public WorkingHoursMiddleware(RequestDelegate next )
        {
            _next = next;
        }

        private void WriteAccessDeniedResponse(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/problem+json";
            context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "Access is not allowed outside working hours.",
                Status = StatusCodes.Status403Forbidden,
                Type = "https://httpstatuses.com/403",
                Instance = context.Request.Path
            });
        }       

        public async Task InvokeAsync(HttpContext context , 
            IOptions<AppSettings> appSetings , 
            IAuditLogService auditLogService)
        {

            var now = DateTime.UtcNow.TimeOfDay;


                if (now < appSetings.Value.OpenAt || now > appSetings.Value.CloseAt)
                {


                if (context.User.Identity?.IsAuthenticated == true)
                {
                    var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                        context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                    if (Guid.TryParse(userId, out var id))
                    {
                        await auditLogService.SaveUserLoggingAudits(new CreateUserLoggingCommands()
                        {
                            Action = Domain.AuditLoggs.AuditActions.AccessDeniedOutsideWorkingHours,
                            Success = false,
                            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                            UserAgent = context.Request.Headers.UserAgent.ToString(),
                            UserId = id
                        });
                    }
                    ;
                }

                WriteAccessDeniedResponse(context);
                return;
                }

            await _next(context);
        }
    }
}