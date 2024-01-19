using ApplicationCore.Abstractions.Common;
using ApplicationCore.Employees.Responses;
using Domain.Common;

namespace ApplicationCore.Employees.Queries.GetById;

/// <summary>
/// Query to get employee by id
/// </summary>
/// <param name="Request"> Request with employee id </param>
public record GetEmployeeByIdQuery(GetEmployeeByIdQueryRequest Request) : IQuery<Result<EmployeeResultResponse>>;

