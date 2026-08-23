
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Dtos
{
    public class UserForListDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTimeOffset LastLoginAt { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string ?WarehouseName { get; set; } = string.Empty;
        public Guid PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;

    }
}

