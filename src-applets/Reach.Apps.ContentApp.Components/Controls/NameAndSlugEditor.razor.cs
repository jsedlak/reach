using Microsoft.AspNetCore.Components;
using Tazor.Extensions;

namespace Reach.Apps.ContentApp.Components.Controls;

/// <summary>
/// Provides a common pattern for rendering a name and slug combination editor
/// </summary>
public partial class NameAndSlugEditor : ComponentBase
{
    private async Task OnNameChanged(string value)
    {
        Name = value;

        if (NameChanged.HasDelegate)
        {
            await NameChanged.InvokeAsync(Name);
        }

        Slug = value.ToSlug();

        if (SlugChanged.HasDelegate)
        {
            await SlugChanged.InvokeAsync(Slug);
        }
    }
    
    [Parameter] public string Name { get; set; } = null!;
    
    [Parameter] public EventCallback<string> NameChanged { get; set; }

    [Parameter] public string Slug { get; set; } = null!;
    
    [Parameter] public EventCallback<string> SlugChanged { get; set; }
}