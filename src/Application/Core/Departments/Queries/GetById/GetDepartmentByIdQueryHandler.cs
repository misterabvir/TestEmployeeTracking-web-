using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Departments.Errors;
using Core.Departments.Requests;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace Core.Departments.Queries.GetById;

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
            return DepartmentErrors.NotFound(departmentId.Value);
        }
        return Result<DepartmentResultResponse>.Success(DepartmentResultResponse.FromDomain(department));
    }
}
