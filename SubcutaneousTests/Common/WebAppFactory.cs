using Contract.Common.Interfaces;
using Infrastructure.BackgroundServices;
using Infrastructure.Common.Options;
using Infrastructure.Data;
using InventoryManagementSystemAPI;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Testcontainers.MsSql;
using Xunit;

namespace SubcutaneousTests.Common;

public class WebAppFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{

    private readonly MsSqlContainer _dbContainer =
        new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public IMediator CreateMediator()
    {
         var serviceScope = Services.CreateScope();

        return serviceScope.ServiceProvider.GetRequiredService<IMediator>();
    }

    public IAppDbContext CreateAppDbContext()
    {
        var serviceScope = Services.CreateScope();

        return serviceScope.ServiceProvider.GetRequiredService<IAppDbContext>();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

      }

    public new Task DisposeAsync() => _dbContainer.StopAsync();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IHostedService>();
            services.RemoveAll<OrderCancellationBackgroundService>();
            services.RemoveAll<RefreshTokenInvokerBackgroundService>();

            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(_dbContainer.GetConnectionString());
            });

            services.RemoveAll<AppSettings>();

            // Explicit override AFTER Configure
            services.PostConfigure<AppSettings>(opts =>
            {
                opts.OpenAt = TimeSpan.FromHours(3);
                opts.CloseAt = TimeSpan.FromHours(18);
            });
        });
        //private readonly MsSqlContainer _msSqlContainer =
        //    new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        //        .Build();

        //public IMediator CreateMediator()
        //{
        //    var scope = Services.CreateScope();
        //    return scope.ServiceProvider.GetRequiredService<IMediator>();
        //}

        //public IAppDbContext CreateAppDbContext()
        //{
        //    var scope = Services.CreateScope();
        //    return scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        //}

        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.ConfigureTestServices(services =>
        //    {
        //        services.RemoveAll<IHostedService>();
        //        services.RemoveAll<OrderCancellationBackgroundService>();
        //        services.RemoveAll<RefreshTokenInvokerBackgroundService>();

        //        services.RemoveAll<DbContextOptions<AppDbContext>>();

        //        services.AddDbContext<AppDbContext>((sp, options) =>
        //        {
        //            options.UseSqlServer(_msSqlContainer.GetConnectionString());
        //            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        //        });

        //        services.RemoveAll<AppSettings>();

        //        services.PostConfigure<AppSettings>(options =>
        //        {
        //            options.OpenAt = TimeSpan.FromHours(8);
        //            options.CloseAt = TimeSpan.FromHours(16);
        //        });
        //    });
        //}

        //public async Task InitializeAsync()
        //{
        //    await _msSqlContainer.StartAsync();

        //     using var scope = Services.CreateScope();

        //     var context = scope.ServiceProvider
        //        .GetRequiredService<AppDbContext>();

        //     await context.Database.MigrateAsync();
        //}

        //public new async Task DisposeAsync()
        //{
        //    await _msSqlContainer.DisposeAsync();
        //}
    }

   
}