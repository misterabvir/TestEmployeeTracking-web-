using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Departments.Errors;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Abstractions.Services;

namespace ApplicationCore.Departments.Commands.SetParent;

public class SetDepartmentParentCommandHandler : ICommandHandler<SetDepartmentParentCommand, Result<DepartmentResultResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IDepartmentService _departmentService;
    public SetDepartmentParentCommandHandler(IDepartmentRepository departmentRepository, IDepartmentService departmentService)
    {
        _departmentRepository = departmentRepository;
        _departmentService = departmentService;
    }

    public async Task<Result<DepartmentResultResponse>> Handle(SetDepartmentParentCommand command, CancellationToken cancellationToken)
    {
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if(department is null)
        {
            return DepartmentErrors.NotFound(departmentId.Value);
        }

        if(department.ParentId is null && command.Request.ParentDepartmentId is null)
        {
            return DepartmentErrors.AlreadyRoot(departmentId.Value);
        }
        DepartmentId? parentId = null;
        
        if(command.Request.ParentDepartmentId is not null)
        { 
            parentId = DepartmentId.Create(command.Request.ParentDepartmentId.Value);
            Department? parent = await _departmentRepository.Get(parentId, cancellationToken);
            if(parent is null)
            {
                return DepartmentErrors.NotFound(parentId.Value);
            }
        }

        if(_departmentService.ChangeParentDepartment(department, parentId).IsFailure)
        {
            return DepartmentErrors.Unexpected(departmentId.Value);   //TODO refactor
        }
        
        await _departmentRepository.Update(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }

}
