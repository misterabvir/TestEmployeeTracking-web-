using Entities.Abstractions.General;

namespace Entities.Users.ValueObjects;

/// <summary>
/// Email of <see cref="User"/>
/// </summary>
public sealed class Email : ValueObject
{
    /// <summary>
    /// Value of <see cref="Email"/>
    /// </summary>
    public string Value { get; private set; } = null!;
    
    private Email() { }

    /// <summary>
    /// Create <see cref="Email"/>
    /// </summary>
    /// <param name="value"> Value of <see cref="Email"/></param>
    /// <returns> Domain entity of <see cref="Email"/> </returns>
    public static Email Create(string value)
        => new() { Value = value };
    
    /// <summary>
    /// Get components for equality
    /// </summary>
    /// <returns> Components for equality </returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
