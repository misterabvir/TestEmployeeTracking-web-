using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Employees.Responses;

namespace ApplicationCore.Employees.Queries.GetAll;

public sealed class GetAllEmployeeQuery : IQuery<Result<IEnumerable<EmployeeResultResponse>>>
{
}
