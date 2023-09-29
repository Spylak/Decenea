using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Decenea.Domain.Aggregates.MicroAdAggregate;
using Mediator;
using Serilog;

namespace Decenea.Application.MicroAds.Commands.CreateMicroAd;

public class CreateMicroAdCommandHandler : ICommandHandler<CreateMicroAdCommand,Result<MicroAdDto,Exception>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public CreateMicroAdCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result<MicroAdDto, Exception>> Handle(CreateMicroAdCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var createResult = MicroAd.Create(command.Title,
                command.Description,
                command.CityId,
                command.UserId,
                command.ContactEmail,
                command.ContactPhone);
            
            if(!createResult.IsSuccess || createResult.Value is null)
                return Result<MicroAdDto, Exception>.Anticipated(null, createResult.Messages);

            await _dbContext.Set<MicroAd>().AddAsync(createResult.Value, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result<MicroAdDto, Exception>.Anticipated(createResult.Value.MicroAdToMicroAdDto(),"Successfully created!", true);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to CreateMicroAd from request: {command} with error: {ex}", command,ex);
            return Result<MicroAdDto, Exception>.Excepted(ex);
        }
    }
}