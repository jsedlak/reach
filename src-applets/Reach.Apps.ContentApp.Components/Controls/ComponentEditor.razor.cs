using Microsoft.AspNetCore.Components;
using Reach.Content.Views;
using Reach.Platform.Providers;
using Tazor.Components;

namespace Reach.Apps.ContentApp.Components.Controls;

public partial class ComponentEditor : TazorBaseComponent
{
    private readonly IContentContextProvider _contentContextProvider;

    public ComponentEditor(IContentContextProvider contentContextProvider)
    {
        _contentContextProvider = contentContextProvider;
    }

    [Parameter]
    public ComponentView? Component { get; set; }
}
