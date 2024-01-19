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

namespace ApplicationCore.Employees.Commands.Create;

/// <summary>
/// Handler for create new employee
/// </summary>
public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, Result<EmployeeResultResponse>>
{
    /// <summary>
    /// Repository for <see cref="Department"/>
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;
    /// <summary>
    /// Repository for <see cref="Employee"/>
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;
    /// <summary>
    ///  Repository for <see cref="History"/>
    /// </summary>
    private readonly IHistoryRepository _historyRepository;
    
    /// <summary>
    /// Service for get current date and time
    /// </summary> 
    private readonly IDateTimeService _dateTimeService;
   
   /// <summary>
   /// Initializes a new instance of the <see cref="CreateEmployeeCommandHandler"/> class.
   /// </summary>
   /// <param name="employeeRepository"> Repository for <see cref="Employee"/></param>
   /// <param name="departmentRepository"> Repository for <see cref="Department"/></param>
   /// <param name="historyRepository"> Repository for <see cref="History"/></param>
   /// <param name="dateTimeService"> Service for get current date and time</param>
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

    /// <summary>
    /// Handler for create new employee
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> Result with created employee or error </returns>
    public async Task<Result<EmployeeResultResponse>> Handle( CreateEmployeeCommand command, CancellationToken cancellationToken)
    {        
        // Check if department exists
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if (department is null)
        {
            return new EmployeeDepartmentNotExistError(departmentId.Value);
        }

        // Create new employee
        LastName lastName = LastName.Create(command.Request.LastName);
        FirstName firstName = FirstName.Create(command.Request.FirstName);
        Employee employee = Employee.Create(lastName, firstName, departmentId);
        await _employeeRepository.Create(employee, cancellationToken);

        // Create new history
        History history = History.Create(employee.Id, departmentId, _dateTimeService.Today);
        await _historyRepository.Create(history, cancellationToken);

        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(employee));
    }
}
