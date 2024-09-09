using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Helpers;
using Decenea.Application.Mappers;
using ErrorOr;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Domain.Aggregates.UserAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Decenea.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ErrorOr<UserDto>>
{
    private readonly IDeceneaDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public RegisterUserCommandHandler(IDeceneaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    public async Task<ErrorOr<UserDto>> ExecuteAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(i => i.Email == command.Email, cancellationToken);

            if (existingUser is not null)
            {
                return Error.NotFound(description: "Email already in use.");
            }
            
            var passwordHelper = new PasswordHelper(Convert.FromBase64String(_configuration["Auth:Pepper"]));
            var validatePassword = passwordHelper.ValidatePassword(command.Password);
        
            if (validatePassword.IsError)
                return validatePassword.Errors;
        
            var passHash = passwordHelper.HashPassword(command.Password);
            var user = User.Create(command.FirstName,
                command.Email,
                command.UserName,
                command.LastName,
                command.MiddleName,
                command.PhoneNumber,
                passHash);

            if (user.IsError)
                return user.Errors;
            
            _dbContext.ModifiedBy = user.Value.Id;

            await _dbContext.Set<User>()
                .AddAsync(user.Value, cancellationToken);
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result.IsError)
                return result.Errors;
            
            var userDto = user.Value.UserToUserDto();
            return userDto;
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong registering user {email} : {ex}",command.Email,ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}