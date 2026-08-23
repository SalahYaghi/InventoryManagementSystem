using Contract;
using Infrastructure.Data;
using InventoryManagementSystemAPI;
using InventoryManagementSystemAPI.Shared.Middewares;
using InventoryManagementSystemAPI.Shared.Middlewares;
using Microsoft.AspNetCore.RateLimiting;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;
using static Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services
                .AddProgramServices(builder.Configuration)
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCoreMiddlewares(builder.Configuration);

if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();

    await app.InitialiseDatabaseAsync();

    app.UseSwaggerUI(options =>
    {
        


        options.SwaggerEndpoint("/openapi/v1.json", "Project API V1");
        options.SwaggerEndpoint("/openapi/v2.json", "Project API V2");

        options.EnableDeepLinking();
        options.DisplayRequestDuration();
        options.EnableFilter();
    });

    app.MapScalarApiReference();
}

app.MapControllerRoute( "default" , "{controller=Home}/{action=Index}/{id?}"); 


app.Run();

