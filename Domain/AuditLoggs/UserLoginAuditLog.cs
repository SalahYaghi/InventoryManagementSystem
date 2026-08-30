using Domain.Identity.Users;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.AuditLoggs
{
    public class UserLoginAuditLog : AuditLog
    {
        public AuditActions Action { get; private set; } = default!;
        
        private UserLoginAuditLog() { }

        public static Result<UserLoginAuditLog> Create(
         Guid userId,
         AuditActions action ,
         string? ipAddress ,
         string? userAgent,
         bool success,
         string ? errorMessages = null) {

            if (userId == Guid.Empty) return Error.Validation("AuditLog.InvlidUserId" , "user id sent is invalid please try another one.");

            if(!Enum.IsDefined(typeof(AuditActions), action))
                return Error.Validation("AuditLog.InvlidAuditAction", "invalid audit action sent!");

            return new UserLoginAuditLog()
            {
                CreatedAtUtc = DateTime.UtcNow,
                IpAddress = string.IsNullOrEmpty(ipAddress) ? null : ipAddress,
                Action = action,
                UserAgent = userAgent,
                UserId  = userId,
                 Id = Guid.NewGuid(),
                IsSuccess  = success,
                ErrorMessage = errorMessages
                
      
            };
        }

    }
}
