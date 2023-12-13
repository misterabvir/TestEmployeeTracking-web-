using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Abstractions.Services;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.ChangeTitle;

public class ChangeDepartmentTitleCommandHandler : ICommandHandler<ChangeDepartmentTitleCommand, Result<DepartmentResultResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IDepartmentService _departmentService;

    public ChangeDepartmentTitleCommandHandler(IDepartmentRepository departmentRepository, IDepartmentService departmentService)
    {
        _departmentRepository = departmentRepository;
        _departmentService = departmentService;
    }

    public async Task<Result<DepartmentResultResponse>> Handle(ChangeDepartmentTitleCommand command, CancellationToken cancellationToken)
    {
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if(department is null)
        {
            return new DepartmentNotFoundError(departmentId.Value);
        }
        
        Title title = Title.Create(command.Request.Title);

        var result = _departmentService.ChangeTitle(department, title);
        if(result.IsFailure)
        {
            return new DepartmentUnexpectedError(result.Error);
        }

        await _departmentRepository.Update(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
