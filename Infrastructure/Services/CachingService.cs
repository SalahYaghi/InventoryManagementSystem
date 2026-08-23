using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class CachingService(HybridCache hybridCache , IOutputCacheStore outputCacheStore , 
        ILogger<CachingService> logger , IConnectionMultiplexer connectionMultiplexer) : ICachingService
    {

        public async Task<bool> CanConnectToRedisAsync(CancellationToken ct = default)
        {
            try {

                var db = connectionMultiplexer.GetDatabase(); 
                await db.PingAsync();
                return true;  
                
            }
            catch {

                return false;
            }

        }

        public async Task RemoveByTagAsync(string[] tags, CancellationToken ct = default)
        {

            try
            {

                foreach(var tag in tags) 
                    await outputCacheStore.EvictByTagAsync(tag , ct);
                //await Task.WhenAll(tags.Select(async t => {
                //   await outputCacheStore.EvictByTagAsync(t,ct);
                //})); 
            }
            catch (Exception ex) {

                logger.LogError(ex.Message);
            }
            try
            {

                foreach (var t in tags)
                    await hybridCache.RemoveByTagAsync(t, ct);

             }
            catch (RedisConnectionException ex) {

                logger.LogError(ex.Message);

            }
            return;
        }
    }
}
