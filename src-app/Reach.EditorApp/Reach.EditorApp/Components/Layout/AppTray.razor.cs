using Microsoft.AspNetCore.Components;
using Reach.Applets;

namespace Reach.EditorApp.Components.Layout;

public partial class AppTray : ComponentBase
{
    /// <summary>
    /// Handles when an applet is selected
    /// </summary>
    /// <param name="applet"></param>
    /// <returns></returns>
    private async Task OnAppletClicked(AppletDefinition applet)
    {
        // TODO: Do some validation checks here or something
        // TODO: Call the current applet to see if we can navigate from it
        NavigationManager.NavigateTo($"/apps/{applet.BaseUrl.TrimStart('/')}");
    }

    [Parameter]
    public bool IsOpen { get; set; }

    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }

    [CascadingParameter]
    protected AppletContext AppletContext { get; set; } = null!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;
}
