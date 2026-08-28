using Application.Common.Constants;
using Application.Common.Interfaces;
using Contract.Common.Interfaces;
using Domain.AuditLoggs;
using HotelManagementSystemAPI.Helpers;
using Infrastructure.BackgroundServices;
using Infrastructure.Common.Options;
using Infrastructure.Data;
using Infrastructure.Data.Configurations;
using Infrastructure.Data.Interceptors;
using Infrastructure.Data.Interseptors;
using Infrastructure.Identity;
using Infrastructure.Identity.Policies;
using Infrastructure.Policies.OutputCachePolicies;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {

        private static IServiceCollection RegisterBackgroundServices(this IServiceCollection services)
        {

            services.AddHostedService<RefreshTokenInvokerBackgroundService>();
            services.AddHostedService< RedisHealthCheckBackgroundService>();
            services.AddHostedService<OrderCancellationBackgroundService>();
            return services;
        }

        private static IServiceCollection AddOutputCache(
            this IServiceCollection services,
            CachingSettings? settings) {

            services.AddOutputCache(options => {
                options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(settings?.OutputCache?.DefaultCacheExpirationSeconds ?? 60);
            options.MaximumBodySize = settings?.OutputCache?.MaxbodySize ?? 1024 * 1024 * 60;
            options.SizeLimit = settings?.OutputCache?.MaxCacheSize ?? 1024 * 1024 * 100;
                options.AddPolicy(nameof(AuthenticatedUserCachePolicy) , new AuthenticatedUserCachePolicy());
            }
            );

            return services;
        }



        private static IServiceCollection AddCache(this IServiceCollection services 
            , IConfiguration configuration)
        {

            var settings = configuration.GetSection("Caching").Get<CachingSettings>();

            
            services.AddOutputCache(settings);
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                ConnectRetry = settings?.Redis.ConnectRetry ?? 3,
                ConnectTimeout = settings?.Redis.ConnectTimeout ?? 1000,
                AsyncTimeout = settings?.Redis.AsyncTimeout ?? 300,
                SyncTimeout = settings?.Redis.SyncTimeout ?? 300,
                BacklogPolicy = BacklogPolicy.FailFast
            };

            services.AddSingleton<IConnectionMultiplexer>((sp) =>
            {


    options.EndPoints.Add(settings?.Redis.ConnectionString ?? "localhost:6379");

    return ConnectionMultiplexer.Connect(options);         });


            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints = { settings?.Redis.ConnectionString ?? "localhost:6379" },
                    AbortOnConnectFail = settings?.Redis.AbortOnConnectFail ?? false,
                    ConnectRetry = settings?.Redis.ConnectRetry ?? 3,
                    ConnectTimeout = settings?.Redis.ConnectTimeout ?? 1000,
                    AsyncTimeout = settings?.Redis.AsyncTimeout ?? 300,
                    SyncTimeout = settings?.Redis.SyncTimeout ?? 300,
                    BacklogPolicy = StackExchange.Redis.BacklogPolicy.FailFast,


                };
                options.InstanceName = settings?.Redis.InstanceName ?? "Inventory System API";
            });

            
            services.AddHybridCache(  options =>
            {
                options.MaximumPayloadBytes = settings?.MaximumPayloadBytes ?? 1024*1024*5;
                options.MaximumKeyLength = settings?.MaximumKeyLength ?? 512;
                options.DefaultEntryOptions = new HybridCacheEntryOptions
                {
                    Expiration = TimeSpan.FromMinutes(settings?.DefaultCacheExpirationMinutes ?? 10),
                    LocalCacheExpiration = TimeSpan.FromMinutes(settings?.DefaultLocalCacheExpirationMinutes ?? 2)
                };
            });

            return services;
        }
       


        public static IServiceCollection RegisterSecurity(this IServiceCollection services,
            IConfiguration configuration) {
    
            var jwtOptions = configuration
       .GetSection("JwtSettings")
       .Get<JwtOptions>();

            services.AddAuthentication(optoins => {
                optoins.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                optoins.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        
            }).AddJwtBearer(options => {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,

                    ValidIssuer = jwtOptions!.Issuer ,
                    ValidAudience = jwtOptions.Audience ,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    ClockSkew = TimeSpan.Zero


                };
            });


            services.AddAuthorizationBuilder()
                   .AddPolicy(PolicyNames.WarehouseUpdate, policy =>
                    policy.Requirements.Add(new WarehouseUpdateRequirement()));



            return services;
        }
        public static IServiceCollection RegisterOptions(this IServiceCollection services , 
            IConfiguration configuration) {

            services.Configure<JwtOptions>(
                configuration.GetSection("JwtSettings")
            );

            services.Configure<RateLimitingOptions>(
                configuration.GetSection("RateLimiting")
            );

            services.Configure<CachingSettings>(
                configuration.GetSection("Caching"));



            services.Configure<AppSettings>(
                configuration.GetSection("AppSettings"));

            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services , IConfiguration configuration)
        {

 
            services.AddDbContext<AppDbContext>((sp , options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                        .AddInterceptors( new EntityUpdateCreateInterceptor(sp.GetRequiredService<IUser>()))
                     .AddInterceptors(new SoftDeleteInterceptor())
                    ;

            });

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {


             services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddScoped<IFileStorage , FilesHelper>();
            services.AddScoped<IOrderPolicies, OrderServices>();
            services.AddScoped<IProductPolicies, ProductServices>();
         
            services.AddScoped<IHashingHelper, HashingService>();

            services.AddScoped<IIdentityService, IdentityService > ();
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddScoped<IInvoicePdfGenerator, InvoicePdfGeneratorServices>();
            services.AddScoped<IAuditLogService, AuditServices>();

            services.AddScoped<INotificationService, NotificationService>();


            services.AddScoped<ICachingService , CachingService>();
            services.AddSingleton<IRedisHealthState, RedisHealthState>();

            services.AddScoped<IAuthorizationHandler , WarehouseUpdateHandler>();

            return services;
        }
        public static IServiceCollection RegisterQuestPDF(this IServiceCollection services) {

            Settings.License = LicenseType.Community;

            FontManager.RegisterFont(File.OpenRead(Path.Combine(AppContext.BaseDirectory, "NotoEmoji-Regular.ttf")));

            FontManager.RegisterFont(File.OpenRead(Path.Combine(AppContext.BaseDirectory, "NotoSansArabic.ttf")));
             
            return services;
        }
        public static IServiceCollection RegisterSerilog(this IServiceCollection services) {



            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterQuestPDF();
            services.AddDatabase(configuration);
            services.RegisterServices(configuration);
            services.RegisterOptions(configuration);
            services.RegisterSecurity(configuration)
                     .AddCache(configuration)
                     .RegisterBackgroundServices();
                      

            return services;
        }

    }
}

