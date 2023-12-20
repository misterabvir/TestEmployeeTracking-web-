using Client;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<HttpClient>();
builder.RootComponents.Add<App>("#app");

builder.Services.AddSingleton(services =>
{
    var config = services.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("Server") ?? throw new Exception("Connection string not found.");

    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());

    return GrpcChannel.ForAddress(connectionString, new GrpcChannelOptions { HttpHandler = httpHandler });
});

await builder.Build().RunAsync();
