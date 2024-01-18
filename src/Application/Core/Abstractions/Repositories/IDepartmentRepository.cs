using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace ApplicationCore.Abstractions.Repositories;

/// <summary>
/// Repository for <see cref="Department"/>
/// </summary>
public interface IDepartmentRepository : IRepository<Department>
{
    /// <summary>
    /// Get Department by title and parentId
    /// </summary>
    /// <param name="title"> Title of Department</param>
    /// <param name="parentId"> ParentId of Department</param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> Department or null </returns>
    Task<Department?> GetByNameAndParentId(Title title, DepartmentId? parentId, CancellationToken cancellationToken);
}
