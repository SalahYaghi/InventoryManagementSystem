using Domain.Identity.Users;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Contract.Common.Interfaces
{
    public interface IIdentityService
    {
         ClaimsPrincipal? GetPrincipalFromToken(string token);
         Task<Result<User>> AuthenticateAsync(string email , string password);
         Task<Result<Updated>> UpdateLastLoginAt(Guid userId, CancellationToken ct);
    }
}

