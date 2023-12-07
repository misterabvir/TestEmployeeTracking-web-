using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Departments.Errors;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace Core.Departments.Commands.Delete;

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
            return (Error)DepartmentErrors.NotFound(departmentId.Value);
        }

        var employees = await _employeeRepository.GetByDepartmentId(departmentId, cancellationToken);
        if (employees.Any())
        {
            return DepartmentErrors.CantDeleteNotEmptyDepartment(departmentId.Value);
        }

        await _departmentRepository.Delete(departmentId, cancellationToken);
        return Result.Success();
    }
}
