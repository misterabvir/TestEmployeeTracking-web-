using Blazored.LocalStorage;
using Client;
using Client.Authorizations;
using Client.Services;
using Client.Services.Abstractions;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<HttpClient>();
builder.RootComponents.Add<App>("#app");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthorizeApi>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton(services =>
{
    var config = services.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("Server") ?? throw new Exception("Connection string not found.");

    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());

    return GrpcChannel.ForAddress(connectionString, new GrpcChannelOptions { HttpHandler = httpHandler });
});

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

await builder.Build().RunAsync();
