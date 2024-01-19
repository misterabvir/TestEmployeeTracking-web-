using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;
using Entities.Histories;

namespace ApplicationCore.Abstractions.Repositories;

/// <summary>
/// Repository for <see cref="History"/>
/// </summary>
public interface IHistoryRepository : IRepository<History>
{
    /// <summary>
    /// Get history by EmployeeId and DepartmentId
    /// </summary>
    /// <param name="employeeId"> EmployeeId </param>
    /// <param name="departmentId"> DepartmentId </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns></returns>
    Task<History?> Get(EmployeeId employeeId, DepartmentId departmentId, CancellationToken cancellationToken);

    /// <summary>
    /// Get full history by DepartmentId
    /// </summary>
    /// <param name="departmentId"> DepartmentId </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> List of histories </returns>
    Task<IEnumerable<History>> GetDepartmentHistory(DepartmentId departmentId, CancellationToken cancellationToken);

    /// <summary>
    /// Get full history by EmployeeId
    /// </summary>
    /// <param name="employeeId"> EmployeeId </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> List of histories </returns>
    Task<IEnumerable<History>> GetEmployeeHistory(EmployeeId employeeId, CancellationToken cancellationToken);
}