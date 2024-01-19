using Entities.Employees;

namespace ApplicationCore.Employees.Responses;

/// <summary>
/// Response with employee data
/// </summary>
public sealed record EmployeeResultResponse
{
    private EmployeeResultResponse() { }   
    
    /// <summary>
    /// Id of employee
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Last name of employee
    /// </summary>
    public string LastName { get; private  set; } = null!;
    /// <summary>
    /// First name of employee
    /// </summary>
    public string FirstName { get; private  set; } = null!;
    /// <summary>
    /// Id of department
    /// </summary>
    public Guid DepartmentId { get; private  set; }

    /// <summary>
    /// Creates new instance of <see cref="EmployeeResultResponse"/> from domain entity
    /// </summary>
    /// <param name="employee"> Domain entity of employee to convert </param>
    /// <returns> Instance of <see cref="EmployeeResultResponse"/> </returns>
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
