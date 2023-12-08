namespace ApplicationCore.Employees.Commands.ChangeDepartment;

public record ChangeDepartmentCommandRequest(Guid EmployeeId, Guid DepartmentId);
