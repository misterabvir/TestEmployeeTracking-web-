//using Grpc.Core;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using UI.Services.Abstractions;

namespace UI.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    private readonly IConfigurationService _service;
    public DepartmentService(IConfigurationService service)
    {
        _service = service;
    }


    public Task<DepartmentResponse?> ChangeTitle(Guid departmenId, string title)
    {
        throw new NotImplementedException();
    }

    public Task<DepartmentResponse?> Create(string title, Guid? parentId = null)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid departmenId)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<DepartmentResponse?>> Get()
    {
        await Task.CompletedTask;
        //var departmnets =_service.DepartmentsClient.GetAll(new GetAllDepartmentRequest());
        var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
        var channel = GrpcChannel.ForAddress("http://localhost:5003", new GrpcChannelOptions { HttpHandler = httpHandler });
        var departmnets = new DepartmentsProto.DepartmentsProtoClient(channel);
        var res = departmnets.GetAll(new GetAllDepartmentRequest()).Departments;

        //await foreach (var item in res.ResponseStream.ReadAllAsync())
        //{
        //    Console.WriteLine(item.Id);
        //}
        return res;
    }

    public Task<DepartmentResponse?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<DepartmentResponse?> SetParent(Guid departmenId, Guid? parentId)
    {
        throw new NotImplementedException();
    }
}
