using Decenea.Common.DataTransferObjects.Advertisement;

namespace Decenea.Application.Abstractions.Persistance.IRepositories;

public interface IMicroAdRepository
{
    ValueTask<MicroAdDto> GetMicroAdDtoByMicroAdId(string microAdId, string? userId = null);

    ValueTask<IEnumerable<MicroAdDto>> GetManyMicroAdDtos(int countryId,
        string? cityId = null,
        string? communityId = null,
        string? municipalUnitId = null,
        string? municipalityId = null,
        string? regionalUnitId = null,
        string? regionId = null);
}