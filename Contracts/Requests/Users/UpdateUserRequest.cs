
using Contract.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
    public record UpdateUserRequest(
        Guid id, string username, string email, bool isActive, Role role
        );
    
}

