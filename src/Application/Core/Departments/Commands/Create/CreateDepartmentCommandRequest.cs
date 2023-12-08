namespace ApplicationCore.Departments.Commands.Create;

public record CreateDepartmentCommandRequest(string Title, Guid? ParentDepartmentId = null);
