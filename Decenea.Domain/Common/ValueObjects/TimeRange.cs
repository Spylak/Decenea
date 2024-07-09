using Decenea.Common.Common;

namespace Decenea.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        if (end <= start)
            throw new Exception("Start date is greater than end date.");
        Start = start;
        End = end;
    }

    public static Result<TimeRange, Exception> FromDateTimes(DateTime start, DateTime end)
    {
        if (start.Date != end.Date || start >= end)
        {
            return Result<TimeRange, Exception>.Anticipated(null, ["Start date is greater than end date."]);
        }

        return Result<TimeRange, Exception>.Anticipated(new TimeRange(TimeOnly.FromDateTime(start),
            TimeOnly.FromDateTime(end)));
    }

    public bool OverlapsWith(TimeRange other)
    {
        if (Start >= other.End) return false;
        if (other.Start >= End) return false;

        return true;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}