using Domain.Common;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace Entities.Abstractions.Services;

public interface IDepartmentService
{
    Result ChangeTitle(Department department, Title title);
    Result ChangeParentDepartment(Department department, DepartmentId? parentDepartmentId);
}