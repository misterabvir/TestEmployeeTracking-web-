using Domain.Common;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;

namespace Entities.Abstractions.Services;

/// <summary>
/// Service for changing <see cref="Employee"/> entity
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Changes personal data of <see cref="Employee"/>
    /// </summary>
    /// <param name="employee"> Employee to change </param>
    /// <param name="lastName"> New last name </param>
    /// <param name="firstName"> New first name </param>
    /// <returns> Result of operation </returns>
    Result<Employee> ChangePersonalData(Employee employee, LastName lastName, FirstName firstName);
    /// <summary>
    /// Changes department of <see cref="Employee"/>
    /// </summary>
    /// <param name="employee"> Employee to change </param>
    /// <param name="departmentId"> New department id </param>
    /// <returns></returns>
    Result<Employee> ChangeDepartment(Employee employee, DepartmentId departmentId);
}
