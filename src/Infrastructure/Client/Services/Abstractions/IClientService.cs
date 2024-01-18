namespace Client.Services.Abstractions;

public interface IClientService
{
    DepartmentsProto.DepartmentsProtoClient DepartmentsClient { get; }
    EmployeesProto.EmployeesProtoClient EmployeesClient { get; }
    HistoriesProto.HistoriesProtoClient HistoriesClient { get; }
    IdentityProto.IdentityProtoClient IdentityClient { get; }
}
