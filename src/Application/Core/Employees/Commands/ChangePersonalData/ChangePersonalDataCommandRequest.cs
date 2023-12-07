namespace Core.Employees.Commands.ChangePersonalData;

public record ChangePersonalDataCommandRequest(Guid EmployeeId, string LastName, string FirstName);
