using Entities.Abstractions;
using Entities.Departments.ValueObjects;

namespace Entities.Departments;

public sealed class Department : Entity<DepartmentId>
{
    private Department() { }
    public DepartmentId? ParentId { get; private set; }
    public Title Title { get; private set; } = default!;

    public static Department Create(Title title, 
        DepartmentId? parentDepartmentId = null)
    {
        return new() { 
            Id = DepartmentId.CreateUnique(),
            Title = title,
            ParentId = parentDepartmentId
        };
    }

    public static Department Create(DepartmentId departmentId, 
        Title title, 
        DepartmentId? parentDepartmentId = null)
    {
        return new()
        {
            Id = departmentId,
            Title = title,
            ParentId = parentDepartmentId
        };
    }

    internal void ChangeTitle(Title title)
    {
        Title = title;
    }

    internal void SetParent(DepartmentId? parentId)
    {
        ParentId = parentId;
    }
}
