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

namespace Core.Employees.Commands.ChangeDepartment;

public class ChangeDepartmentCommandHandler : ICommandHandler<ChangeDepartmentCommand, Result<EmployeeResultResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IHistoryRepository _historyRepository;
    private readonly IDateTimeService _dateTimeService;

    public ChangeDepartmentCommandHandler(
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

    public async Task<Result<EmployeeResultResponse>> Handle(ChangeDepartmentCommand command, CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(command.Request.EmployeeId);
        Employee? employee = await _employeeRepository.Get(employeeId, cancellationToken);
        if (employee is null)
        {
            return EmployeeErrors.NotFound(employeeId.Value);
        }

        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);

        if (department is null)
        {
            return EmployeeErrors.DepartmentNotExist(departmentId.Value);
        }

        if (employee.DepartmentId == departmentId)
        {
            return EmployeeErrors.AlreadyInDepartment(employeeId.Value);
        }

        History? last = await _historyRepository.Get(employeeId, employee.DepartmentId, cancellationToken);
        if (last is not null)
        {
            last.Complete(_dateTimeService.Today);
            await _historyRepository.Update(last, cancellationToken);
        }

        employee.ChangeDepartment(departmentId);
        last = History.Create(employeeId, departmentId, _dateTimeService.Today);
        await _historyRepository.Create(last, cancellationToken);
        await _employeeRepository.Update(employee, cancellationToken);
        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(employee));
    }
}