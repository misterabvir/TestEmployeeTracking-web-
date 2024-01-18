using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace Client.Authorizations;

public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public IdentityAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsStringAsync("token");
        if (!string.IsNullOrEmpty(token)) 
            return MarkUserAsAuthenticated(token.Trim('\"'));
        return new AuthenticationState(new ClaimsPrincipal());
    }

    internal AuthenticationState MarkUserAsAuthenticated(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var email = jwtToken.Claims.First(c => c.Type == ClaimTypes.Name).Value;

        var identity = new ClaimsIdentity(
                new List<Claim>()
                {
                    new(ClaimTypes.Name, email)
                },
                "jwt"
            );
        
        var user = new ClaimsPrincipal(identity);

        var state = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return state;
    }


}
