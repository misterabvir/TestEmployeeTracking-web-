using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.Delete;

public class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommand, Result>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
    {
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if (department is null)
        {
            return (Error)new DepartmentNotFoundError(departmentId.Value);
        }

        var employees = await _employeeRepository.GetByDepartmentId(departmentId, cancellationToken);
        if (employees.Any())
        {
            return new DepartmentCantDeleteNotEmptyError(departmentId.Value);
        }

        await _departmentRepository.Delete(departmentId, cancellationToken);
        return Result.Success();
    }
}
