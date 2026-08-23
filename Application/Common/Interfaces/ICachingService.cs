using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Interfaces
{
    public interface ICachingService
    {
        Task<bool> CanConnectToRedisAsync(CancellationToken ct = default);
        Task RemoveByTagAsync( string []tags, CancellationToken ct = default);
    }
}
