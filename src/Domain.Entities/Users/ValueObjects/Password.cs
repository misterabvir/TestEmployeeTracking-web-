using Entities.Abstractions.General;

namespace Entities.Users.ValueObjects;

/// <summary>
/// Password of <see cref="User"/>
/// </summary>
public sealed class Password : ValueObject
{
    /// <summary>
    /// Value of <see cref="Password"/>
    /// </summary>
    public string Value { get; private set; } = null!;

    private Password() { }

    /// <summary>
    /// Create <see cref="Password"/>
    /// </summary>
    /// <param name="value"> Value of <see cref="Password"/></param>
    /// <returns> Domain entity of <see cref="Password"/></returns>
    public static Password Create(string value)
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
