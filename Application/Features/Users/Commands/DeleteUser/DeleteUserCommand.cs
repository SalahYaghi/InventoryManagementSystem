using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid userId) : IRequest<Result<Deleted>>
    ;
}
