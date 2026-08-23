using Domain.Identity.Users;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity.RefreshToken
{
    public class RefreshToken : AuditableEntity
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTimeOffset? RevokedAt { get; set; }
        public DateTimeOffset  ExpiresAt { get; set; }
        public bool IsRevoked => RevokedAt != null;
        public bool IsExpired => ExpiresAt <= DateTimeOffset.UtcNow;


        public Result<Updated> Revoke() {

            if (IsRevoked) return RefreshTokenErrors.AlreadyRevoked;

            this.RevokedAt = DateTimeOffset.UtcNow;
            return Result.Updated;
        }
        private RefreshToken() { }
        private RefreshToken(Guid id , string Token, Guid UserId, DateTimeOffset ExpiresAt):base (id) {

            this.Token = Token;
            this.UserId = UserId;
            this.ExpiresAt = ExpiresAt;

        }
 

        public static Result<RefreshToken> Create(Guid id  , string token, Guid userId, DateTimeOffset expiresAt) {

            if (string.IsNullOrEmpty(token))
                return RefreshTokenErrors.TokenIsRequired;

            if (userId == Guid.Empty)
                return RefreshTokenErrors.UserIsRequired;

            if (expiresAt < DateTimeOffset.UtcNow)
                return RefreshTokenErrors.InvalidExpiratoinDate;

            return new RefreshToken(id ,token ,userId , expiresAt);
        }
         
    }
}

