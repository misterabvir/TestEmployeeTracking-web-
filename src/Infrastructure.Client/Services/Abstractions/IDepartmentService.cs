using ProtoContracts;

namespace Infrastructure.Client.Services.Abstractions;

public interface IDepartmentService
{
    Task<DepartmentResultSingleResponse> GetById(string id);
    Task<DepartmentResultMulitpleResponse> GetAll();
    Task<DepartmentResultSingleResponse> Create(string title, string parentId = "");
    Task<DepartmentResultSingleResponse> ChangeTitle(string id, string title);
    Task<DepartmentResultSingleResponse> SetParent(string id, string parentId = "");
    Task<DepartmentResultEmptyResponse> Delete(string id);
}
