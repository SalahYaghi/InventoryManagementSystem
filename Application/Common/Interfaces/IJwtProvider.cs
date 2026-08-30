using Domain.Identity.Users;
using Infrastructure.Identity;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Interfaces
{
    public interface IJwtProvider
    {
        Task<Result<JwtDto>> GenereateJwtToken(User user,
            CancellationToken ct = default);
        Task<Result<JwtDto>> GenereateJwtTokenByRefreshToken(string refreshToken,
            CancellationToken ct = default);

    }
}

