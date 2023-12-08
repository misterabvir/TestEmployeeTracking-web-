using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Departments.Responses;

namespace ApplicationCore.Departments.Commands.ChangeTitle;

public record ChangeDepartmentTitleCommand(ChangeDepartmentTitleCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;