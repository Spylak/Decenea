using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.AdvertisementAggregate;
using Mediator;
using Serilog;

namespace Decenea.Application.Advertisements.Commands.CreateMicroAd;

public class CreateMicroAdCommandHandler : ICommandHandler<CreateMicroAdCommand,Result<object,Exception>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public CreateMicroAdCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result<object, Exception>> Handle(CreateMicroAdCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newMicroAd = new MicroAd()
            {
                Title = command.Title,
                Description = command.Description,
                CityId = command.CityId,
                UserId = command.UserId,
                ContactEmail = command.ContactEmail,
                ContactPhone = command.ContactPhone
            };
            
            await _dbContext.Set<MicroAd>().AddAsync(newMicroAd, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result<object, Exception>.Anticipated(null,"Successfully created!");
        }
        catch (Exception ex)
        {
            Log.Error("Failed to CreateMicroAd from request: {command} with error: {ex}", command,ex);
            return Result<object, Exception>.Excepted(ex);
        }
    }
}