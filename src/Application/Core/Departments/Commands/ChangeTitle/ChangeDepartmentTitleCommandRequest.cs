namespace ApplicationCore.Departments.Commands.ChangeTitle;

public record ChangeDepartmentTitleCommandRequest(Guid DepartmentId, string Title);
