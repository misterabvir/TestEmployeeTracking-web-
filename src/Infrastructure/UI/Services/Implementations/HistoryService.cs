using UI.Services.Abstractions;

namespace UI.Services.Implementations;

public class HistoryService : IHistoryService
{
    private readonly IConfigurationService _service;

    public HistoryService(IConfigurationService service)
    {
        _service = service;
    }

    public Task<IEnumerable<HistoryResponse>> GetDepartmentHistory()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<HistoryResponse>> GetEmployeeHistory()
    {
        throw new NotImplementedException();
    }
}
