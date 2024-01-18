using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Queries.GetById;

/// <summary>
/// Query for getting department by id
/// </summary>
/// <param name="Request"> Request for getting department by id </param>
public record GetDepartmentByIdQuery(GetDepartmentByIdQueryRequest Request) : IQuery<Result<DepartmentResultResponse>>;