using Decenea.Domain.Entities.AdvertisementEntities;
using Decenea.Shared.DataTransferObjects.Advertisement;

namespace Decenea.Domain.Mappers;

public static class MicroAdMapper
{
    public static MicroAdDto MicroAdToMicroAdDto(this MicroAd microAd, MicroAdDto? microAdDto = null)
    {
        microAdDto ??= new MicroAdDto();
        microAdDto.Title = microAd.Title;
        microAdDto.Description = microAd.Description;
        microAdDto.ContactPhone = microAd.ContactPhone;
        microAdDto.ContactEmail = microAd.ContactEmail;
        microAdDto.ApplicationUserName = microAd.ApplicationUser.FullName;
        microAdDto.Location = microAd.Title;
        return microAdDto;
    }
}