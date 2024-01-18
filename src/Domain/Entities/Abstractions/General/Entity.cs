namespace Entities.Abstractions.General;

/// <summary>
/// Base domain entity
/// </summary>
/// <typeparam name="T"> Id of entity. Must be <see cref="ValueObject"/> </typeparam>
public abstract class Entity<T> : IEquatable<Entity<T>> 
      where T : ValueObject
{
    /// <summary>
    /// Id of entity
    /// </summary>
    public T Id { get; set; } = default!;

    /// <summary>
    /// Implements IEquatable
    /// </summary>
    /// <param name="other"> Other entity </param>
    /// <returns> Result of comparison of entities ids </returns>
    public bool Equals(Entity<T>? other) => other is not null && Id == other.Id;
    
    /// <summary>
    /// Overrides object.Equals
    /// </summary>
    /// <param name="obj"> Object to compare </param>
    /// <returns> Result of comparison </returns>
    public override bool Equals(object? obj) => Equals(obj as Entity<T>);
    
    /// <summary>
    /// Overrides object.GetHashCode
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Id.GetHashCode();
    
    /// <summary>
    /// Overrides operator == 
    /// </summary>
    /// <param name="left"> First entity </param>
    /// <param name="right"> Second entity </param>
    /// <returns> Result of comparison </returns>
    public static bool operator ==(Entity<T> left, Entity<T> right) => left.Equals(right);
    
    /// <summary>
    /// Overrides operator != 
    /// </summary>
    /// <param name="left"> First entity </param>
    /// <param name="right"> Second entity </param>
    /// <returns> Result of comparison </returns>
    public static bool operator !=(Entity<T> left, Entity<T> right) => !(left is not null && left == right);
}
