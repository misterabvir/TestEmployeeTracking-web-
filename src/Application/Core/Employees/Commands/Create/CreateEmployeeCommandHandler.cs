using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Common;
using Core.Employees.Requests;
using Core.Employees.Errors;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using Entities.Histories;

namespace Core.Employees.Commands.Create;

public class CreateEmployeeCommandHandler : 
    ICommandHandler<CreateEmployeeCommand, Result<EmployeeResultResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IHistoryRepository _historyRepository;
    private readonly IDateTimeService _dateTimeService;
    public CreateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IHistoryRepository historyRepository,
        IDateTimeService dateTimeService)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _historyRepository = historyRepository;
        _dateTimeService = dateTimeService;
    }

    public async Task<Result<EmployeeResultResponse>> Handle(
        CreateEmployeeCommand command, 
        CancellationToken cancellationToken)
    {
        LastName lastName = LastName.Create(command.Request.LastName);
        FirstName firstName = FirstName.Create(command.Request.FirstName);

        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);

        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if (department is null)
        {
            return EmployeeErrors.DepartmentNotExist(departmentId.Value);
        }

        Employee employee = Employee.Create(lastName, firstName, departmentId);
        await _employeeRepository.Create(employee, cancellationToken);

        History history = History.Create(employee.Id, departmentId, _dateTimeService.Today);
        await _historyRepository.Create(history, cancellationToken);

        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(employee));
    }
}
