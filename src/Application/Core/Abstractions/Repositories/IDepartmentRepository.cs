using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace ApplicationCore.Abstractions.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAndParentId(Title title, DepartmentId? parentId, CancellationToken cancellationToken);
}
