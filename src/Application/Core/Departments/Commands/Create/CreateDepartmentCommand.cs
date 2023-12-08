using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Commands.Create;

public record CreateDepartmentCommand(CreateDepartmentCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;

