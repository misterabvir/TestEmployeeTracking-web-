
namespace Entities.Abstractions.General;

/// <summary>
/// Base domain value object
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Get components for comparison
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// Implements IEquatable
    /// </summary>
    /// <param name="other"> Other value object </param>
    /// <returns> Result of comparison </returns>
    public bool Equals(ValueObject? other) => other is not null && other.GetEqualityComponents().SequenceEqual(GetEqualityComponents());

    /// <summary>
    /// Overrides object.Equals
    /// </summary>
    /// <param name="obj"> Object to compare </param>
    /// <returns> Result of comparison </returns>
    public override bool Equals(object? obj) => Equals(obj as ValueObject);
    

    /// <summary>
    /// Overrides object.GetHashCode
    /// </summary>
    /// <returns> Hash code </returns>
    public override int GetHashCode() => GetEqualityComponents().GetHashCode();
    

    /// <summary>
    /// Overrides operator ==
    /// </summary>
    /// <param name="left"> First value object </param>
    /// <param name="right"> Second value object </param>
    /// <returns> Result of comparison </returns>
    public static bool operator ==(ValueObject? left, ValueObject? right) => left is not null && left.Equals(right);
    

    /// <summary>
    /// Overrides operator !=
    /// </summary>
    /// <param name="left"> First value object </param>
    /// <param name="right"> Second value object </param>
    /// <returns> Result of comparison </returns>
    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

}

