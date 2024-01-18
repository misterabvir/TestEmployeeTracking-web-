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

/// <summary>
/// Handler for change department for employee
/// </summary>
public class ChangeDepartmentCommandHandler : ICommandHandler<ChangeDepartmentCommand, Result<EmployeeResultResponse>>
{
    /// <summary>
    /// Repository for <see cref="Employee"/>
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;
    /// <summary>
    /// Repository for <see cref="Department"/>
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;
    /// <summary>
    /// Repository for <see cref="History"/>
    /// </summary>
    private readonly IHistoryRepository _historyRepository;
    /// <summary>
    /// Service for <see cref="DateTime"/>
    /// </summary>
    private readonly IDateTimeService _dateTimeService;
    /// <summary>
    /// Service for <see cref="Employee"/>
    /// </summary>
    private readonly IEmployeeService _employeeService;
    /// <summary>
    /// Service for <see cref="History"/>
    /// </summary>
    private readonly IHistoryService _historyService;

    /// <summary>
    ///  Initializes a new instance of the <see cref="ChangeDepartmentCommandHandler"/> class.
    /// </summary>
    /// <param name="employeeRepository"> Repository for <see cref="Employee"/></param>
    /// <param name="departmentRepository"> Repository for <see cref="Department"/></param>
    /// <param name="historyRepository"> Repository for <see cref="History"/></param>
    /// <param name="dateTimeService"> Service for <see cref="DateTime"/></param>
    /// <param name="employeeService"> Service for <see cref="Employee"/></param>
    /// <param name="historyService"> Service for <see cref="History"/></param>
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

    /// <summary>
    /// Handler for change department for employee
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> Result with employee or error </returns>
    public async Task<Result<EmployeeResultResponse>> Handle(ChangeDepartmentCommand command, CancellationToken cancellationToken)
    {
        // Check if employee exist
        EmployeeId employeeId = EmployeeId.Create(command.Request.EmployeeId);
        Employee? employee = await _employeeRepository.Get(employeeId, cancellationToken);
        if (employee is null)
        {
            return new EmployeeNotFoundError(employeeId.Value);
        }

        // Check if department exist
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);

        if (department is null)
        {
            return new EmployeeDepartmentNotExistError(departmentId.Value);
        }

        // Check if employee already in this department
        if (employee.DepartmentId == departmentId)
        {
            return new EmployeeAlreadyInDepartmentError(employeeId.Value);
        }

        // Change department
        var lastDepartmentId = employee.DepartmentId;
        var result = _employeeService.ChangeDepartment(employee, departmentId);
        if (result.IsFailure)
        {
            return new EmployeeUnexpectedError(result.Error);
        }

        // Update history
        History? last = await _historyRepository.Get(employeeId, lastDepartmentId, cancellationToken);
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

        // Update employee
        await _employeeRepository.Update(employee, cancellationToken);

        // Create and save history
        last = History.Create(employeeId, departmentId, _dateTimeService.Today);
        await _historyRepository.Create(last, cancellationToken);
        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(employee));
    }
}

