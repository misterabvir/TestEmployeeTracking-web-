namespace Core.Departments.Commands.ChangeTitle;

public record ChangeDepartmentTitleCommandRequest(Guid DepartmentId, string Title);
