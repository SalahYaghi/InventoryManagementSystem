using Contract.Common.Constants;
using MediatR;

namespace Contract.Common.Interfaces
{
    public interface ICachedQuery
    {
        string CacheKey { get; }

        string[] Tags => [];

        TimeSpan? Expiration => TimeSpan.FromMinutes(ApplicationDefaults.DefaultCacheExpirationMinutes);

        TimeSpan? LocalCacheExpiration => TimeSpan.FromMinutes(ApplicationDefaults.DefaultLocalCacheExpirationMinutes);
    }

    public interface ICachedQuery<TResponse> : IRequest<TResponse>, ICachedQuery
    {
    }
}
