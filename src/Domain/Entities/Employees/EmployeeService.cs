using Domain.Common;
using Entities.Abstractions.Services;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;

namespace Entities.Employees;

/// <summary>
/// Service for changing <see cref="Employee"/>
/// </summary>
internal sealed class EmployeeService: IEmployeeService
{
    /// <summary>
    /// Change <see cref="Employee"/> personal data
    /// </summary>
    /// <param name="employee">Domain entity of <see cref="Employee"/></param>
    /// <param name="lastName">New last name</param>
    /// <param name="firstName">New first name</param>
    /// <returns></returns>
    public Result<Employee> ChangePersonalData(Employee employee, LastName lastName, FirstName firstName)
    {
        if(employee is null)
        {
            return EmployeeDomainErrors.EmployeeIsNull;
        }
        
        if(lastName is null)
        {
            return EmployeeDomainErrors.LastNameIsNull;
        }

        if(firstName is null)
        {
            return EmployeeDomainErrors.FirstNameIsNull;
        }
        
        employee.ChangePersonalData(lastName, firstName);
        return Result<Employee>.Success(employee);   
    }

    /// <summary>
    /// Change <see cref="Department"/> where <see cref="Employee"/> work
    /// </summary>
    /// <param name="employee">Domain entity of <see cref="Employee"/></param>
    /// <param name="departmentId">Id of new <see cref="Department"/></param>
    /// <returns></returns>
    public Result<Employee> ChangeDepartment(Employee employee, DepartmentId departmentId)
    {
        if(employee is null)
        {
            return EmployeeDomainErrors.EmployeeIsNull;
        }
        
        if(departmentId is null)
        {
            return EmployeeDomainErrors.DepartmentIsNull;
        }
        
        employee.ChangeDepartment(departmentId);
        return Result<Employee>.Success(employee);       
    }
}