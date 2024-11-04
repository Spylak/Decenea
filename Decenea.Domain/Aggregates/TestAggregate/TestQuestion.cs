using Decenea.Domain.Aggregates.QuestionAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class TestQuestion : LinkingTable
{
    public required string QuestionId { get; set; }
    public Question? Question { get; set; }
    public required string TestId { get; set; }
    public Test? Test { get; set; }
    public int? SecondsToAnswer { get; set; } 
    public int? Order { get; set; } 
    public double? Weight { get; set; }
}