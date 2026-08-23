using Contract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Contract.Requests.Users
{
    public class CreateUserRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Role Role { get; set; }
        public string Email { get; set; } = string.Empty;
        public Guid EmployeeId { get; set; }
    }
}


