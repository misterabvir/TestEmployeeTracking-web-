using Core.Abstractions.Common;
using Core.Abstractions.Repositories;
using Core.Common;
using Core.Departments.Requests;

namespace Core.Departments.Queries.GetAll;

public class GetAllDepartmentsQueryHandler : IQueryHandler<GetAllDepartmentsQuery, Result<IEnumerable<DepartmentResultResponse>>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<IEnumerable<DepartmentResultResponse>>> Handle(GetAllDepartmentsQuery query, CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.Get(cancellationToken);
        return Result<IEnumerable<DepartmentResultResponse>>.Success(departments.Select(s=>DepartmentResultResponse.FromDomain(s)));
    }
}