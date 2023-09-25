using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.MicroAdAggregate;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.MicroAds.Commands.UpdateMicroAd;

public class UpdateMicroAdCommandHandler : ICommandHandler<UpdateMicroAdCommand, Result<object, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;

    public UpdateMicroAdCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result<object, Exception>> Handle(UpdateMicroAdCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var existingMicroAd = await _dbContext
                .Set<MicroAd>()
                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if (existingMicroAd is null)
                return Result<object, Exception>.Anticipated(null, "MicroAd not found.");

            if (existingMicroAd.Version != command.Version)
                return Result<object, Exception>.Anticipated(existingMicroAd, "Concurrency issue.", false);

            var updateResult = MicroAd.Update(existingMicroAd, command.Title,
                command.Description,
                command.CityId,
                command.ContactEmail,
                command.ContactPhone);

            if (!updateResult.IsSuccess)
                return Result<object, Exception>.Anticipated(null, updateResult.Messages);

            _dbContext.Set<MicroAd>().Update(existingMicroAd);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Result<object, Exception>.Anticipated(null, "Successfully updated entity.", true);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpdateMicroAd from request: {command} with error: {ex}", command, ex);
            return Result<object, Exception>.Excepted(ex);
        }
    }
}