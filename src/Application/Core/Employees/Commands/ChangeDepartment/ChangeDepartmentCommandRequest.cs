namespace Core.Employees.Commands.ChangeDepartment;

public record ChangeDepartmentCommandRequest(Guid EmployeeId, Guid DepartmentId);
