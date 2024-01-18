using Entities.Abstractions.General;

namespace Entities.Employees.ValueObjects;

/// <summary>
/// Last name of <see cref="Employee"/>
/// </summary>
public sealed class LastName : ValueObject
{
    private LastName() { }
    
    /// <summary>
    /// Value of <see cref="LastName"/>
    /// </summary>
    /// <value></value>
    public string Value { get; private set; } = default!;

    /// <summary>
    /// Create <see cref="LastName"/>
    /// </summary>
    /// <param name="lastName"> Value of <see cref="LastName"/></param>
    /// <returns> Domain entity of <see cref="LastName"/> </returns>
    public static LastName Create(string lastName)
        => new() { Value = lastName };

    /// <summary>
    /// Get components for equality
    /// </summary>
    /// <returns> Components for equality </returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
