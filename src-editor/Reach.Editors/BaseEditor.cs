using Microsoft.AspNetCore.Components;

namespace Reach.Editors;

public class BaseEditor : ComponentBase, IEditor
{
    protected async Task OnValueChanged(string newValue)
    {
        Value = newValue;

        if (ValueChanged is { HasDelegate: true })
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Console.WriteLine($"[{nameof(BaseEditor)}] Value set: " + Value);
    }

    [Parameter]
    public string Value { get; set; } = null!;

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public bool? Required { get; set; }
}
