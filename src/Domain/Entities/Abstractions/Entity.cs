namespace Entities.Abstractions;

public abstract class Entity<T> : IEquatable<Entity<T>> 
      where T : ValueObject
{
    public T Id { get; set; } = default!;

    public bool Equals(Entity<T>? other)
    {
        return other is not null && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity<T>);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

public abstract class Id : ValueObject
{
    public Guid Value { get; protected set; }
}