using Entities.Abstractions.Shared;

namespace Entities.Histories.ValueObjects;

/// <summary>
/// Domain entity Id for <see cref="History"/>
/// </summary>
public sealed class HistoryId : Id
{
    private HistoryId() { }

    /// <summary>
    /// Create <see cref="HistoryId"/>
    /// </summary>
    /// <param name="id"> Value of <see cref="HistoryId"/></param>
    /// <returns> Domain entity of <see cref="HistoryId"/></returns>
    public static HistoryId Create(Guid id)
        => new() { Value = id };

    /// <summary>
    /// Create <see cref="HistoryId"/>
    /// </summary>
    /// <returns> Domain entity of <see cref="HistoryId"/></returns>
    public static HistoryId CreateUnique()
        => new() { Value = Guid.NewGuid() };

    /// <summary>
    /// Get components for equality
    /// </summary>
    /// <returns> Components for equality </returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
