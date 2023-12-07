using Entities.Abstractions;

namespace Entities.Employees.ValueObjects;

public sealed class FirstName : ValueObject
{
    private FirstName() { }
    public string Value { get; private set; } = default!;

    public static FirstName Create(string fistName)
        => new() { Value = fistName };

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}