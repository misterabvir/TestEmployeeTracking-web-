namespace ApplicationCore.Departments.Commands.Create;

/// <summary>
/// Request for creating department
/// </summary>
/// <param name="Title"> Title of department </param>
/// <param name="ParentDepartmentId"> Id of parent department </param>
public record CreateDepartmentCommandRequest(string Title, Guid? ParentDepartmentId = null);
