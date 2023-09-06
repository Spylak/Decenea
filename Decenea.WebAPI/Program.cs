global using FastEndpoints;
using Decenea.Application;
using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Decenea.Infrastructure;
using Decenea.Infrastructure.DataSeed;
using Decenea.Infrastructure.Persistance;
using Decenea.WebAPI.ServiceCollections;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true)
    .CreateLogger();

builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");

builder.Services
    .AddFastEndpoints()
    .AddInfrastructure(builder.Configuration);

builder.Services.SwaggerDocument()
    .AddApplication(); 

builder.AddAuthServices();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(c => {
    c.Endpoints.RoutePrefix = "api";
});
app.UseSwaggerGen();

app.Run();