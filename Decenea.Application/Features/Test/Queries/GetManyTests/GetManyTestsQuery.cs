using Decenea.Common.DataTransferObjects.Test;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.Test.Queries.GetManyTests;

public class GetManyTestsQuery : ICommand<ErrorOr<IEnumerable<TestDto>>>
{
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 20;
}