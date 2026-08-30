using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.User.Dtos;
using Inventory.Domain.Common.Results;
using Inventory.Domain.Common.Results.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Users.Queries.GetUserById
{
    public sealed record GetUsersQuery : IRequest<Result<List<UserForListDto>>>
    {
        public string CacheKey => CacheKeys.ForEntityList(CacheGroups.Identity , 
            CacheEntities.User , nameof(GetUsersQuery));
        public string[] Tags => [CacheEntities.User];

    }
}

