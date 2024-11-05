using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.EditorApp.Client.Authentication;
using Reach.EditorApp.ContextProviders;

namespace Reach.EditorApp.Components.Layout;

public partial class TopBar
{
    private bool _isAppTrayOpen = false;
    private bool _isUserTrayOpen = false;
    private UserContext _userContext = new UserContext();

    protected override async Task OnInitializedAsync()
    {
        if(AuthenticationState is not null)
        {
            _userContext = (await AuthenticationState).AsContext();
        }

        AppletContext.AppletChanged += (_, _) =>
        {
            _isAppTrayOpen = false;
            _isUserTrayOpen = false;

            InvokeAsync(StateHasChanged);
        };
    }

    private void ToggleAppTray(bool? value = null)
    {
        if (value.HasValue)
        {
            _isAppTrayOpen = value.Value;
        }
        else
        {
            _isAppTrayOpen = !_isAppTrayOpen;
        }

        StateHasChanged();
    }

    private void ToggleUserTray(bool? value = null)
    {
        if(value.HasValue)
        {
            _isUserTrayOpen = value.Value;
        }
        else
        {
            _isUserTrayOpen = !_isUserTrayOpen;
        }
    }

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    [CascadingParameter]
    private AppletContext AppletContext { get; set; } = null!;
}
