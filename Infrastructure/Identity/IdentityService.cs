using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Domain.Identity.Users;
using Infrastructure.Common.Options;
using MechanicShop.Domain.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity
{
    public class IdentityService(IAppDbContext context,IOptions<JwtOptions> jwtOptions) : IIdentityService
    {
        
        public async Task<Result<Updated>> UpdateLastLoginAt(Guid userId , CancellationToken ct) {

            var currentUser = await context.Users.FirstOrDefaultAsync(r => r.Id == userId , ct);

            if (currentUser == null) {
                return Error.NotFound("User.NotFound" , "User is not found.");
            }

            currentUser.LastLoginAt = DateTimeOffset.UtcNow;

            await context.SaveChangesAsync(ct);

            return Result.Updated;
        }

        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var settings = jwtOptions.Value;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret)),
                ValidateIssuer = true,
                ValidIssuer = settings.Issuer,
                ValidateAudience = true,
                ValidAudience = settings.Audience,
                ValidateLifetime = false, 
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;
        }
        public async Task<Result<User>> AuthenticateAsync(string email, string password)
        {
     
           var user =
                await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == default)
                return ApplicationErrors.UserNotFound;

            var hasher = new PasswordHasher<User>();

             var verfied =  hasher.VerifyHashedPassword(null , user.HashedPassword , password ); 

            if(verfied != PasswordVerificationResult.Success)
                return ApplicationErrors.PasswordIsWrong;

            if (!user.IsActive)
                return UserErrors.UserDeactivated;
            return user;

        }
    }
}

