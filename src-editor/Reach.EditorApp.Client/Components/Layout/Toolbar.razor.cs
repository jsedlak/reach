using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.EditorApp.Client.Authentication;

namespace Reach.EditorApp.Client.Components.Layout;

public partial class Toolbar : ComponentBase
{
    private UserContext _userContext = new UserContext("","","");

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationState is not null)
        {
            _userContext = (await AuthenticationState).AsContext();
        }
    }

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }
}
