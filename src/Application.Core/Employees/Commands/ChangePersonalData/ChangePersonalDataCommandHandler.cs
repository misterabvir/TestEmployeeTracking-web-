using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Employees.Responses;
using static Core.Errors;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using Entities.Abstractions.Services;

namespace ApplicationCore.Employees.Commands.ChangePersonalData;

/// <summary>
/// Handler for change personal data
/// </summary>
public class ChangePersonalDataCommandHandler : ICommandHandler<ChangePersonalDataCommand, Result<EmployeeResultResponse>>
{
    /// <summary>
    /// Repository for <see cref="Employee"/>
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;
    /// <summary>
    /// Domain service for updating <see cref="Employee"/>
    /// </summary>
    private readonly IEmployeeService _employeeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePersonalDataCommandHandler"/> class.
    /// </summary>
    /// <param name="employeeRepository"> Repository for <see cref="Employee"/></param>
    /// <param name="employeeService"> Domain service for updating <see cref="Employee"/></param>
    public ChangePersonalDataCommandHandler(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
    {
        _employeeRepository = employeeRepository;
        _employeeService = employeeService;
    }

    /// <summary>
    /// Handler for change personal data
    /// </summary>
    /// <param name="command"> Command with employee id and new personal data</param>
    /// <param name="cancellationToken"> Cancellation token</param>
    /// <returns> Result with updated employee or error </returns>
    public async Task<Result<EmployeeResultResponse>> Handle(ChangePersonalDataCommand command, CancellationToken cancellationToken)
    {
        // Check if employee exists
        EmployeeId employeeId = EmployeeId.Create(command.Request.EmployeeId);
        Employee? employee = await _employeeRepository.Get(employeeId, cancellationToken);
        if (employee is null)
        {
            return new EmployeeNotFoundError(employeeId.Value);
        }
        
        // Change personal data
        LastName lastName = LastName.Create(command.Request.LastName);
        FirstName firstName = FirstName.Create(command.Request.FirstName);
        var result = _employeeService.ChangePersonalData(employee, lastName, firstName);
        if(result.IsFailure)
        {
            return new EmployeeUnexpectedError(result.Error);
        }
        
        // Update employee
        await _employeeRepository.Update(employee, cancellationToken);
        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(employee));
    }
}
