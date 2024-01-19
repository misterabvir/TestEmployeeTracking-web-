using Domain.Common;
using Entities.Abstractions.Services;
using Entities.Departments.ValueObjects;

namespace Entities.Departments;

/// <summary>
/// Service for changing <see cref="Department"/> entity
/// </summary>
internal sealed class DepartmentService : IDepartmentService
{
    /// <summary>
    /// Sets parent department
    /// </summary>
    /// <param name="department"> Department to change </param>
    /// <param name="parentDepartmentId"> Id of parent department </param>
    /// <returns> Result of operation </returns>
    public Result<Department>  ChangeParentDepartment(Department department, DepartmentId? parentDepartmentId)
    {
        department.SetParent(parentDepartmentId);
        return Result<Department> .Success(department);
    }

    /// <summary>
    /// Changes title
    /// </summary>
    /// <param name="department"> Department to change </param>
    /// <param name="title"> New title </param>
    /// <returns> Result of operation </returns>
    public Result<Department>  ChangeTitle(Department department, Title title)
    {
        if(department is null)
        {
            return DepartmentDomainErrors.IsNull;
        }
        if( title  is null)
        {
            return DepartmentDomainErrors.TitleIsNull;
        }
        department.ChangeTitle(title);
        return Result<Department> .Success(department);
    }
}