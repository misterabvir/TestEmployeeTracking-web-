using Entities.Abstractions.General;

namespace Entities.Employees.ValueObjects;

/// <summary>
/// First name of <see cref="Employee"/>
/// </summary>
public sealed class FirstName : ValueObject
{
    private FirstName() { }
    /// <summary>
    /// Value of <see cref="FirstName"/>
    /// </summary>
    public string Value { get; private set; } = default!;

    /// <summary>
    /// Create <see cref="FirstName"/>
    /// </summary>
    /// <param name="fistName"></param>
    /// <returns></returns>
    public static FirstName Create(string fistName)
        => new() { Value = fistName };

    /// <summary>
    /// Get components for equality
    /// </summary>
    /// <returns> Components for equality </returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}