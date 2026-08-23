
using Contract.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
    public class UpdateUserRequest
    { 
        public Guid id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
        public Role role { get; set; }

    }
}


