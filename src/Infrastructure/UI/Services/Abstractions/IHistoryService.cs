namespace UI.Services.Abstractions;

public interface IHistoryService
{
    Task<IEnumerable<HistoryResponse>> GetEmployeeHistory();
    Task<IEnumerable<HistoryResponse>> GetDepartmentHistory();
}