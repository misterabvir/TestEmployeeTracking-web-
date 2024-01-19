using Infrastructure.Client.Services.Abstractions;
using ProtoContracts;

namespace Infrastructure.Client.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IClientService _service;

    public DepartmentService(IClientService service)
    {
        _service = service;
    }

    public async Task<DepartmentResultSingleResponse> ChangeTitle(string id, string title)
    {
        var request = new DepartmentChangeTitleRequest() { Id = id, NewTitle = title };
        return await _service.DepartmentsClient.ChangeTitleAsync(request);
    }

    public async Task<DepartmentResultSingleResponse> Create(string title, string parentId = "")
    {
        var request = new DepartmentCreateRequest() { Title = title, ParentId = parentId };
        return await _service.DepartmentsClient.CreateAsync(request);
    }

    public async Task<DepartmentResultEmptyResponse> Delete(string id)
    {
        var request = new DepartmentDeleteRequest() { Id = id };
        return await _service.DepartmentsClient.DeleteAsync(request);
    }

    public async Task<DepartmentResultMulitpleResponse> GetAll()
    {
        var request = new DepartmentGetAllRequest();
        return await _service.DepartmentsClient.GetAllAsync(request);
    }

    public async Task<DepartmentResultSingleResponse> GetById(string id)
    {
        var request = new DepartmentGetByIdRequest() { Id = id };
        return await _service.DepartmentsClient.GetByIdAsync(request);
    }

    public async Task<DepartmentResultSingleResponse> SetParent(string id, string parentId = "")
    {
        var request = new DepartmentChangeParentRequest() { Id = id, NewParentId = parentId };
        return await _service.DepartmentsClient.ChangeParentAsync(request);
    }
}
