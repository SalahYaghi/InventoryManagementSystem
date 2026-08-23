using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity.Users
{
    public static class UserRules
    {
        public static int PasswordMaxLength => 16;
        public static int PasswordMinLength => 8;
        public static int UsernameMinLength => 5;
        public static int UsernameMaxLength => 20;

    }
}

