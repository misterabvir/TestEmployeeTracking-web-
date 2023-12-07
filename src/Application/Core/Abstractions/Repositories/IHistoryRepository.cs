using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;
using Entities.Histories;

namespace Core.Abstractions.Repositories;

public interface IHistoryRepository : IRepository<History>
{
    Task<History?> Get(EmployeeId employeeId, DepartmentId departmentId, CancellationToken cancellationToken);
}