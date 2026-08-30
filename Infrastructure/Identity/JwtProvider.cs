using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Domain.Identity.RefreshToken;
using Domain.Identity.Users;
using Infrastructure.Common.Options;
using Inventory.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Identity
{
    public class JwtProvider : IJwtProvider
    {
        private JwtOptions _options;
        private IAppDbContext _context;
        public JwtProvider(IOptions<JwtOptions> jwtOptions ,
            IAppDbContext context) {

            this._options = jwtOptions.Value;
            this._context = context;
        }
        private  string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
        public async Task<Result<JwtDto>> GenereateJwtToken(User user , 
            CancellationToken ct = default)
        {
             
            var expires = DateTimeOffset.UtcNow.AddMinutes(_options.TokenExpirationInMinutes);

            var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Nickname, user.Username),
        };

            claims.Add(new(ClaimTypes.Role , (user.Role.ToString())));
            

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = (expires.UtcDateTime),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var oldRefreshTokens = await _context.RefreshTokens
                  .Where(rt => rt.UserId == user.Id)
                  .ExecuteDeleteAsync(ct);



            var refreshToken = RefreshToken.Create(Guid.NewGuid(),
                GenerateRefreshToken() , user.Id
                , DateTimeOffset.UtcNow.AddDays(_options.RefreshTokenExpirationInDays));

            if (refreshToken.IsError)
                return refreshToken.Errors;

            var refreshTokenValue =  refreshToken.Value;

            _context.RefreshTokens.Add(refreshTokenValue);

            await _context.SaveChangesAsync(ct);

            return new JwtDto
            {
                AccessToken = tokenHandler.WriteToken(securityToken),
                RefreshToken = refreshTokenValue.Token,
                ExpiresOnUtc = expires
            };




        }

        public async Task<Result<JwtDto>> GenereateJwtTokenByRefreshToken(string refreshToken , 
            CancellationToken ct = default) {

            var foundToken = await _context.RefreshTokens.Where(t => t.Token == refreshToken &&
           t.ExpiresAt > DateTimeOffset.UtcNow && !t.RevokedAt.HasValue).FirstOrDefaultAsync(ct);

            if (foundToken == default) {

                return Error.NotFound("RefreshToken.NotFound","Given refresh token is not found"); 
            }

            var user = await _context.Users.Where(u => u.Id == foundToken.UserId).FirstOrDefaultAsync(ct);

            if (user == default) {
                return ApplicationErrors.UserNotFound; 
            }

            return await GenereateJwtToken(user);
        }

    }
}

