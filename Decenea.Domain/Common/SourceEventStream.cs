namespace Decenea.Domain.Common;

public record SourceEventStream
{
    public int AggregateVersion { get; set; }
}