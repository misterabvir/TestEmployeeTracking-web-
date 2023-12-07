using Entities.Employees;

namespace Core.Employees.Requests;

public class EmployeeResultResponse
{
    private EmployeeResultResponse() { }
    
    public Guid Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public Guid DepartmentId { get; set; }

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
