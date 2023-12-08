using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Queries.GetById;

public record GetDepartmentByIdQuery(GetDepartmentByIdQueryRequest Request) : IQuery<Result<DepartmentResultResponse>>;