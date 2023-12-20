namespace UI.Services.Abstractions;

public interface IEmployeeService
{
    Task<EmployeeResponse?> GetById(Guid id);
    Task<IEnumerable<EmployeeResponse>> Get();
    Task<EmployeeResponse?> Create(string lastname, string firstname, Guid departmentId);
    Task<EmployeeResponse?> ChangePersonalData(Guid employeeId, string lastname, string firstname);
    Task<EmployeeResponse?> ChangeDepartment(Guid employeeId, Guid? departmentId);
    Task Delete(Guid employeeId);
}
