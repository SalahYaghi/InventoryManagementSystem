using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Users
{
    public class UpdateUserPasswordRequest
    {
        public string oldpassword { get; set; } = string.Empty;
        public string newpassword { get; set; } = string.Empty;

    }
}

