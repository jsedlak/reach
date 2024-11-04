using Microsoft.AspNetCore.Components;
using Reach.Applets;

namespace Reach.EditorApp.Components.Pages;

public partial class Home : ComponentBase
{
    protected override Task OnInitializedAsync()
    {
        AppletContext.AppletChanged += OnAppletChanged;
        return base.OnInitializedAsync();
    }

    private void OnAppletChanged(object? sender, AppletDefinition e)
    {
        InvokeAsync(StateHasChanged);
        // NavigationManager.NavigateTo(e.Name.ToLowerInvariant());
    }

    [Parameter]
    public string PageRoute { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter]
    private AppletContext AppletContext { get; set; } = null!;
}
