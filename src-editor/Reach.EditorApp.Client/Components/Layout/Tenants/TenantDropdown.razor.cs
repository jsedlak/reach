using Microsoft.AspNetCore.Components;
using Reach.Components.Context;

namespace Reach.EditorApp.Client.Components.Layout.Tenants;

public partial class TenantDropdown : ComponentBase
{
    private readonly NavigationManager _navigationManager;

    private bool _isOpen = false;

    public TenantDropdown(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    protected override void OnInitialized()
    {
        TenantContext.PropertyChanged += OnTenantContextChanged;
    }

    private void OnTenantContextChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        StateHasChanged();
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

    //private Task SelectTenant(AvailableTenantView tenant)
    //{
    //    _currentTenant = tenant;
    //    _isOpen = false;
    //    StateHasChanged();
    //    return Task.CompletedTask;
    //}

    [CascadingParameter]
    private TenantContext TenantContext { get; set; } = null!;
}
