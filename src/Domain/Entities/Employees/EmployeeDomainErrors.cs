using Domain.Common;

namespace Entities.Employees;

public static class EmployeeDomainErrors
{
    public static Error IsNull => new("Employee.IsNull", "Employee is null");
    public static Error LastNameIsNull => new("Employee.LastName.IsNull", "Employee LastName is null");
    public static Error FirstNameIsNull => new("Employee.FirstName.IsNull", "Employee FirstName is null");
    public static Error DepartmentIsNull => new("Employee.Department.IsNull", "Employee Department is null");
}
