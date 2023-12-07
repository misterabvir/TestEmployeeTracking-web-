using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Employees.Errors;
using Entities.Employees;
using Entities.Employees.ValueObjects;

namespace Core.Employees.Commands.Delete;

public class DeleteEmployeeCommandHandler : 
    ICommandHandler<DeleteEmployeeCommand, Result>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(
        DeleteEmployeeCommand command, 
        CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(command.Request.EmployeeId);
        Employee? employee = await _employeeRepository.Get(employeeId, cancellationToken);
        if(employee is null)
        {
            return (Error)EmployeeErrors.NotFound(employeeId.Value);
        }
        await _employeeRepository.Delete(employeeId, cancellationToken);
        return Result.Success();
    }
}
