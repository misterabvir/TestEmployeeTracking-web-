using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.Create;

/// <summary>
/// Handler for creating department
/// </summary>
public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, Result<DepartmentResultResponse>>
{
    /// <summary>
    /// Repository for departments
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDepartmentCommandHandler"/> class.
    /// </summary>
    /// <param name="departmentRepository"> Repository for <see cref="Department"/> </param>
    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    /// <summary>
    /// Handler for creating department. Returns <see cref="Result{T}"/> of <see cref="DepartmentResultResponse"/>.
    /// </summary>
    /// <param name="command"> Command for creating department </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> <see cref="Result{T}"/> of <see cref="DepartmentResultResponse"/> </returns>
    public async Task<Result<DepartmentResultResponse>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        Title title = Title.Create(command.Request.Title);
        DepartmentId? parentId = null;
        Department? department;

        // Check if parent department exists
        if (command.Request.ParentDepartmentId is not null)
        {
            parentId = DepartmentId.Create(command.Request.ParentDepartmentId.Value);
            Department? parent = await _departmentRepository.Get(parentId, cancellationToken);
            if(parent is null)
            {
                return new DepartmentParentNotFoundError(command.Request.ParentDepartmentId.Value);
            }
        }

        // Check if department with the same name and parent exists
        department = await _departmentRepository.GetByNameAndParentId(title, parentId!, cancellationToken);
        if (department is not null)
        {
            return new DepartmentAlreadyExistError(department.Id.Value);
        }

        // Create new department
        department = Department.Create(title, parentId);
        await _departmentRepository.Create(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
