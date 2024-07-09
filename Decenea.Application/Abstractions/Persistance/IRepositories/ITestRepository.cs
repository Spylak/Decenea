using Decenea.Common.DataTransferObjects.Test;

namespace Decenea.Application.Abstractions.Persistance.IRepositories;

public interface ITestRepository
{
    Task<TestDto> GetTestDtoByTestId(string testId, string? userId = null);

    Task<IEnumerable<TestDto>> GetManyTestDtos(int countryId,
        string? communityId = null,
        string? municipalUnitId = null,
        string? municipalityId = null,
        string? regionalUnitId = null,
        string? regionId = null);
}