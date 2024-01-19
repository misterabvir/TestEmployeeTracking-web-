using Infrastructure.Client.Services.Abstractions;
using ProtoContracts;

namespace Infrastructure.Client.Services;

public class IdentityService : IIdentityService
{
    private readonly IClientService _service;

    public IdentityService(IClientService service)
    {
        _service = service;
    }

    public async Task<AuthenticationResponse> Login(string email, string password)
    {
        var command = new IdentityRequest() { Email = email, Password = password };
        return await _service.IdentityClient.LoginAsync(command);
    }

    public async Task<AuthenticationResponse> Register(string email, string password)
    {
        var command = new IdentityRequest() { Email = email, Password = password };
        return await _service.IdentityClient.RegisterAsync(command);
    }
}