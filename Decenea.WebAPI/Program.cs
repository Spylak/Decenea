global using FastEndpoints;
using Decenea.Application;
using Decenea.Infrastructure;
using Decenea.Infrastructure.Middleware;
using FastEndpoints.Swagger;
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
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.SwaggerDocument();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<HttpContextMiddleware>();
app.UseFastEndpoints(c => {
    c.Endpoints.RoutePrefix = "api";
});
app.UseSwaggerGen();

app.Run();