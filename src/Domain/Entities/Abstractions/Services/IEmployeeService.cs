using Domain.Common;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;

namespace Entities.Abstractions.Services;

public interface IEmployeeService
{
    Result ChangePersonalData(Employee employee, LastName lastName, FirstName firstName);
    Result ChangeDepartment(Employee employee, DepartmentId departmentId);
}
