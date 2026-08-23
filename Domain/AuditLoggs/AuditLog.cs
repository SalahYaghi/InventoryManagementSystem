using Domain.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AuditLoggs
{
    public abstract class AuditLog
    {
        public Guid Id {  get; set; }
        public Guid UserId { get; set; }
        public User? User { get; private set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; }

    }
}
