using System.Text.Json;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.UserAggregate;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result<object,Exception>>
{
    private readonly IDeceneaDbContext _dbContext;
    public RegisterUserCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Result<object, Exception>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(i => i.Email == command.Email, cancellationToken);

            if (existingUser is not null)
            {
                return Result<object, Exception>.Anticipated(null,"Email already in use.");
            }
            
            var user = User.Create(command.FirstName,
                command.Email,
                command.UserName,
                command.LastName,
                command.MiddleName,
                command.PhoneNumber,
                command.CityId,
                command.Password);
            
            await _dbContext.Set<User>()
                .AddAsync(user, cancellationToken);
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            
            if (result == 0)
                return Result<object, Exception>.Anticipated(null,"Something went wrong.");
            
            return Result<object, Exception>.Anticipated(1,"Successfully registered user.");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong registering user {email} : {ex}",command.Email,ex);
            return Result<object, Exception>.Excepted(ex);
        }
    }
}