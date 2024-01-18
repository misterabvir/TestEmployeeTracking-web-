namespace ApplicationCore.Abstractions.Services;

/// <summary>
/// Service for date and time
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Get Today Date
    /// </summary>
    /// <value> Today Date </value>
    public DateOnly Today { get; }
    /// <summary>
    /// Get time now
    /// </summary>
    /// <value> Time now </value>
    public TimeOnly UtcNow { get; }
}
