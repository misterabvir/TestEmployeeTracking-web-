using Entities.Departments;

namespace ApplicationCore.Departments.Responses;

public sealed class DepartmentResultResponse
{
    public Guid Id { get; private  set; }
    public Guid? ParentId { get; private  set; }
    public string Title { get; private  set; } = string.Empty;

    internal static DepartmentResultResponse FromDomain(Department department)
    {
        return new(){
            Id = department.Id.Value,
            ParentId = department.ParentId?.Value,
            Title = department.Title.Value
        };
    }
}