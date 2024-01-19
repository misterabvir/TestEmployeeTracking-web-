namespace ApplicationCore.Histories.Queries.GetEmployeeHistory;

/// <summary>
/// Request with employee id
/// </summary>
/// <param name="EmployeeId"> Id of employee to get history </param>
public record GetEmployeeHistoryQueryRequest(Guid EmployeeId);
