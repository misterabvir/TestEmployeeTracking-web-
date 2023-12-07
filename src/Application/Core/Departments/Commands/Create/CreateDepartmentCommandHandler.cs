using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Departments.Requests;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace Core.Departments.Commands.Create;

public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, Result<DepartmentResultResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<DepartmentResultResponse>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var departmentId = command.Request.ParentDepartmentId is null ? null : DepartmentId.Create(command.Request.ParentDepartmentId.Value);
        Department department = Department.Create(Title.Create(command.Request.Title), departmentId);
        await _departmentRepository.Create(department, cancellationToken);
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
