namespace Core.Abstractions.Services;

public interface IDateTimeService
{
    public DateOnly Today { get; }
    public TimeOnly UtcNow { get; }
}
