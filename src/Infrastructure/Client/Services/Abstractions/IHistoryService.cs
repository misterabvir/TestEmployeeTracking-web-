namespace Client.Services.Abstractions;

public interface IHistoryService
{ 
    Task<HistoryResultMultipleResponse> GetEmployeeHistory(string employeeId);
    Task<HistoryResultMultipleResponse> GetDepartmentHistory(string departmentId);
}