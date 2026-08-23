using Application.Common.Dtos.Loggs;
using Contract.Common.Interfaces;

public class UnauthorizedLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UnauthorizedLoggingMiddleware> _logger;

    public UnauthorizedLoggingMiddleware(
        RequestDelegate next,
        ILogger<UnauthorizedLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IAuditLogService audit,
        IUser user)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var path = context.Request.Path;

            await audit.SaveUserLoggingAudits(new CreateUserLoggingCommands
            {
                Action = Domain.AuditLoggs.AuditActions.UnAuthrizedResourcesAccessDenied,
                Success = false,
                UserId = user.UserId ?? Guid.Empty,
                IpAddress = ip,
                UserAgent = context.Request.Headers.UserAgent.ToString()
            });

            _logger.LogWarning(
                "Unauthorized access attempt. IP: {IP}, Path: {Path}",
                ip,
                path);
        }
    }
}