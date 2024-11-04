using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Reach.EditorApp.Components.Debugging;

public partial class UserData : ComponentBase
{
    private string _username = "";
    private IEnumerable<string> _claims = [];

    protected override async Task OnInitializedAsync()
    {
        if (authenticationState is not null)
        {
            var state = await authenticationState;
            _username = state?.User?.Identity?.Name ?? "uh oh";
            _claims = state?.User.Claims.Select(m => m.ToString()) ?? [];

        }
        await base.OnInitializedAsync();
    }

    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }
}
