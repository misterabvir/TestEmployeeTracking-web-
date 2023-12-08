using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Departments.Errors;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Abstractions.Services;

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
            return DepartmentErrors.NotFound(departmentId.Value);
        }
        
        Title title = Title.Create(command.Request.Title);
        if(!_departmentService.ChangeTitle(department, title).IsSuccess)
        {
            return DepartmentErrors.Unexpected(departmentId.Value);
        }

        await _departmentRepository.Update(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
