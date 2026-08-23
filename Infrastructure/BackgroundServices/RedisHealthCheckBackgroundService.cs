using Application.Common.Interfaces;
using Contract.Common.Interfaces;
using Infrastructure.Common.Options;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.BackgroundServices
{
    public class RedisHealthCheckBackgroundService : BackgroundService
    {
        private AppSettings _appSettings;

        private readonly ILogger<RedisHealthCheckBackgroundService> _logger;
        private readonly IRedisHealthState _redisHealth;
        private readonly IServiceScopeFactory _serviceFacotry;
        public RedisHealthCheckBackgroundService(IOptions<AppSettings> appSettings  , ILogger<RedisHealthCheckBackgroundService> logger , 
            IRedisHealthState redisHealth , IServiceScopeFactory serviceFactory)
        {
            _logger = logger;
            _serviceFacotry = serviceFactory;
            _redisHealth = redisHealth;
            _appSettings = appSettings.Value;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(_appSettings.RedisConnectionHealthCheckMinutes));

            while (await timer.WaitForNextTickAsync()) {

                using var scope = _serviceFacotry.CreateScope();
                var cache = scope.ServiceProvider.GetRequiredService<ICachingService>();


                bool canConnect = await cache.CanConnectToRedisAsync(stoppingToken);

                _logger.LogInformation("Redis health check at {Time}: Can connect to Redis: {CanConnect}", DateTime.UtcNow, canConnect);

                _redisHealth.IsRedisAvailable = canConnect;

                _redisHealth.LastCheckedAt = DateTime.UtcNow;

                if (canConnect) { 
                
                    _logger.LogInformation("Redis is healthy. Last checked at {Time}", _redisHealth.LastCheckedAt);
                }

            }
        }
    }
}
