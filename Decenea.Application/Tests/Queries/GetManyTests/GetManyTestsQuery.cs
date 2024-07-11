using Decenea.Application.Abstractions.Messaging;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;


namespace Decenea.Application.Tests.Queries.GetManyTests;

public class GetManyTestsQuery : ICommand<Result<IEnumerable<TestDto>, Exception>>
{
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 20;
}