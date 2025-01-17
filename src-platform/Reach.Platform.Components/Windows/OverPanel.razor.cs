using Microsoft.AspNetCore.Components;

namespace Reach.Platform.Components.Windows;

public partial class OverPanel : IPanel
{
    protected async Task HandleCloseClicked()
    {
        IsOpen = false;
        await IsOpenChanged.InvokeAsync();
    }

    public async Task CloseAsync()
    {
        await HandleCloseClicked();
    }

    [Parameter]
    public string Title { get; set; } = "";

    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Parameter]
    public bool IsOpen { get; set; }

    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }
}
