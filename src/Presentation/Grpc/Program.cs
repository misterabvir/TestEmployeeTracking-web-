using Grpc.Services;
using Persistence;
using Core;
using TimeService;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDateTimeService()
    .AddPersistence(builder.Configuration)
    .AddApplication();
builder.Services.AddGrpc();

var app = builder.Build();


app.MapGrpcService<EmployeeService>();
app.Run();
