using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Tazor.Components.App;
using Tazor.Components.Content;

namespace Reach.Platform.Components.Data;

public partial class ShortGuid : ComponentBase
{
    private readonly IJSRuntime _jsRuntime;
    private readonly INotificationProvider _notificationProvider;

    public ShortGuid(IJSRuntime jsRuntime, INotificationProvider notificationProvider)
    {
        _notificationProvider = notificationProvider;
        _jsRuntime = jsRuntime;
    }

    private async Task OnValueClicked()
    {
        if (Clicked.HasDelegate)
        {
            await Clicked.InvokeAsync();
        }
    }

    private async Task OnCopyClicked()
    {
        await _jsRuntime.InvokeVoidAsync("copyToClipboard", Value.ToString());

        _notificationProvider.Add(new NotificationItem
        {
            Type = NotificationType.Success,
            Title = "Copied!",
            Message = $"Copied {Value.ToString()} to clipboard",
            Icon = HeroIcons.ClipboardDocumentCheck()
        });
    }

    [Parameter]
    public Guid? Value { get; set; }
    
    [Parameter]
    public EventCallback Clicked { get; set; }
}