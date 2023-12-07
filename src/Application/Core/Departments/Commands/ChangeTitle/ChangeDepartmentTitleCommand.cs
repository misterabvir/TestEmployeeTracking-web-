using Core.Abstractions.Common;
using Core.Common;
using Core.Departments.Requests;

namespace Core.Departments.Commands.ChangeTitle;

public record ChangeDepartmentTitleCommand(ChangeDepartmentTitleCommandRequest Request) : ICommand<Result<DepartmentResultResponse>>;