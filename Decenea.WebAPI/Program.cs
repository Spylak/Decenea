global using FastEndpoints;
global using FastEndpoints.Security;
using Decenea.WebAPI.ServiceCollections;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();

builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .Build();

builder.Services.AddFastEndpoints();
builder.Services.AddJWTBearerAuth(builder.Configuration["JWTSigningKey"]);
builder.Services.SwaggerDocument(); 

InfrastructureServices.AddInfrastructureServices(builder);
ApplicationServices.AddApplicationServices(builder);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(c => {
    c.Endpoints.RoutePrefix = "api";
});
app.UseSwaggerGen();
app.Run();