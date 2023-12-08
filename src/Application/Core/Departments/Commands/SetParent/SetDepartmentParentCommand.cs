using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Commands.SetParent;

public record SetDepartmentParentCommand(SetDepartmentParentCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;
