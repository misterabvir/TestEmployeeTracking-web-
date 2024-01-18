using ApplicationCore;
using Entities;
using Grpc;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddInfrastructureServices(builder.Configuration)
    .AddPersistence(builder.Configuration)
    .AddApplication()
    .AddPresentation(builder.Configuration);


builder
    .Build()
    .UseIdentity()
    .UseGrpc()
    .UseMapServices(builder.Configuration)
    .Run();
