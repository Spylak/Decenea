using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Decenea.Infrastructure.Persistence.Converters;

public class DateTimeToDateTimeUtc : ValueConverter<DateTime, DateTime>
{
    public DateTimeToDateTimeUtc() : base(c => DateTime.SpecifyKind(c, DateTimeKind.Utc), c => c)
    {

    }
}