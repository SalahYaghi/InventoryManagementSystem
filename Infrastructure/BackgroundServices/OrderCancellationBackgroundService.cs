using Contract.Common.Constants;
using Domain.Orders;
using Infrastructure.Common.Options;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.BackgroundServices
{
    public class OrderCancellationBackgroundService : BackgroundService
    {
        private AppSettings _appSettings;
        private IServiceScopeFactory _serviceFactory;
        private ILogger<RefreshTokenInvokerBackgroundService> _logger;
        private HybridCache _cache;

        public OrderCancellationBackgroundService(IOptions<AppSettings> appSettings , 
            IServiceScopeFactory serviceFactory ,
            ILogger<RefreshTokenInvokerBackgroundService> logger , 
            HybridCache cache) { 
            this._appSettings = appSettings.Value;
            this._serviceFactory = serviceFactory;
            this._logger = logger;
            this._cache = cache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var timer = new PeriodicTimer(TimeSpan.FromMinutes(_appSettings.OrderingCancellationFrequecyCheckInMinutes));

            while (await timer.WaitForNextTickAsync())
            {
                _logger.LogInformation("Checking Canceled Orders at {Now}", DateTime.UtcNow);

                 try
                {
                    using var scope = _serviceFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var orders =
                        await context.Orders.Where(r => r.DueDate.AddMinutes(_appSettings.OrderingCancellationThresholdMinutes)
                        < DateTimeOffset.UtcNow &&
                        r.OrderStatus == OrderStatus.Pending)
                        .ToListAsync(stoppingToken);

                    if (orders.Any())
                    {
                        foreach (var order in orders)
                        {
                            var result = order.UpdateStatus(OrderStatus.Cancelled);
                            if (result.IsError)
                            {
                                _logger.LogWarning("Failed to cancel WorkOrder {Id}: {Error}", order.Id, result.Errors);
                                
                            }

                        }

                        await context.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation("Orders Canceled {Count} : {Ids}", orders.Count
                            ,orders.Select(w => w.Id));
                        await _cache.RemoveByTagAsync(CacheEntities.Order);

                    }
                    else
                    {
                        _logger.LogInformation("No order to cancel found.");
                    }
           
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error revoking expired refresh tokens.");
                }
            }
        }
    }
}
