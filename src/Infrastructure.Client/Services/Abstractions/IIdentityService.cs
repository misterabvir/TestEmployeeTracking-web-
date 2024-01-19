using ProtoContracts;

namespace Infrastructure.Client.Services.Abstractions;

public interface IIdentityService
{
    Task<AuthenticationResponse> Register(string email, string password);
    Task<AuthenticationResponse> Login(string email, string password);
}