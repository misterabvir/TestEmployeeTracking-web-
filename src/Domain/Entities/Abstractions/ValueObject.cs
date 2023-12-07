
namespace Entities.Abstractions;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        return other is not null && other.GetEqualityComponents().SequenceEqual(GetEqualityComponents());
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueObject);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().GetHashCode();
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left is not null && left.Equals(right);
    }
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}

