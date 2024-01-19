using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.Delete;

public class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommand, Result>
{
    /// <summary>
    /// Repository for departments
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;
    
    /// <summary>
    /// Repository for employees
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDepartmentCommandHandler"/> class
    /// </summary>
    /// <param name="departmentRepository"> Repository for departments </param>
    /// <param name="employeeRepository"> Repository for employees </param>
    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Handler for deleting department
    /// </summary>
    /// <param name="command"> Command for deleting department </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> Result success or error </returns>
    public async Task<Result> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
    {
        // Check if department exists
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if (department is null)
        {
            return (Error)new DepartmentNotFoundError(departmentId.Value);
        }

        // Check if department is not empty
        var employees = await _employeeRepository.GetByDepartmentId(departmentId, cancellationToken);
        if (employees.Any())
        {
            return new DepartmentCantDeleteNotEmptyError(departmentId.Value);
        }

        // Delete department
        await _departmentRepository.Delete(departmentId, cancellationToken);
        return Result.Success();
    }
}
