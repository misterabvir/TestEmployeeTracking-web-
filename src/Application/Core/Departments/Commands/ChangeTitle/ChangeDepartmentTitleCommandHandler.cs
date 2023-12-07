using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Departments.Errors;
using Core.Departments.Requests;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace Core.Departments.Commands.ChangeTitle;

public class ChangeDepartmentTitleCommandHandler : ICommandHandler<ChangeDepartmentTitleCommand, Result<DepartmentResultResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public ChangeDepartmentTitleCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<DepartmentResultResponse>> Handle(ChangeDepartmentTitleCommand command, CancellationToken cancellationToken)
    {
        DepartmentId departmentId = DepartmentId.Create(command.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if(department is null)
        {
            return DepartmentErrors.NotFound(departmentId.Value);
        }
        department.ChangeTitle(Title.Create(command.Request.Title));
        await _departmentRepository.Update(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
