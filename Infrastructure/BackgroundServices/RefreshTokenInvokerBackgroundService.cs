using Contract.Common.Constants;
using Infrastructure.Common.Options;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.BackgroundServices
{
    public class RefreshTokenInvokerBackgroundService : BackgroundService
    {
        private AppSettings _appSettings;
        private IServiceScopeFactory _serviceFactory;
        private ILogger<RefreshTokenInvokerBackgroundService> _logger;
        private HybridCache _cache;
        public RefreshTokenInvokerBackgroundService(IOptions<AppSettings> appSettings , 
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

            var timer = new PeriodicTimer(TimeSpan.FromMinutes(_appSettings.RefreshTokenRevokerFrequentCheckInMinutes));

            while (await timer.WaitForNextTickAsync())
            {

                _logger.LogInformation("Checking expired refresh tokens at {Now}", DateTime.UtcNow);

                try
                {
                    using var scope = _serviceFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var tokens =
                        await context.RefreshTokens.Where(r => r.ExpiresAt <= DateTimeOffset.UtcNow &&
                        r.RevokedAt == null)
                        .ToListAsync(stoppingToken);

                    if (tokens.Any())
                    {
                        foreach (var token in tokens)
                        {
                            var result = token.Revoke();
                            if (result.IsError)
                                _logger.LogWarning("Failed to revoke token {Id}: {Error}", token.Id, result.Errors);
                        }

                        await context.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation(@"Refresh Token Refoked {Count} : {Ids}", tokens.Count
                            ,tokens.Select(w => w.Id));
                     }
                    else
                    {
                        _logger.LogInformation("No revoked tokens found.");
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
