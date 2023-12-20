using ApplicationCore.Abstractions.Services;

namespace TimeService;

public class DateTimeService : IDateTimeService
{
    public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
    public TimeOnly UtcNow => TimeOnly.FromDateTime(DateTime.UtcNow);
   
}
