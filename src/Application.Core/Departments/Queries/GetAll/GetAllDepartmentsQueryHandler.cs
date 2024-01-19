using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Queries.GetAll;

/// <summary>
/// Handler for getting all departments
/// </summary>
public class GetAllDepartmentsQueryHandler : IQueryHandler<GetAllDepartmentsQuery, Result<IEnumerable<DepartmentResultResponse>>>
{
    /// <summary>
    /// Repository for <see cref="Department"/>
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllDepartmentsQueryHandler"/> class.
    /// </summary>
    /// <param name="departmentRepository"> Repository for <see cref="Department"/></param>
    public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    /// <summary>
    /// Handler for getting all departments
    /// </summary>
    /// <param name="query"> Query for getting all departments </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns> Result with list of departments or error </returns>
    public async Task<Result<IEnumerable<DepartmentResultResponse>>> Handle(GetAllDepartmentsQuery query, CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.Get(cancellationToken);
        return Result<IEnumerable<DepartmentResultResponse>>.Success(departments.Select(DepartmentResultResponse.FromDomain));
    }
}