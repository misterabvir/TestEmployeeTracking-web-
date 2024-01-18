namespace ApplicationCore.Departments.Queries.GetById;

/// <summary>
/// Request for getting department by id
/// </summary>
/// <param name="DepartmentId"> Id of department to get</param> 
public record GetDepartmentByIdQueryRequest(Guid DepartmentId);
