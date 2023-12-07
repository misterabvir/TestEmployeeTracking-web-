namespace Core.Departments.Commands.SetParent;

public record SetDepartmentParentCommandRequest(Guid DepartmentId, Guid? ParentDepartmentId);
