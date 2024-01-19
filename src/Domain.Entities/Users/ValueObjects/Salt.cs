using Entities.Abstractions.General;

namespace Entities.Users.ValueObjects;

/// <summary>
/// Salt of <see cref="User"/> for encryption
/// </summary>
public sealed class Salt : ValueObject
{
    /// <summary>
    /// Value of <see cref="Salt"/>
    /// </summary>
    /// <value></value>
    public Guid Value { get; private set; }

    private Salt() { }

    /// <summary>
    /// Create <see cref="Salt"/>
    /// </summary>
    /// <param name="value"> Value of <see cref="Salt"/></param>
    /// <returns> Domain entity of <see cref="Salt"/></returns>
    public static Salt Create(Guid value) => new() { Value = value };
    
    /// <summary>
    /// Create <see cref="Salt"/>
    /// </summary>
    /// <returns> Domain entity of <see cref="Salt"/></returns>
    public static Salt CreateUnique() => new() { Value = Guid.NewGuid() };

    /// <summary>
    /// Get components for equality
    /// </summary>
    /// <returns> Components for equality </returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}