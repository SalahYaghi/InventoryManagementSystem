using Domain.Common.Helpers;
using Domain.Identity.Employee;
using Domain.People;
using Domain.Warehouses;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Identity.Users
{
    public class User : AuditableEntity
    {

        public string Username { get; set; }
        public string HashedPassword { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }
        public bool IsActive { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee.Employee? Employee { get; set; }

        public DateTimeOffset LastLoginAt { get; set; }

        private User() { }

        private User(string username, string hashedpassword,
            Role role, string email, bool isAtive, Guid employeeId) {

            this.Username = username;
            this.HashedPassword = hashedpassword;
            this.Role = role;
            this.IsActive = isAtive;
            this.EmployeeId = employeeId;
            this.Email = email;
        }

        public static Result<User> Create(string username, string hashedpassword, string email,
            Role role, bool isAtive, Guid employeeId) {


            if (Guid.Empty == employeeId)
                return EmployeeErrors.EmployeeIsRequired;

            if (!Enum.IsDefined(typeof(Role), role))
                return UserErrors.InvalidRoleValueSent;

            if (!ValidationHelper.ValidateUsername(username))
                return UserErrors.InvalidUsernameSent;

            if (!ValidationHelper.ValidateEmail(email))
                return UserErrors.EmailNotValid;

            if (string.IsNullOrWhiteSpace(hashedpassword)) {
                return UserErrors.InvalidPasswordSent;
            }

            return new User(username, hashedpassword, role, email, isAtive, employeeId);
        }


        public  Result<Updated> UpdatePassword(string hashedPassword) {

            this.HashedPassword = hashedPassword;

            return Result.Updated;
        }


        public  Result<Updated> Update(string username,string email,
           bool isAtive , Role role)
        {

            if (!Enum.IsDefined(typeof(Role), role))
                return UserErrors.InvalidRoleValueSent;

            if (!ValidationHelper.ValidateUsername(username))
                return UserErrors.InvalidUsernameSent;

            if (!ValidationHelper.ValidateEmail(email))
                return UserErrors.EmailNotValid;

            this.Username = username;
            this.Email = email;
            this.IsActive= isAtive;
            this.Role = role;


            return Result.Updated;
        }

    }
}

