using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;


namespace Decenea.Application.Tests.Queries.GetTest;

public class GetTestQuery : ICommand<Result<TestDto, Exception>>
{
    public required string Id { get; set; }
}