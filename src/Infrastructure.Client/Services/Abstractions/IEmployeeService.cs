using ProtoContracts;

namespace Infrastructure.Client.Services.Abstractions;

public interface IEmployeeService
{
    Task<EmployeeResultMulitpleResponse> GetAll();
    Task<EmployeeResultSingleResponse> GetById(string id);
    Task<EmployeeResultMulitpleResponse> GetEmployeeByDepartmentId(string departmentId);
    Task<EmployeeResultSingleResponse> ChangePersonalData(string id, string lastname, string firstname);
    Task<EmployeeResultSingleResponse> ChangeDepartment(string id, string departmentId);
    Task<EmployeeResultEmptyResponse> Delete(string id);
    Task<EmployeeResultSingleResponse> Create(string lastname, string firstname, string departmentId);
}
