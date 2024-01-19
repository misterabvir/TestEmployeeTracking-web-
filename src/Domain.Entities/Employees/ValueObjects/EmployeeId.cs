using Entities.Abstractions.Shared;

namespace Entities.Employees.ValueObjects;

/// <summary>
/// Id of <see cref="Employee"/>
/// </summary>
public sealed class EmployeeId : Id
{
    private EmployeeId() { }

    /// <summary>
    /// Create <see cref="EmployeeId"/>
    /// </summary>
    /// <param name="id"> Value of <see cref="EmployeeId"/> </param>
    /// <returns> Domain entity of <see cref="EmployeeId"/> </returns>//
    public static EmployeeId Create(Guid id)
        => new() { Value = id };

    /// <summary>
    /// Create <see cref="DepartmentId"/>
    /// </summary>
    /// <returns> Domain entity of <see cref="EmployeeId"/> </returns>
    public static EmployeeId CreateUnique()
        => new() { Value = Guid.NewGuid() };

    /// <summary>
    /// Get components for equality
    /// </summary>
    /// <returns> Components for equality </returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
