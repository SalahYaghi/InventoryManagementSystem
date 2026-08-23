using Application.Common.Dtos.Loggs;
using Contract.Common.Interfaces;
using MechanicShop.Domain.Common.Results.Abstractions;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Behaviors
{
    public class LoggingPostProcessor<TRequest, TResponse>(IUser user , 
        ILogger<TRequest> logger , IAppDbContext context , 
        IHttpContextAccessor httpContext , IAuditLogService auditService)
    : IRequestPostProcessor<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
        where TResponse : IResult
    {
        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            logger.LogInformation(
               "Request: {Name} {UserId} {UserName} {@Request}", nameof(TRequest)
               , user.UserId, user.UserName, request);


            if (
                user.UserId.HasValue) {

                Guid userId = user.UserId.Value;
                string nameOfRequest = typeof(TRequest).Name ?? string.Empty;
                string? ip = httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? null;
                string? agent = httpContext.HttpContext?.Request.Headers["User-Agent"].ToString();
                DateTimeOffset CurrentDate = DateTimeOffset.Now;

                if (response.IsSuccess)
                {
                    await auditService.SaveUserOperationsAudits(new CreateUserOperationsCommands()
                    {

                        IpAddress = ip,
                        UserId = userId,
                        Success = true,
                        Request = nameOfRequest,
                        UserAgent = agent,
                         
                    });
                }
                else
                {
                    await auditService.SaveUserOperationsAudits(new CreateUserOperationsCommands()
                    {

                        IpAddress = ip,
                        UserId = userId,
                        Success = false,
                        Request = nameOfRequest,
                        UserAgent = agent,
                        ErrorMessages = response.Errors?.Count > 0 ? string.Join(',', response.Errors.Select(e => e.Description)) : null
           
                    }); 

                }


            }



        }
    }
}
