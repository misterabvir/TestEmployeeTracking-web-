using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Queries.GetAll;

/// <summary>
/// Query for getting all departments
/// </summary>
public class GetAllDepartmentsQuery : IQuery<Result<IEnumerable<DepartmentResultResponse>>>
{
    
}
