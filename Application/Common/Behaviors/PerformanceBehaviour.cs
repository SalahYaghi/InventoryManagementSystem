using Domain.Identity.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Contract.Common.Behaviors
{
    public class PerformanceBehaviour<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {

        private Stopwatch _timer;
        private ILogger<TRequest> _logger;
        private IUser _user;

        public PerformanceBehaviour( 
         ILogger<TRequest> logger,
         IUser user){
            this._user   = user;
            this._timer  = new Stopwatch();
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;


            if (elapsedMilliseconds > 500) {

                _logger.LogWarning(
                               "Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {UserId} {UserName} {@Request}",
                               nameof(TRequest), elapsedMilliseconds,
                               _user.UserId == Guid.Empty ? string.Empty : _user.UserId
                               , _user.UserName ?? string.Empty, request);
            }

            return response;
        }
    }
}
