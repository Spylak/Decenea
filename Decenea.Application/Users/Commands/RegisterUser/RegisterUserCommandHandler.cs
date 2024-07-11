using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Helpers;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Domain.Aggregates.UserAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Decenea.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result<UserDto, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public RegisterUserCommandHandler(IDeceneaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    public async Task<Result<UserDto, Exception>> ExecuteAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(i => i.Email == command.Email, cancellationToken);

            if (existingUser is not null)
            {
                return Result<UserDto, Exception>.Anticipated(null,["Email already in use."]);
            }
            
            var passwordHelper = new PasswordHelper(Convert.FromBase64String(_configuration["Auth:Pepper"]));
            var validatePassword = passwordHelper.ValidatePassword(command.Password);
        
            if (!validatePassword.IsSuccess)
                return Result<UserDto, Exception>.Anticipated(null, validatePassword.Messages);
        
            var passHash = passwordHelper.HashPassword(command.Password);
            var user = User.Create(command.FirstName,
                command.Email,
                command.UserName,
                command.LastName,
                command.MiddleName,
                command.PhoneNumber,
                passHash);

            if (!user.IsSuccess || user.Value is null)
                return Result<UserDto, Exception>.Anticipated(null, user.Messages);;
            
            _dbContext.CreatedBy = user.Value.Id;

            await _dbContext.Set<User>()
                .AddAsync(user.Value, cancellationToken);
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            
            if (!result.IsSuccess)
                return Result<UserDto, Exception>.Anticipated(null, result.Messages);
            var userDto = user.Value.UserToUserDto();
            return Result<UserDto, Exception>.Anticipated(userDto,["Successfully registered user."]);
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong registering user {email} : {ex}",command.Email,ex);
            return Result<UserDto, Exception>.Excepted(ex);
        }
    }
}