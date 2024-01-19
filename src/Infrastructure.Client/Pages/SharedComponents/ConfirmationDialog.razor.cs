using Microsoft.AspNetCore.Components;

namespace Infrastructure.Client.Pages.SharedComponents;

public partial class ConfirmationDialog
{

    [Parameter] public bool IsShow { get; set; }
    [Parameter] public string Title { get; set; } = "Delete";
    [Parameter] public string Class { get; set; } = "btn btn-danger";
    [Parameter] public string Message { get; set; } = "Are you sure?";
    [Parameter] public EventCallback<bool> ConfirmedChanged { get; set; }

    public async Task Confirmation(bool value)
    {
        await ConfirmedChanged.InvokeAsync(value);
    }

}