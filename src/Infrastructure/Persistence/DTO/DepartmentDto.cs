using Entities.Departments.ValueObjects;
using Entities.Departments;

namespace Persistence.DTO;

internal class DepartmentDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public static DepartmentDto FromDomain(Department department)
    {
        return new(){
            Id = department.Id.Value,
            ParentId = department.ParentId?.Value,
            Title = department.Title.Value
        };
    }

    public Department ToDomain()
    {
        return Department.Create(
            Entities.Departments.ValueObjects.DepartmentId.Create(Id),
            Entities.Departments.ValueObjects.Title.Create(Title),
            ParentId is not null ? Entities.Departments.ValueObjects.DepartmentId.Create(ParentId.Value) : null
        );
    }
}
