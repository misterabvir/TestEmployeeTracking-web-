namespace ApplicationCore.Employees.Commands.Create;

public record CreateEmployeeCommandRequest(string LastName, string FirstName, Guid DepartmentId);
