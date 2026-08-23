using Azure;
using Domain.Identity.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace Infrastructure.Policies.OutputCachePolicies
{
    public class AuthenticatedUserCachePolicy : IOutputCachePolicy
    {
        public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
        {

            var currentContext = context.HttpContext;
            if (!(currentContext.User.Identity?.IsAuthenticated ?? false))
            {
                context.EnableOutputCaching = false;
                return ValueTask.CompletedTask;
            }
            var isSafeMethod =
    HttpMethods.IsGet(currentContext.Request.Method) ||
    HttpMethods.IsHead(currentContext.Request.Method);

            if (!isSafeMethod)
            {
                context.EnableOutputCaching = false;
                return ValueTask.CompletedTask;
            }
 
            var userId =
          currentContext.User?.FindFirstValue(ClaimTypes.NameIdentifier)
          ?? currentContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                context.EnableOutputCaching = false;
                return ValueTask.CompletedTask;
            }

            context.AllowCacheLookup = true;
            context.AllowCacheStorage = true;
            context.AllowLocking = true;
            context.EnableOutputCaching = true;

            context.CacheVaryByRules.VaryByValues["user"] = userId;
           // context.CacheVaryByRules.QueryKeys = "*";

            return ValueTask.CompletedTask;
        }

        public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellation)
        => ValueTask.CompletedTask;

        public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellation)
        {
            var response = context.HttpContext.Response;    

            if (response.Headers.ContainsKey("Set-Cookie"))
            {
                context.AllowCacheStorage = false;
            }

            if (context.HttpContext.Response.StatusCode != StatusCodes.Status200OK)
            {
                context.AllowCacheStorage = false;
            }

            return ValueTask.CompletedTask;
        }
    }
}
