using Entities.Departments.ValueObjects;
using Entities.Employees;

namespace ApplicationCore.Abstractions.Repositories;


/// <summary>
/// Repository for <see cref="Employee"/>
/// </summary>
public interface IEmployeeRepository : IRepository<Employee>
{
    /// <summary>
    /// Get employees by DepartmentId
    /// </summary>
    /// <param name="departmentId"> DepartmentId </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> List of employees </returns>
    Task<IEnumerable<Employee>> GetByDepartmentId(DepartmentId departmentId, CancellationToken cancellationToken);
}
