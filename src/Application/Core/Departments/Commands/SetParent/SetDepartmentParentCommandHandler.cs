using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Departments.Errors;
using Core.Departments.Requests;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace Core.Departments.Commands.SetParent;

public class SetDepartmentParentCommandHandler : ICommandHandler<SetDepartmentParentCommand, Result<DepartmentResultResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public SetDepartmentParentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
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

        department.SetParent(parentId);
        await _departmentRepository.Update(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }

}
