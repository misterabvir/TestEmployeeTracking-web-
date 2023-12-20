using Client.Services.Abstractions;
using Grpc.Net.Client;

namespace Client.Services;

public class ConfigurationService : IConfigurationService
{
    public DepartmentsProto.DepartmentsProtoClient DepartmentsClient { get; init; }

    public EmployeesProto.EmployeesProtoClient EmployeesClient { get; init; }

    public HistoriesProto.HistoriesProtoClient HistoriesClient { get; init; }

    public ConfigurationService(GrpcChannel channel)
    {
        DepartmentsClient = new(channel);
        EmployeesClient = new(channel);
        HistoriesClient = new(channel);
    }
}
