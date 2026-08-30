using Contract.Features.User.Dtos;
using Domain.Identity.Users;
using Inventory.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
    public record UpdateUserPasswordCommand(Guid id , string oldpassword , string newpassword
        
        )
        : IRequest<Result<Updated>>
    ;
}

