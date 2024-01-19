using ApplicationCore.Abstractions.Common;
using ApplicationCore.Employees.Responses;
using Domain.Common;
using Entities.Departments;

namespace Application.Core.Employees.Queries.GetByDepartmentId;

/// <summary>
/// Query for get employees in <see cref="Department"/>
/// </summary>
/// <param name="Request">Reuest for query with department id</param>
public record GetEmployeesByDepartmentIdQuery(GetEmployeesByDepartmentIdQueryRequest Request) : IQuery<Result<IEnumerable<EmployeeResultResponse>>>;
