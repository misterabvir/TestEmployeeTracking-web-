using Grpc.Net.Client;
using UI.Services.Abstractions;

namespace UI.Services.Implementations;

public class ConfigurationService : IConfigurationService
{
   
    public EmployeesProto.EmployeesProtoClient EmployeesClient { get; }
    public DepartmentsProto.DepartmentsProtoClient DepartmentsClient { get; }
    public HistoriesProto.HistoriesProtoClient HistoriesClient { get; }


    public ConfigurationService(GrpcChannel channel)
    {

        EmployeesClient = new EmployeesProto.EmployeesProtoClient(channel);
        DepartmentsClient = new(channel);
        HistoriesClient = new(channel);
    }
}
