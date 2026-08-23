
using Contract.Common;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OldContract.Features.User.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        public Guid EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }
        public DateTime LastLoginAt { get; set; }

    }
}


