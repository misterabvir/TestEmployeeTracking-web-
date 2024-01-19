namespace ApplicationCore.Employees.Commands.Create;

/// <summary>
/// Request with new employee data
/// </summary>
/// <param name="LastName"> Last name of new employee </param>
/// <param name="FirstName"> First name of new employee </param>
/// <param name="DepartmentId"> Id of department for new employee </param>
/// <returns></returns>
public record CreateEmployeeCommandRequest(string LastName, string FirstName, Guid DepartmentId);
