using ApplicationCore.Abstractions.Common;
using ApplicationCore.Abstractions.Repositories;
using Domain.Common;
using static Core.Errors;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;

namespace ApplicationCore.Departments.Queries.GetById;

/// <summary>
/// Handler for getting department by id
/// </summary> 
public class GetDepartmentByIdQueryHandler : IQueryHandler<GetDepartmentByIdQuery, Result<DepartmentResultResponse>>
{
   /// <summary>
   /// Repository for <see cref="Department"/>
   /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDepartmentByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="departmentRepository"></param>
    public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    
    /// <summary>
    /// Handler for getting department by id
    /// </summary>
    /// <param name="request"> Request for getting department by id </param>
    /// <param name="cancellationToken"> CancellationToken </param>
    /// <returns>Result with department or error</returns>
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
