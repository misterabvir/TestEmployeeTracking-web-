using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Abstractions.Services;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.ChangeTitle;

/// <summary>
/// Handler for changing department title
/// </summary>
public class ChangeDepartmentTitleCommandHandler : ICommandHandler<ChangeDepartmentTitleCommand, Result<DepartmentResultResponse>>
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
    /// Initializes a new instance of the <see cref="ChangeDepartmentTitleCommandHandler"/> class.
    /// </summary>
    /// <param name="departmentRepository"> Repository for <see cref="Department"/> </param>
    /// <param name="departmentService"> Domain service for <see cref="Department"/> </param>
    public ChangeDepartmentTitleCommandHandler(IDepartmentRepository departmentRepository, IDepartmentService departmentService)
    {
        _departmentRepository = departmentRepository;
        _departmentService = departmentService;
    }

    /// <summary>
    /// Handler for changing department title
    /// </summary>
    /// <param name="command"> Command for changing department title </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns></returns>
    public async Task<Result<DepartmentResultResponse>> Handle(ChangeDepartmentTitleCommand command, CancellationToken cancellationToken)
    {
        // Get department
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        
        // Check if department exists
        if(department is null)
        {
            return new DepartmentNotFoundError(departmentId.Value);
        }

        // Check if title is apply  
        Title title = Title.Create(command.Request.Title);
        var result = _departmentService.ChangeTitle(department, title);
        if(result.IsFailure)
        {
            return new DepartmentUnexpectedError(result.Error);
        }

        // Update department
        await _departmentRepository.Update(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
