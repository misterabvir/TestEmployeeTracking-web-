using Infrastructure.Client.Services.Abstractions;
using Grpc.Net.Client;
using ProtoContracts;

namespace Infrastructure.Client.Services;

public class ClientService : IClientService
{
    public DepartmentsProto.DepartmentsProtoClient DepartmentsClient { get; init; }

    public EmployeesProto.EmployeesProtoClient EmployeesClient { get; init; }

    public HistoriesProto.HistoriesProtoClient HistoriesClient { get; init; }

    public IdentityProto.IdentityProtoClient IdentityClient { get; init; }

    public ClientService(GrpcChannel channel)
    {
        DepartmentsClient = new(channel);
        EmployeesClient = new(channel);
        HistoriesClient = new(channel);
        IdentityClient = new(channel);
    }
}
