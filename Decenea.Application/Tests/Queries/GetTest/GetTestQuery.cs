using ErrorOr;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;


namespace Decenea.Application.Tests.Queries.GetTest;

public class GetTestQuery : ICommand<ErrorOr<TestDto>>
{
    public required string Id { get; set; }
}