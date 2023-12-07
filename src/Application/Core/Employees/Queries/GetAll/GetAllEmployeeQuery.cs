using Core.Abstractions.Common;
using Core.Common;
using Core.Employees.Requests;

namespace Core.Employees.Queries.GetAll;

public sealed class GetAllEmployeeQuery : IQuery<Result<IEnumerable<EmployeeResultResponse>>>
{
}
