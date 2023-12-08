using ApplicationCore.Abstractions.Common;
using Domain.Common;
using ApplicationCore.Employees.Responses;

namespace ApplicationCore.Employees.Commands.ChangePersonalData;

public record ChangePersonalDataCommand(ChangePersonalDataCommandRequest Request) : ICommand<Result<EmployeeResultResponse>>;
