using Microsoft.AspNetCore.Components;

namespace Reach.Platform.Components.App;

public partial class TenantDropdown : BaseReachComponent
{
    private bool _isOpen;
    private readonly NavigationManager _navigationManager;

    public TenantDropdown(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    protected void OnClickedOutside()
    {

        _isOpen = false;
        StateHasChanged();

    }

    protected void OnDropdownClicked()
    {
        _isOpen = !_isOpen;
        StateHasChanged();
    }
}
