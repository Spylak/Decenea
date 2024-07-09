using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler
{
    private readonly IDeceneaDbContext _dbContext;
    public UpdateUserCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<UserDto, Exception>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(i => i.Id == command.Id && i.Version == command.Version, cancellationToken);

            if (existingUser is null)
            {
                return Result<UserDto, Exception>.Anticipated(null,["User not found."]);
            }

            if (existingUser.Version != command.Version)
                return Result<UserDto, Exception>.Anticipated(existingUser.UserToUserDto(), ["Concurrency issue."],false);

            var user = User.Update(existingUser,
                command.FirstName,
                command.Email,
                command.UserName,
                command.LastName,
                command.MiddleName,
                command.PhoneNumber);

            if (!user.IsSuccess || user.Value is null)
                return Result<UserDto, Exception>.Anticipated(null, user.Messages);;
            
            _dbContext.CreatedBy = user.Value.Id;

            await _dbContext.Set<User>()
                .AddAsync(user.Value, cancellationToken);
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            
            if (!result.IsSuccess)
                return Result<UserDto, Exception>.Anticipated(null, result.Messages);
            
            var userDto = user.Value.UserToUserDto();
            
            return Result<UserDto, Exception>.Anticipated(userDto,["Successfully updated user info."]);
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong updating user {email} : {ex}",command.Email,ex);
            return Result<UserDto, Exception>.Excepted(ex);
        }
    }
}