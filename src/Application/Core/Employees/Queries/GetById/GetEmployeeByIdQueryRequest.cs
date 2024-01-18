namespace ApplicationCore.Employees.Queries.GetById;

/// <summary>
/// Request with employee id
/// </summary>
/// <param name="EmployeeId"> Id of employee to get </param>
public record GetEmployeeByIdQueryRequest(Guid EmployeeId);
