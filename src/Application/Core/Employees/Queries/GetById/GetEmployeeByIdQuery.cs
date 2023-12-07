using Core.Abstractions.Common;
using Core.Common;
using Core.Employees.Requests;

namespace Core.Employees.Queries.GetById;

public record GetEmployeeByIdQuery(GetEmployeeByIdQueryRequest Request) : IQuery<Result<EmployeeResultResponse>>;

