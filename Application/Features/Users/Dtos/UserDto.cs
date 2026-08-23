using Contract.Features.Parties.Employees.Dtos;
using Domain.Identity.Employee;
using Domain.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        public Guid EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }
        public DateTimeOffset LastLoginAt { get; set; }

    }
}

