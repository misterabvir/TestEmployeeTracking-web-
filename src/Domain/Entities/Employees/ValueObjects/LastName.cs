using Entities.Abstractions;

namespace Entities.Employees.ValueObjects;

public sealed class LastName : ValueObject
{
    private LastName() { }
    public string Value { get; private set; } = default!;

    public static LastName Create(string lastName)
        => new() { Value = lastName };

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
