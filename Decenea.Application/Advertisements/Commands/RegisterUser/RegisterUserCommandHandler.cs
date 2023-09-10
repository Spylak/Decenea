using System.Text.Json;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Advertisements.Commands.RegisterUser;

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
                .Set<UserSourceEventStream>()
                .FirstOrDefaultAsync(i => i.Snapshot.Contains($"\"Email\":\"{command.Email}\""), cancellationToken);

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

            var serUser = JsonSerializer.Serialize(user);
            var userRegisterEvent = new UserSourceEvent(0,serUser,user.Id);

            var userRegisterEventStream = new UserSourceEventStream()
            {
                Snapshot = serUser,
                LastEventTimestampUtc = userRegisterEvent.TimestampUtc,
                TimestampUtc = userRegisterEvent.TimestampUtc,
                AggregateVersion = 0,
                SnapshotVersion = 0
            };
            
            userRegisterEventStream.UserSourceEvents.Add(userRegisterEvent);
            
            await _dbContext.Set<UserSourceEventStream>()
                .AddAsync(userRegisterEventStream,cancellationToken);
            
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