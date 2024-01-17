using Decenea.Application.Abstractions.Persistance.IRepositories;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;


namespace Decenea.Application.Tests.Queries.GetManyTests;

public class GetManyTestsQueryHandler
{
    private readonly ITestRepository _testRepository;

    public GetManyTestsQueryHandler(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public async ValueTask<Result<IEnumerable<TestDto>, Exception>> Handle(GetManyTestsQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var testDtos = await _testRepository
                .GetManyTestDtos(query.CountryId,
                    query.CityId,
                    query.CommunityId,
                    query.MunicipalUnitId,
                    query.MunicipalityId,
                    query.RegionalUnitId,
                    query.RegionId);

            return Result<IEnumerable<TestDto>, Exception>.Anticipated(testDtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<TestDto>, Exception>
                .Excepted(ex, "Didn't manage to get list of Micro Ads.");
        }
    }
}