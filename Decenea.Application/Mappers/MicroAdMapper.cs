using Decenea.Common.DataTransferObjects.Advertisement;
using Decenea.Domain.Aggregates.MicroAdAggregate;

namespace Decenea.Application.Mappers;

public static class MicroAdMapper
{
    public static MicroAdDto MicroAdToMicroAdDto(this MicroAd microAd, MicroAdDto? microAdDto = null)
    {
        microAdDto ??= new MicroAdDto();
        microAdDto.Title = microAd.Title;
        microAdDto.Description = microAd.Description;
        microAdDto.ContactPhone = microAd.ContactPhone;
        microAdDto.ContactEmail = microAd.ContactEmail;
        microAdDto.UserId = microAd.UserId;
        // microAdDto.ApplicationUserName = microAd.ApplicationUser.FullName;
        return microAdDto;
    }
}