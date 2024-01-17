using Decenea.Application.Location.Queries.GetManyCities;
using Decenea.Application.Tests.Commands.CreateTest;
using Decenea.Application.Tests.Commands.UpdateTest;
using Decenea.Application.Tests.Queries.GetManyTests;
using Decenea.Application.Tests.Queries.GetTest;
using Decenea.Application.Users.Commands.LoginUser;
using Decenea.Application.Users.Commands.RegenerateAuthTokens;
using Decenea.Application.Users.Commands.RegisterUser;
using Decenea.Application.Users.Commands.UpdateUser;
using Decenea.Application.Users.Queries.GetManyUsers;
using Microsoft.Extensions.DependencyInjection;

namespace Decenea.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<CreateTestCommandHandler>();
        services.AddTransient<UpdateTestCommandHandler>();
        services.AddTransient<GetManyTestsQueryHandler>();
        services.AddTransient<GetTestQueryHandler>();
        
        services.AddTransient<LoginUserCommandHandler>();
        services.AddTransient<RegenerateAuthTokensCommandHandler>();
        services.AddTransient<RegisterUserCommandHandler>();
        services.AddTransient<UpdateUserCommandHandler>();
        services.AddTransient<GetManyUsersQueryHandler>();
        
        services.AddTransient<GetManyCitiesQueryHandler>();
        return services;
    }
}