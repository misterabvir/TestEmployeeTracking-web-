using Entities.Abstractions;

namespace ApplicationCore.Abstractions.Repositories;

/// <summary>
/// Generic repository
/// </summary>
/// <typeparam name="T"> Type of entity </typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Get all
    /// </summary>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> List of entities </returns>    
    Task<IEnumerable<T>> Get(CancellationToken cancellationToken);

    /// <summary>
    /// Get entity by Id
    /// </summary>
    /// <param name="Id"> Id of entity </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> Entity or null </returns>
    Task<T?> Get(Id Id, CancellationToken cancellationToken);

    /// <summary>
    /// Create entity    
    /// </summary>
    /// <param name="entity"> Entity to create </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    Task Create(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Update entity    
    /// </summary>
    /// <param name="entity"> Entity to update </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    Task Update(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete entity    
    /// </summary>
    /// <param name="Id"> Id of entity </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    Task Delete(Id Id, CancellationToken cancellationToken);
}
