using Entities.Abstractions;

namespace Entities.Employees.ValueObjects;

public sealed class EmployeeId : Id
{
    private EmployeeId() { }

    public static EmployeeId Create(Guid id)
        => new() { Value = id };

    public static EmployeeId CreateUnique()
        => new() { Value = Guid.NewGuid() };

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
