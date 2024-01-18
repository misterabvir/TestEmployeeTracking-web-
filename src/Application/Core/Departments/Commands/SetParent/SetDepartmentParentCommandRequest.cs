namespace ApplicationCore.Departments.Commands.SetParent;

/// <summary>
/// Request for setting parent department
/// </summary>
/// <param name="DepartmentId">Id of department</param>
/// <param name="ParentDepartmentId">Id of parent department</param>
public record SetDepartmentParentCommandRequest(Guid DepartmentId, Guid? ParentDepartmentId);
