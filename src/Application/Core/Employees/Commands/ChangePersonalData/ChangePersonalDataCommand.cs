using Core.Abstractions.Common;
using Core.Common;
using Core.Employees.Requests;

namespace Core.Employees.Commands.ChangePersonalData;

public record ChangePersonalDataCommand(ChangePersonalDataCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
