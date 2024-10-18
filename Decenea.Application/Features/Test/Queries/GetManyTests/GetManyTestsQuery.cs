using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;

namespace Decenea.Application.Features.Test.Queries.GetManyTests;

public class GetManyTestsQuery : ICommand<ErrorOr<IEnumerable<TestDto>>>
{
    public required string UserId { get; set; }
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 20;
    public bool IncludeDetails { get; set; } = false;
}