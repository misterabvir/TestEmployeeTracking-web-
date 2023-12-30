using Client.Protos;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Shared.Components
{
    public partial class ErrorComponent
    {
        private bool _isShow => Error is not null;
        [Parameter] public ErrorModel? Error { get; set; }

        private void HideError()
        {
            Error = null;
        }
    }
}