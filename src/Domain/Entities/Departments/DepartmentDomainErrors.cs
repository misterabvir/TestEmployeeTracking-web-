using Domain.Common;

namespace Entities.Departments;

public static class DepartmentDomainErrors
{
    public static Error IsNull => new ("Department.IsNull", "Department is null");
    public static Error TitleIsNull => new ("Department.Title.IsNull", "Department Title is null");
}
