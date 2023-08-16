global using FastEndpoints;
using Decenea.Domain.Entities.ApplicationUser;
using Decenea.Infrastructure.Data;
using Decenea.Infrastructure.DataSeed;
using Decenea.WebAPI.ServiceCollections;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(); 

builder.AddInfrastructureServices();
builder.AddApplicationServices();

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