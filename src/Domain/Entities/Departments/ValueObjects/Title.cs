using Entities.Abstractions;

namespace Entities.Departments.ValueObjects;

public sealed class Title : ValueObject
{
    private Title() { }
    public string Value { get; private set; } = default!;

    public static Title Create(string title)
        => new() { Value = title };

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
