using UI.Services.Abstractions;

namespace UI.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IConfigurationService _service;

    public EmployeeService(IConfigurationService service)
    {
        _service = service;
    }

    public Task<EmployeeResponse?> ChangeDepartment(Guid employeeId, Guid? departmentId)
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeResponse?> ChangePersonalData(Guid employeeId, string lastname, string firstname)
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeResponse?> Create(string lastname, string firstname, Guid departmentId)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EmployeeResponse>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeResponse?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
