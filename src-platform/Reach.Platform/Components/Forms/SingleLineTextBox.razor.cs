using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Tazor.Components;

namespace Reach.Platform.Components.Forms;

public partial class SingleLineTextBox : TazorBaseComponent
{
    private async Task OnChange(ChangeEventArgs args)
    {
        Value = args.Value?.ToString() ?? "";
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private async Task OnKeyUp(KeyboardEventArgs args)
    {
        if(ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private async Task OnKeyDown(KeyboardEventArgs args)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private async Task OnKeyPress(KeyboardEventArgs args)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    [Parameter] 
    public string Type { get; set; } = "text";

    [Parameter]
    public string Value { get; set; } = "";

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }
}
