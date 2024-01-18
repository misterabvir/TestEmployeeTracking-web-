namespace ApplicationCore.Employees.Commands.Delete;

/// <summary>
/// Request with employee id
/// </summary>
/// <param name="EmployeeId"> Id of employee to delete </param>
public record DeleteEmployeeCommandRequest(Guid EmployeeId);
