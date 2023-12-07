using Entities.Employees;
using Entities.Employees.ValueObjects;

namespace Persistence.DTO;

internal class EmployeeDto
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }

    public static EmployeeDto FromDomain(Employee employee)
    {
        return new() { 
            Id = employee.Id.Value,
            LastName = employee.LastName.Value,
            FirstName = employee.FirstName.Value,
            DepartmentId = employee.DepartmentId.Value
        };
    }

    public Employee ToDomain()
    {
        return Employee.Create(
                EmployeeId.Create(Id),
                Entities.Employees.ValueObjects.LastName.Create(LastName),
                Entities.Employees.ValueObjects.FirstName.Create(FirstName),
                Entities.Departments.ValueObjects.DepartmentId.Create(DepartmentId)
            );
    }
}

