using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Shared
{
    public partial class MainLayout
    {
        [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; } = null!;
        private string? _name;
        protected override async Task OnInitializedAsync()
        {
            var state = await _provider.GetAuthenticationStateAsync();
            if (state.User.Identity is null || !state.User.Identity.IsAuthenticated)
            {
                manager.NavigateTo("/login");
            }
            else
            {
                _name = state.User.Identity.Name;
            }
        }

        private async Task OnLogout()
        {
            await _authorizeApi.Logout();
            manager.NavigateTo("/login");
        }
    }
}