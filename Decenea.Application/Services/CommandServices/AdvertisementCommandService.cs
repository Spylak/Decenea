using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.Aggregates.AdvertisementAggregate;
using Decenea.Domain.Common;
using Decenea.Infrastructure.Persistance;
using Decenea.Shared.DataTransferObjects.Advertisement;
using Serilog;

namespace Decenea.Application.Services.CommandServices;

public class AdvertisementCommandService : IAdvertisementCommandService
{
    private readonly DeceneaDbContext _dbContext;

    public AdvertisementCommandService(DeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<object, Exception>> CreateMicroAd(CreateMicroAdServiceRequestDto requestDto)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var newMicroAd = new MicroAd()
            {
                Title = requestDto.Title,
                Description = requestDto.Description,
                CityId = requestDto.CityId,
                ApplicationUserId = requestDto.ApplicationUserId,
                ContactEmail = requestDto.ContactEmail,
                ContactPhone = requestDto.ContactPhone
            };
            
            await _dbContext.Set<MicroAd>().AddAsync(newMicroAd);
            await _dbContext.SaveChangesAsync(requestDto.ApplicationUserId.ToString());
            await transaction.CommitAsync();
            return Result<object, Exception>.Anticipated(null,"Successfully created!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Log.Error("Failed to CreateMicroAd from request: {requestDto} with error: {ex}",requestDto,ex);
            return Result<object, Exception>.Excepted(ex);
        }
    }
}