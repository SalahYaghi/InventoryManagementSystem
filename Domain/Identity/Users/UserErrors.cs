using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity.Users
{
    public class UserErrors
    {
        public static Error InvalidRoleValueSent => Error.Validation("User.Errors" , 
            "Invalid role value sent");
        public static Error InvalidPasswordSent => Error.Validation("User.Username",
           @"Password must be at least 8 characters long and include at least three of the following: uppercase letters, lowercase letters, numbers, and special characters. It must not contain common patterns or your username.");

        public static Error UserDeactivated => Error.Conflict("User.UserDeactivated",
            @"use is deactivated.");

        public static Error InvalidUsernameSent => Error.Validation("User.Username",
            @$"Invalid username sent.Username must start with a letter and be between {UserRules.UsernameMinLength} and {UserRules.UsernameMaxLength} characters long. It can only contain letters, numbers, and underscores.");

        public static Error EmailNotValid => Error.Validation("User.EmailNotValid",
            @"Invalid email format. Rules:
- Must contain exactly one '@'
- Must have text before and after '@'
- Must include a domain (e.g., .com, .org)
- Must not contain spaces");
    }
}

