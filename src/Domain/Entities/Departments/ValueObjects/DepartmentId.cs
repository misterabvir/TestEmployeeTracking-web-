using Entities.Abstractions;

namespace Entities.Departments.ValueObjects;

public sealed class DepartmentId : Id
{
    private DepartmentId() { }

    public static DepartmentId Create(Guid id)
        => new() { Value = id };

    public static DepartmentId CreateUnique()
        => new() { Value = Guid.NewGuid() };

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
