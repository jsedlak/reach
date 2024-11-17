using Microsoft.AspNetCore.Components;
using Reach.Membership.Views;

namespace Reach.EditorApp.Client.Components.Layout;

public partial class TenantDropdown : ComponentBase
{
    private AvailableTenantView[] _availableTenants = [];
    private AvailableTenantView _currentTenant = new();
    private bool _isOpen = false;

    public TenantDropdown()
    {
        _availableTenants = [
            new AvailableTenantView { Name = "Blanco Mac", IconUrl = "https://picsum.photos/seed/blanco/200" },
            new AvailableTenantView { Name = "ArcJet Systems", IconUrl = "https://picsum.photos/seed/arcjet/200" },
            new AvailableTenantView { Name = "Super-Duper Mart", IconUrl = "https://picsum.photos/seed/super/200" },
            new AvailableTenantView { Name = "Sunset Sarsaparilla Company", IconUrl = "https://picsum.photos/seed/sunset/200" },
        ];

        _currentTenant = _availableTenants.Last();
    }

    private void OnClickedOutside()
    {
        _isOpen = false;
        StateHasChanged();
    }

    private void OnDropdownClicked()
    {
        _isOpen = !_isOpen;
        StateHasChanged();
    }

    private Task SelectTenant(AvailableTenantView tenant)
    {
        _currentTenant = tenant;
        _isOpen = false;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
