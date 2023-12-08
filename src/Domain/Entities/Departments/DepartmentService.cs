using Domain.Common;
using Entities.Abstractions.Services;
using Entities.Departments.ValueObjects;

namespace Entities.Departments;

internal sealed class DepartmentService : IDepartmentService
{
    public Result ChangeParentDepartment(Department department, DepartmentId? parentDepartmentId)
    {
        department.SetParent(parentDepartmentId);
        return Result.Success();
    }

    public Result ChangeTitle(Department department, Title title)
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
        return Result.Success();
    }
}