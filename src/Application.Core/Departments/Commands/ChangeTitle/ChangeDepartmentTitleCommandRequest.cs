namespace ApplicationCore.Departments.Commands.ChangeTitle;

/// <summary>
/// Request for changing department title
/// </summary>
/// <param name="DepartmentId"> DepartmentId </param>
/// <param name="Title"> New title </param>
public record ChangeDepartmentTitleCommandRequest(Guid DepartmentId, string Title);
