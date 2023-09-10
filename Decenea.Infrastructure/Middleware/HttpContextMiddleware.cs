using System.Security.Claims;
using Decenea.Application.Abstractions.Persistance;
using Microsoft.AspNetCore.Http;

namespace Decenea.Infrastructure.Middleware;

public class HttpContextMiddleware
{
    private readonly RequestDelegate _next;

    public HttpContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IDeceneaDbContext dbContext)
    {
        var userId = context
            .User
            .FindFirstValue("userId");

        dbContext.CreatedBy = userId;
        await _next(context);
    }
}
