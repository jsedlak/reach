using Microsoft.AspNetCore.Components;
using Reach.Platform.Providers;
using Tazor.Components.Theming;

namespace Reach.Platform.Components.Content;

partial class ComponentDefinitionSelector : BaseReachComponent
{
    private readonly IThemeManager _themeManager;

    private readonly IContentContextProvider _contentContextProvider;

    public ComponentDefinitionSelector(
        IThemeManager themeManager,
        IContentContextProvider contentContextProvider)
    {
        _themeManager = themeManager;
        _contentContextProvider = contentContextProvider;
    }

    private async Task OnComponentDefinitionChanged(ChangeEventArgs args)
    {
        if (Guid.TryParse(args.Value?.ToString() ?? Guid.Empty.ToString(), out var newValue))
        {
            SelectedComponentDefinitionId = newValue;
            await SelectedComponentDefinitionIdChanged.InvokeAsync(SelectedComponentDefinitionId);
        }
    }

    [Parameter]
    public Guid SelectedComponentDefinitionId { get; set; }

    [Parameter]
    public EventCallback<Guid> SelectedComponentDefinitionIdChanged { get; set; }
}
