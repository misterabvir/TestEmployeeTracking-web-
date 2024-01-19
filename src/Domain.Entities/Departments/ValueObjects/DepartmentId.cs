using Entities.Abstractions.Shared;

namespace Entities.Departments.ValueObjects;

public sealed class DepartmentId : Id
{
    private DepartmentId() { }

    /// <summary>
    /// Factory for <see cref="DepartmentId"/>
    /// </summary>
    /// <param name="id"> Value of <see cref="DepartmentId"/> </param>
    /// <returns> Domain entity of <see cref="DepartmentId"/> </returns>
    public static DepartmentId Create(Guid id)
        => new() { Value = id };

    /// <summary>
    /// Factory for <see cref="DepartmentId"/>
    /// </summary>
    /// <returns> Domain entity of <see cref="DepartmentId"/> </returns>
    public static DepartmentId CreateUnique()
        => new() { Value = Guid.NewGuid() };

    /// <summary>
    /// Implementing <see cref="ValueObject"/>
    /// </summary>
    /// <returns> Components for comparison </returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
