using Contract.Features.User.Dtos;
using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Users.Queries.GetUserById
{
    public sealed record GetUserByEmailQuery(string email) : IRequest<Result<UserDto>>;
   
}

