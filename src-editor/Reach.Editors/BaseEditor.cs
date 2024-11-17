using Microsoft.AspNetCore.Components;

namespace Reach.Editors;

public class BaseEditor : ComponentBase, IEditor
{
    protected virtual Task OnValueSetAsync(string value)
    {
        return Task.CompletedTask;
    }

    public virtual async Task SetValueAsync(string value)
    {
        await OnValueSetAsync(value);
        StateHasChanged();
    }

    [Parameter]
    public bool? Required { get; set; }

    public EventCallback<string> ValueChanged { get; set; }
}
