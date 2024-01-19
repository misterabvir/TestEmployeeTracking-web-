using Entities.Abstractions.General;

namespace Entities.Abstractions.Shared;

/// <summary>
/// Base domain Id for entities
/// </summary>
public abstract class Id : ValueObject
{
    /// <summary>
    /// Value of Id
    /// </summary>
    public Guid Value { get; protected set; }
}