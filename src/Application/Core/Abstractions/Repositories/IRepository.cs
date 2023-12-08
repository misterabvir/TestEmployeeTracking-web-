using Entities.Abstractions;

namespace ApplicationCore.Abstractions.Repositories;

public interface IRepository<T> 
{ 
    Task<IEnumerable<T>> Get(CancellationToken cancellationToken);
    Task<T?> Get(Id Id, CancellationToken cancellationToken);
    Task Create(T entity, CancellationToken cancellationToken);
    Task Update(T entity, CancellationToken cancellationToken);
    Task Delete(Id Id, CancellationToken cancellationToken);
}
