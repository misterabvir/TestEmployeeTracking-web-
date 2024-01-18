using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Abstractions.Services;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.SetParent;

/// <summary>
/// Handler for setting parent department
/// </summary>
public class SetDepartmentParentCommandHandler : ICommandHandler<SetDepartmentParentCommand, Result<DepartmentResultResponse>>
{
    /// <summary>
    /// Repository for <see cref="Department"/>
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;
    /// <summary>
    /// Domain service for <see cref="Department"/>
    /// </summary>
    private readonly IDepartmentService _departmentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetDepartmentParentCommandHandler"/> class.
    /// </summary>
    /// <param name="departmentRepository"> Repository for <see cref="Department"/></param>
    /// <param name="departmentService"> Domain service for <see cref="Department"/></param>
    public SetDepartmentParentCommandHandler(IDepartmentRepository departmentRepository, IDepartmentService departmentService)
    {
        _departmentRepository = departmentRepository;
        _departmentService = departmentService;
    }

    /// <summary>
    /// Handler for setting parent department
    /// </summary>
    /// <param name="command"> Command for setting parent department </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns></returns>
    public async Task<Result<DepartmentResultResponse>> Handle(SetDepartmentParentCommand command, CancellationToken cancellationToken)
    {
        // Check if department exists
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if (department is null)
        {
            return new DepartmentNotFoundError(departmentId.Value);
        }

        // Check if department is not root if parent department is not set
        if (department.ParentId is null && command.Request.ParentDepartmentId is null)
        {
            return new DepartmentAlreadyRootError(departmentId.Value);
        }

        // Check if parent department exists
        DepartmentId? parentId = null;
        if (command.Request.ParentDepartmentId is not null)
        {
            parentId = DepartmentId.Create(command.Request.ParentDepartmentId.Value);
            Department? parent = await _departmentRepository.Get(parentId, cancellationToken);
            if (parent is null)
            {
                return new DepartmentParentNotFoundError(parentId.Value);
            }
        }

        // Change parent department
        var result = _departmentService.ChangeParentDepartment(department, parentId);
        if (result.IsFailure)
        {
            return new DepartmentUnexpectedError(result.Error);
        }

        // Update department
        await _departmentRepository.Update(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }

}
