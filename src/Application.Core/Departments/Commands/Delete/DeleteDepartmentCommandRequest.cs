namespace ApplicationCore.Departments.Commands.Delete;

/// <summary>
/// Request for deleting department
/// </summary>
/// <param name="DepartmentId"> Id of department to delete </param>
public record DeleteDepartmentCommandRequest(Guid DepartmentId);
