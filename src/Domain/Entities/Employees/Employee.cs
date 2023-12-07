using Entities.Abstractions;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;

namespace Entities.Employees;

public sealed class Employee : Entity<EmployeeId>
{

    private Employee() { }
    public LastName LastName { get; set; } = default!; 
    public FirstName FirstName { get; set; } = default!;
    public DepartmentId DepartmentId { get; set; } = default!;

    public static Employee Create(LastName lastName,
                                  FirstName firstName,
                                  DepartmentId departmentId)
    {
        return new() {
            Id = EmployeeId.CreateUnique(),
            LastName = lastName,
            FirstName = firstName,
            DepartmentId = departmentId
        };
    }

    public static Employee Create(EmployeeId employeeId,
            LastName lastName,
            FirstName firstName,
            DepartmentId departmentId)
    {
        return new()
        {
            Id = employeeId,
            LastName = lastName,
            FirstName = firstName,
            DepartmentId = departmentId
        };
    }

    public void ChangeDepartment(DepartmentId departmentId)
    {
        DepartmentId = departmentId;
    }

    public void ChangePersonalData(LastName lastName, FirstName firstName)
    {
        LastName = lastName;
        FirstName = firstName;
    }
}
