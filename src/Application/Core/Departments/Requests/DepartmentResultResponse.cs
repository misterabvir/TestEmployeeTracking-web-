using Entities.Departments;

namespace Core.Departments.Requests;

public class DepartmentResultResponse
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;

    internal static DepartmentResultResponse FromDomain(Department department)
    {
        return new(){
            Id = department.Id.Value,
            ParentId = department.ParentId?.Value,
            Title = department.Title.Value
        };
    }
}