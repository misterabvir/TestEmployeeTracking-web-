using ApplicationCore.Abstractions.Services;

namespace TimeService;

/// <summary>
/// Service for current date and time
/// </summary>
public class DateTimeService : IDateTimeService
{
    /// <summary>
    /// Today date (Utc)
    /// </summary>
    public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
    /// <summary>
    /// Time now (Utc)
    /// </summary>
    public TimeOnly UtcNow => TimeOnly.FromDateTime(DateTime.UtcNow);
   
}
