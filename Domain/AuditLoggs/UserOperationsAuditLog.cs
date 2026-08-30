using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AuditLoggs
{
    public class UserOperationsAuditLog : AuditLog
    {
        public string RequsetName { get; set; } = string.Empty;

        public static Result<UserOperationsAuditLog> Create(
         Guid userId,
         string request,
         string? ipAddress,
         string? userAgent,
         bool success,
         string? errorMessages = null)
        {

            if (userId == Guid.Empty) return Error.Validation("AuditLog.InvlidUserId", "user id sent is invalid please try another one.");

            return new UserOperationsAuditLog()
            {
                CreatedAtUtc = DateTime.UtcNow,
                IpAddress = string.IsNullOrEmpty(ipAddress) ? null : ipAddress,
                UserAgent = userAgent,
                UserId = userId,
                Id = Guid.NewGuid(),
                IsSuccess = success,
                ErrorMessage = errorMessages,
                RequsetName = request


            };
        

    }

}
}
