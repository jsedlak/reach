using Microsoft.AspNetCore.Components;
using Reach.Applets;

namespace Reach.EditorApp.Components.Pages;

public partial class Apps : ComponentBase
{
    private Type? _componentType = null;

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrWhiteSpace(PageRoute))
        {
            _componentType = null;
            return;
        }

        var appletRoute = PageRoute.StartsWith("apps/") ? PageRoute.Substring(5) : PageRoute;
        var foundApplet = AppletContext.Applets.FirstOrDefault(
            m => m.BaseUrl.TrimStart('/').StartsWith(appletRoute)
        );

        if(foundApplet is null)
        {
            _componentType = null;
            return;
        }

        // Setup the context for the applet
        await AppletContext.SetAppletAsync(foundApplet.Id);
        _componentType = Type.GetType(foundApplet.AppletComponentType);
    }

    [Parameter]
    public string PageRoute { get; set; } = null!;

    [CascadingParameter]
    private AppletContext AppletContext { get; set; } = null!;
}
