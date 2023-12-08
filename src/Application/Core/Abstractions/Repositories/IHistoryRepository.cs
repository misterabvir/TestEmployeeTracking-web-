using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using Entities.Histories;

namespace ApplicationCore.Abstractions.Repositories;

public interface IHistoryRepository : IRepository<History>
{
    Task<History?> Get(EmployeeId employeeId, DepartmentId departmentId, CancellationToken cancellationToken);
    Task<IEnumerable<History>> GetDepartmentHistory(DepartmentId departmentId, CancellationToken cancellationToken);
    Task<IEnumerable<History>> GetEmployeeHistory(EmployeeId employeeId, CancellationToken cancellationToken);
}