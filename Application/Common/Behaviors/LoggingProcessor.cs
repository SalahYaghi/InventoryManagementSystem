using Domain.Identity.Users;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Behaviors
{
    public class LoggingProcessor<TRequest>(ILogger<TRequest> logger , IUser user) :
        IRequestPreProcessor<TRequest>
        where TRequest : notnull
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            
            logger.LogInformation(
                "Request: {Name} {UserId} {UserName} {@Request}", nameof(TRequest)
                , user.UserId, user.UserName, request);

            return Task.CompletedTask;
        }
    }
}
