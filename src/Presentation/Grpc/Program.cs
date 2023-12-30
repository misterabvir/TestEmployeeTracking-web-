using Grpc.Services;
using Persistence;
using ApplicationCore;
using TimeService;
using Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDomain()
    .AddDateTimeService()
    .AddPersistence(builder.Configuration)
    .AddApplication();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));



var app = builder.Build();
app.UseGrpcWeb();
app.UseCors();

app.MapGrpcService<EmployeeService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<DepartmentService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<HistoryService>().EnableGrpcWeb().RequireCors("AllowAll");
app.Run();
