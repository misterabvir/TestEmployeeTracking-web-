using Blazored.LocalStorage;
using Infrastructure.Client.Services.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using ProtoContracts;


namespace Infrastructure.Client.Authorizations;

public class AuthorizeApi
{
    private readonly IdentityAuthenticationStateProvider _provider;
    private readonly ILocalStorageService _localStorage;
    private readonly IIdentityService _identityService;

    public AuthorizeApi(AuthenticationStateProvider provider, ILocalStorageService localStorage, IIdentityService identityService)
    {
        _provider = (provider as IdentityAuthenticationStateProvider)!;
        _localStorage = localStorage;
        _identityService = identityService;
    }

    public async Task<AuthenticationResponse> Login(string email, string password)
    {
        var identity = await _identityService.Login(email, password);

        if (identity.IsSucces)
        {
            await _localStorage.SetItemAsync("token", identity.User.Token);
            _provider.MarkUserAsAuthenticated(identity.User.Token);
        }
        return identity;
    }

    public async Task<AuthenticationResponse> Register(string email, string password)
    {
        var identity = await _identityService.Register(email, password);

        if (identity.IsSucces)
        {
            await _localStorage.SetItemAsync("token", identity.User.Token);
            _provider.MarkUserAsAuthenticated(identity.User.Token);
        }
        return identity;
    }

    public async Task Logout()
    {  
         await _localStorage.RemoveItemAsync("token");
            
    }
}
