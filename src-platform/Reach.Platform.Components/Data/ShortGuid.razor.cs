using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Reach.Platform.Components.Data;

public partial class ShortGuid : ComponentBase
{
    private readonly IJSRuntime _jsRuntime;

    public ShortGuid(IJSRuntime jsRuntime)
    {
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
    }
    
    [Parameter]
    public Guid? Value { get; set; }
    
    [Parameter]
    public EventCallback Clicked { get; set; }
}