using Entities.Abstractions.General;
using Entities.Departments.ValueObjects;

namespace Entities.Departments;

/// <summary>
/// Department entity
/// </summary>
public sealed class Department : Entity<DepartmentId>
{
    private Department() { }
    /// <summary>
    /// Id of parent department
    /// </summary>
    public DepartmentId? ParentId { get; private set; }
    /// <summary>
    /// Title of department
    /// </summary>
    public Title Title { get; private set; } = default!;

    /// <summary>
    /// Factory for <see cref="Department"/>
    /// </summary>
    /// <param name="title"> Title of department </param>
    /// <param name="parentDepartmentId"> Id of parent department </param>
    /// <returns> Domain entity of <see cref="Department"/> </returns>
    public static Department Create(Title title, 
        DepartmentId? parentDepartmentId = null)
    {
        return new() { 
            Id = DepartmentId.CreateUnique(),
            Title = title,
            ParentId = parentDepartmentId
        };
    }

        /// <summary>
    /// Factory for <see cref="Department"/>
    /// </summary>
    /// <param name="departmentId"> Id of department </param>
    /// <param name="title"> Title of department </param>
    /// <param name="parentDepartmentId"> Id of parent department </param>
    /// <returns> Domain entity of <see cref="Department"/> </returns>
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

    /// <summary>
    /// Changes title
    /// </summary>
    /// <param name="title"> New title </param>
    internal void ChangeTitle(Title title)
    {
        Title = title;
    }

    /// <summary>
    /// Changes parent
    /// </summary>
    /// <param name="parentId"> New parent id</param> 
    internal void SetParent(DepartmentId? parentId)
    {
        ParentId = parentId;
    }
}
