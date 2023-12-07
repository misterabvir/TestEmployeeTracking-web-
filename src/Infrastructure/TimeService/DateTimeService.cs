using Core.Abstractions.Services;

namespace TimeService;

public class DateTimeService : IDateTimeService
{ 
    private DateTimeService(){}
    public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
    public TimeOnly UtcNow => TimeOnly.FromDateTime(DateTime.UtcNow);
    private static DateTimeService? _instance;
    public static DateTimeService Instance => _instance ??= new DateTimeService();
}
