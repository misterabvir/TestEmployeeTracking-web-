using Core.Abstractions.Common;
using Core.Common;
using Core.Departments.Requests;

namespace Core.Departments.Queries.GetById;

public record GetDepartmentByIdQuery(GetDepartmentByIdQueryRequest Request) : IQuery<Result<DepartmentResultResponse>>;