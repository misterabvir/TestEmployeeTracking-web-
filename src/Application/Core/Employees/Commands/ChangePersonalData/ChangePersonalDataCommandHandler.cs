using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Employees.Requests;
using Core.Employees.Errors;
using Entities.Employees;
using Entities.Employees.ValueObjects;

namespace Core.Employees.Commands.ChangePersonalData;

public class ChangePersonalDataCommandHandler : ICommandHandler<ChangePersonalDataCommand, Result<EmployeeResultResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public ChangePersonalDataCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
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
        employee.ChangePersonalData(lastName, firstName);
        await _employeeRepository.Update(employee, cancellationToken);
        return Result<EmployeeResultResponse>.Success(EmployeeResultResponse.FromDomain(employee));
    }
}
