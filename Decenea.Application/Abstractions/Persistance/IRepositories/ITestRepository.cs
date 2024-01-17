using Decenea.Common.DataTransferObjects.Test;

namespace Decenea.Application.Abstractions.Persistance.IRepositories;

public interface ITestRepository
{
    ValueTask<TestDto> GetTestDtoByTestId(string testId, string? userId = null);

    ValueTask<IEnumerable<TestDto>> GetManyTestDtos(int countryId,
        string? cityId = null,
        string? communityId = null,
        string? municipalUnitId = null,
        string? municipalityId = null,
        string? regionalUnitId = null,
        string? regionId = null);
}