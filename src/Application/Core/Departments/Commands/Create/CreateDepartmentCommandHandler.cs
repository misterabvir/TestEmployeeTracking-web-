using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using static Core.Errors;

namespace ApplicationCore.Departments.Commands.Create;

public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, Result<DepartmentResultResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<DepartmentResultResponse>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        Title title = Title.Create(command.Request.Title);
        DepartmentId? parentId = null;
        Department? department;
        if (command.Request.ParentDepartmentId is not null)
        {
            parentId = DepartmentId.Create(command.Request.ParentDepartmentId.Value);
            Department? parent = await _departmentRepository.Get(parentId, cancellationToken);
            if(parent is null)
            {
                return new DepartmentParentNotFoundError(command.Request.ParentDepartmentId.Value);
            }
        }

        department = await _departmentRepository.GetByNameAndParentId(title, parentId, cancellationToken);
        if (department is not null)
        {
            return new DepartmentAlreadyExistError(department.Id.Value);
        }

        department = Department.Create(title, parentId);
        await _departmentRepository.Create(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
