using Domain.Common;

namespace Entities.Departments;

/// <summary>
/// Department domain errors
/// </summary>
public static class DepartmentDomainErrors
{
    /// <summary>
    /// Error when <see cref="Department"/> is null
    /// </summary>
    /// <returns> Error when <see cref="Department"/> is null</returns>
    public static Error<Department> IsNull => new ("Department.IsNull", "Department is null", ResultErrorStatus.InvalidArgument);
    /// <summary>
    /// Error when <see cref="Department.Title"/> is null
    /// </summary>
    /// <returns> Error when <see cref="Department.Title"/> is null </returns>
    public static Error<Department>  TitleIsNull => new ("Department.Title.IsNull", "Department Title is null", ResultErrorStatus.InvalidArgument);
}
