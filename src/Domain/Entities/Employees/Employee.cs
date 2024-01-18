using Entities.Abstractions.General;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;

namespace Entities.Employees;

/// <summary>
/// Domain entity of <see cref="Employee"/>
/// </summary>
public sealed class Employee : Entity<EmployeeId>
{
    private Employee() { }
    /// <summary>
    /// Last name of <see cref="Employee"/>
    /// </summary>
    public LastName LastName { get; set; } = default!; 
    /// <summary>
    /// First name of <see cref="Employee"/>
    /// </summary>
    public FirstName FirstName { get; set; } = default!;
    /// <summary>
    /// Id of <see cref="Department"/> where <see cref="Employee"/> work
    /// </summary>
    public DepartmentId DepartmentId { get; set; } = default!;

    /// <summary>
    /// Create <see cref="Employee"/>
    /// </summary>
    /// <param name="lastName">Last name of <see cref="Employee"/></param>
    /// <param name="firstName">First name of <see cref="Employee"/></param>
    /// <param name="departmentId">Id of <see cref="Department"/> where <see cref="Employee"/> work</param>
    /// <returns> Domain entity of <see cref="Employee"/> </returns>
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

    /// <summary>
    /// Create <see cref="Employee"/>
    /// </summary>
    /// <param name="employeeId">Id of <see cref="Employee"/></param>
    /// <param name="lastName">Last name of <see cref="Employee"/></param>
    /// <param name="firstName">First name of <see cref="Employee"/></param>
    /// <param name="departmentId">Id of <see cref="Department"/> where <see cref="Employee"/> work</param>
    /// <returns> Domain entity of <see cref="Employee"/> </returns>
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

    /// <summary>
    /// Change <see cref="Department"/> where <see cref="Employee"/> work
    /// </summary>
    /// <param name="departmentId"> Id of new <see cref="Department"/> where <see cref="Employee"/> work</param>

    internal void ChangeDepartment(DepartmentId departmentId)
    {
        DepartmentId = departmentId;
    }

    /// <summary>
    /// Change personal data of <see cref="Employee"/>
    /// </summary>
    /// <param name="lastName">New last name of <see cref="Employee"/></param>
    /// <param name="firstName"> New first name of <see cref="Employee"/></param>
    internal void ChangePersonalData(LastName lastName, FirstName firstName)
    {
        LastName = lastName;
        FirstName = firstName;
    }
}
