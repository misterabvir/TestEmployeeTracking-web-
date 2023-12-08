using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Queries.GetAll;

public class GetAllDepartmentsQuery : IQuery<Result<IEnumerable<DepartmentResultResponse>>>
{
    
}
