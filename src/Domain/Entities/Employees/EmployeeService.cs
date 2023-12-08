using Domain.Common;
using Entities.Abstractions.Services;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;

namespace Entities.Employees;

internal sealed class EmployeeService: IEmployeeService
{
    public Result ChangePersonalData(Employee employee, LastName lastName, FirstName firstName)
    {
        if(employee is null)
        {
            return EmployeeDomainErrors.IsNull;
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
        return Result.Success();   
    }

    public Result ChangeDepartment(Employee employee, DepartmentId departmentId)
    {
        employee.ChangeDepartment(departmentId);
        return Result.Success();       
    }
}