using Contract.Features.User.Dtos;
using Domain.Identity.Users;
using Inventory.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
    public record UpdateUserCommand(Guid id , string username, string email , bool isActive , Role role
        )
        : IRequest<Result<UserDto>>
    ;
}

