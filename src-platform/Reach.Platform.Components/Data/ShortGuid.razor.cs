using Microsoft.AspNetCore.Components;

namespace Reach.Platform.Components.Data;

public partial class ShortGuid : ComponentBase
{
    private async Task OnValueClicked()
    {
        if (Clicked.HasDelegate)
        {
            await Clicked.InvokeAsync();
        }
    }
    
    [Parameter]
    public Guid? Value { get; set; }
    
    [Parameter]
    public EventCallback Clicked { get; set; }
}