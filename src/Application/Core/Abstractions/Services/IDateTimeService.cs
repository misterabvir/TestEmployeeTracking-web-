namespace ApplicationCore.Abstractions.Services;

public interface IDateTimeService
{
    public DateOnly Today { get; }
    public TimeOnly UtcNow { get; }
}
