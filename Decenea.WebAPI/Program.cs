global using FastEndpoints;
using Decenea.Application;
using Decenea.Domain.Entities.ApplicationUserEntities;
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

using var scope = app.Services.CreateScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
var db = scope.ServiceProvider.GetRequiredService<DeceneaDbContext>();
db.Database.Migrate();
await ApplicationUserSeed.Seed(db,userManager);

app.Run();