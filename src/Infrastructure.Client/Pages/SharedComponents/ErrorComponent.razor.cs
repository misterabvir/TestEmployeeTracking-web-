using Microsoft.AspNetCore.Components;
using ProtoContracts;

namespace Infrastructure.Client.Pages.SharedComponents
{
    public partial class ErrorComponent
    {
        private bool IsShow => Error is not null;
        [Parameter] public ErrorModel? Error { get; set; }

        private void HideError()
        {
            Error = null;
        }
    }
}