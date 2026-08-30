using Api.OpenApi.Transformers;
using Infrastructure;
using Infrastructure.Common.Options;
using InventoryManagementSystemAPI.ExceptionHandler;
 using InventoryManagementSystemAPI.Services;
using InventoryManagementSystemAPI.Shared.Converters;
using InventoryManagementSystemAPI.Shared.Interfaces;
using InventoryManagementSystemAPI.Shared.Middewares;
using InventoryManagementSystemAPI.Shared.Middlewares;
using Inventory.Api.OpenApi.Transformers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;
using Serilog;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace InventoryManagementSystemAPI
{
    public static class DependencyInjection
    {
      
        private static IServiceCollection AddOpenAPIDocumentation(this IServiceCollection services)
        {
            string[] versions = ["v1"];

            foreach (var version in versions)
            {
                services.AddOpenApi(version, options =>
                {
                     options.AddDocumentTransformer<VersionInfoTransformer>();

                     options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                    options.AddOperationTransformer<BearerSecuritySchemeTransformer>();
                });
            }

            return services;
        }
        private static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

  
            return services;
        }
        private static IServiceCollection AddResponseCompressoinToProgram(this IServiceCollection services)
        {

            services.AddResponseCompression(option => {
                option.EnableForHttps = true;
                option.Providers.Add<GzipCompressionProvider>();
                option.Providers.Add<BrotliCompressionProvider>();
                option.MimeTypes = new[] {
      "text/plain",
        "text/css",
        "text/html",
        "text/javascript",

        "application/javascript",
        "application/json",
        "application/xml",
        "text/xml",

        "application/problem+json",
        "application/problem+xml",

        "application/octet-stream",

        "image/svg+xml"
                };
            });

            services.Configure<GzipCompressionProviderOptions>(options => {
                options.Level = System.IO.Compression.CompressionLevel.Fastest; 
            });

            services.Configure<BrotliCompressionProviderOptions>(options => {
                options.Level = System.IO.Compression.CompressionLevel.Fastest;
            });

            return services;
        }
        private static IServiceCollection AddApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            return services;
        }
        private static IServiceCollection AddInjectedServices(this IServiceCollection services)
        {
            services.AddScoped<UserTimeZone>();
 
            return services;
        }
        private static IServiceCollection AddJsonConverters(this IServiceCollection services) {

            services.AddControllers()
                    .AddJsonOptions(options =>
                     {
                         options.JsonSerializerOptions.Converters
                          .Add(new FlexibleDateOnlyJsonConverter());
                         options.JsonSerializerOptions.Converters
                          .Add(new UtcDateTimeOffsetConverter(services
                    .BuildServiceProvider()
                    .GetRequiredService<IHttpContextAccessor>() ));
                         options.JsonSerializerOptions.Converters
                   .Add(new UtcDateTimeConverter(services
             .BuildServiceProvider()
             .GetRequiredService<IHttpContextAccessor>()));
                     });

            return services;
        }
      
        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
  
        public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
        {
            services.AddProblemDetails(options => options.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);
            });

            return services;
        }
        public static IServiceCollection AddRateLimiter(this IServiceCollection services , IConfiguration configuration)
        {

            var config = configuration
      .GetSection("RateLimiting")
      .Get<RateLimitingOptions>();

            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter =
                    PartitionedRateLimiter.Create<HttpContext, string>(context =>
                    {
                        var ip =
                            context.Connection.RemoteIpAddress?.ToString()
                            ?? "anonymous";

                        return RateLimitPartition.GetSlidingWindowLimiter(
                            partitionKey: ip,
                            factory: _ => new SlidingWindowRateLimiterOptions
                            {
                                PermitLimit =
                                    config?.Global?.PermitLimit ?? 100,

                                Window = TimeSpan.FromMinutes(
                                    config?.Global?.WindowInMinutes ?? 1),

                                SegmentsPerWindow =
                                    config?.Global?.SegmentsPerWindow ?? 6,

                                QueueLimit =
                                    config?.Global?.QueueLimit ?? 10,

                                AutoReplenishment =
                                    config?.Global?.AutoReplenishment ?? true,

                                QueueProcessingOrder =
                                    QueueProcessingOrder.OldestFirst
                            });
                    });

                options.AddPolicy("AuthLimiter", context =>
                {
                    var ip =
                        context.Connection.RemoteIpAddress?.ToString()
                        ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: ip,
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit =
                                config?.Auth?.PermitLimit ?? 5,

                            Window = TimeSpan.FromMinutes(
                                config?.Auth?.WindowInMinutes ?? 1),

                            QueueLimit =
                                config?.Auth?.QueueLimit ?? 0,

                            QueueProcessingOrder =
                                QueueProcessingOrder.OldestFirst,

                            AutoReplenishment = true
                        });
                });
            });

            return services;
        }
        public static IServiceCollection AddOpenTelemetry(this IServiceCollection services , 
            IConfiguration configuration)
        {
            services.AddOpenTelemetry()
     .ConfigureResource(resources =>
     {
         resources.AddService("InventoryManagementSystemAPI");
     })
 .WithTracing(tracing =>
      tracing.AddAspNetCoreInstrumentation()
     .AddHttpClientInstrumentation()
     .AddOtlpExporter(option =>
     {
         option.Protocol = OtlpExportProtocol.HttpProtobuf;
         option.Endpoint = new Uri(configuration["Otlp:Endpoint"] ?? "http://localhost:8080/ingest/otlp/v1/traces");
     })).WithMetrics(metrice =>
        metrice.AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddPrometheusExporter());

            return services;
        }
        public static IServiceCollection AddHealthCheck(this IServiceCollection services,
            IConfiguration configurations) {
            services.AddHealthChecks();                   
            return services;
        }
        public static IServiceCollection AddProgramServices(this IServiceCollection services , IConfiguration configuration) {

            services.AddControllers();
            services
                .AddOpenTelemetry(configuration)
                .AddHealthCheck(configuration)
                .AddCustomProblemDetails()
                .AddRateLimiter(configuration)
                .AddInjectedServices()
                .AddApiVersioning()
                .AddExceptionHandling()
                .AddJsonConverters()
                .AddResponseCompressoinToProgram()
                .AddIdentityInfrastructure()
                .AddOpenAPIDocumentation()
                ;
 

            return services;
        }
        public static IApplicationBuilder UseCoreMiddlewares(this IApplicationBuilder app
            , IConfiguration configuration)
        {

            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            
            app.UseSerilogRequestLogging();

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            app.UseRateLimiter();
            app.UseHealthChecks("/health");

            app.UseOpenTelemetryPrometheusScrapingEndpoint();

            app.UseAuthentication();

            app.UseMiddleware<UserTimeZoneMiddleware>();
            app.UseMiddleware<WorkingHoursMiddleware>();
            app.UseMiddleware<UnauthorizedLoggingMiddleware>();

            app.UseAuthorization();

            app.UseResponseCompression();
 
            app.UseOutputCache();
           
            return app;
         }

      


    }
}

