
using Contract.Common;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Dtos
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        public Guid EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }
        public DateTimeOffset LastLoginAt { get; set; }

    }
}

