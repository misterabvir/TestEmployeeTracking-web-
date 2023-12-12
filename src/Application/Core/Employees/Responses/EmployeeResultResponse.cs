using Entities.Employees;

namespace ApplicationCore.Employees.Responses;

public sealed record EmployeeResultResponse
{
    private EmployeeResultResponse() { }
    
    public Guid Id { get; private set; }
    public string LastName { get; private  set; } = null!;
    public string FirstName { get; private  set; } = null!;
    public Guid DepartmentId { get; private  set; }

    internal static EmployeeResultResponse FromDomain(Employee employee)
    {
        return new() {
            Id = employee.Id.Value,
            LastName = employee.LastName.Value,
            FirstName = employee.FirstName.Value,
            DepartmentId = employee.DepartmentId.Value,
        };
    }
}
