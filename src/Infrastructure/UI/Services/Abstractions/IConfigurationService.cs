namespace UI.Services.Abstractions;

public interface IConfigurationService
{
    DepartmentsProto.DepartmentsProtoClient DepartmentsClient { get; }
    EmployeesProto.EmployeesProtoClient EmployeesClient { get; }
    HistoriesProto.HistoriesProtoClient HistoriesClient { get; }
}
