using ApplicationCore.Abstractions.Common;
using ApplicationCore.Employees.Responses;
using Domain.Common;

namespace ApplicationCore.Employees.Queries.GetById;

public record GetEmployeeByIdQuery(GetEmployeeByIdQueryRequest Request) : IQuery<Result<EmployeeResultResponse>>;

