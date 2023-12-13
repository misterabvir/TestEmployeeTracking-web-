using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using static Core.Errors;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace ApplicationCore.Departments.Queries.GetById;

public class GetDepartmentByIdQueryHandler : IQueryHandler<GetDepartmentByIdQuery, Result<DepartmentResultResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    
    
    public async Task<Result<DepartmentResultResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        DepartmentId departmentId = DepartmentId.Create(request.Request.DepartmentId);
        Department? department = await _departmentRepository.Get(departmentId, cancellationToken);
        if (department is null)
        {
            return new DepartmentNotFoundError(departmentId.Value);
        }
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
