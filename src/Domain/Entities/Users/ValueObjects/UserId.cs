using Entities.Abstractions.Shared;

namespace Entities.Users.ValueObjects;


/// <summary>
/// Id of <see cref="User"/>
/// </summary>
public sealed class UserId : Id
{
    private UserId() { }

    /// <summary>
    /// Create <see cref="UserId"/>
    /// </summary>
    /// <param name="id">Value of <see cref="UserId"/></param>
    /// <returns> Domain entity of <see cref="UserId"/></returns>
    public static UserId Create(Guid id)
        => new() { Value = id };
    
    /// <summary>
    /// Create <see cref="UserId"/>
    /// </summary>
    /// <returns> Domain entity of <see cref="UserId"/></returns>
    public static UserId CreateUnique()
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
