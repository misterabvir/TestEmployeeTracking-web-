using Entities.Departments.ValueObjects;
using Entities.Employees;

namespace ApplicationCore.Abstractions.Repositories;



public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetByDepartmentId(DepartmentId departmentId, CancellationToken cancellationToken);
}
