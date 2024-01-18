namespace ApplicationCore.Histories.Queries.GetDepartmentHistory;

/// <summary>
/// Request with department id
/// </summary>
/// <param name="DepartmentId"> Id of department to get history </param>
/// <returns></returns>
public record GetDepartmentHistoryQueryRequest(Guid DepartmentId);
