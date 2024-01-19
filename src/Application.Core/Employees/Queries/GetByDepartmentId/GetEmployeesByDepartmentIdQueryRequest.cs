using Entities.Departments;

namespace Application.Core.Employees.Queries.GetByDepartmentId;

/// <summary>
/// Reuest for query with department id
/// </summary>
/// <param name="DepartmentId">Value of <see cref="Department"/> id</param>
public record GetEmployeesByDepartmentIdQueryRequest(Guid DepartmentId);
