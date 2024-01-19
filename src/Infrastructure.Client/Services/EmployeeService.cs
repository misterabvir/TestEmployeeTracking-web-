using Infrastructure.Client.Services.Abstractions;
using ProtoContracts;

namespace Infrastructure.Client.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IClientService _service;

    public EmployeeService(IClientService service)
    {
        _service = service;
    }

    public async  Task<EmployeeResultSingleResponse> ChangeDepartment(string id, string departmentId)
    {
        var request = new EmployeeChangeDepartmentRequest { Id = id, NewDepartmentId = departmentId };
        var result = await _service.EmployeesClient.ChangeDepartmentAsync(request);
        return result;
    }

    public async Task<EmployeeResultSingleResponse> ChangePersonalData(string id, string lastname, string firstname)
    {
        var request = new EmployeeChangePersonalDataRequest { Id = id, NewLastname = lastname, NewFirstname = firstname};
        var result = await _service.EmployeesClient.ChangePersonalDataAsync(request);
        return result;
    }

    public async Task<EmployeeResultSingleResponse> Create(string lastname, string firstname, string departmentId)
    {
        var request = new EmployeeCreateRequest { Lastname = lastname, Firstname = firstname, DepartmentId = departmentId };
        var result = await _service.EmployeesClient.CreateAsync(request);
        return result;
    }

    public async Task<EmployeeResultEmptyResponse> Delete(string id)
    {
        var request = new EmployeeDeleteRequest() { Id = id };
        return await _service.EmployeesClient.DeleteAsync(request);
    }

    public async Task<EmployeeResultMulitpleResponse> GetAll()
    {
        var request = new EmployeeAllRequest();
        var result = await _service.EmployeesClient.GetAllAsync(request);
        return result;
    }

    public async Task<EmployeeResultSingleResponse> GetById(string id)
    {
        var request = new EmployeeByIdRequest() { Id = id };
        var result = await _service.EmployeesClient.GetByIdAsync(request);
        return result;
    }

    public async Task<EmployeeResultMulitpleResponse> GetEmployeeByDepartmentId(string departmentId)
    {
        var request = new EmployeeByDepartmentIdRequest() { Id = departmentId };
        var result = await _service.EmployeesClient.GetByDepartmentIdAsync(request);
        return result;

    }
}
