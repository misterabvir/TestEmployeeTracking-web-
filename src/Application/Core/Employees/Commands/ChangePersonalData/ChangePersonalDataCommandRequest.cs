namespace ApplicationCore.Employees.Commands.ChangePersonalData;

public record ChangePersonalDataCommandRequest(Guid EmployeeId, string LastName, string FirstName);
