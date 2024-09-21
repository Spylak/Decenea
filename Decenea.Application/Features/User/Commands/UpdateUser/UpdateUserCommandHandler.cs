using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Extensions;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, ErrorOr<UserDto>>
{
    private readonly IDeceneaDbContext _dbContext;
    public UpdateUserCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<UserDto>> ExecuteAsync(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _dbContext
                .Set<Domain.Aggregates.UserAggregate.User>()
                .FirstOrDefaultAsync(i => i.Id == command.Id && i.Version == command.Version, cancellationToken);

            if (existingUser is null)
            {
                return Error.NotFound(description: "User not found.");
            }

            if (existingUser.Version != command.Version)
                return ErrorOrExt.ConcurrencyError(existingUser.UserToUserDto());

            var user = Domain.Aggregates.UserAggregate.User.Update(existingUser,
                command.FirstName,
                command.Email,
                command.UserName,
                command.LastName,
                command.MiddleName,
                command.PhoneNumber);

            if (user.IsError)
                return user.Errors;
            
            _dbContext.ModifiedBy = user.Value.Id;

            await _dbContext.Set<Domain.Aggregates.UserAggregate.User>().AddAsync(user.Value, cancellationToken);
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result.IsError)
                return result.Errors;
            
            var userDto = user.Value.UserToUserDto();

            return userDto;
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong updating user {email} : {ex}",command.Email,ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}