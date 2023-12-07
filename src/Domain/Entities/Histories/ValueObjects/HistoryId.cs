using Entities.Abstractions;

namespace Entities.Histories.ValueObjects;

public sealed class HistoryId : Id
{
    private HistoryId() { }

    public static HistoryId Create(Guid id)
        => new() { Value = id };

    public static HistoryId CreateUnique()
        => new() { Value = Guid.NewGuid() };

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
