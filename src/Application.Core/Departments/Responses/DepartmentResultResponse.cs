using Entities.Departments;

namespace ApplicationCore.Departments.Responses;

/// <summary>
/// Response for department
/// </summary>
public sealed record DepartmentResultResponse
{
    /// <summary>
    /// Id of department
    /// </summary>
    public Guid Id { get; private  set; }
    /// <summary>
    /// Parent id of department
    /// </summary>
    public Guid? ParentId { get; private  set; }
    /// <summary>
    /// Title of department
    /// </summary>
    public string Title { get; private  set; } = string.Empty;

    /// <summary>
    /// Create instance of <see cref="DepartmentResultResponse"/> from domain entity
    /// </summary>
    /// <param name="department"> Domain entity of department to convert </param>
    /// <returns> Instance of <see cref="DepartmentResultResponse"/> </returns>
    internal static DepartmentResultResponse FromDomain(Department department)
    {
        return new(){
            Id = department.Id.Value,
            ParentId = department.ParentId?.Value,
            Title = department.Title.Value
        };
    }
}