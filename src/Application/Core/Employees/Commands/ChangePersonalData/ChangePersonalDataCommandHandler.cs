using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Employees.Responses;
using ApplicationCore.Employees.Errors;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using Entities.Abstractions.Services;

namespace ApplicationCore.Employees.Commands.ChangePersonalData;

public class ChangePersonalDataCommandHandler : ICommandHandler<ChangePersonalDataCommand, Result<EmployeeResultResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeService _employeeService;

    public ChangePersonalDataCommandHandler(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
    {
        _employeeRepository = employeeRepository;
        _employeeService = employeeService;
    }

    public async Task<Result<EmployeeResultResponse>> Handle(ChangePersonalDataCommand command, CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(command.Request.EmployeeId);
        Employee? employee = await _employeeRepository.Get(employeeId, cancellationToken);
        if (employee is null)
        {
            return EmployeeErrors.NotFound(employeeId.Value);
        }
        
        LastName lastName = LastName.Create(command.Request.LastName);
        FirstName firstName = FirstName.Create(command.Request.FirstName);
        
        if(_employeeService.ChangePersonalData(employee, lastName, firstName).IsFailure)
        {
            return EmployeeErrors.UnexpectedError(employeeId.Value);
        }
        
        await _employeeRepository.Update(employee, cancellationToken);
        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(employee));
    }
}
