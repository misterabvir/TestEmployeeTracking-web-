using ProtoContracts;

namespace Infrastructure.Client.Pages.LoginPage
{
    public partial class LoginPage
    {
        private string _email = string.Empty;
        private string _password = string.Empty;
        private bool isDisabled => string.IsNullOrWhiteSpace(_email) || string.IsNullOrWhiteSpace(_password);
        private ErrorModel? _error;
        private async Task OnLogin()
        {
            var identity = await _authorizeApi.Login(_email, _password);
            if (identity.IsSucces)
            {
                _navigation.NavigateTo("/");
            }
            else
            {
                _error = identity.Error;
            }
        }

        private async Task OnRegister()
        {
            var identity = await _authorizeApi.Register(_email, _password);
            if (identity.IsSucces)
            {
                _navigation.NavigateTo("/");
            }
            else
            {
                _error = identity.Error;
            }

        }
        protected override async Task OnInitializedAsync()
        {
            var state = await _provider.GetAuthenticationStateAsync();
            if (state.User.Identity is not null && state.User.Identity.IsAuthenticated)
            {
                _navigation.NavigateTo("/");
            }
        }
    }
}