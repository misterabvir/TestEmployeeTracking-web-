using Grpc.Core;
using Grpc.Net.Client;
using GrpcConsoleTests;

var channel = GrpcChannel.ForAddress("http://localhost:5003");
var d = new DepartmentsProto.DepartmentsProtoClient(channel);
var res = d.GetAll(new GetAllDepartmentRequest());

foreach (var item in res.Departments)
{
    Console.WriteLine(item.Id);
}
