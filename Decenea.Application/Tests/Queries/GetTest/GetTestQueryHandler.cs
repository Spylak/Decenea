using Decenea.Application.Abstractions.Persistance.IRepositories;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;


namespace Decenea.Application.Tests.Queries.GetTest;

public class GetTestQueryHandler
{
    private readonly ITestRepository _testRepository;

    public GetTestQueryHandler(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }
    public async Task<Result<TestDto, Exception>> Handle(GetTestQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var testDto = await _testRepository
                .GetTestDtoByTestId(query.Id);

            return Result<TestDto, Exception>.Anticipated(testDto);
        }
        catch (Exception ex)
        {
            return Result<TestDto, Exception>
                .Excepted(ex, ["Didn't manage to get Micro Ad."]);
        }
    }
}