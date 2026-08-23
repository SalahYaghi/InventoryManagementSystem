using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContracOldCompatibile.Requests.Users
{
    public class UpdateUserPasswordRequest
    {
        public string oldpassword { get; set; } = string.Empty;
        public string newpassword { get; set; } = string.Empty;

    }
}
