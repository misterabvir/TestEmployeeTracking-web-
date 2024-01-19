namespace ApplicationCore.Employees.Commands.ChangeDepartment;

/// <summary>
/// Request with employee id and new department id
/// </summary>
/// <param name="EmployeeId"> Id of employee to change </param>
/// <param name="DepartmentId"> Id of new department </param>
public record ChangeDepartmentCommandRequest(Guid EmployeeId, Guid DepartmentId);
