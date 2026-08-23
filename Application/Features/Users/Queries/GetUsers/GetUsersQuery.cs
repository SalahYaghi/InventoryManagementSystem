using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.User.Dtos;
using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Users.Queries.GetUserById
{
    public sealed record GetUsersQuery : ICachedQuery<Result<List<UserForListDto>>>
    {
        public string CacheKey => CacheKeys.ForEntityList(CacheGroups.Identity , 
            CacheEntities.User , nameof(GetUsersQuery));
        public string[] Tags => [CacheEntities.User];

    }
}

