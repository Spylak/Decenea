using ErrorOr;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;


namespace Decenea.Application.Tests.Queries.GetManyTests;

public class GetManyTestsQuery : ICommand<ErrorOr<IEnumerable<TestDto>>>
{
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 20;
}