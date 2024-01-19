using Domain.Common;

namespace Entities.Employees;

/// <summary>
/// Employee domain errors
/// </summary>
public static class EmployeeDomainErrors
{
    /// <summary>
    /// Error when <see cref="Employee"/> is null
    /// </summary>
    /// <returns> Error when <see cref="Employee"/> is null</returns>
    public static Error<Employee> EmployeeIsNull => new("Employee.IsNull", "Employee is null", ResultErrorStatus.InvalidArgument);
    /// <summary>
    /// Error when <see cref="Employee.LastName"/> is null
    /// </summary>
    /// <returns>Error when <see cref="Employee.LastName"/> is null</returns>
    public static Error<Employee> LastNameIsNull => new("Employee.LastName.IsNull", "Employee LastName is null", ResultErrorStatus.InvalidArgument);
    /// <summary>
    /// Error when <see cref="Employee.FirstName"/> is null
    /// </summary>
    /// <returns> Error when <see cref="Employee.FirstName"/> is null</returns>
    public static Error<Employee> FirstNameIsNull => new("Employee.FirstName.IsNull", "Employee FirstName is null", ResultErrorStatus.InvalidArgument);
    /// <summary>
    /// Error when <see cref="Employee.Department"/> is null
    /// </summary>
    /// <returns> Error when <see cref="Employee.Department"/> is null</returns>
    public static Error<Employee> DepartmentIsNull => new("Employee.Department.IsNull", "Employee Department is null", ResultErrorStatus.InvalidArgument);
}
