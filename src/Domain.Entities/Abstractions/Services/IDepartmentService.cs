using Domain.Common;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace Entities.Abstractions.Services;

/// <summary>
/// Service for changing <see cref="Department"/> entity 
/// </summary>
public interface IDepartmentService
{
    /// <summary>
    /// Changes title of <see cref="Department"/>
    /// </summary>
    /// <param name="department"> Department to change </param>
    /// <param name="title"> New title </param>
    /// <returns> Result of operation </returns>
    Result<Department> ChangeTitle(Department department, Title title);
    /// <summary>
    /// Changes parent department of <see cref="Department"/>
    /// </summary>
    /// <param name="department"> Department to change </param>
    /// <param name="parentDepartmentId"> New parent department id</param>
    /// <returns> Result of operation </returns>
    Result<Department> ChangeParentDepartment(Department department, DepartmentId? parentDepartmentId);
}