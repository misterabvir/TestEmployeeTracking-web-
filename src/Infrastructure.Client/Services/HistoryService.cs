using Infrastructure.Client.Services.Abstractions;
using ProtoContracts;

namespace Infrastructure.Client.Services;

public class HistoryService : IHistoryService
{
    private readonly IClientService _service;

    public HistoryService(IClientService service)
    {
        _service = service;
    }

    public async Task<HistoryResultMultipleResponse> GetDepartmentHistory(string departmentId)
    {
        var query = new HistoryDepartmentRequest { DepartmentId = departmentId };
        return await _service.HistoriesClient.GetDepartmentHistoryAsync(query);
    }

    public async Task<HistoryResultMultipleResponse> GetEmployeeHistory(string employeeId)
    {
        var query = new HistoryEmployeeRequest  { EmployeeId = employeeId };
        return await _service.HistoriesClient.GetEmployeeHistoryAsync(query);
    }
}
