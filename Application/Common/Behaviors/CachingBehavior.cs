using Application.Common.Interfaces;
using Contract.Common.Interfaces;
using Inventory.Domain.Common.Results.Abstractions;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using System.Diagnostics;

namespace Contract.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse>(
        HybridCache cache,
        ILogger<CachingBehavior<TRequest, TResponse>> logger,
        IRedisHealthState redisHealth)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly HybridCache _cache = cache;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {
             if (request is not ICachedQuery cachedRequest)
            {
                return await next(ct);
            }

            _logger.LogInformation("Checking cache for {RequestName}", typeof(TRequest).Name);



              TResponse? result = default(TResponse);
 


            result =
                        await _cache.GetOrCreateAsync<TResponse>(
                    cachedRequest.CacheKey,
                    _ => new ValueTask<TResponse>((TResponse)(object)null!),
                    new HybridCacheEntryOptions
                    {
                        Flags = HybridCacheEntryFlags.DisableUnderlyingData | 
                        ((redisHealth.IsRedisAvailable) ? HybridCacheEntryFlags.None : HybridCacheEntryFlags.DisableDistributedCache)  
                    },
                    cancellationToken: ct);
            

            if (result is null)
            {
                result = await next(ct);

                if (result is IResult res && res.IsSuccess)
                {
                    _logger.LogInformation("Caching result for {RequestName}", typeof(TRequest).Name);
                           await _cache.SetAsync(
                        cachedRequest.CacheKey,
                        result,
                        new HybridCacheEntryOptions
                        {
                            Expiration = cachedRequest.Expiration,
                            Flags = ((redisHealth.IsRedisAvailable) ? HybridCacheEntryFlags.None : HybridCacheEntryFlags.DisableDistributedCache) 
                            ,
                            LocalCacheExpiration = cachedRequest.LocalCacheExpiration
                        },
                        cachedRequest.Tags,
                        ct);

                }
            }
            
            return result;
        }
    }
    }
