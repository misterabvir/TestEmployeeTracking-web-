using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Abstractions.Services;
using Domain.Common;
using ApplicationCore.Employees.Responses;
using static Core.Errors;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using Entities.Histories;
using Entities.Abstractions.Services;

namespace ApplicationCore.Employees.Commands.ChangeDepartment;

public class ChangeDepartmentCommandHandler : ICommandHandler<ChangeDepartmentCommand, Result<EmployeeResultResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IHistoryRepository _historyRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly IEmployeeService _employeeService;
    private readonly IHistoryService _historyService;

    public ChangeDepartmentCommandHandler(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IHistoryRepository historyRepository,
        IDateTimeService dateTimeService,
        IEmployeeService employeeService,
        IHistoryService historyService)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _historyRepository = historyRepository;
        _dateTimeService = dateTimeService;
        _employeeService = employeeService;
        _historyService = historyService;
    }

    public async Task<Result<EmployeeResultResponse>> Handle(ChangeDepartmentCommand command, CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(command.Request.EmployeeId);
        Employee? employee = await _employeeRepository.Get(employeeId, cancellationToken);
        if (employee is null)
        {
            return new EmployeeNotFoundError(employeeId.Value);
        }

        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);

        if (department is null)
        {
            return new EmployeeDepartmentNotExistError(departmentId.Value);
        }

        if (employee.DepartmentId == departmentId)
        {
            return new EmployeeAlreadyInDepartmentError(employeeId.Value);
        }

        var result = _employeeService.ChangeDepartment(employee, departmentId);
        if (result.IsFailure)
        {
            return new EmployeeUnexpectedError(result.Error);
        }

        History? last = await _historyRepository.Get(employeeId, employee.DepartmentId, cancellationToken);
        if (last is not null)
        {
            var complete = _historyService.Complete(last, _dateTimeService.Today);

            if (complete.IsFailure)
            {
                return new EmployeeUnexpectedError(complete.Error);
            }
            last = complete.Value!;
            await _historyRepository.Update(last, cancellationToken);
        }

        last = History.Create(employeeId, departmentId, _dateTimeService.Today);
        await _employeeRepository.Update(employee, cancellationToken);
        await _historyRepository.Create(last, cancellationToken);
        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(employee));
    }
}

