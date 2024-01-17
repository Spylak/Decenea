using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler
{
    private readonly IDeceneaDbContext _dbContext;
    public RegisterUserCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Result<UserDto, Exception>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(i => i.Email == command.Email, cancellationToken);

            if (existingUser is not null)
            {
                return Result<UserDto, Exception>.Anticipated(null,"Email already in use.");
            }
            
            var user = User.Create(command.FirstName,
                command.Email,
                command.UserName,
                command.LastName,
                command.MiddleName,
                command.PhoneNumber,
                command.CityId,
                command.Password);

            if (!user.IsSuccess || user.Value is null)
                return Result<UserDto, Exception>.Anticipated(null, user.Messages);;
            
            _dbContext.CreatedBy = user.Value.Id;

            await _dbContext.Set<User>()
                .AddAsync(user.Value, cancellationToken);
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            
            if (!result.IsSuccess)
                return Result<UserDto, Exception>.Anticipated(null, result.Messages);
            var userDto = user.Value.UserToUserDto();
            return Result<UserDto, Exception>.Anticipated(userDto,"Successfully registered user.");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong registering user {email} : {ex}",command.Email,ex);
            return Result<UserDto, Exception>.Excepted(ex);
        }
    }
}