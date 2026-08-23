using Domain.AuditLoggs;
using Domain.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos.Loggs
{
    public class CreateUserLoggingCommands
    {
            public Guid UserId { get; set; }
            public AuditActions Action { get; set; } = default!;
            public string? IpAddress { get; set; }
            public string? UserAgent { get; set; }
            public bool Success { get; set; }
            public string? ErrorMessages { get; set; }

          }
}
